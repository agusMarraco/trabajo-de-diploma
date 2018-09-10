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
    public partial class ListaDeUsuarios : Form
    {
        private ServicioSeguridad servicioSeguridad;
        public ListaDeUsuarios()
        {
            InitializeComponent();
            servicioSeguridad = new ServicioSeguridad();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AltaModificacionUsuario user = new AltaModificacionUsuario(false,null);
            user.Show();
        }

        private void ListaDeUsuarios_Load(object sender, EventArgs e)
        {
            
        }
    }
}
