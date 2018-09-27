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

namespace TrabajoDeCampo.Pantallas.Seguridad
{
    public partial class Menu : Form
    {
        private ServicioSeguridad servicioSeguridad;
        private FormUtils traductor;
        public Menu()
        {
            InitializeComponent();
            traductor = new TraductorIterador();
            servicioSeguridad = new SERVICIO.ServicioSeguridad();
                
            
        }

        private void opcionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void docenteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void recalcularDVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListaDeUsuarios usuarios = new ListaDeUsuarios();
            usuarios.ShowDialog();
        }



        private void restaurarBDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListarFamilias familias = new ListarFamilias();
            familias.ShowDialog();
        }

        private void respaldarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Respaldo_Base_de_Datos respaldo = new Respaldo_Base_de_Datos();
            respaldo.ShowDialog();
        }

        private void restaurarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Restaurar_Backup restore = new Restaurar_Backup();
            restore.ShowDialog();
        }

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarContraseña contra = new CambiarContraseña();
            contra.ShowDialog();
        }

        private void bitácoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitácora bitacora = new Bitácora();
            bitacora.ShowDialog();

        }
       
        private void cursosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Administración.Cursos cursos = new Administración.Cursos();
            cursos.ShowDialog();
        }

        private void horariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Administración.Horarios horarios = new Administración.Horarios();
            horarios.ShowDialog();

        }

        private void materiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Administración.Materias form = new Administración.Materias();
            form.ShowDialog();
            
        }

        private void promociónDeAlumnosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Administración.PromocionDeAlumnos().ShowDialog();
        }

        private void alumnosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new Alumnos.Alumnos().ShowDialog();
        }

        private void amonestacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Alumnos.Amonestaciones().ShowDialog();
        }

        private void inasistenciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Alumnos.Inasistencias().ShowDialog();
        }

        private void tutoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Alumnos.Tutores().ShowDialog();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
         
        }



        private void inglesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Idioma = "en";
            List<String> tags = new List<string>();
            traductor.process(tags, this, null, null);
            Dictionary<String, String> traducciones = servicioSeguridad.traerTraducciones(tags, Properties.Settings.Default.Idioma);
            traductor = new TraductorReal();
            traductor.process(null, this, traducciones, null);
            traductor = new TraductorIterador();

        }

        private void españolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Idioma = "es";
            List<String> tags = new List<string>();
            traductor.process(tags, this, null, null);
            Dictionary<String, String> traducciones = servicioSeguridad.traerTraducciones(tags, Properties.Settings.Default.Idioma);
            traductor = new TraductorReal();
            traductor.process(null, this, traducciones, null);
            traductor = new TraductorIterador();
        }

        private void recalcularDígitosVerificadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.servicioSeguridad.recalcularDigitosVerificadores();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            servicioSeguridad.verificarDigitosVerificadores();
        }

        private void asignaciónDeMateriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AsignacionDeMaterias asignacionDematerias = new AsignacionDeMaterias();
            asignacionDematerias.ShowDialog();
        }
    }
}
