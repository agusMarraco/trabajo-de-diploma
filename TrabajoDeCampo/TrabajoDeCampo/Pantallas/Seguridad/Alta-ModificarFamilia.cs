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
    public partial class Alta_ModificarFamilia : Form
    {
        private ServicioSeguridad servicioSeguridad;
        public Alta_ModificarFamilia()
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad();
        }

        private void Alta_ModificarFamilia_Load(object sender, EventArgs e)
        {
            
        }
    }
}
