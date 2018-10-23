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

namespace TrabajoDeCampo.Pantallas.Administración
{
    public partial class Horarios : Form
    {
        private ServicioSeguridad servicioSeguridad;
        private ServicioAdministracion servicioAdministracion;
        private List<Curso> cursos;
        private Dictionary<String, String> traducciones;
        public Horarios()
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad();
            this.servicioAdministracion = new ServicioAdministracion();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new AltaModificacionHorario(null,this).Show();
        }

        private void Horarios_Load(object sender, EventArgs e)
        {

            this.helpProvider1.SetHelpKeyword(this, Properties.Settings.Default.Idioma.Equals("es") ? "Horarios.htm" : "Schedules.htm");
            this.helpProvider1.HelpNamespace = Application.StartupPath + @"\\DocumentsDeAyuda.chm";

            this.cursos = servicioAdministracion.listarCursos(null, null, null);
            this.comboNivel.DataSource = null;
            List<Nivel> niveles = new List<Nivel>();
            niveles.Add(new Nivel());
            niveles.AddRange(servicioAdministracion.listarNiveles(null, null, null));
            this.comboNivel.DataSource = niveles;
            this.comboNivel.DisplayMember = "codigo";
            desbloquearControles();

            //traduccion
            FormUtils traductor = new TraductorIterador();
            List<String> tags = new List<string>();
            long id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            traductor.process(tags, this, null, null);
            tags.Add("com.td.módulo");
            tags.Add("com.td.lunes");
            tags.Add("com.td.martes");
            tags.Add("com.td.miercoles");
            tags.Add("com.td.jueves");
            tags.Add("com.td.viernes");
            tags.Add("com.td.seleccione.horario");
            tags.Add("com.td.falta.horario");
            tags.Add("com.td.seguro");
            traducciones = servicioSeguridad.traerTraducciones(tags, Properties.Settings.Default.Idioma);
            traductor = new TraductorReal();
            traductor.process(null, this, traducciones, null);
            traductor = new TraductorIterador();
            this.listar(null, null);

        }

        public void desbloquearControles()
        {
            long id = (long)TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            bool crear = servicioSeguridad.tienePatente(id, EnumPatentes.CrearHorario.ToString());
            bool modificar = servicioSeguridad.tienePatente(id, EnumPatentes.ModificarHorario.ToString());
            bool borrar = servicioSeguridad.tienePatente(id, EnumPatentes.BorrarHorario.ToString());
            bool exportar = servicioSeguridad.tienePatente(id, EnumPatentes.GenerarReportes.ToString());

            this.btnCrear.Enabled = crear;
            this.btnMod.Enabled = modificar;
            this.btnDel.Enabled = borrar;
            this.btnExpo.Enabled = exportar;
        }


        public void listar(String filtro, String valor)
        {
            DataTable table = servicioAdministracion.listarHorarios(filtro, valor, null);
            this.dataGridView1.DataSource = null;
            this.dataGridView1.CellFormatting += cellFormat;
            this.dataGridView1.DataSource = table;
            this.dataGridView1.AllowUserToResizeRows = true;
            this.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[3].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[4].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[5].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.Columns[0].HeaderText = traducciones["com.td.módulo"];
            this.dataGridView1.Columns[1].HeaderText = traducciones["com.td.lunes"];
            this.dataGridView1.Columns[2].HeaderText = traducciones["com.td.martes"];
            this.dataGridView1.Columns[3].HeaderText = traducciones["com.td.miercoles"];
            this.dataGridView1.Columns[4].HeaderText = traducciones["com.td.jueves"];
            this.dataGridView1.Columns[5].HeaderText = traducciones["com.td.viernes"];
        }

        public void cellFormat(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is Horario|| e.Value is Modulo)
            {
                switch (e.ColumnIndex)
                {

                    case 0:
                        Modulo mod = (Modulo)e.Value;
                        e.Value = new StringBuilder(mod.horaInicio.TimeOfDay.ToString()).Append(Environment.NewLine).
                            Append(mod.horaFin.TimeOfDay.ToString());
                        break;
                    default:
                            Horario hor = (Horario)e.Value;
                            e.Value = new StringBuilder(hor.curso.codigo).Append(Environment.NewLine).Append(hor.materia.nombre).
                                Append(Environment.NewLine).Append(hor.docente.apellido + ", " + hor.docente.nombre);
                        break;
                }

            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(this.dataGridView1.CurrentRow!= null && this.dataGridView1.CurrentCell != null)
            {
                if(this.dataGridView1.CurrentCell.ColumnIndex == 0)
                {
                    MessageBox.Show(traducciones["com.td.seleccione.horario"]);
                }else if(this.dataGridView1.CurrentCell.Value == null || this.dataGridView1.CurrentCell.Value == DBNull.Value)
                {
                    MessageBox.Show(traducciones["com.td.falta.horario"]);
                }
                else
                {
                    DialogResult result = MessageBox.Show(traducciones["com.td.seguro"],"",MessageBoxButtons.OKCancel);
                    if (!result.Equals(DialogResult.OK))
                    {
                        return;
                    }
                    Horario hor = (Horario)this.dataGridView1.CurrentCell.Value;
                    this.servicioAdministracion.borrarHorario(hor);
                    this.listar(null, null);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (this.dataGridView1.CurrentRow != null && this.dataGridView1.CurrentCell != null)
            {
                if (this.dataGridView1.CurrentCell.ColumnIndex == 0)
                {
                    MessageBox.Show(traducciones["com.td.seleccione.horario"]);
                }
                else if (this.dataGridView1.CurrentCell.Value == null || this.dataGridView1.CurrentCell.Value == DBNull.Value)
                {
                    MessageBox.Show(traducciones["com.td.falta.horario"]);
                }
                else
                {
                    Horario hor = (Horario)this.dataGridView1.CurrentCell.Value;
                    AltaModificacionHorario alta = new AltaModificacionHorario(hor, this);
                    alta.ShowDialog();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            if (combo.SelectedItem != null)
            {
                long nivelId = ((Nivel)combo.SelectedItem).id;
                List<Curso> cursosSeleccionados = new List<Curso>();
                cursosSeleccionados.Add(new Curso());
                foreach (Curso curso in cursos)
                {
                    if (curso.nivel.id == nivelId)
                    {
                        cursosSeleccionados.Add(curso);
                    }
                }
                
                this.comboCurso.DataSource = null;
                this.comboCurso.DataSource = cursosSeleccionados;
                this.comboCurso.DisplayMember = "codigo";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.comboNivel.SelectedItem != null )
            {
                String filtro = "";
                String valor = "";
                if(this.comboCurso.SelectedItem != null && 0 < this.comboCurso.Items.Count && ((Curso) this.comboCurso.SelectedItem).id != 0)
                {
                    filtro = "curso";
                    valor = ((Curso)this.comboCurso.SelectedItem).id.ToString();
                }
                else if(((Nivel)this.comboNivel.SelectedItem).id != 0)
                {

                    filtro = "nivel";
                    valor = ((Nivel)this.comboNivel.SelectedItem).id.ToString();
                }

                this.listar(filtro, valor);

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            new ServicioReportes().ejecutarReporte<DataTable>("ReporteHorarios", new List<DataTable>()
            { (this.dataGridView1.DataSource as DataTable).DefaultView.ToTable() });
        }
    }
}
