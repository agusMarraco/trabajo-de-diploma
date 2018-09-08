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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void CambiarContraseña_Load(object sender, EventArgs e)
        {
            
        }
    }
}
