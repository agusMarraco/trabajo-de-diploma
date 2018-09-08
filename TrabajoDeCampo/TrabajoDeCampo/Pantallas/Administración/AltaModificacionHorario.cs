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

namespace TrabajoDeCampo.Pantallas
{
    public partial class AltaModificacionHorario : Form
    {
        private ServicioSeguridad servicioSeguridad;
        public AltaModificacionHorario()
        {
            
            InitializeComponent();
            servicioSeguridad = new ServicioSeguridad();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void AltaModificacionHorario_Load(object sender, EventArgs e)
        {
            
        }
    }
}
