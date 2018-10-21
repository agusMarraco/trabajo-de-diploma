using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoDeCampo.SEGURIDAD;
using TrabajoDeCampo.SERVICIO;

namespace TrabajoDeCampo.Pantallas.Alumnos
{
    public partial class Alumnos : Form
    {
        private ServicioSeguridad servicioSeguridad;
        private ServicioAlumnos servicioAlumnos;

        public Alumnos()
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad();
            this.servicioAlumnos = new ServicioAlumnos();
            this.dataGridView1.CellFormatting += formatting;
            this.dataGridView1.ColumnHeaderMouseClick += customSort;
            this.dataGridView1.SelectionChanged += rowChange;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new AltaModificacionAlumno(null, this).ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow != null && this.dataGridView1.CurrentRow.DataBoundItem != null)
            {
                new Inasistencias(((Alumno)this.dataGridView1.CurrentRow.DataBoundItem), this).ShowDialog();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow != null && this.dataGridView1.CurrentRow.DataBoundItem != null)
            {
                new Amonestaciones(((Alumno)this.dataGridView1.CurrentRow.DataBoundItem), this).ShowDialog();
            }
            
        }

        private void Alumnos_Load(object sender, EventArgs e)
        {
            this.helpProvider1.SetHelpKeyword(this, Properties.Settings.Default.Idioma.Equals("es") ? "Alumnos.htm" : "Students.htm");
            this.helpProvider1.HelpNamespace = Application.StartupPath + @"\\DocumentsDeAyuda.chm";
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.Columns[0].Tag = "com.td.nombre";
            this.dataGridView1.Columns[1].Tag = "com.td.apellido";
            this.dataGridView1.Columns[2].Tag = "com.td.d.n.i.";
            this.dataGridView1.Columns[3].Tag = "com.td.curso";
            FormUtils traductor = new TraductorIterador();
            List<String> tags = new List<string>();
            traductor.process(tags, this, null, null);
            Dictionary<String, String> traducciones = servicioSeguridad.traerTraducciones(tags, Properties.Settings.Default.Idioma);
            traductor = new TraductorReal();
            traductor.process(null, this, traducciones, null);
            traductor = new TraductorIterador();

            Dictionary<string, string> filtros = new Dictionary<string, string>();
            filtros.Add("nombre", traducciones["com.td.nombre"]);
            filtros.Add("apellido", traducciones["com.td.apellido"]);
            filtros.Add("dni", traducciones["com.td.d.n.i."]);
            filtros.Add("curso", traducciones["com.td.curso"]);

            this.comboBox2.DataSource = null;
            this.comboBox2.DataSource = filtros.ToList();
            this.comboBox2.DisplayMember = "value";
            this.dataGridView1.Columns[0].DataPropertyName = "nombre";
            this.dataGridView1.Columns[1].DataPropertyName = "apellido";
            this.dataGridView1.Columns[2].DataPropertyName = "dni";
            this.dataGridView1.Columns[3].DataPropertyName = "curso";
            listarAlumnos(null, null);
            desbloquearControles();
        }

        public void desbloquearControles()
        {
            long id = (long)TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            bool crear = servicioSeguridad.tienePatente(id, EnumPatentes.CrearAlumno.ToString());
            bool modificar = servicioSeguridad.tienePatente(id, EnumPatentes.ModificarAlumno.ToString());
            bool borrar = servicioSeguridad.tienePatente(id, EnumPatentes.BorrarAlumno.ToString());
            
            bool inasistencias = servicioSeguridad.tienePatente(id, EnumPatentes.RegistrarInasistencia.ToString());
            bool amonestaciones = servicioSeguridad.tienePatente(id, EnumPatentes.RegistrarAmonestación.ToString());
            bool repetir = servicioSeguridad.tienePatente(id, EnumPatentes.ModificarAlumno.ToString());
            this.btnRegistrar.Enabled = crear;
            this.btnMod.Enabled = modificar;
            this.btnDel.Enabled = borrar;
            this.btnIna.Enabled = inasistencias;
            this.btnAmon.Enabled = amonestaciones;
            this.btnRepetir.Enabled = repetir;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.comboBox2.SelectedItem != null && !String.IsNullOrEmpty(this.textBox1.Text))
            {
                String filtro = ((KeyValuePair<String, String>)this.comboBox2.SelectedItem).Key;
                String valor = this.textBox1.Text;
                this.listarAlumnos(filtro, valor);
            }
            else
            {
                this.listarAlumnos(null, null);
            }
        }

        public void listarAlumnos(String filtro, String valor)
        {

            List<Alumno> alumnos =  this.servicioAlumnos.listarAlumno(filtro, valor, null);
            this.dataGridView1.DataSource = null;
            this.dataGridView1.DataSource = alumnos;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(this.dataGridView1.CurrentRow != null && this.dataGridView1.CurrentRow.DataBoundItem != null)
            {
                new AltaModificacionAlumno(((Alumno)this.dataGridView1.CurrentRow.DataBoundItem), this).ShowDialog();
            }
        }

        private void formatting(object sender , DataGridViewCellFormattingEventArgs ev){
            if(ev.ColumnIndex == 3)
            {
                ev.Value = ((Curso)ev.Value).codigo;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow != null && this.dataGridView1.CurrentRow.DataBoundItem != null)
            {
                this.servicioAlumnos.borrarAlumno((Alumno)this.dataGridView1.CurrentRow.DataBoundItem);
                this.listarAlumnos(null, null);
            }
        }

        //sorting
        private void customSort(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach (DataGridViewColumn column in ((DataGridView)sender).Columns)
            {
                if (column.Index != e.ColumnIndex)
                {
                    column.HeaderCell.SortGlyphDirection = SortOrder.None;
                    column.HeaderCell.Style.BackColor = Color.White;
                }
            }
            //initialSorting
            if (this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None || this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                List<Alumno> alumnos = (List<Alumno>)this.dataGridView1.DataSource;
                string propertyName = this.dataGridView1.Columns[e.ColumnIndex].Name;
                this.dataGridView1.DataSource = null;
                if (propertyName.Equals("curso"))
                {

                    alumnos.Sort((x, y) => (x.curso.GetType().GetProperty("codigo").GetValue(x.curso).ToString().CompareTo
                     (y.curso.GetType().GetProperty("codigo").GetValue(y.curso).ToString())));
                    this.dataGridView1.DataSource = alumnos;
                }
                else
                {
                    
                    alumnos.Sort((x,y) =>  x.GetType().GetProperty(propertyName).GetValue(x).ToString().CompareTo
                     (y.GetType().GetProperty(propertyName).GetValue(y).ToString()));
                    this.dataGridView1.DataSource = alumnos;
                }
                this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.LightSkyBlue;
                this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                
            }
            else
            {
                List<Alumno> alumnos = (List<Alumno>)this.dataGridView1.DataSource;
                this.dataGridView1.DataSource = null;
                string propertyName = this.dataGridView1.Columns[e.ColumnIndex].Name;
                if (propertyName.Equals("curso"))
                {
                    alumnos.Sort((x, y) => y.curso.GetType().GetProperty("codigo").GetValue(y.curso).ToString().CompareTo
                     (x.curso.GetType().GetProperty("codigo").GetValue(x.curso).ToString()));
                    this.dataGridView1.DataSource = alumnos;
                }
                else
                {

                    alumnos.Sort((x, y) => y.GetType().GetProperty(propertyName).GetValue(y).ToString().CompareTo
                     (x.GetType().GetProperty(propertyName).GetValue(x).ToString()));
                    this.dataGridView1.DataSource = alumnos;
                }
                this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.LightSkyBlue;
                this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;

            }


        }

        private void rowChange( Object sender, EventArgs evnt)
        {
            if(dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.DataBoundItem != null)
            {
                Alumno alumno = dataGridView1.CurrentRow.DataBoundItem as Alumno;
                if (alumno.puedeRepetir)
                {
                    this.btnRepetir.Enabled = true;
                }
                else
                {
                        this.btnRepetir.Enabled = false;
                }
            }
        }

        private void btnRepetir_Click(object sender, EventArgs e)
        {
            if(this.dataGridView1.CurrentRow != null && this.dataGridView1.CurrentRow.DataBoundItem != null)
            {
                Alumno alumno = dataGridView1.CurrentRow.DataBoundItem as Alumno;
                this.servicioAlumnos.repetirAlumno(alumno);
                this.listarAlumnos(null, null);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
