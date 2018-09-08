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

namespace TrabajoDeCampo.Pantallas.Administración
{
    public partial class Materias : Form
    {
        ServicioSeguridad servicioSeguridad;
        public Materias()
        {
            InitializeComponent();
            servicioSeguridad = new ServicioSeguridad();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new AltaModificacionMateria().Show();
        }

        private void Materias_Load(object sender, EventArgs e)
        {
            
        }
    }
}
