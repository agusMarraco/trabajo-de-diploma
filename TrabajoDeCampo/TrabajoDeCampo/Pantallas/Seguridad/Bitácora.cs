using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoDeCampo.Pantallas.Reports;
using TrabajoDeCampo.SEGURIDAD;
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
        private Dictionary<string, string> traducciones;
        private Regex alphanumericRegex = new Regex("^[a-zA-Z0-9 ñÑ]+$");
        private List<KeyValuePair<long, String>> criticidad;
        List<Usuario> usuarios;

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
            this.dataGridView1.Columns[0].Tag = "com.td.fecha";
            this.dataGridView1.Columns[1].Tag = "com.td.alias";
            this.dataGridView1.Columns[2].Tag = "com.td.criticidad";
            this.dataGridView1.Columns[3].Tag = "com.td.mensaje";
            this.dataGridView1.DataMember = "Table";
            this.dataGridView1.CellFormatting += criticidadFormatter;
            DataSet set = this.servicioSeguridad.listarBitacora("","","");
            DataTable table = set.Tables[0];
            this.dataGridView1.ColumnHeaderMouseClick += sorting;
            this.dataGridView1.DataMember = "";
            //this.dataGridView1.DataSource = table; deshabilito el default search
            this.chUsuario.CheckedChanged += eventoCheckbox;
            this.chFecha.CheckedChanged += eventoCheckbox;
            this.chCriticidad.CheckedChanged += eventoCheckbox;
        }

        private void eventoCheckbox(object sender, EventArgs e)
        {
            String chName = ((CheckBox)sender).Name;
            CheckBox check = ((CheckBox)sender);
            if (chName.Equals("chUsuario"))
            {
                if (check.CheckState.Equals(CheckState.Checked)){
                    this.comboUsuarios.Enabled = true;
                    this.comboUsuarios.DataSource = null;
                    this.comboUsuarios.ValueMember = "alias";
                    this.comboUsuarios.DisplayMember = "nombreCompleto";
                    this.comboUsuarios.DataSource = usuarios;
                    if(this.comboUsuarios.Items[0] != null)
                    {
                        this.comboUsuarios.SelectedItem = this.comboUsuarios.Items[0];
                    }
                }
                else
                {
                    this.comboUsuarios.Enabled = false;
                    this.comboUsuarios.DataSource = null;
                    this.comboUsuarios.Text = "";
                }
            }
            else if (chName.Equals("chCriticidad"))
            {
                if (check.CheckState.Equals(CheckState.Checked))
                {
                    this.comboBox1.Enabled = true;
                    this.comboBox1.DataSource = criticidad;
                    this.comboBox1.DisplayMember = "value";
                    this.comboBox1.SelectedItem = this.comboBox1.Items[0];
                }
                else
                {
                    this.comboBox1.Enabled = false;
                    this.comboBox1.DataSource = null;
                }
            }
            else if (chName.Equals("chFecha"))
            {
                if (check.CheckState.Equals(CheckState.Checked))
                {
                    this.fromDatepicker.Enabled = true;
                    this.toDatepicker.Enabled = true;
                }
                else
                {
                    this.fromDatepicker.Enabled = false;
                    this.toDatepicker.Enabled = false;
                }
            }
        }


        private void Bitácora_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;

            this.helpProvider1.SetHelpKeyword(this, Properties.Settings.Default.Idioma.Equals("es") ? "Bitácora.htm" : "Audits.htm");
            this.helpProvider1.HelpNamespace = Application.StartupPath + @"\\DocumentsDeAyuda.chm";
            //traduccion
            FormUtils traductor = new TraductorIterador();
            List<String> tags = new List<string>();
            long id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            traductor.process(tags, this, null, null);
            tags.AddRange(new string[] { "com.td.criticidad.alta", "com.td.rango.fecha.invalido", "com.td.criticidad.media", "com.td.criticidad.baja" });
            traducciones = servicioSeguridad.traerTraducciones(tags, Properties.Settings.Default.Idioma);
            traductor = new TraductorReal();
            traductor.process(null, this, traducciones, null);
            traductor = new TraductorIterador();

            //data del dropdown de seguridad
            criticidad = new List<KeyValuePair<long, string>>();
            criticidad.Add(new KeyValuePair<long, string>(1, traducciones["com.td.criticidad.alta"]));
            criticidad.Add(new KeyValuePair<long, string>(2, traducciones["com.td.criticidad.media"]));
            criticidad.Add(new KeyValuePair<long, string>(3, traducciones["com.td.criticidad.baja"]));



            this.comboUsuarios.KeyPress += validarAlphaKP;
            //agregando la llamada a los usuarios
            usuarios = this.servicioSeguridad.listarUsuarios(null, null, null);

            //agregando los 2 usuarios custom
            Usuario usuSYS = new Usuario();
            Usuario usuDV = new Usuario();
            usuSYS.alias = "SYS";
            usuDV.alias = "DV";
            usuarios.Add(usuSYS);
            usuarios.Add(usuDV);

            //end usuario
            //data de los datepickers
            this.toDatepicker.MaxDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            this.fromDatepicker.MaxDate = DateTime.Now.Date;
            this.toDatepicker.Value = this.toDatepicker.MaxDate;
            this.fromDatepicker.Value  = this.fromDatepicker.MaxDate;
            this.comboUsuarios.Enabled = false;
            this.comboBox1.Enabled = false;
            this.fromDatepicker.Enabled = false;
            this.toDatepicker.Enabled = false;

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
            String userText = "";
            
            if(this.comboUsuarios.SelectedItem != null)
            {
                userText = this.comboUsuarios.SelectedValue.ToString();
            }
            else
            {
                userText = this.comboUsuarios.Text;
            }
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
                if (this.fromDatepicker.Value > this.toDatepicker.Value)
                {
                    MessageBox.Show(traducciones["com.td.rango.fecha.invalido"]);
                    return;
                }
                seBuscoForFecha = true;
                DateTime from = this.fromDatepicker.Value.Date;
                DateTime to = new DateTime(this.toDatepicker.Value.Date.Year,
                    this.toDatepicker.Value.Date.Month, this.toDatepicker.Value.Date.Day,23,59,59);
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.Append(" BIT_FECHA >= #" + from.ToString("MM/dd/yyyy HH:mm:ss") + "# AND BIT_FECHA <= #" + to.ToString("MM/dd/yyyy HH:mm:ss") + "#");
            }
            if (this.chUsuario.Checked)
            {
                if (sb.Length > 0)
                {
                    sb.Append(" AND ");
                }
                if (String.IsNullOrEmpty(userText))
                {
                    sb.Append(" USU_ALIAS  is null");
                }
                else
                {
                    sb.Append(" USU_ALIAS = '" + userText+ "'");
                }

            }

                table.DefaultView.RowFilter = sb.ToString();
                this.dataGridView1.DataSource = null;
                this.dataGridView1.DataSource = table;

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
                        translation = traducciones["com.td.criticidad.alta"];
                        break;
                    case 2:
                        translation = traducciones["com.td.criticidad.media"];
                        break;
                    case 3:
                        translation = traducciones["com.td.criticidad.baja"];
                        break;
                }


                e.Value = translation;


            }


            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable datatable = this.dataGridView1.DataSource as DataTable;
            if(datatable != null)
            {
                DataTable view = datatable.DefaultView.ToTable();
                    DateTime minDate = (DateTime)datatable.Rows[0].ItemArray[0];
                    DateTime maxDate = (DateTime)datatable.Rows[0].ItemArray[0];
                foreach (DataRow Row in datatable.Rows)
                {
                   if(minDate > (DateTime) Row.ItemArray[0])
                    {
                        minDate = (DateTime)Row.ItemArray[0];
                    }
                    if (maxDate < (DateTime)Row.ItemArray[0])
                    {
                        maxDate = (DateTime)Row.ItemArray[0];
                    }
                }

                String desde = minDate.ToShortDateString();
                String hasta = maxDate.ToShortDateString();
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void validarAlphaKP(object sender, KeyPressEventArgs e)
        {
            if (!e.KeyChar.Equals('\b'))//tecla borrar
            {
                if (!alphanumericRegex.IsMatch(e.KeyChar.ToString()))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
