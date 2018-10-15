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
using TrabajoDeCampo.SEGURIDAD;
using TrabajoDeCampo.SERVICIO;

namespace TrabajoDeCampo.Pantallas.Alumnos
{
    public partial class Inasistencias : Form
    {
        private ServicioSeguridad servicioSeguridad;
        private ServicioAlumnos servicioAlumnos;
        private FormUtils utils;
        private Boolean haciendoCambios;
        private Boolean isEdit;
        private InasistenciaAlumno current;

        private Alumno alumno;
        public Inasistencias()
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad();

        }

        public Inasistencias(Alumno alu , Alumnos parentForm)
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad();
            this.servicioAlumnos = new ServicioAlumnos();

            this.alumno = alu;
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Inasistencias_Load(object sender, EventArgs e)
        {
            utils = new Bloqueador();
            this.dateTimePicker1.MaxDate = DateTime.Now;
            List<Control> controles = new List<Control>() { this.groupBox1 };
            utils.process(null, null, null, controles);
            this.dataGridView1.CellFormatting += formatting;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.Columns[0].DataPropertyName = "fecha";
            this.dataGridView1.Columns[1].DataPropertyName = "valor";
            this.dataGridView1.Columns[2].DataPropertyName = "justificada";
            listarInasistencias();

        }

        public void listarInasistencias()
        {
            this.dataGridView1.DataSource = null;
            String id = this.alumno.legajo.ToString();
            this.dataGridView1.DataSource = this.servicioAlumnos.listarInasistencias(null, id, null);

        }

        private void registrar_Click(object sender, EventArgs e)
        {
            utils = new Bloqueador();
            utils.process(null, null, null, new List<Control>() {this.registrar, this.modificar});
            utils = new Desbloqueador();
            utils.process(null, null, null, new List<Control>() { this.groupBox1,this.btnGuardar,this.chJustificado,this.completa,this.media,this.dateTimePicker1});
            haciendoCambios = true;
            isEdit = false;
            this.current = null;

        }

        private void modificar_Click(object sender, EventArgs e)
        {
            if(this.dataGridView1.CurrentRow != null && this.dataGridView1.CurrentRow.DataBoundItem != null)
            {
                utils = new Bloqueador();
                utils.process(null, null, null, new List<Control>() { this.registrar, this.modificar,this.dateTimePicker1,this.completa,this.media });
                utils = new Desbloqueador();
                utils.process(null, null, null, new List<Control>() { this.groupBox1,this.btnGuardar });
                haciendoCambios = true;
                isEdit = true;

                this.current = this.dataGridView1.CurrentRow.DataBoundItem as InasistenciaAlumno;
                this.chJustificado.Checked = this.current.Justificada;
                if (this.current.valor == 1.0) {
                    completa.Checked = true;
                }
                else {
                    media.Checked = true;
                }
                this.dateTimePicker1.Value = current.fecha;

            }
        }

        private void cancelar_Click(object sender, EventArgs e)
        {
            
            if (haciendoCambios)
            {
                limpiar();
                haciendoCambios = false;
            }
            else
            {
                this.Close();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if((!this.completa.Checked && !this.media.Checked))
            {
                MessageBox.Show("Complete los campos requeridos");
            }
            if(this.current != null)
            {
                this.current.fecha = this.dateTimePicker1.Value;
                if (this.completa.Checked)
                {
                    this.current.valor = 1.0;
                }
                else
                {
                    this.current.valor = 0.5;
                }
                this.current.Justificada = this.chJustificado.Checked;
                this.current.Alumno = alumno;
                try
                {
                    bool repetidas = verificarFechasRepetidas(this.current);
                    if (!repetidas)
                    {
                        this.servicioAlumnos.modificarInasistencia(this.current);
                        MessageBox.Show("Inasistencia Modificada");
                        this.limpiar();
                        this.listarInasistencias();
                    }
                    else
                    {
                        MessageBox.Show("Ya registro una fecha en ese dia.");
                    }
                 
                }
                catch (Exception ex)
                {
                        MessageBox.Show(ex.Message);
                }
            }
            else
            {
                InasistenciaAlumno inas = new InasistenciaAlumno();
                inas.fecha = this.dateTimePicker1.Value;
                inas.Alumno = this.alumno;
                inas.valor = (this.completa.Checked) ? 1.0 : 0.5;
                inas.Justificada = this.chJustificado.Checked;
                try
                {
                    bool repetidas = verificarFechasRepetidas(inas);
                    if (!repetidas)
                    {
                        this.servicioAlumnos.guardarInasistencia(inas);
                        MessageBox.Show("Inasistencia Guardada");
                        this.limpiar();
                        this.listarInasistencias();

                    }else
                    {
                        MessageBox.Show("Ya registro una fecha en ese dia.");
                    }
                    
                }
                catch (Exception ex)
                {            
                        MessageBox.Show(ex.Message);
                }
            }           
        }

        public void limpiar()
        {

            this.chJustificado.Checked = false;
            this.completa.Checked = false;
            this.media.Checked = false;
            this.dateTimePicker1.Value = this.dateTimePicker1.MaxDate;
            utils = new Bloqueador();
            utils.process(null, null, null, new List<Control>() { this.btnGuardar, this.groupBox1 });
            utils = new Desbloqueador();
            utils.process(null, null, null, new List<Control>() { this.registrar, this.modificar });
        }

        private void formatting(object sender, DataGridViewCellFormattingEventArgs ev)
        {
            if (ev.ColumnIndex == 2)
            {
                ev.Value = ((Boolean)ev.Value) ? "SI" : "NO";
            }else if (ev.ColumnIndex == 1)
            {
                ev.Value = ((Double)ev.Value) == 0.5 ? "MEDIA" : "COMPLETA";
            }
        }

        public Boolean verificarFechasRepetidas(InasistenciaAlumno inas) {
            bool hayRepetidas = false;

            List<InasistenciaAlumno> inasistenciasGuardadas = this.dataGridView1.DataSource as List<InasistenciaAlumno>;
            foreach (InasistenciaAlumno item in inasistenciasGuardadas)
            {
                if (item.fecha.ToString("yyyy-MM-dd").Equals(inas.fecha.ToString("yyyy-MM-dd")) 
                   && item.GetHashCode() != inas.GetHashCode()){
                    hayRepetidas = true;
                    break;
                }
            }

            return hayRepetidas;

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            List<Alumno> alumno = new List<Alumno>();
            this.alumno.inasistencias = this.dataGridView1.DataSource as List<InasistenciaAlumno>;
            alumno.Add(this.alumno);
            new ServicioReportes().ejecutarReporte<Alumno>("ReporteInasistencias", alumno);
        }
    }
}
