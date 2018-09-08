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
    public partial class Cursos : Form
    {

        private ServicioSeguridad servicioSeguridad;
        public Cursos()
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            new AltaModificacionCurso().Show();

        }

        private void Cursos_Load(object sender, EventArgs e)
        {
            
        }
    }
}
