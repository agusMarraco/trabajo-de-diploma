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
        private ServicioAlumnos servicioAlumnos;
        public Tutores()
        {
            InitializeComponent();
            this.dataGridView1.Columns[0].Tag = "com.td.nombre";
            this.dataGridView1.Columns[1].Tag = "com.td.apellido";
            this.dataGridView1.Columns[2].Tag = "com.td.d.n.i";
            this.dataGridView1.Columns[3].Tag = "com.td.mail";
            this.dataGridView1.Columns[4].Tag = "com.td.telefonos";
            this.dataGridView1.Columns[0].DataPropertyName = "nombre";
            this.dataGridView1.Columns[1].DataPropertyName = "apellido";
            this.dataGridView1.Columns[2].DataPropertyName = "dni";
            this.dataGridView1.Columns[3].DataPropertyName = "email";
            this.dataGridView1.Columns[4].DataPropertyName = "";
            this.dataGridView1.CellFormatting += telefonosFormatter;
            this.dataGridView1.ColumnHeaderMouseClick += customSort;
            this.servicioSeguridad = new ServicioSeguridad();
            this.servicioAlumnos = new ServicioAlumnos();

            this.dataGridView1.DataSource = null;
            this.dataGridView1.AutoGenerateColumns = false;
            List<KeyValuePair<String, String>> comboOptions = new List<KeyValuePair<string, string>>();
            comboOptions.Add(new KeyValuePair<string, string>("nombre", "nombre"));
            comboOptions.Add(new KeyValuePair<string, string>("apellido", "apellido"));
            comboOptions.Add(new KeyValuePair<string, string>("dni", "dni"));
            this.comboBox2.DataSource = comboOptions;
            this.comboBox2.DisplayMember = "value";


        }

        private void button4_Click(object sender, EventArgs e)
        {
            new AltaModificacionTutor(null, this).ShowDialog();
        }

        private void Tutores_Load(object sender, EventArgs e)
        {
            buscarTutores(null, null);

        }

        private void telefonosFormatter(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                Tutor tut = (Tutor)this.dataGridView1.Rows[e.RowIndex].DataBoundItem;
                String telefonos = tut.telefono1;
                if (!String.IsNullOrEmpty(tut.telefono2)){
                    telefonos += ", " + tut.telefono2;
                }
                e.Value = telefonos;


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //busqueda
            if (comboBox2.SelectedItem != null)
            {
                String filtro = ((KeyValuePair<String, String>)this.comboBox2.SelectedItem).Key.ToString();
                String value = this.textBox1.Text;
                buscarTutores(filtro, value);
            }
            else
            {
                buscarTutores(null, null);
            }
        }

        public void buscarTutores(String filtro, String valor)
        {
            this.dataGridView1.DataSource = null;
            List<Tutor> tutores = this.servicioAlumnos.listarTutor(filtro, valor, null);
            this.dataGridView1.DataSource = tutores;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.DataBoundItem!= null)
            {
                Tutor tutorAMod = (Tutor)dataGridView1.CurrentRow.DataBoundItem;

                new AltaModificacionTutor(tutorAMod, this).ShowDialog(); ;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.DataBoundItem != null)
            {
                Tutor tutorParaBorrar = (Tutor)dataGridView1.CurrentRow.DataBoundItem;
                try
                {
                    this.servicioAlumnos.BorrarTutor(tutorParaBorrar);
                    this.buscarTutores(null, null);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                
            }
        }

        //sorting
        private void customSort(object sender, DataGridViewCellMouseEventArgs e)
        {   
            if(e.ColumnIndex != 4)
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
                    List<Tutor> tutores = (List<Tutor>)this.dataGridView1.DataSource;
                    string propertyName = this.dataGridView1.Columns[e.ColumnIndex].Name;
                    this.dataGridView1.DataSource = null;
               
                        tutores.Sort((x, y) => x.GetType().GetProperty(propertyName).GetValue(x).ToString().CompareTo
                         (y.GetType().GetProperty(propertyName).GetValue(y).ToString()));
                        this.dataGridView1.DataSource = tutores;
                    this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.LightSkyBlue;
                    this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;

                }
                else
                {
                    List<Tutor> tutores = (List<Tutor>)this.dataGridView1.DataSource;
                    this.dataGridView1.DataSource = null;
                    string propertyName = this.dataGridView1.Columns[e.ColumnIndex].Name;
                     tutores.Sort((x, y) => y.GetType().GetProperty(propertyName).GetValue(y).ToString().CompareTo
                         (x.GetType().GetProperty(propertyName).GetValue(x).ToString()));
                        this.dataGridView1.DataSource = tutores;
                    this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.LightSkyBlue;
                    this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;

                }

            }

        }
    }

}
