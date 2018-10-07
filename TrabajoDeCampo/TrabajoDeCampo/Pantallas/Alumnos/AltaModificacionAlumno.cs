using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            this.dataGridView1.ColumnHeaderMouseClick += customSort;

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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //validaciones


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
                        MessageBox.Show("La orientacion no corresponde con ese curso");
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
                    MessageBox.Show("Un Alumno tiene que tener al menos un tutor asignado");
                    return;
                }
                try
                {
                    this.servicioAlumnos.actualizarAlumno(currentAlumno);
                    MessageBox.Show(" Alumno actualizado con exito ");
                    this.parentForm.listarAlumnos(null, null);
                    this.Close();
                }
                catch (Exception ex)
                {
                    if (ex.Message.Equals("DNI"))
                    {
                        MessageBox.Show("Dni Repetido");
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
                        MessageBox.Show("La orientacion no corresponde con ese curso");
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
                    MessageBox.Show("Un Alumno tiene que tener al menos un tutor asignado");
                    return;
                }
                try
                {
                    this.servicioAlumnos.guardarAlumno(nuevo);
                    MessageBox.Show(" Alumno actualizado con exito ");
                    this.parentForm.listarAlumnos(null, null);
                    this.Close();
                }
                catch (Exception ex)
                {
                    if (ex.Message.Equals("DNI"))
                    {
                        MessageBox.Show("Dni Repetido");
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
    }
}
