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
        private DataGridViewColumn previousSort;
        private String previousSortMode = " ASC ";
        
        private DataGridViewColumn currentSort;
        private String currentSortMode = " ASC ";
        public Bitácora()
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad();
            this.dataGridView1.DataSource = null;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.Columns[0].DataPropertyName = "BIT_FECHA";
            this.dataGridView1.Columns[1].DataPropertyName = "USU_ALIAS";
            this.dataGridView1.Columns[2].DataPropertyName = "BIT_CRITICIDAD_ID";
            this.dataGridView1.Columns[3].DataPropertyName = "BIT_MENSAJE";
            this.dataGridView1.DataMember = "Table";
            DataSet set = this.servicioSeguridad.listarBitacora("","","");
            DataTable table = set.Tables[0];
            this.chCriticidad.CheckStateChanged += handleCheckboxChange;
            this.chUsuario.CheckStateChanged += handleCheckboxChange;
            this.chFecha.CheckStateChanged += handleCheckboxChange;
            this.dataGridView1.ColumnHeaderMouseClick += sorting;
            this.dataGridView1.DataMember = "";
            this.dataGridView1.DataSource = table;
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
            List<KeyValuePair<long, String>> criticidad = new List<KeyValuePair<long, string>>();
            criticidad.Add(new KeyValuePair<long, string>(1, "ALTA"));
            criticidad.Add(new KeyValuePair<long, string>(2, "MEDIA"));
            criticidad.Add(new KeyValuePair<long, string>(3, "BAJA"));
            this.comboBox1.DisplayMember = "value";

            this.comboBox1.DataSource = criticidad;
            this.comboBox1.SelectedItem = this.comboBox1.Items[0];
            this.toDatepicker.MaxDate = DateTime.Now;
            this.fromDatepicker.MaxDate = DateTime.Now;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            previousSort = null;
            currentSort = null;
            previousSortMode = " ASC ";
            currentSortMode = " ASC ";
            DataSet response = new DataSet();
            DataTable table;
            if (chCriticidad.Checked)
            {
                long id = ((KeyValuePair<long, String>)this.comboBox1.SelectedItem).Key;
                response = this.servicioSeguridad.listarBitacora("CRITICIDAD", id.ToString() , null);
                table = response.Tables[0];
                this.dataGridView1.DataSource = null;
                this.dataGridView1.DataSource = table;

            }
            else if (chUsuario.Checked)
            {
                if (!String.IsNullOrEmpty(this.textBox1.Text))
                {
                    response = this.servicioSeguridad.listarBitacora("ALIAS", this.textBox1.Text, null);
                    table = response.Tables[0];
                    this.dataGridView1.DataSource = null;
                    this.dataGridView1.DataSource = table;
                }
            }else if (chFecha.Checked)
            {
                if(this.fromDatepicker.Value > this.toDatepicker.Value)
                {
                    MessageBox.Show("Rango de fechas invalido");
                }
                else
                {
                    response = this.servicioSeguridad.listarBitacora("FECHA", this.fromDatepicker.Value.ToShortDateString() + ";" + this.toDatepicker.Value.ToShortDateString(), null);
                    table = response.Tables[0];
                    this.dataGridView1.DataSource = null;
                    this.dataGridView1.DataSource = table;
                }             

            }
            else
            {

                response = this.servicioSeguridad.listarBitacora(null, null, null);
                table = response.Tables[0];
                this.dataGridView1.DataSource = null;
                this.dataGridView1.DataSource = table;
            }
        }

        private void fromDatepicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void toDatepicker_ValueChanged(object sender, EventArgs e)
        {
            this.fromDatepicker.MaxDate = this.toDatepicker.Value;
        }

        private void handleCheckboxChange(object sender, EventArgs e)
        {
            CheckBox trigger = (CheckBox)sender;
            if(trigger.Name == "chFecha")
            {
                if (trigger.Checked)
                {
                    this.chUsuario.Checked = false;
                    this.chCriticidad.Checked = false;
                    this.chUsuario.Enabled = false;
                    this.chCriticidad.Enabled = false;
                }
                else
                {
                    this.chUsuario.Enabled = true;
                    this.chCriticidad.Enabled = true;
                }

            }else if(trigger.Name == "chUsuario")
            {
                if (trigger.Checked)
                {
                    this.chFecha.Checked = false;
                    this.chCriticidad.Checked = false;
                    this.chFecha.Enabled = false;
                    this.chCriticidad.Enabled = false;
                }
                else
                {
                    this.chFecha.Enabled = true;
                    this.chCriticidad.Enabled = true;
                }
            }
            else
            {
                if (trigger.Checked)
                {
                    this.chUsuario.Checked = false;
                    this.chFecha.Checked = false;
                    this.chUsuario.Enabled = false;
                    this.chFecha.Enabled = false;
                }
                else
                {
                    this.chUsuario.Enabled = true;
                    this.chFecha.Enabled = true;
                }
            }
        }

        private void sorting(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumnHeaderCell header = this.dataGridView1.Columns[e.ColumnIndex].HeaderCell;
            DataGridViewColumn column = header.OwningColumn;
            DataTable table = this.dataGridView1.DataSource as DataTable;



            if (previousSort == null)
            {
                previousSort = column;
                currentSort = column;
                table.DefaultView.Sort = column.Name + currentSortMode;


            }
            else
            {
                if (previousSort == currentSort)
                {
                    if (column == currentSort)
                    {
                        currentSortMode = currentSortMode == " ASC " ? " DESC " : " ASC ";
                        currentSort.HeaderCell.SortGlyphDirection = currentSort.HeaderCell.SortGlyphDirection  == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
                        previousSortMode = currentSortMode;
                        table.DefaultView.Sort = currentSort.Name + currentSortMode;
                    }
                    else
                    {
                        currentSort = column;
                        currentSortMode = " ASC ";
                        currentSort.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                        table.DefaultView.Sort = previousSort.Name + previousSortMode + " , " + currentSort.Name + currentSortMode;
                    }
                }
                else
                {
                    if (currentSort == column)
                    {
                        currentSortMode = currentSortMode == " ASC " ? " DESC " : " ASC ";
                        
                        currentSort.HeaderCell.SortGlyphDirection = currentSort.HeaderCell.SortGlyphDirection == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
                    }
                    else
                    {
                        previousSort = currentSort;

                        previousSort.HeaderCell.SortGlyphDirection = previousSort.HeaderCell.SortGlyphDirection == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending; 
                        currentSort = column;
                        previousSortMode = currentSortMode;
                        currentSortMode = " ASC ";
                        
                        currentSort.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }


                    table.DefaultView.Sort = previousSort.Name + previousSortMode + " , " + currentSort.Name + currentSortMode;
                   // table.DefaultView.Sort = previousSort.Name + " , " + currentSort.Name;
                }


            }


        }
    }
}
