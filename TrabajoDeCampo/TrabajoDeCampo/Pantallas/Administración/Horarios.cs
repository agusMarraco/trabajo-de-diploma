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
    public partial class Horarios : Form
    {
        private ServicioSeguridad servicioSeguridad;
        public Horarios()
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new AltaModificacionHorario().Show();
        }

        private void Horarios_Load(object sender, EventArgs e)
        {
            
        }
    }
}
