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
    public partial class AltaModificacionMateria : Form
    {
        private ServicioSeguridad servicioSeguridad;
        public AltaModificacionMateria()
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad();
        }

        private void AltaModificacionMateria_Load(object sender, EventArgs e)
        {
            
        }
    }
}
