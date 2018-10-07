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
    public partial class AltaModificacionTutor : Form
    {
        private ServicioSeguridad servicioSeguridad;
        private ServicioAlumnos  servicioAlumnos;
        private Tutores parentForm;
        private Tutor currentTutor = null;
        public AltaModificacionTutor(){ }

        public AltaModificacionTutor(Tutor tutor, Tutores parentForm)
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad();
            this.servicioAlumnos = new ServicioAlumnos();
            this.parentForm = parentForm;
            this.currentTutor = tutor;
        }
        private void AltaModificacionTutor_Load(object sender, EventArgs e)
        {
            if(currentTutor != null)
            {
                this.txtNombre.Text = currentTutor.nombre;
                this.txtApellido.Text = currentTutor.apellido;
                this.txtDni.Text = currentTutor.dni;
                this.txtEmail.Text = currentTutor.email;
                this.txtTel1.Text = currentTutor.telefono1;
                this.txtTel2.Text = currentTutor.telefono2;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(currentTutor != null)
            {
                currentTutor.nombre = this.txtNombre.Text;
                currentTutor.apellido = this.txtApellido.Text;
                currentTutor.dni = this.txtDni.Text;
                currentTutor.email = this.txtEmail.Text;
                currentTutor.telefono1 = this.txtTel1.Text;
                currentTutor.telefono2 = this.txtTel2.Text;
                try
                {
                    this.servicioAlumnos.modificarTutor(currentTutor);
                    MessageBox.Show("Completado");
                    this.parentForm.buscarTutores(null, null);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                Tutor nuevoTutor = new Tutor();
                nuevoTutor.nombre = this.txtNombre.Text;
                nuevoTutor.apellido = this.txtApellido.Text;
                nuevoTutor.dni = this.txtDni.Text;
                nuevoTutor.email = this.txtEmail.Text;
                nuevoTutor.telefono1 = this.txtTel1.Text;
                nuevoTutor.telefono2 = this.txtTel2.Text;
                try
                {
                    this.servicioAlumnos.guardarTutor(nuevoTutor);
                    MessageBox.Show("Completado");
                    this.parentForm.buscarTutores(null, null);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
