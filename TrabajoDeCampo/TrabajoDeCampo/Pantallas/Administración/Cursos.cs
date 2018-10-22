using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoDeCampo.SEGURIDAD;
using TrabajoDeCampo.SERVICIO;

namespace TrabajoDeCampo.Pantallas.Administración
{
    public partial class Cursos : Form
    {

        private ServicioSeguridad servicioSeguridad;
        private ServicioAdministracion servicioAdministracion;
        private Boolean valido  = false;
        private Regex alphanumericRegex = new Regex("[0-9a-zA-z]");
        private Dictionary<String, String> traducciones;
        public Cursos()
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad();
            this.servicioAdministracion = new ServicioAdministracion();
            this.dataGridView1.ColumnHeaderMouseClick += customSort;
            this.textBox1.KeyDown += TextBox1_KeyDown;
            this.textBox1.KeyPress += TextBox1_KeyPress;
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!valido)
            {
                e.Handled = true;
            }
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            valido = true;
            if (!e.KeyValue.Equals(8))//tecla borrar
            {
                if (!alphanumericRegex.IsMatch(e.KeyData.ToString()) || e.KeyData.ToString().Contains("Oem"))
                {
                    valido = false;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
 
            new AltaModificacionCurso(null,this).ShowDialog();

        }

        private void Cursos_Load(object sender, EventArgs e)
        {
            this.helpProvider1.SetHelpKeyword(this, Properties.Settings.Default.Idioma.Equals("es") ? "Cursos.htm" : "Courses.htm");
            this.helpProvider1.HelpNamespace = Application.StartupPath + @"\\DocumentsDeAyuda.chm";
            this.dataGridView1.DataSource = null;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.Columns[0].DataPropertyName = "nivel";
            this.dataGridView1.Columns[1].DataPropertyName = "codigo";
            this.dataGridView1.Columns[2].DataPropertyName = "capacidad";
            this.dataGridView1.Columns[3].DataPropertyName = "turno";
            this.dataGridView1.Columns[0].Tag = "com.td.nivel";
            this.dataGridView1.Columns[1].Tag = "com.td.código";
            this.dataGridView1.Columns[2].Tag = "com.td.capacidad";
            this.dataGridView1.Columns[3].Tag = "com.td.turno";
            this.comboBox1.DataSource = null;
            this.dataGridView1.CellFormatting += formatter;

            //traduccion
            FormUtils traductor = new TraductorIterador();
            List<String> tags = new List<string>();
            tags.Add("com.td.curso.tiene.alumnos");
            long id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            traductor.process(tags, this, null, null);
            traducciones = servicioSeguridad.traerTraducciones(tags, Properties.Settings.Default.Idioma);
            traductor = new TraductorReal();
            traductor.process(null, this, traducciones, null);
            traductor = new TraductorIterador();

            List<KeyValuePair<String, String>> comboOptions = new List<KeyValuePair<string, string>>();
            comboOptions.Add(new KeyValuePair<string, string>("nivel", traducciones["com.td.nivel"]));
            comboOptions.Add(new KeyValuePair<string, string>("codigo", traducciones["com.td.código"]));
            comboOptions.Add(new KeyValuePair<string, string>("capacidad", traducciones["com.td.capacidad"]));
            comboOptions.Add(new KeyValuePair<string, string>("turno", traducciones["com.td.turno"]));

            comboBox1.DataSource = comboOptions;
            comboBox1.DisplayMember = "value";
            cargarCursos(null, null, null);
            desbloquearControles();

       



        }
        public void desbloquearControles()
        {
            long id = (long)TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            bool crear = servicioSeguridad.tienePatente(id, EnumPatentes.CrearCurso.ToString());
            bool modificar = servicioSeguridad.tienePatente(id, EnumPatentes.ModificarCurso.ToString());
            bool borrar = servicioSeguridad.tienePatente(id, EnumPatentes.BorrarCurso.ToString());
            bool exportar = servicioSeguridad.tienePatente(id, EnumPatentes.GenerarReportes.ToString());

            this.createBtn.Enabled = crear;
            this.modBtn.Enabled = modificar;
            this.delBtn.Enabled = borrar;
            this.expBtn.Enabled = exportar;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(this.dataGridView1.Rows.Count > 0 && this.dataGridView1.CurrentRow != null && this.dataGridView1.CurrentRow.DataBoundItem != null)
            {
                Curso curso = (Curso)this.dataGridView1.CurrentRow.DataBoundItem;
                new AltaModificacionCurso(curso, this).ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows.Count > 0 && this.dataGridView1.CurrentRow != null && this.dataGridView1.CurrentRow.DataBoundItem != null)
            {
                Curso curso = (Curso)this.dataGridView1.CurrentRow.DataBoundItem;
                try
                {

                this.servicioAdministracion.borrarCurso(curso);
                this.cargarCursos(null, null, null);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.Equals("TIENE ALUMNOS")  ? traducciones["com.td.curso.tiene.alumnos"] : ex.Message);
                }
            }

        }

