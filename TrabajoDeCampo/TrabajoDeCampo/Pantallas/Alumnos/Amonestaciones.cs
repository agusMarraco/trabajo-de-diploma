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
    public partial class Amonestaciones : Form
    {
        private ServicioSeguridad servicioSeguridad;
        public Amonestaciones()
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad();
        }

        private void Amonestaciones_Load(object sender, EventArgs e)
        {
            
        }
    }
}
