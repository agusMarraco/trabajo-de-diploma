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

namespace TrabajoDeCampo.Pantallas.Seguridad
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if(TrabajoDeCampo.Properties.Settings.Default.Bloqueado == 1)
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
    }
}
