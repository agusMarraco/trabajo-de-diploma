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
    public partial class Materias : Form
    {
        private ServicioSeguridad servicioSeguridad;

        private ServicioAdministracion servicioAdministracion;
        public Materias()
        {
            InitializeComponent();
            servicioSeguridad = new ServicioSeguridad();
            servicioAdministracion = new ServicioAdministracion();
            this.dataGridView1.DataSource = null;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.Columns[0].DataPropertyName = "nombre";
            this.dataGridView1.Columns[1].DataPropertyName = "tipo";
            this.dataGridView1.Columns[2].DataPropertyName = "descripcion";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new AltaModificacionMateria(null,this).ShowDialog();
        }

        private void Materias_Load(object sender, EventArgs e)
        {
            try
            {
                actualizarLista();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(this.dataGridView1.Rows.Count > 0 && this.dataGridView1.CurrentRow.DataBoundItem != null)
            {
                Materia materiaAModificar = (Materia) this.dataGridView1.CurrentRow.DataBoundItem;
                AltaModificacionMateria vista = new AltaModificacionMateria(materiaAModificar,this);
                vista.ShowDialog();
                
            }
        }

        public void actualizarLista()
        {
            List<Materia> materias = this.servicioAdministracion.listarMaterias("", "", "");
            this.dataGridView1.DataSource = materias;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows.Count > 0 && this.dataGridView1.CurrentRow.DataBoundItem != null)
            {
                Materia materiaAModificar = (Materia)this.dataGridView1.CurrentRow.DataBoundItem;
                try
                {
                    servicioAdministracion.borrarMateria(materiaAModificar);
                    this.actualizarLista();
                }
                catch (Exception ex)
                {

                    if(ex.Message == "ASIGNADA")
                    {
                        MessageBox.Show("La materia esta asignada");
                    }
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                
            }
        }
    }
}
