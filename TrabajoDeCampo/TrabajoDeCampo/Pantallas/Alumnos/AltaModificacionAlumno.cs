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

namespace TrabajoDeCampo.Pantallas.Alumnos
{
    public partial class AltaModificacionAlumno : Form
    {
        private ServicioSeguridad servicioSeguridad;
        private ServicioAdministracion servicioAdministracion;
        private ServicioAlumnos servicioAlumnos;

        private Alumno currentAlumno = null;
        private Alumnos parentForm = null;

        private Regex lettersRegex = new Regex("[a-zA-z]");
        private Regex numbersRegex = new Regex("[0-9]");
        private Regex alphanumericRegex = new Regex("[0-9a-zA-z]");
        private Boolean valido = false;

        public Dictionary<string, string> traducciones { get;  set; }

        public AltaModificacionAlumno()
        {
            InitializeComponent();
            servicioSeguridad = new ServicioSeguridad();

        }

        public AltaModificacionAlumno(Alumno alumno, Alumnos parentform)
        {
            InitializeComponent();
            this.servicioAlumnos = new ServicioAlumnos();
            this.servicioAdministracion = new ServicioAdministracion();
            this.servicioSeguridad = new ServicioSeguridad();
            this.parentForm = parentform;
            if (alumno != null)
            {
                this.currentAlumno = alumno;
            }
            this.nombretx.KeyDown += validarLetrasKeyDown;
            this.nombretx.KeyPress += validarLetrasKeyPress;
            this.apellidotx.KeyDown += validarLetrasKeyDown;
            this.apellidotx.KeyPress += validarLetrasKeyPress;
            this.dnitx.KeyDown += Dnitx_KeyDown;
            this.dnitx.KeyPress += Dnitx_KeyPress;
            this.domicilotx.KeyPress += Domicilotx_KeyPress;
            this.domicilotx.KeyDown += Domicilotx_KeyDown;
        }

        

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void AltaModificacionAlumno_Load(object sender, EventArgs e)
        {
            this.helpProvider1.SetHelpKeyword(this, Properties.Settings.Default.Idioma.Equals("es") ? "Alumnos.htm" : "Students.htm");
            this.helpProvider1.HelpNamespace = Application.StartupPath + @"\\DocumentsDeAyuda.chm";
            List<Curso> cursos = servicioAdministracion.listarCursos(null, null, null);
            List<Orientacion> orientaciones = servicioAlumnos.listarOrientacion();
            Orientacion fake = new Orientacion();
            fake.nombre = "";
            fake.codigo = "null";
            orientaciones.Add(fake);
            List<Tutor> tutores = servicioAlumnos.listarTutor(null, null, null);
            this.dateTimePicker1.MaxDate = DateTime.Now;

            this.oricombo.DataSource = null;
            this.oricombo.DataSource = orientaciones;
            this.oricombo.DisplayMember = "nombre";
            this.cursocombo.DataSource = null;
            this.cursocombo.DataSource = cursos;
            this.cursocombo.DisplayMember = "codigo";

            this.dataGridView1.DataSource = null;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.Columns[0].ReadOnly = true;
            this.dataGridView1.Columns[1].ReadOnly = true;
            this.dataGridView1.Columns[2].ReadOnly = true;
            this.dataGridView1.Columns[3].ReadOnly = true;
            this.dataGridView1.Columns[0].DataPropertyName = "nombre";
            this.dataGridView1.Columns[1].DataPropertyName = "apellido";
            this.dataGridView1.Columns[2].DataPropertyName = "dni";
            this.dataGridView1.Columns[3].DataPropertyName = "email";
            this.dataGridView1.Columns[4].DataPropertyName = "asignado";
            this.dataGridView1.Columns[0].Tag = "com.td.nombre";
            this.dataGridView1.Columns[1].Tag = "com.td.apellido";
            this.dataGridView1.Columns[2].Tag = "com.td.d.n.i.";
            this.dataGridView1.Columns[3].Tag = "com.td.mail";
            this.dataGridView1.Columns[4].Tag = "com.td.asignado";
            this.dataGridView1.ColumnHeaderMouseClick += customSort;
            this.groupBox1.Tag = "com.td.información";
            this.groupBox2.Tag = "com.td.tutores";
        

            if (this.currentAlumno != null)
            {
                this.nombretx.Text = currentAlumno.nombre;
                this.apellidotx.Text = currentAlumno.apellido;
                this.dnitx.Text = currentAlumno.dni;
                this.dateTimePicker1.Value = currentAlumno.fechaNacimiento;
                this.domicilotx.Text = currentAlumno.domicilio;
                foreach (Orientacion iter in orientaciones)
                {
                    if (iter.codigo.Equals(currentAlumno.orientacion.codigo))
                    {
                        this.oricombo.SelectedItem = iter;
                        break;
                    }
                }

                foreach (Curso cur in cursos)
                {
                    if (cur.id == this.currentAlumno.curso.id)
                    {
                        this.cursocombo.SelectedItem = cur;
                        break;
                    }
                }
                foreach (Tutor tut in tutores)
                {
                    foreach (Tutor tut2 in currentAlumno.tutores)
                    {
                        if (tut2.id.Equals(tut.id))
                        {
                            tut.asignado = true;
                        }
                    }
                }
            }
            BindingList<Tutor> bind = new BindingList<Tutor>(tutores);
            this.dataGridView1.DataSource = bind;


            //traduccion
            FormUtils traductor = new TraductorIterador();
            List<String> tags = new List<string>();
            long id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            traductor.process(tags, this, null, null);
            tags.AddRange(new String[] {"com.td.complete.campos","com.td.orientacion.incorrecta","com.td.tutor.requerido","com.td.completado","com.td.dni.repetido"});
            traducciones = servicioSeguridad.traerTraducciones(tags, Properties.Settings.Default.Idioma);
            traductor = new TraductorReal();
            traductor.process(null, this, traducciones, null);
            traductor = new TraductorIterador();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //validaciones
            if (String.IsNullOrEmpty(this.nombretx.Text) || String.IsNullOrEmpty(this.apellidotx.Text) || String.IsNullOrEmpty(this.dnitx.Text)
                || String.IsNullOrEmpty(this.domicilotx.Text))
            {
                MessageBox.Show(traducciones["com.td.complete.campos"]);
            }

            Boolean hayQueValidarCursoOrientacion = false;
            hayQueValidarCursoOrientacion = (((Curso)cursocombo.SelectedItem).codigo.Contains("4") || ((Curso)cursocombo.SelectedItem).codigo.Contains("5") ||
                ((Curso)cursocombo.SelectedItem).codigo.Contains("6"));
            if (this.currentAlumno != null)
            {
                //edit
                currentAlumno.nombre = this.nombretx.Text;
                currentAlumno.apellido = this.apellidotx.Text;
                currentAlumno.dni = this.dnitx.Text;
                currentAlumno.fechaNacimiento = this.dateTimePicker1.Value;
                currentAlumno.domicilio = this.domicilotx.Text;
                currentAlumno.curso = (Curso)cursocombo.SelectedItem;
                Nivel nivel = currentAlumno.curso.nivel;
                if (hayQueValidarCursoOrientacion)
                {
                    if ((nivel.orientacion == null && ((Orientacion)this.oricombo.SelectedItem).codigo != "null") || nivel.orientacion.codigo != ((Orientacion)this.oricombo.SelectedItem).codigo)
                    {
                        MessageBox.Show(traducciones["com.td.orientacion.incorrecta"]);
                        return;
                    }
                }
               
                if (this.oricombo.SelectedItem != null && ((Orientacion)this.oricombo.SelectedItem).codigo != "null")
                {
                    
                    currentAlumno.orientacion = ((Orientacion)this.oricombo.SelectedItem);
                }
                else
                {
                    currentAlumno.orientacion = null;
                }
                currentAlumno.tutores = new List<Tutor>();
                foreach (Tutor tut in (BindingList<Tutor>)this.dataGridView1.DataSource)
                {
                    if (tut.asignado)
                        currentAlumno.tutores.Add(tut);
                }
                if(currentAlumno.tutores.Count == 0)
                {
                    MessageBox.Show(traducciones["com.td.tutor.requerido"]);
                    return;
                }
                try
                {
                    this.servicioAlumnos.actualizarAlumno(currentAlumno);
                    MessageBox.Show(traducciones["com.td.completado"]);
                    this.parentForm.listarAlumnos(null, null);
                    this.Close();
                }
                catch (Exception ex)
                {
                    if (ex.Message.Equals("DNI"))
                    {
                        MessageBox.Show(traducciones["com.td.dni.repetido"]);
                    }
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }

                }

            }
            else
            {
                //nuevo
                Alumno nuevo = new Alumno();
                nuevo.nombre = this.nombretx.Text;
                nuevo.apellido = this.apellidotx.Text;
                nuevo.dni = this.dnitx.Text;
                nuevo.fechaNacimiento = this.dateTimePicker1.Value;
                nuevo.domicilio = this.domicilotx.Text;
                nuevo.curso = (Curso)cursocombo.SelectedItem;
                Nivel nivel = nuevo.curso.nivel;
                if (hayQueValidarCursoOrientacion)
                {

                    if ((nivel.orientacion == null && ((Orientacion)this.oricombo.SelectedItem).codigo != "null") || nivel.orientacion.codigo != ((Orientacion)this.oricombo.SelectedItem).codigo)
                    {
                        MessageBox.Show(traducciones["com.td.orientacion.incorrecta"]);
                        return;
                    }
                }
                if (this.oricombo.SelectedItem != null && ((Orientacion)this.oricombo.SelectedItem).codigo != "null")
                {
                    nuevo.orientacion = ((Orientacion)this.oricombo.SelectedItem);
                }
                else
                {
                    nuevo.orientacion = null;
                }
                nuevo.tutores = new List<Tutor>();
                foreach (Tutor tut in (BindingList<Tutor>)this.dataGridView1.DataSource)
                {
                    if (tut.asignado)
                        nuevo.tutores.Add(tut);
                }
                if (nuevo.tutores.Count == 0)
                {
                    MessageBox.Show(traducciones["com.td.tutor.requerido"]);
                    return;
                }
                try
                {
                    this.servicioAlumnos.guardarAlumno(nuevo);
                    MessageBox.Show(traducciones["com.td.completado"]);
                    this.parentForm.listarAlumnos(null, null);
                    this.Close();
                }
                catch (Exception ex)
                {
                    if (ex.Message.Equals("DNI"))
                    {
                        MessageBox.Show(traducciones["com.td.dni.repetido"]);
                    }
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
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
                }
            }
            //initialSorting
            if (this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None || this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                List<Tutor> tutores = (List<Tutor>)this.dataGridView1.DataSource;
                string propertyName = this.dataGridView1.Columns[e.ColumnIndex].Name;
                this.dataGridView1.DataSource = tutores.OrderBy(x => x.GetType().GetProperty(propertyName).GetValue(x)).ToList();
                this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;

            }
            else
            {
                List<Tutor> tutores = (List<Tutor>)this.dataGridView1.DataSource;
                string propertyName = this.dataGridView1.Columns[e.ColumnIndex].Name;
                this.dataGridView1.DataSource = tutores.OrderBy(x => x.GetType().GetProperty(propertyName).GetValue(x)).ToList();
                this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;

            }
        }


        //validaciones 
        private void Domicilotx_KeyDown(object sender, KeyEventArgs e)
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

        private void Domicilotx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!valido)
            {
                e.Handled = true;
            }
        }

        private void Dnitx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!valido)
            {
                e.Handled = true;
            }
        }

        private void Dnitx_KeyDown(object sender, KeyEventArgs e)
        {
            valido = true;
            if (!e.KeyValue.Equals(8))//tecla borrar
            {
                if (!numbersRegex.IsMatch(e.KeyData.ToString()) || e.KeyData.ToString().Contains("Oem"))
                {
                    valido = false;
                }
            }
        }

        private void validarLetrasKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!valido)
            {
                e.Handled = true;
            }
        }

        private void validarLetrasKeyDown(object sender, KeyEventArgs e)
        {
            valido = true;
            if (!e.KeyValue.Equals(8))//tecla borrar
            {
                if (!lettersRegex.IsMatch(e.KeyData.ToString()) || e.KeyData.ToString().Contains("Oem"))
                {
                    valido = false;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
