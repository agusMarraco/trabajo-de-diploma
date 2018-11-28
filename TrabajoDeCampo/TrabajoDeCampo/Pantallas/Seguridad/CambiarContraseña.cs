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

namespace TrabajoDeCampo.Pantallas.Seguridad
{
    public partial class CambiarContraseña : Form
    {
        private ServicioSeguridad servicioSeguridad;
        private Regex alphanumericRegex = new Regex("^[a-zA-Z0-9ñÑ]+$");
        private Boolean valido = false;
        private Dictionary<string, string> traducciones;

        public CambiarContraseña()
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad();
        }

   

        private void CambiarContraseña_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.helpProvider1.SetHelpKeyword(this, Properties.Settings.Default.Idioma.Equals("es") ? "Opciones.htm" : "Options.htm");
            this.helpProvider1.HelpNamespace = Application.StartupPath + @"\\DocumentsDeAyuda.chm";
            this.actual.KeyPress += validarAlphaKP;
            this.nueva.KeyPress += validarAlphaKP;
            this.nuevaRepetido.KeyPress += validarAlphaKP;

            //traduccion
            FormUtils traductor = new TraductorIterador();
            List<String> tags = new List<string>();
            long id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            traductor.process(tags, this, null, null);
            tags.Add("com.td.char.count");
            tags.Add("com.td.pass.no.coinciden");
            tags.Add("com.td.pass.iguales");
            tags.Add("com.td.complete.campos");
            tags.Add("com.td.completado");
            traducciones = servicioSeguridad.traerTraducciones(tags, Properties.Settings.Default.Idioma);
            traductor = new TraductorReal();
            traductor.process(null, this, traducciones, null);
            traductor = new TraductorIterador();

        }

        private void validarAlphaKP(object sender, KeyPressEventArgs e)
        {
            if (!e.KeyChar.Equals('\b'))//tecla borrar
            {
                if (!alphanumericRegex.IsMatch(e.KeyChar.ToString()))
                {
                    e.Handled = true;
                }
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if(this.actual.Text != null && this.nueva.Text != null && this.nuevaRepetido.Text != null)
            {
                //verificacion de regex
                if (this.actual.Text.Length < 8 || this.nueva.Text.Length < 8 || this.nuevaRepetido.Text.Length < 8)
                {
                    MessageBox.Show(traducciones["com.td.char.count"]);
                    return;
                }

                    if (this.actual.Text.Equals(this.nueva))
                {
                    MessageBox.Show(traducciones["com.td.pass.iguales"]);
                    return;
                }
                else
                {
                    if (this.nueva.Text.Equals(this.nuevaRepetido.Text))
                    {
                        long usuario = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
                        this.servicioSeguridad.cambiarContraseña(usuario, this.nueva.Text);
                        MessageBox.Show(traducciones["com.td.completado"]);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(traducciones["com.td.pass.no.coinciden"]);
                    }

                }
            }
            else
            {
                MessageBox.Show(traducciones["com.td.complete.campos"]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
