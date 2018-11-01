using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoDeCampo.SEGURIDAD;
using TrabajoDeCampo.SERVICIO;

namespace TrabajoDeCampo.Pantallas.Alumnos
{
    public partial class Amonestaciones : Form
    {
        private ServicioSeguridad servicioSeguridad;
        private ServicioAlumnos servicioAlumnos;
        private Alumno alumno;
        private Boolean editando = false;
        private Dictionary<string, string> traducciones;

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
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.helpProvider1.SetHelpKeyword(this, Properties.Settings.Default.Idioma.Equals("es") ? "Alumnos.htm" : "Students.htm");
            this.helpProvider1.HelpNamespace = Application.StartupPath + @"\\DocumentsDeAyuda.chm";
            this.dataGridView1.AutoGenerateColumns = false;
            this.dateTimePicker1.MaxDate = DateTime.Now;
            this.dataGridView1.Columns[0].DataPropertyName = "fecha";
            this.dataGridView1.Columns[1].DataPropertyName = "motivo";
            this.dataGridView1.Columns[0].Tag ="com.td.fecha";
            this.dataGridView1.Columns[1].Tag= "com.td.motivo";

            listar();
            this.groupBox1.Enabled = false;
            desbloquearControles();

            //traduccion
            FormUtils traductor = new TraductorIterador();
            List<String> tags = new List<string>();
            tags.Add("com.td.complete.campos");
            tags.Add("com.td.fecha.ocupada");
            tags.Add("com.td.guardar");
            tags.Add("com.td.seguro");
            long id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            traductor.process(tags, this, null, null);
            traducciones = servicioSeguridad.traerTraducciones(tags, Properties.Settings.Default.Idioma);
            traductor = new TraductorReal();
            traductor.process(null, this, traducciones, null);
            traductor = new TraductorIterador();
        }

        public void desbloquearControles()
        {
            long id = (long)TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            bool export = servicioSeguridad.tienePatente(id, EnumPatentes.GenerarReportes.ToString());
            this.button3.Enabled = export;
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
                this.registrar.Text = traducciones["com.td.guardar"];
                this.groupBox1.Enabled = true;
            }
            else
            {
                if (String.IsNullOrEmpty(this.richTextBox1.Text))
                {
                    MessageBox.Show(traducciones["com.td.complete.campos"]);
                    return;
                }
                editando = false;
                this.registrar.Text = "Registrar";
                this.groupBox1.Enabled = false;
                // logica del save

                Amonestacion amonestacion = new Amonestacion();
                amonestacion.motivo = this.richTextBox1.Text;
                amonestacion.fecha = this.dateTimePicker1.Value;
                amonestacion.alumno = alumno;
                this.richTextBox1.Text = "";
                this.dateTimePicker1.Value = this.dateTimePicker1.MaxDate;
                try
                {
                    DialogResult result = MessageBox.Show(traducciones["com.td.seguro"], "", MessageBoxButtons.OKCancel);
                    if (!result.Equals(DialogResult.OK))
                    {
                        return;
                    }
                    this.servicioAlumnos.guardarAmonestacion(amonestacion);
                }
                catch (Exception ex)
                {
                    if(ex.Message == "FECHA")
                    {
                        MessageBox.Show(traducciones["com.td.fecha.ocupada"]);
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
                this.dateTimePicker1.Value = this.dateTimePicker1.MaxDate;
                this.registrar.Text = traducciones["com.td.registrar"];
            }
            else
            {
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<Alumno> alumno = new List<Alumno>();
            this.alumno.amonestaciones = this.dataGridView1.DataSource as List<Amonestacion>;
            alumno.Add(this.alumno);
            new ServicioReportes().ejecutarReporte<Alumno>("ReporteAmonestaciones", alumno);
        }
    }
}
