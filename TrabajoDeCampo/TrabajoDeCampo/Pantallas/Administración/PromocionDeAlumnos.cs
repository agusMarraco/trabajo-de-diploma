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

    public partial class PromocionDeAlumnos : Form
    {
        private ServicioSeguridad servicioSeguridad;
        public PromocionDeAlumnos()
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad();
        }

        private void PromocionDeAlumnos_Load(object sender, EventArgs e)
        {
            
        }
    }
}
