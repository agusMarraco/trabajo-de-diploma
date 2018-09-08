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
    public partial class Inasistencias : Form
    {
        private ServicioSeguridad servicioSeguridad;
        public Inasistencias()
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad();

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Inasistencias_Load(object sender, EventArgs e)
        {
            
        }
    }
}
