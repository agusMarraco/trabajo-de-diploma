using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
    public partial class AltaModificacionTutor : Form
    {
        private ServicioSeguridad servicioSeguridad;
        private ServicioAlumnos  servicioAlumnos;
        private Tutores parentForm;
        private Tutor currentTutor = null;

        private Regex lettersRegex = new Regex("^[a-zA-Z]+$");
        private Regex numbersRegex = new Regex("^[0-9]+$");
        private Boolean valido = false;
        private Dictionary<string, string> traducciones;

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
            this.helpProvider1.SetHelpKeyword(this, Properties.Settings.Default.Idioma.Equals("es") ? "Tutores.htm" : "Tutors.htm");
            this.helpProvider1.HelpNamespace = Application.StartupPath + @"\\DocumentsDeAyuda.chm";
            if (currentTutor != null)
            {
                this.txtNombre.Text = currentTutor.nombre;
                this.txtApellido.Text = currentTutor.apellido;
                this.txtDni.Text = currentTutor.dni;
                this.txtEmail.Text = currentTutor.email;
                this.txtTel1.Text = currentTutor.telefono1;
                this.txtTel2.Text = currentTutor.telefono2;

                this.txtNombre.KeyPress += validarLetrasKP;
                this.txtApellido.KeyPress += validarLetrasKP;
                this.txtDni.KeyPress += validarNumerosKP;
                this.txtTel1.KeyPress += validarNumerosKP;
                this.txtTel2.KeyPress += validarNumerosKP;



            }


            //traduccion
            FormUtils traductor = new TraductorIterador();
            List<String> tags = new List<string>();
            tags.AddRange(new String[] { "com.td.complete.campos", "com.td.mail.invalido", "com.td.completado" });
            long id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            traductor.process(tags, this, null, null);
            traducciones = servicioSeguridad.traerTraducciones(tags, Properties.Settings.Default.Idioma);
            traductor = new TraductorReal();
            traductor.process(null, this, traducciones, null);
            traductor = new TraductorIterador();
        }

        private void validarNumerosKP(object sender, KeyPressEventArgs e)
        {
            if (!e.KeyChar.Equals('\b'))//tecla borrar
            {
                if (!numbersRegex.IsMatch(e.KeyChar.ToString()))
                {
                    e.Handled = true;
                }
            }
        }

        private void validarLetrasKP(object sender, KeyPressEventArgs e)
        {
            if (!e.KeyChar.Equals('\b'))//tecla borrar
            {
                if (!lettersRegex.IsMatch(e.KeyChar.ToString()))
                {
                    e.Handled = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(this.txtNombre.Text.Trim()) || String.IsNullOrEmpty(this.txtApellido.Text.Trim()) || String.IsNullOrEmpty(this.txtDni.Text.Trim()) ||
                String.IsNullOrEmpty(this.txtEmail.Text.Trim()) || String.IsNullOrEmpty(this.txtTel1.Text.Trim())
                || !this.txtTel1.MaskCompleted
                ){
                MessageBox.Show(traducciones["com.td.complete.campos"]);
                return;
            }
            var email = new EmailAddressAttribute();
            bool valid;
            valid = email.IsValid(this.txtEmail.Text);
            if (!valid)
            {
                MessageBox.Show(traducciones["com.td.mail.invalido"]);
                return;
            }
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
                    MessageBox.Show(traducciones["com.td.completado"]);
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
                    MessageBox.Show(traducciones["com.td.completado"]);
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
