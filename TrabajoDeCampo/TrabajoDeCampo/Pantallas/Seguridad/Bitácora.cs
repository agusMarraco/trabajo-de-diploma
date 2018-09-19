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
    public partial class Bitácora : Form
    {
        private ServicioSeguridad servicioSeguridad;
        public Bitácora()
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad();
            this.dataGridView1.DataSource = null;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.Columns[0].DataPropertyName = "BIT_FECHA";
            this.dataGridView1.Columns[1].DataPropertyName = "BIT_USUARIO";
            this.dataGridView1.Columns[2].DataPropertyName = "BIT_CRITICIDAD_ID";
            this.dataGridView1.Columns[3].DataPropertyName = "BIT_MENSAJE";
            this.dataGridView1.DataMember = "Table";
            DataSet set = this.servicioSeguridad.listarBitacora("","","");
            DataTable table = set.Tables[0];
            table.AsEnumerable().
            this.dataGridView1.DataSource = set;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Bitácora_Load(object sender, EventArgs e)
        {
            
        }
    }
}
