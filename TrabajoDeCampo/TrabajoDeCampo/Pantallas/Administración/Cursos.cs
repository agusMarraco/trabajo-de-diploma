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
    public partial class Cursos : Form
    {

        private ServicioSeguridad servicioSeguridad;
        private ServicioAdministracion servicioAdministracion;
        public Cursos()
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad();
            this.servicioAdministracion = new ServicioAdministracion();
            
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
 
            new AltaModificacionCurso(null,this).ShowDialog();

        }

        private void Cursos_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.Columns[0].DataPropertyName = "nivel";
            this.dataGridView1.Columns[1].DataPropertyName = "codigo";
            this.dataGridView1.Columns[2].DataPropertyName = "capacidad";
            this.dataGridView1.Columns[3].DataPropertyName = "turno";
            this.comboBox1.DataSource = null;
            this.dataGridView1.CellFormatting += formatter;
            List<KeyValuePair<String, String>> comboOptions = new List<KeyValuePair<string, string>>();
            comboOptions.Add(new KeyValuePair<string, string>("nivel", "nivel"));
            comboOptions.Add(new KeyValuePair<string, string>("codigo", "codigo"));
            comboOptions.Add(new KeyValuePair<string, string>("capacidad", "capacidad"));
            comboOptions.Add(new KeyValuePair<string, string>("turno", "turno"));

            comboBox1.DataSource = comboOptions;
            comboBox1.DisplayMember = "value";
            cargarCursos(null, null, null);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(this.dataGridView1.Rows.Count > 0 && this.dataGridView1.CurrentRow != null && this.dataGridView1.CurrentRow.DataBoundItem != null)
            {
                Curso curso = (Curso)this.dataGridView1.CurrentRow.DataBoundItem;
                new AltaModificacionCurso(curso, this).ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows.Count > 0 && this.dataGridView1.CurrentRow != null && this.dataGridView1.CurrentRow.DataBoundItem != null)
            {
                Curso curso = (Curso)this.dataGridView1.CurrentRow.DataBoundItem;
                try
                {

                this.servicioAdministracion.borrarCurso(curso);
                this.cargarCursos(null, null, null);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }

        }

        public void cargarCursos(String filtro,String valor, String orden)
        {
            try
            {
                this.dataGridView1.DataSource = null;
                this.dataGridView1.DataSource = this.servicioAdministracion.listarCursos(filtro, valor, orden);
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
                String valor = this.textBox1.Text;
                String filtro = ((KeyValuePair<String, String>)this.comboBox1.SelectedItem).Key;
                this.cargarCursos(filtro, valor, null);
         
        }

        private void formatter(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                String valor = e.Value.ToString();
                e.Value = valor == "M" ? "Mañana" : "Tarde";
            }
           else if (e.ColumnIndex == 0)
            {
                Nivel valor = (Nivel) e.Value;
                e.Value = valor.codigo;
            }
        }
    }
}
