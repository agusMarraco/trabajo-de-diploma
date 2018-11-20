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
        private Dictionary<String, String> traducciones = new Dictionary<string, string>();
        public Menu()
        {
            InitializeComponent();
            servicioSeguridad = new SERVICIO.ServicioSeguridad();

            this.GotFocus += correrValidaciones;
            desbloquearControles();
            traductor = new TraductorIterador();
            List<String> tags = new List<string>();
            tags.Add("com.td.seguro");
            long id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            this.servicioSeguridad.cambiarIdioma(id, Properties.Settings.Default.Idioma);
            traductor.process(tags, this, null, null);
            traducciones = servicioSeguridad.traerTraducciones(tags, Properties.Settings.Default.Idioma);
            traductor = new TraductorReal();
            traductor.process(null, this, traducciones, null);
            traductor = new TraductorIterador();
        }

        private void correrValidaciones(object sender, EventArgs e)
        {
            desbloquearControles();
        }

        public void desbloquearControles()
        {

            Boolean sistemaBloqueado = TrabajoDeCampo.Properties.Settings.Default.Bloqueado == 1;
            long id = (long)TrabajoDeCampo.Properties.Settings.Default.SessionUser;

            if (!sistemaBloqueado)
            {
                bool verAlumnos = servicioSeguridad.tienePatente(id, EnumPatentes.ListadoAlumnos.ToString());
                bool verTutores = servicioSeguridad.tienePatente(id, EnumPatentes.ListarTutores.ToString());
                this.alumnosToolStripMenuItem1.Enabled = verAlumnos;
                this.tutoresToolStripMenuItem.Enabled = verTutores;


                if (verAlumnos || verTutores)
                {
                    this.alumnosToolStripMenuItem.Enabled = true;
                }

                bool verCursos = servicioSeguridad.tienePatente(id, EnumPatentes.ListarCursos.ToString());
                bool verHorarios = servicioSeguridad.tienePatente(id, EnumPatentes.ListarHorarios.ToString());
                bool verMaterias = servicioSeguridad.tienePatente(id, EnumPatentes.ListarMateria.ToString());
                bool verPromociones = servicioSeguridad.tienePatente(id, EnumPatentes.PromocionarAlumnos.ToString());
                bool verAsignacion = servicioSeguridad.tienePatente(id, EnumPatentes.AsignarMateriaNivel.ToString());
                this.cursosToolStripMenuItem.Enabled = verCursos;
                this.horariosToolStripMenuItem.Enabled = verHorarios;
                this.materiasToolStripMenuItem.Enabled = verMaterias;
                this.promociónDeAlumnosToolStripMenuItem.Enabled = verPromociones;
                this.asignaciónDeMateriasToolStripMenuItem.Enabled = verAsignacion;

                if (verCursos || verHorarios || verMaterias || verPromociones || verAsignacion)
                    this.administraciónToolStripMenuItem.Enabled = true;

                bool verUsuarios = servicioSeguridad.tienePatente(id, EnumPatentes.ListarUsuarios.ToString());
                bool verPermisos = servicioSeguridad.tienePatente(id, EnumPatentes.ListarFamilias.ToString());
                bool restore = servicioSeguridad.tienePatente(id, EnumPatentes.RestaurarBackup.ToString());
                bool backup = servicioSeguridad.tienePatente(id, EnumPatentes.GenerarBackups.ToString());
                bool digitos = servicioSeguridad.tienePatente(id, EnumPatentes.RecalcularDígitosVerificadores.ToString());
                bool bitacora = servicioSeguridad.tienePatente(id, EnumPatentes.VerBitácora.ToString());

                this.usuarioMenuItem.Enabled = verUsuarios;
                this.permisoMenuITem.Enabled = verPermisos;
                this.restaurarToolStripMenuItem.Enabled = restore;
                this.respaldarToolStripMenuItem.Enabled = backup;
                //Los digitos solo se van a habilitar si el sistema esta bloqueado.
                //this.recalcularDígitosVerificadoresToolStripMenuItem.Enabled = digitos ;
                this.bitácoraToolStripMenuItem.Enabled = bitacora;

                if (verUsuarios || verPermisos || restore || backup || digitos || bitacora)
                    this.seguridadToolStripMenuItem.Enabled = true;
                if (restore || backup)
                    this.backupToolStripMenuItem.Enabled = true;
            }
            else
            {
                this.seguridadToolStripMenuItem.Enabled = true;
                this.restaurarToolStripMenuItem.Enabled = servicioSeguridad.tienePatente(id, EnumPatentes.RestaurarBackup.ToString());
                this.recalcularDígitosVerificadoresToolStripMenuItem.Enabled = servicioSeguridad.tienePatente(id, EnumPatentes.RecalcularDígitosVerificadores.ToString());
                this.respaldarToolStripMenuItem.Enabled = servicioSeguridad.tienePatente(id, EnumPatentes.GenerarBackups.ToString());
                this.backupToolStripMenuItem.Enabled = true;
                this.bitácoraToolStripMenuItem.Enabled = servicioSeguridad.tienePatente(id, EnumPatentes.VerBitácora.ToString()); ;
            }

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
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.helpProvider1.SetHelpKeyword(this, Properties.Settings.Default.Idioma.Equals("es") ? "MenuES.htm" : "MenuEN.htm");
            this.helpProvider1.HelpNamespace = Application.StartupPath + @"\\DocumentsDeAyuda.chm";
        }



        private void inglesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Idioma = "en";
            List<String> tags = new List<string>();
            tags.Add("com.td.seguro");
            long id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            this.servicioSeguridad.cambiarIdioma(id, "en");
            traductor.process(tags, this, null, null);
            traducciones = servicioSeguridad.traerTraducciones(tags, Properties.Settings.Default.Idioma);
            traductor = new TraductorReal();
            traductor.process(null, this, traducciones, null);
            traductor = new TraductorIterador();

        }

        private void españolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Idioma = "es";
            List<String> tags = new List<string>();
            tags.Add("com.td.seguro");
            long id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            this.servicioSeguridad.cambiarIdioma(id, "es");
            traductor.process(tags, this, null, null);
            traducciones = servicioSeguridad.traerTraducciones(tags, Properties.Settings.Default.Idioma);
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

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(traducciones["com.td.seguro"], "", MessageBoxButtons.OKCancel);
            if (!result.Equals(DialogResult.OK))
            {
                return;
            }
            this.Close();
            Usuario usu = new Usuario();
            usu.id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            this.servicioSeguridad.grabarBitacora(usu, "El usuario cerró sesión", CriticidadEnum.BAJA);

            Login login = new Login();
            login.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(traducciones["com.td.seguro"], "", MessageBoxButtons.OKCancel);
            if (!result.Equals(DialogResult.OK))
            {
                return;
            }
            Usuario usu = new Usuario();
            usu.id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            this.servicioSeguridad.grabarBitacora(usu, "El usuario salio de la aplicación", CriticidadEnum.BAJA);
            
            Application.Exit();
        }

    }
}
