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
    public partial class Tutores : Form
    {
        private ServicioSeguridad servicioSeguridad;
        public Tutores()
        {
            InitializeComponent();
            this.dataGridView1.Columns[0].Tag = "com.td.nombre";
            this.dataGridView1.Columns[1].Tag = "com.td.apellido";
            this.dataGridView1.Columns[2].Tag = "com.td.d.n.i";
            this.dataGridView1.Columns[3].Tag = "com.td.mail";
            this.dataGridView1.Columns[4].Tag = "com.td.telefonos";
            this.servicioSeguridad = new ServicioSeguridad();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new AltaModificacionTutor().Show();
        }

        private void Tutores_Load(object sender, EventArgs e)
        {
            
        }
    }
}
