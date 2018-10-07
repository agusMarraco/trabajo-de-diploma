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
            this.dataGridView1.ColumnHeaderMouseClick += customSort;
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

        private void customSort(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach (DataGridViewColumn column in ((DataGridView)sender).Columns)
            {
                if (column.Index != e.ColumnIndex)
                {
                    column.HeaderCell.SortGlyphDirection = SortOrder.None;
                    column.HeaderCell.Style.BackColor = Color.White;
                }
            }
            //initialSorting
            if (this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None || this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                List<Materia> materias = (List<Materia>)this.dataGridView1.DataSource;
                string propertyName = this.dataGridView1.Columns[e.ColumnIndex].Name;
                this.dataGridView1.DataSource = null;
                    materias.Sort((x, y) => x.GetType().GetProperty(propertyName).GetValue(x).ToString().CompareTo
                     (y.GetType().GetProperty(propertyName).GetValue(y).ToString()));
                    this.dataGridView1.DataSource = materias;
                this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.LightSkyBlue;
                this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;

            }
            else
            {
                List<Materia> materias = (List<Materia>)this.dataGridView1.DataSource;
                this.dataGridView1.DataSource = null;
                string propertyName = this.dataGridView1.Columns[e.ColumnIndex].Name;
                    materias.Sort((x, y) => y.GetType().GetProperty(propertyName).GetValue(y).ToString().CompareTo
                     (x.GetType().GetProperty(propertyName).GetValue(x).ToString()));
                    this.dataGridView1.DataSource = materias;                
                this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.LightSkyBlue;
                this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;

            }


        }
    }
}