        public void cargarCursos(String filtro,String valor, String orden)
        {
            try
            {
                this.dataGridView1.DataSource = null;
                this.dataGridView1.DataSource = this.servicioAdministracion.listarCursos(filtro, valor, orden);
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
                String valor = this.textBox1.Text;
                String filtro = ((KeyValuePair<String, String>)this.comboBox1.SelectedItem).Key;
                this.cargarCursos(filtro, valor, null);
         
        }

        private void formatter(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                String valor = e.Value.ToString();
                e.Value = valor == "M" ? "Mañana" : "Tarde";
            }
           else if (e.ColumnIndex == 0)
            {
                Nivel valor = (Nivel) e.Value;
                e.Value = valor.codigo;
            }
        }

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
                List<Curso> cursos = (List<Curso>)this.dataGridView1.DataSource;
                string propertyName = this.dataGridView1.Columns[e.ColumnIndex].Name;
                this.dataGridView1.DataSource = null;
                if (propertyName.Equals("nivel"))
                {

                    cursos.Sort((x, y) => (x.nivel.GetType().GetProperty("codigo").GetValue(x.nivel).ToString().CompareTo
                     (y.nivel.GetType().GetProperty("codigo").GetValue(y.nivel).ToString())));
                    this.dataGridView1.DataSource = cursos;
                }
                else
                {

                    cursos.Sort((x, y) => x.GetType().GetProperty(propertyName).GetValue(x).ToString().CompareTo
                     (y.GetType().GetProperty(propertyName).GetValue(y).ToString()));
                    this.dataGridView1.DataSource = cursos;
                }
                this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.LightSkyBlue;
                this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;

            }
            else
            {
                List<Curso> cursos = (List<Curso>)this.dataGridView1.DataSource;
                this.dataGridView1.DataSource = null;
                string propertyName = this.dataGridView1.Columns[e.ColumnIndex].Name;
                if (propertyName.Equals("nivel"))
                {
                    cursos.Sort((x, y) => y.nivel.GetType().GetProperty("codigo").GetValue(y.nivel).ToString().CompareTo
                     (x.nivel.GetType().GetProperty("codigo").GetValue(x.nivel).ToString()));
                    this.dataGridView1.DataSource = cursos;
                }
                else
                {

                    cursos.Sort((x, y) => y.GetType().GetProperty(propertyName).GetValue(y).ToString().CompareTo
                     (x.GetType().GetProperty(propertyName).GetValue(x).ToString()));
                    this.dataGridView1.DataSource = cursos;
                }
                this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.LightSkyBlue;
                this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;

            }


        }

        private void button6_Click(object sender, EventArgs e)
        {
            List<Curso> cursos = this.dataGridView1.DataSource as List<Curso>;
            foreach (Curso item in cursos)
            {
                item.alumnos = servicioAdministracion.listarAlumnosPorCursoYNivel(item.nivel, item);
            }

            new ServicioReportes().ejecutarReporte<Curso>("ReporteCursos",cursos);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
