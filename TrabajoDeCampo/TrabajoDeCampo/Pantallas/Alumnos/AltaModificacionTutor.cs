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
    public partial class AltaModificacionTutor : Form
    {
        private ServicioSeguridad servicioSeguridad;
        public AltaModificacionTutor()
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad();
        }

        private void AltaModificacionTutor_Load(object sender, EventArgs e)
        {
            
        }
    }
}
