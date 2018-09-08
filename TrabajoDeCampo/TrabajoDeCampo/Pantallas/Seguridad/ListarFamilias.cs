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
    public partial class ListarFamilias : Form
    {
        private ServicioSeguridad servicioSeguridad;
        public ListarFamilias()
        {
            InitializeComponent();
            servicioSeguridad = new ServicioSeguridad();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Alta_ModificarFamilia familia = new Alta_ModificarFamilia();
            familia.Show();
        }

        private void ListarFamilias_Load(object sender, EventArgs e)
        {
            
        }
    }
}
