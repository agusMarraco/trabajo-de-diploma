using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoDeCampo.Pantallas.Administración;
using TrabajoDeCampo.SEGURIDAD;
using TrabajoDeCampo.SERVICIO;

namespace TrabajoDeCampo.Pantallas
{
    public partial class AltaModificacionHorario : Form
    {
        private ServicioSeguridad servicioSeguridad;
        private ServicioAdministracion administracion;
        private List<Curso> cursos;
        private ServicioDocentes servicioDocentes;
        private List<Materia> materias;
        private Horario currentHorario;
        private Horarios parentForm;
        private Dictionary<String, String> traducciones;
        public AltaModificacionHorario()
        {
            InitializeComponent();
            servicioSeguridad = new ServicioSeguridad();
            administracion = new ServicioAdministracion();
            servicioDocentes = new ServicioDocentes();
        }

        public AltaModificacionHorario(Horario horario , Horarios parentForm)
        {
            InitializeComponent();
            servicioSeguridad = new ServicioSeguridad();
            administracion = new ServicioAdministracion();
            servicioDocentes = new ServicioDocentes();
            this.currentHorario = horario;
            this.parentForm = parentForm;
        }


        private void AltaModificacionHorario_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.helpProvider1.SetHelpKeyword(this, Properties.Settings.Default.Idioma.Equals("es") ? "Horarios.htm" : "Schedules.htm");
            this.helpProvider1.HelpNamespace = Application.StartupPath + @"\\DocumentsDeAyuda.chm";
            List <Nivel> niveles = administracion.listarNiveles(null, null, null);
            cursos = administracion.listarCursos(null, null, null);
            List<Docente> docentes = servicioDocentes.listarDocentes(null, null, null);
            Dictionary<long, String> modulos = new Dictionary<long, string>();
            modulos.Add(1, "8am / 10am");
            modulos.Add(2, "10am / 12pm");
            modulos.Add(3, "12pm / 14pm");
            modulos.Add(4, "14pm / 16pm");
            modulos.Add(5, "16pm / 18pm");
            
            this.cbModulo.DataSource = modulos.ToList();
            this.cbModulo.DisplayMember = "value";

            Dictionary<int, String> dias = new Dictionary<int, string>();
            dias.Add(1, "LUNES");
            dias.Add(2, "MARTES");
            dias.Add(3, "MIERCOLES");
            dias.Add(4, "JUEVES");
            dias.Add(5, "VIERNES");

            this.cbDia.DataSource = dias.ToList();
            this.cbDia.DisplayMember = "value";

            this.cbNivel.DataSource = niveles;
            this.cbNivel.DisplayMember = "codigo";

            this.cbDocente.DataSource = docentes;

            this.cbMateria.DataSource = null;
            this.cbMateria.DisplayMember = "nombre";

            this.cbDocente.Format += ComboBoxFormat;
      
            if (cbNivel.SelectedItem != null)
            {
                long nivelId = ((Nivel)cbNivel.SelectedItem).id;
                this.displayCursosYMaterias(nivelId);
            }
            if(this.currentHorario!= null)
            {
                populateDataFromHorario(this.currentHorario);
            }

            //traduccion
            FormUtils traductor = new TraductorIterador();
            List<String> tags = new List<string>();
            tags.Add("com.td.complete.campos");
            tags.Add("com.td.horario.no.disponible");
            long id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            traductor.process(tags, this, null, null);
            traducciones = servicioSeguridad.traerTraducciones(tags, Properties.Settings.Default.Idioma);
            traductor = new TraductorReal();
            traductor.process(null, this, traducciones, null);
            traductor = new TraductorIterador();

        }


        public void ComboBoxFormat(object sender, ListControlConvertEventArgs e)
        {
            Docente doc = (Docente)e.Value;
            e.Value = doc.apellido + " , " + doc.nombre;
        }

