using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoDeCampo.SEGURIDAD;
using TrabajoDeCampo.SERVICIO;
namespace TrabajoDeCampo.Pantallas.Administración
{
    public partial class AltaModificacionCurso : Form
    {
        private ServicioSeguridad servicioSeguridad;
        private ServicioAdministracion servicioAdministracion;
        private Curso currentCurso;
        private Cursos callerForm;
        private Regex onlyNumbers = new Regex("^[0-9]+$");
        private Regex onlyLetters = new Regex("^[a-zA-Z]+$");
        
        private Dictionary<String, String> traducciones;
        public AltaModificacionCurso()
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad();
            this.servicioAdministracion = new ServicioAdministracion();
           
        }

      
        private void TxtLetra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!e.KeyChar.Equals('\b'))//tecla borrar
            {
                if (!onlyLetters.IsMatch(e.KeyChar.ToString()))
                {
                    e.Handled = true;
                }
            }
        }

        private void TxtCapacidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!e.KeyChar.Equals('\b'))//tecla borrar
            {
                if (!onlyNumbers.IsMatch(e.KeyChar.ToString()))
                {
                    e.Handled = true;
                }
            }
        }

     

        public AltaModificacionCurso(Curso curso, Cursos callerForm)
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad();
            this.servicioAdministracion = new ServicioAdministracion();
            this.comboNiveles.DataSource = null;
            List<Nivel> niveles = this.servicioAdministracion.listarNiveles(null, null, null);
            this.comboNiveles.DisplayMember = "codigo";
            this.comboNiveles.DataSource = niveles;
            this.currentCurso = curso;
            this.callerForm = callerForm;

            this.txtCapacidad.KeyPress += TxtCapacidad_KeyPress;
            this.txtLetra.KeyPress += TxtLetra_KeyPress;

            if (currentCurso != null)
            {
                this.txtCapacidad.Text = currentCurso.capacidad.ToString();
                if(currentCurso.turno == "M")
                {
                    this.rbTurnoMañana.Checked = true;
                }else
                {
                    this.rbTurnoTarde.Checked = true;
                }
                this.txtLetra.Text = currentCurso.letra;
                foreach (var item in this.comboNiveles.Items)
                {
                    if (((Nivel)item).id == currentCurso.nivel.id)
                        this.comboNiveles.SelectedItem = item;
                }

            }
            else
            {
                this.rbTurnoMañana.Checked = true;
            }

        }

        private void AltaModificacionCurso_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            //traduccion
            this.helpProvider1.SetHelpKeyword(this, Properties.Settings.Default.Idioma.Equals("es") ? "Cursos.htm" : "Courses.htm");
            this.helpProvider1.HelpNamespace = Application.StartupPath + @"\\DocumentsDeAyuda.chm";
            FormUtils traductor = new TraductorIterador();
            List<String> tags = new List<string>();
            long id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            traductor.process(tags, this, null, null);
            tags.AddRange( new String[] {"com.td.completado","com.td.complete.campos", "com.td.curso.codigo.existe" });
            traducciones = servicioSeguridad.traerTraducciones(tags, Properties.Settings.Default.Idioma);
            traductor = new TraductorReal();
            traductor.process(null, this, traducciones, null);
            traductor = new TraductorIterador();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if(String.IsNullOrEmpty(this.txtCapacidad.Text.Trim()) || String.IsNullOrEmpty(this.txtLetra.Text.Trim()))
            {
                MessageBox.Show(traducciones["com.td.complete.campos"]);
                return;
            }
            if(currentCurso != null)
            {
                //edit
                
                currentCurso.nivel = (Nivel)this.comboNiveles.SelectedItem;
                currentCurso.letra = this.txtLetra.Text;
                currentCurso.turno = this.rbTurnoMañana.Checked ? "M" : "T";
                currentCurso.codigo = currentCurso.nivel.codigo + currentCurso.turno + currentCurso.letra;
                currentCurso.capacidad = int.Parse(this.txtCapacidad.Text);
                try
                {
                    this.servicioAdministracion.actualizarCurso(currentCurso);
                    MessageBox.Show(traducciones["com.td.completado"]);
                    this.callerForm.cargarCursos(null, null, null);
                    this.Close();
                   

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.Equals("CODIGO REPETIDO") ? traducciones["com.td.curso.codigo.existe"] : ex.Message);
                }
            }
            else
            {
                //create
                Curso curso = new Curso();
                curso.nivel = (Nivel)this.comboNiveles.SelectedItem;
                curso.letra = this.txtLetra.Text;
                curso.turno = this.rbTurnoMañana.Checked ? "M" : "T";
                curso.capacidad = int.Parse(this.txtCapacidad.Text);
                curso.codigo = curso.nivel.codigo + curso.turno + curso.letra;
                try
                {
                    this.servicioAdministracion.guardarCurso(curso);
                    MessageBox.Show(traducciones["com.td.completado"]);
                    this.callerForm.cargarCursos(null, null, null);
                    this.Close();

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.Equals("CODIGO REPETIDO") ? traducciones["com.td.curso.codigo.existe"] : ex.Message);
                }
            }

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
