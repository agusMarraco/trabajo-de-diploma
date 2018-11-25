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
    public partial class Login : Form
    {
        private Regex alphanumericRegex = new Regex("^[a-zA-Z0-9ñÑ]+$");
        
        public Login()
        {
            InitializeComponent();
            //this.txtUsername.KeyPress += TxtUsername_KeyPress;
            //this.txtPassword.KeyPress += TxtPassword_KeyPress;
            this.txtUsername.KeyPress += capturarEnter;
            this.txtPassword.KeyPress += capturarEnter;
            this.button1.KeyPress += capturarEnter;
            this.button2.KeyPress += capturarEnter;
        }


        private void Login_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.helpProvider1.SetHelpKeyword(this, "LoginES.htm");
            this.helpProvider1.HelpNamespace = Application.StartupPath + @"\\DocumentsDeAyuda.chm";
            
        }

        private void capturarEnter(object sender, KeyPressEventArgs e)
        {
            bool esUnEnter = e.KeyChar.ToString().Equals("\r");
            if (esUnEnter)
            {
                if (this.button1.Focused)
                    this.button1.PerformClick();
                else if (this.button2.Focused)
                    this.button2.PerformClick();
                else
                    this.button1.PerformClick();
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
                Usuario usuario = new Usuario();
                usuario.id = 1L;
                StringBuilder sb = new StringBuilder();
                sb.Append("Alguien intentó ingresar sin completar los campos");
                if(!String.IsNullOrEmpty(user))
                    sb.Append(", con este alias: " + user);
                servicioSeguridad.grabarBitacora(usuario, sb.ToString(), CriticidadEnum.ALTA);
                MessageBox.Show("Complete los campos requeridos");
                this.txtUsername.Clear();
                this.txtPassword.Clear();
                this.txtUsername.Focus();
                return;
            }
            if (!alphanumericRegex.IsMatch(user) || !alphanumericRegex.IsMatch(pass))
            {
                MessageBox.Show("Los campos solo admiten letras y números");
                this.txtUsername.Clear();
                this.txtPassword.Clear();
                this.txtUsername.Focus();
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
                    MessageBox.Show("Su usuario o contraseña son incorrectos");
                    this.txtUsername.Clear();
                    this.txtPassword.Clear();
                    this.txtUsername.Focus();
                }
                else if(mensaje == "BLOQUEADO" )
                {
                    MessageBox.Show("Su usuario se encuentra bloqueado, se ha restablecido su contraseña y enviado la misma a su mail");
                    this.txtUsername.Clear();
                    this.txtPassword.Clear();
                    this.txtUsername.Focus();
                }
                else if (mensaje == "PERMISOS")
                {

                 MessageBox.Show("El sistema se encuentra bloqueado, contacte al administrador");
                    this.txtUsername.Clear();
                    this.txtPassword.Clear();
                    this.txtUsername.Focus();
                }
                else 
                {
                    MessageBox.Show("A ocurrido un error inesperado: " + mensaje);
                    this.txtUsername.Clear();
                    this.txtPassword.Clear();
                    this.txtUsername.Focus();
                }


            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private void TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!e.KeyChar.Equals('\b'))//tecla borrar
            {
                if (!alphanumericRegex.IsMatch(e.KeyChar.ToString()))
                {
                    e.Handled = true;
                }
            }
        }

        private void TxtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!e.KeyChar.Equals('\b'))//tecla borrar
            {
                if (!alphanumericRegex.IsMatch(e.KeyChar.ToString()))
                {
                    e.Handled = true;
                }
            }
        }

    }
}