        private void cbNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            if(combo.SelectedItem != null)
            {
                long nivelId = ((Nivel)combo.SelectedItem).id;
                this.displayCursosYMaterias(nivelId);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.cbCurso.SelectedItem != null && this.cbNivel.SelectedItem != null
                && this.cbDocente.SelectedItem != null && this.cbMateria.SelectedItem != null
                && this.cbDia.SelectedItem != null && this.cbModulo.SelectedItem != null
                )
            {
                if (this.currentHorario == null)
                {
                    Horario hor = new Horario();
                    hor.dia = ((KeyValuePair<int, string>)cbDia.SelectedItem).Key;
                    Modulo mod = new Modulo();
                    mod.id = ((KeyValuePair<long, string>)cbModulo.SelectedItem).Key;
                    hor.modulo = mod;
                    hor.curso = (Curso)this.cbCurso.SelectedItem;
                    hor.docente = (Docente)this.cbDocente.SelectedItem;
                    hor.materia = (Materia)this.cbMateria.SelectedItem;
                    try
                    {
                        this.administracion.guardarHorario(hor);
                        this.parentForm.listar(null, null);
                        this.Close();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message.Equals("Horario No Disponible") ? traducciones["com.td.horario.no.disponible"] : ex.Message );
                    }

                }
                else
                {
                    currentHorario.dia = ((KeyValuePair<int, string>)cbDia.SelectedItem).Key;
                    Modulo mod = new Modulo();
                    mod.id = ((KeyValuePair<long, string>)cbModulo.SelectedItem).Key;
                    currentHorario.modulo = mod;
                    currentHorario.curso = (Curso)this.cbCurso.SelectedItem;
                    currentHorario.docente = (Docente)this.cbDocente.SelectedItem;
                    currentHorario.materia = (Materia)this.cbMateria.SelectedItem;
                    try
                    {
                        this.administracion.actualizarHorario(currentHorario);
                        this.parentForm.listar(null, null);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.Equals("Horario No Disponible") ? traducciones["com.td.horario.no.disponible"] : ex.Message);
                    }

                }



            }
            else
            {
                MessageBox.Show(traducciones["com.td.complete.campos"]);
            }
        }
        public void displayCursosYMaterias(long nivelId)
        {
            List<Curso> cursosSeleccionados = new List<Curso>();
            
            foreach (Curso curso in cursos)
            {
                if (curso.nivel.id == nivelId)
                {
                    cursosSeleccionados.Add(curso);
                }
            }
            Nivel niv = new Nivel();
            niv.id = nivelId;
            materias = this.administracion.traerMateriasPorNivel(niv);
            this.cbMateria.DataSource = null;
            this.cbMateria.DisplayMember = "nombre";
            this.cbMateria.DataSource = materias;
            this.cbCurso.DataSource = null;
            this.cbCurso.DataSource = cursosSeleccionados;
            this.cbCurso.DisplayMember = "codigo";
        }
        public void populateDataFromHorario(Horario horario)
        {
            foreach (Nivel item in cbNivel.Items)
            {
                if(item.id == horario.curso.nivel.id)
                {
                    this.cbNivel.SelectedItem = item;
                }
            }

            foreach (Curso item in cbCurso.Items)
            {
                if (item.id == horario.curso.id)
                {
                    this.cbCurso.SelectedItem = item;
                }
            }

            foreach (Docente item in cbDocente.Items)
            {
                if (item.legajo == horario.docente.legajo)
                {
                    this.cbDocente.SelectedItem = item;
                }
            }

            foreach (Materia item in cbMateria.Items)
            {
                if (item.id == horario.materia.id)
                {
                    this.cbMateria.SelectedItem = item;
                }
            }

            foreach (KeyValuePair<int,String> item in cbDia.Items)
            {
                if (item.Key == horario.dia)
                {
                    this.cbDia.SelectedItem = item;
                }
            }
            foreach (KeyValuePair<long, String> item in cbModulo.Items)
            {
                if (item.Key == horario.modulo.id)
                {
                    this.cbModulo.SelectedItem = item;
                }
            }
        }
    }
}
