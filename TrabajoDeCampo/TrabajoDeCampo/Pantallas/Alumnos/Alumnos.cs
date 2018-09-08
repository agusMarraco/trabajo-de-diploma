using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoDeCampo.SEGURIDAD;
using TrabajoDeCampo.SERVICIO;

namespace TrabajoDeCampo.Pantallas.Alumnos
{
    public partial class Alumnos : Form
    {
        private ServicioSeguridad servicioSeguridad;
        public Alumnos()
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new AltaModificacionAlumno().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Inasistencias().Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new Amonestaciones().Show();
        }

        private void Alumnos_Load(object sender, EventArgs e)
        {

            this.dataGridView1.Columns[0].Tag = "com.td.nombre";
            this.dataGridView1.Columns[1].Tag = "com.td.apellido";
            this.dataGridView1.Columns[2].Tag = "com.td.d.n.i";
            this.dataGridView1.Columns[3].Tag = "com.td.curso";
            Traductor traductor = new TraductorIterador();
            List<String> tags = new List<string>();
            traductor.process(tags, this, null);
            Dictionary<String, String> traducciones = servicioSeguridad.traerTraducciones(tags, Properties.Settings.Default.Idioma);
            traductor = new TraductorReal();
            traductor.process(null, this, traducciones);
            traductor = new TraductorIterador();
        }
    }
}
