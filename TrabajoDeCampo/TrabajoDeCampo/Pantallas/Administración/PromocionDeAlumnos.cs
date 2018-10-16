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

namespace TrabajoDeCampo.Pantallas.Administración
{

    public partial class PromocionDeAlumnos : Form
    {
        private ServicioSeguridad servicioSeguridad;
        private ServicioAdministracion administracion;
        private ServicioAlumnos servicioAlumnos;
        private List<Nivel> niveles;
        private List<Curso> cursos;
        private Dictionary<String, String> traducciones;
        public PromocionDeAlumnos()
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad();
            this.servicioAlumnos = new ServicioAlumnos();
            this.administracion = new ServicioAdministracion();
            this.comboCurso.DataSource = null;
            this.comboNivel.DataSource = null;
            this.dataGridView1.DataSource = null;
            this.dataGridView1.CellFormatting += formatting;
            this.comboNivel.SelectedValueChanged += comboNivelHandler;
            this.dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
            this.dataGridView1.CellContentClick += DataGridView1_CellContentClick;
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView view = sender as DataGridView;
            if(view.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex > -1)
            {
                string tag = view.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag.ToString();
                if (tag.Equals("Promocionar"))
                {
                    if(this.comboBox3.SelectedItem != null)
                    {
                        Curso curso = this.comboBox3.SelectedItem as Curso;
                        Alumno alu = this.dataGridView1.CurrentRow.DataBoundItem as Alumno;
                        try
                        {
                            this.administracion.promocionarAlumno(alu, curso);
                            this.dataGridView1.DataSource = this.administracion.listarAlumnosPorCursoYNivel(null, null);

                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show(ex.Message);
                        }
                            
                    }
                }
                else
                {
                    Alumno alu = this.dataGridView1.CurrentRow.DataBoundItem as Alumno;
                    try
                    {
                        this.servicioAlumnos.borrarAlumno(alu);
                        this.dataGridView1.DataSource = this.administracion.listarAlumnosPorCursoYNivel(null, null);

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            mostrarCursosSiguientes();
        }

        private void PromocionDeAlumnos_Load(object sender, EventArgs e)
        {
            niveles = administracion.listarNiveles(null, null, null);
            this.comboNivel.DataSource = niveles;
            this.comboNivel.DisplayMember = "codigo";
            cursos = administracion.listarCursos(null, null, null);
            actualizarCursos();
            this.comboCurso.DisplayMember = "codigo";
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.Columns[0].DataPropertyName = "nombre";
            this.dataGridView1.Columns[1].DataPropertyName = "apellido";
            this.dataGridView1.Columns[2].DataPropertyName = "dni";
            this.dataGridView1.Columns[3].DataPropertyName = "orientacion";
            this.dataGridView1.Columns[4].DataPropertyName = "curso";
            this.dataGridView1.Columns[0].Tag = "com.td.nombre";
            this.dataGridView1.Columns[1].Tag = "com.td.apellido";
            this.dataGridView1.Columns[2].Tag = "com.td.d.n.i.";
            this.dataGridView1.Columns[3].Tag = "com.td.orientación";
            this.dataGridView1.Columns[0].ReadOnly = true;
            this.dataGridView1.Columns[1].ReadOnly = true;
            this.dataGridView1.Columns[2].ReadOnly = true;
            this.dataGridView1.Columns[3].ReadOnly = true;

            //traduccion
            FormUtils traductor = new TraductorIterador();
            List<String> tags = new List<string>();
            long id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            traductor.process(tags, this, null, null);
            tags.Add("com.td.egresar");
            tags.Add("com.td.promocionar");
            tags.Add("com.td.seleccione.busqueda");
            traducciones = servicioSeguridad.traerTraducciones(tags, Properties.Settings.Default.Idioma);
            traductor = new TraductorReal();
            traductor.process(null, this, traducciones, null);
            traductor = new TraductorIterador();

            this.dataGridView1.DataSource = this.administracion.listarAlumnosPorCursoYNivel(null, null);
        }

        private void actualizarCursos()
        {
            this.comboCurso.DataSource = null;
            this.comboCurso.DisplayMember = "codigo";
            if(cursos!=null)
                this.comboCurso.DataSource = cursos.Where(x => x.nivel.id.Equals(((Nivel)this.comboNivel.SelectedItem).id)).ToArray();
        }

        public void mostrarCursosSiguientes()
        {
           if(this.dataGridView1.CurrentRow != null && this.dataGridView1.CurrentRow.DataBoundItem != null)
            {
                Alumno alu = this.dataGridView1.CurrentRow.DataBoundItem as Alumno;
                int año = int.Parse(alu.curso.nivel.codigo.ElementAt(0).ToString()) ;
                Nivel nivelSiguiente;
                this.comboBox3.DataSource = null;
                if (año != 6){
                    if (alu.orientacion.codigo == "null")
                    {
                        nivelSiguiente = niveles.Where(x => (x.codigo.Contains((año + 1).ToString()))).First();
                    }
                    else
                    {
                        nivelSiguiente = niveles.Where(x => (x.codigo.Contains((año + 1).ToString())) && (x.orientacion.codigo.Equals(alu.orientacion.codigo))).First();
                    }
                    this.comboBox3.DataSource = null;
                    this.comboBox3.DataSource = cursos.Where(x => x.nivel.id.Equals(nivelSiguiente.id)).ToArray();
                }
               
                this.comboBox3.DisplayMember = "codigo";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Nivel nivel = this.comboNivel.SelectedItem as Nivel;
            Curso curso = this.comboCurso.SelectedItem as Curso;
            try
            {
                if(nivel != null && curso != null)
                {
                    this.dataGridView1.DataSource = null;
                    this.dataGridView1.DataSource = this.administracion.listarAlumnosPorCursoYNivel(null, curso);
                }
                else
                {
                    MessageBox.Show(traducciones["com.td.seleccione.busqueda"]);
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        private void formatting(object sender, DataGridViewCellFormattingEventArgs ev)
        {
            if (ev.ColumnIndex == 3)
            {
                ev.Value = ((Orientacion)ev.Value).nombre;
            }else if(ev.ColumnIndex == 4)
            {
                int rowIndex = ev.RowIndex;
                if(this.dataGridView1.Rows[rowIndex] != null && this.dataGridView1.Rows[rowIndex].DataBoundItem != null)
                {
                    Alumno alu = this.dataGridView1.Rows[rowIndex].DataBoundItem as Alumno;
                    if (int.Parse(alu.curso.nivel.codigo.ElementAt(0).ToString()).Equals(6))
                    {
                        this.dataGridView1.Rows[rowIndex].Cells[ev.ColumnIndex].Tag = "Egresar";
                        ev.Value = traducciones["com.td.egresar"];
                    }
                    else
                    {
                        this.dataGridView1.Rows[rowIndex].Cells[ev.ColumnIndex].Tag = "Promocionar";
                        ev.Value = traducciones["com.td.promocionar"];
                    }
                }

            }
        }

        public void comboNivelHandler(object sender , EventArgs ev){
            actualizarCursos();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
