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
using TrabajoDeCampo.SERVICIO;

namespace TrabajoDeCampo.Pantallas.Seguridad
{
    public partial class Login : Form
    {
        private Regex alphanumericRegex = new Regex("[0-9a-zA-z]");
        private Boolean valido = false;
        public Login()
        {
            InitializeComponent();
            this.txtUsername.KeyDown += TxtUsername_KeyDown;
            this.txtUsername.KeyPress += TxtUsername_KeyPress;
            this.txtPassword.KeyPress += TxtPassword_KeyPress;
            this.txtPassword.KeyDown += TxtPassword_KeyDown;
        }


        private void Login_Load(object sender, EventArgs e)
        {
            this.helpProvider1.SetHelpKeyword(this, "LoginES.htm");
            this.helpProvider1.HelpNamespace = Application.StartupPath + @"\\DocumentsDeAyuda.chm";
            if (TrabajoDeCampo.Properties.Settings.Default.Bloqueado == 1)
            {
                this.label3.Visible = true;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServicioSeguridad servicioSeguridad = new ServicioSeguridad();
            string user = this.txtUsername.Text;
            string pass = this.txtPassword.Text;
            if(String.IsNullOrEmpty(user) || String.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Complete los campos requeridos");
                return;
            }
            try
            {
                servicioSeguridad.loguear(user, pass, null);

                Seguridad.Menu menu = new Seguridad.Menu();
                this.Close();
                menu.ShowDialog();
            }
            catch (Exception ex)
            {

                String mensaje = ex.Message;
                if(mensaje == "ALIAS" || mensaje == "PASS")
                {
                    MessageBox.Show("Su usuario o contraseña son incorrectos: ");
                }
                else if(mensaje == "BLOQUEADO" )
                {
                    MessageBox.Show("su usuario se encuentra bloqueado, comuniquese con el administrador");
                }
                else if (mensaje == "PERMISOS")
                {

                 MessageBox.Show("El sistema se encuentra bloqueado, contacte al administrador");
                }
                else 
                {
                    MessageBox.Show("A ocurrido un error inesperado: " + mensaje);

                }


            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            valido = true;
            if (!e.KeyValue.Equals(8))//tecla borrar
            {
                if(!alphanumericRegex.IsMatch(e.KeyData.ToString()) || e.KeyData.ToString().Contains("Oem"))
                {
                    valido = false;
                }
            }
        }

        private void TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!valido)
            {
                e.Handled = true;
            }
        }

        private void TxtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!valido)
            {
                e.Handled = true;
            }
        }

        private void TxtUsername_KeyDown(object sender, KeyEventArgs e)
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
    }
}
