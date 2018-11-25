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
        private Curso ultimoCursoSeleccionado = null;
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
                        DialogResult result = MessageBox.Show(traducciones["com.td.seguro"], "", MessageBoxButtons.OKCancel);
                        if (!result.Equals(DialogResult.OK))
                        {
                            return;
                        }
                        Curso curso = this.comboBox3.SelectedItem as Curso;
                        Alumno alu = this.dataGridView1.CurrentRow.DataBoundItem as Alumno;
                        try
                        {
                            Boolean excedido =  this.administracion.promocionarAlumno(alu, curso);
                            if (excedido)
                            {
                                MessageBox.Show(traducciones["com.td.excedido"], "", MessageBoxButtons.OK);
                            }
                            MessageBox.Show(traducciones["com.td.completado"], "", MessageBoxButtons.OK);
                            if(ultimoCursoSeleccionado!= null)
                            {
                                this.dataGridView1.DataSource = this.administracion.listarAlumnosPorCursoYNivel(null, ultimoCursoSeleccionado);
                            }
                            else
                            {
                                this.dataGridView1.DataSource = this.administracion.listarAlumnosPorCursoYNivel(null, null);
                            }

                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show(ex.Message);
                        }
                            
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show(traducciones["com.td.seguro"], "", MessageBoxButtons.OKCancel);
                    if (!result.Equals(DialogResult.OK))
                    {
                        return;
                    }
                    Alumno alu = this.dataGridView1.CurrentRow.DataBoundItem as Alumno;
                    try
                    {
                        this.servicioAlumnos.borrarAlumno(alu);
                        MessageBox.Show(traducciones["com.td.completado"], "", MessageBoxButtons.OK);
                        if (ultimoCursoSeleccionado != null)
                        {
                            this.dataGridView1.DataSource = this.administracion.listarAlumnosPorCursoYNivel(null, ultimoCursoSeleccionado);
                        }
                        else
                        {
                            this.dataGridView1.DataSource = this.administracion.listarAlumnosPorCursoYNivel(null, null);
                        }


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
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.helpProvider1.SetHelpKeyword(this, Properties.Settings.Default.Idioma.Equals("es") ? "Promoción_de_Alumnos.htm" : "Student_Promotion.htm");
            this.helpProvider1.HelpNamespace = Application.StartupPath + @"\\DocumentsDeAyuda.chm";
            niveles = new List<Nivel>();
            Nivel fakeNivel = new Nivel();
            fakeNivel.codigo = "";
            niveles.Add(fakeNivel);
            List<Nivel> tempo = administracion.listarNiveles(null, null, null);
            niveles.AddRange(tempo);
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
            tags.Add("com.td.seguro");
            tags.Add("com.td.completado");
            tags.Add("com.td.excedido");
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
            if(cursos!=null && this.comboNivel.SelectedItem != null)
                this.comboCurso.DataSource = cursos.Where(x => x.nivel.id.Equals(((Nivel)this.comboNivel.SelectedItem).id)).ToArray();
        }

        public void mostrarCursosSiguientes()
        {
           if(this.dataGridView1.CurrentRow != null && this.dataGridView1.CurrentRow.DataBoundItem != null)
            {
                Alumno alu = this.dataGridView1.CurrentRow.DataBoundItem as Alumno;
                int año = int.Parse(alu.curso.nivel.codigo.ElementAt(0).ToString()) ;
                Nivel nivelSiguiente = null;
                Nivel[] tempNivs = new Nivel[2];
                this.comboBox3.DataSource = null;
                if (año != 6){
                    // no es un egreso
                    if (alu.orientacion.codigo == "null")
                    {
                        tempNivs = niveles.Where(x => (x.codigo.Contains((año + 1).ToString())) && x.materia != null).ToArray();
                    }
                    else
                    {
                        List<Nivel> temp = niveles.Where(x => (x.codigo.Contains((año + 1).ToString())) && (x.orientacion.codigo.Equals(alu.orientacion.codigo)) && x.materia != null).ToList();
                        if(temp.Count > 0)
                        {
                            nivelSiguiente = temp.First();
                        }
                        else
                        {
                            nivelSiguiente = null;
                        }
                            
                    }
                    this.comboBox3.DataSource = null;
                    if(nivelSiguiente != null)
                    {
                        this.comboBox3.DataSource = cursos.Where(x => x.nivel.id.Equals(nivelSiguiente.id)).ToArray();
                    }
                    else
                    {
                        if(tempNivs != null && tempNivs.Length > 0 && tempNivs[0]!=null)
                        this.comboBox3.DataSource = cursos.Where(x => (tempNivs.Length > 0 && x.nivel.id.Equals(tempNivs[0].id)) || (tempNivs.Length > 1 && x.nivel.id.Equals(tempNivs[1].id))).ToArray();
                    }
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
                    ultimoCursoSeleccionado = curso;
                    this.dataGridView1.DataSource = this.administracion.listarAlumnosPorCursoYNivel(null, curso);
                }
                else
                {
                    this.ultimoCursoSeleccionado = null;
                    this.dataGridView1.DataSource = null;
                    this.dataGridView1.DataSource = this.administracion.listarAlumnosPorCursoYNivel(null, null);
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
