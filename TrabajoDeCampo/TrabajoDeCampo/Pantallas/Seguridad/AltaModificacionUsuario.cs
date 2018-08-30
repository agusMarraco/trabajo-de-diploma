using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabajoDeCampo.Pantallas.Seguridad
{
    public partial class AltaModificacionUsuario : Form
    {
        public AltaModificacionUsuario()
        {
            InitializeComponent();
            List<PEP> lista = new List<PEP>();
            lista.Add(new PEP("A", true));
            lista.Add(new PEP("A", true));
            lista.Add(new PEP("A", false));
            List<PEP> lista2 = new List<PEP>();
            lista2.Add(new PEP("b", false));
            lista2.Add(new PEP("b", true));
            lista2.Add(new PEP("b", false));
       
            this.dataGridView1.DataSource = null;
            this.dataGridView2.DataSource = null;
            this.dataGridView1.DataSource = lista;
            foreach (var item in this.dataGridView1.Rows[1].Cells)
            {
                ((DataGridViewCell)item).ReadOnly = true;
            }
            
            this.dataGridView1.Rows[1].ReadOnly = true;
            this.dataGridView2.DataSource = lista2;
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void AltaModificacionUsuario_Load(object sender, EventArgs e)
        {

        }
    }

    public class PEP {

        public PEP(String str, bool bl){
            this.ck = bl;
            this.myVar = str;
        }
        private String myVar;

        public String MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }

        private bool ck;

        public bool _ck
        {
            get { return ck; }
            set { ck = value; }
        }

        public override string ToString()
        {
            return this.myVar;
        }

    }
}
