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
    public partial class CambiarContraseña : Form
    {
        private ServicioSeguridad servicioSeguridad;
        public CambiarContraseña()
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad() ;
        }

   

        private void CambiarContraseña_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.actual.Text != null && this.nueva.Text != null && this.nuevaRepetido.Text != null)
            {
                //verificacion de regex

                if (this.actual.Text.Equals(this.nueva))
                {
                    MessageBox.Show("Las contraseñas no son distintas");
                }
                else
                {
                    if (this.nueva.Text.Equals(this.nuevaRepetido.Text))
                    {
                        long usuario = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
                        this.servicioSeguridad.cambiarContraseña(usuario, this.nueva.Text);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Las contraseñas nuevas no coincided");
                    }

                }
            }
            else
            {
                MessageBox.Show("Todos los campos son obligatorios");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
