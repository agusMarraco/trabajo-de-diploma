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
    public partial class Amonestaciones : Form
    {
        private ServicioSeguridad servicioSeguridad;
        private ServicioAlumnos servicioAlumnos;
        private Alumno alumno;
        private Boolean editando = false;
        public Amonestaciones()
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad();
        }

        public Amonestaciones(Alumno alu , Alumnos parentForm)
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad();
            this.servicioAlumnos = new ServicioAlumnos();
            this.alumno = alu;
        }

        private void Amonestaciones_Load(object sender, EventArgs e)
        {
            this.dataGridView1.AutoGenerateColumns = false;
            this.dateTimePicker1.MaxDate = DateTime.Now;
            this.dataGridView1.Columns[0].DataPropertyName = "fecha";
            this.dataGridView1.Columns[1].DataPropertyName = "motivo";
            listar();
            this.groupBox1.Enabled = false; 
        }


        public void listar()
        {
           
            this.dataGridView1.DataSource = null;
            this.dataGridView1.DataSource = this.servicioAlumnos.listarAmonestaciones(null, alumno.legajo.ToString(), null);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!editando)
            {
                editando = true;
                this.registrar.Text = "Guardar";
                this.groupBox1.Enabled = true;
            }
            else
            {
                editando = false;
                this.registrar.Text = "Registrar";
                this.groupBox1.Enabled = false;
                // logica del save

                Amonestacion amonestacion = new Amonestacion();
                amonestacion.motivo = this.richTextBox1.Text;
                amonestacion.fecha = this.dateTimePicker1.Value;
                amonestacion.alumno = alumno;
                this.richTextBox1.Text = "";
                this.dateTimePicker1.Value = DateTime.Now;
                try
                {
                    this.servicioAlumnos.guardarAmonestacion(amonestacion);
                }
                catch (Exception ex)
                {
                    if(ex.Message == "FECHA")
                    {
                        MessageBox.Show("Ya registro una fecha en ese dia.");
                    }
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }
                    
                }
                listar();
            }
            

        }

        private void cancelar_Click(object sender, EventArgs e)
        {
            if (editando)
            {
                editando = false;
                this.groupBox1.Enabled = false;
                this.richTextBox1.Text = "";
                this.dateTimePicker1.Value = DateTime.Now;
                this.registrar.Text = "Registrar";
            }
            else
            {
                this.Close();
            }
        }
    }
}
