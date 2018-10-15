using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoDeCampo.Pantallas.Reports;
using TrabajoDeCampo.SERVICIO;

namespace TrabajoDeCampo.Pantallas.Seguridad
{
    public partial class Bitácora : Form
    {
        private ServicioSeguridad servicioSeguridad;
        private DataGridViewColumn previousSort;
        private String previousSortMode = " ASC ";
        private Boolean seBuscoForFecha = false;
        
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
            this.dataGridView1.CellFormatting += criticidadFormatter;
            DataSet set = this.servicioSeguridad.listarBitacora("","","");
            DataTable table = set.Tables[0];
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
            this.toDatepicker.MaxDate = DateTime.Now.AddMilliseconds(5);
            this.fromDatepicker.MaxDate = DateTime.Now.AddMilliseconds(5);
            this.toDatepicker.Value = this.toDatepicker.MaxDate;
            this.fromDatepicker.Value  = this.fromDatepicker.MaxDate;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            seBuscoForFecha = false;
            previousSort = null;
            currentSort = null;
            previousSortMode = " ASC ";
            currentSortMode = " ASC ";
            DataSet response = new DataSet();
            DataTable table;
            String userText = this.textBox1.Text.Trim().Replace(" ", "");
            response = this.servicioSeguridad.listarBitacora(null, null, null);
            table = response.Tables[0];
            StringBuilder sb = new StringBuilder();
            if (this.chCriticidad.Checked)
            {
                long id = ((KeyValuePair<long, String>)this.comboBox1.SelectedItem).Key;
                sb.Append(" BIT_CRITICIDAD_ID = " + id);
            }
            if (this.chFecha.Checked)
            {
                seBuscoForFecha = true;
                long id = ((KeyValuePair<long, String>)this.comboBox1.SelectedItem).Key;
                if (sb.Length > 0)
                    sb.Append(" AND ");

                sb.Append(" BIT_FECHA > #" + this.fromDatepicker.Value + "# AND BIT_FECHA < #" + this.toDatepicker.Value+ "#");
            }
            if (this.chUsuario.Checked)
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.Append(" USU_ALIAS = '" + userText + "'");
            }

                table.DefaultView.RowFilter = sb.ToString();
                this.dataGridView1.DataSource = null;
                this.dataGridView1.DataSource = table;

        }

        private void fromDatepicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void toDatepicker_ValueChanged(object sender, EventArgs e)
        {
            this.fromDatepicker.MaxDate = this.toDatepicker.Value;
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
                   
                }


            }


        }

        private void criticidadFormatter(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                String translation = "";
                switch ((long)e.Value)
                {
                    case 1:
                        translation = "BAJA";
                        break;
                    case 2:
                        translation = "MEDIA";
                        break;
                    case 3:
                        translation = "ALTA";
                        break;
                }


                e.Value = translation;


            }


            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable datatable = this.dataGridView1.DataSource as DataTable;
            DataTable view = datatable.DefaultView.ToTable();
            String desde = "";
            String hasta = "";
            if (this.seBuscoForFecha)
            {
                desde = this.fromDatepicker.Value.ToShortDateString();
                hasta = this.toDatepicker.Value.ToShortDateString();
            }
            
            List<Object> objetos = new List<object>();
            objetos.Add(view);
            objetos.Add(desde);
            objetos.Add(hasta);
            new ServicioReportes().ejecutarReporte("ReporteBitacora", objetos);

        }
    }
}
