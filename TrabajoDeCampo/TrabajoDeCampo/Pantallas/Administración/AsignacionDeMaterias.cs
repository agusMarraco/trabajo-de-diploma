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
    public partial class AsignacionDeMaterias : Form
    {
        private ServicioSeguridad servicioSeguridad;
        private ServicioAdministracion administracion;
        private Boolean seHicieronCambios = false;
        private List<Materia> desasignadas = new List<Materia>();
        private List<Materia> asignadas = new List<Materia>();
        private Nivel currentNivel = null;
        private Dictionary<String, String> traducciones;

        public AsignacionDeMaterias()
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(this.dgMaterias.Rows.Count > 0 && this.dgMaterias.CurrentRow!= null &&  this.dgMaterias.CurrentRow.DataBoundItem != null)
            {
                asignarMateria();   
            }
        }

        private void AsignacionDeMaterias_Load(object sender, EventArgs e)
        {
            this.servicioSeguridad = new ServicioSeguridad();
            this.administracion = new ServicioAdministracion();

            this.dgAsignadas.DataSource = null;
            this.dgMaterias.DataSource = null;
            this.comboNiveles.DataSource = null;

            this.dgMaterias.Columns[0].DataPropertyName = "nombre";
            this.dgAsignadas.Columns[0].DataPropertyName = "nombre";
            this.dgMaterias.Columns[0].Tag   = "com.td.nombre";
            this.dgAsignadas.Columns[0].Tag  = "com.td.nombre";
            this.dgMaterias.Columns[0].ReadOnly = true;
            this.dgAsignadas.Columns[0].ReadOnly = true;
            this.dgAsignadas.AutoGenerateColumns = false;
            this.dgMaterias.AutoGenerateColumns = false;


            try
            {
                List<Nivel> niveles = this.administracion.listarNiveles(null, null, null);
                this.comboNiveles.DataSource = niveles;

                this.comboNiveles.DisplayMember = "codigo";

                desasignadas = this.administracion.listarMaterias(null, null, null);
                this.dgMaterias.DataSource = new BindingList<Materia>(desasignadas);
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //traduccion
            FormUtils traductor = new TraductorIterador();
            List<String> tags = new List<string>();
            long id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            traductor.process(tags, this, null, null);
            tags.Add("com.td.descartar");
            traducciones = servicioSeguridad.traerTraducciones(tags, Properties.Settings.Default.Idioma);
            traductor = new TraductorReal();
            traductor.process(null, this, traducciones, null);
            traductor = new TraductorIterador();



        }

        public void desbloquearControles()
        {
            long id = (long)TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            bool exportar = servicioSeguridad.tienePatente(id, EnumPatentes.GenerarReportes.ToString());

            this.btExport.Enabled = exportar;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if(comboNiveles.Items.Count > 0 && comboNiveles.SelectedItem != null)
            {
                currentNivel = (Nivel)comboNiveles.SelectedItem;
                bool seguir = true;
                if (seHicieronCambios)
                {
                    DialogResult result = MessageBox.Show(traducciones["com.td.descartar"], "", MessageBoxButtons.OKCancel);
                    if(result != DialogResult.OK)
                    {
                        seguir = false;
                    }
                    
                }
                try
                {
                    if (seguir)
                    {
                        List<Materia> materias = this.administracion.traerMateriasPorNivel(currentNivel);
                        this.dgAsignadas.DataSource = null;
                        this.asignadas = materias;
                        this.dgAsignadas.DataSource = new BindingList<Materia>( this.asignadas);
                        mostrarMateriasNoAsignadasAlNivel(materias);
                        this.btAsignar.Enabled = true;
                        this.btDesasignar.Enabled = true;
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

            
            }
        }

        public void mostrarMateriasNoAsignadasAlNivel(List<Materia> materiasDelNivel) {
            this.dgMaterias.DataSource = null;
            List<Materia> materias = this.administracion.listarMaterias(null, null, null);

            this.desasignadas = new List<Materia>();

            foreach (Materia materia in materias)
            {
                Boolean estaAsignada = false;
                foreach (Materia materiaDelNivel in this.asignadas)
                {
                    if(materia.id == materiaDelNivel.id)
                    {
                        estaAsignada = true;
                    }
                }
                if (!estaAsignada)
                {
                    this.desasignadas.Add(materia);
                }
            }

            this.dgMaterias.DataSource = new BindingList<Materia>(this.desasignadas);

        }

        public void asignarMateria()
        {
            seHicieronCambios = true;
            Materia materiaAAsignar = (Materia)this.dgMaterias.CurrentRow.DataBoundItem;

            this.dgMaterias.DataSource = null;
            this.dgAsignadas.DataSource = null;

            //la limpio de las desasignadas
            List<Materia> materiasFiltradas = new List<Materia>();
            foreach (Materia mat in desasignadas)
            {
                if (mat.id != materiaAAsignar.id)
                {
                    materiasFiltradas.Add(mat);
                }
            }
            this.desasignadas = materiasFiltradas;
            this.dgMaterias.DataSource = new BindingList<Materia>(materiasFiltradas);
            //la agrego a las asignadas

            this.asignadas.Add(materiaAAsignar);
            this.dgAsignadas.DataSource = new BindingList<Materia>(asignadas);
        }

        public void desasignarMateria()
        {
            seHicieronCambios = true;
            Materia materiaADesasignar = (Materia)this.dgAsignadas.CurrentRow.DataBoundItem;

            this.dgAsignadas.DataSource = null;
            this.dgMaterias.DataSource = null;

            //la limpio de las asignadas
            List<Materia> materiasFiltradas = new List<Materia>();
            foreach (Materia mat in asignadas)
            {
                if (mat.id != materiaADesasignar.id)
                {
                    materiasFiltradas.Add(mat);
                }
            }
            this.asignadas = materiasFiltradas;
            this.dgAsignadas.DataSource = new BindingList<Materia>(materiasFiltradas);
            //la agrego a las desasignadas

            this.desasignadas.Add(materiaADesasignar);
            this.dgMaterias.DataSource = new BindingList<Materia>(this.desasignadas);
        }

        private void btDesasignar_Click(object sender, EventArgs e)
        {
            if (this.dgAsignadas.Rows.Count > 0 && this.dgAsignadas.CurrentRow != null && this.dgAsignadas.CurrentRow.DataBoundItem != null)
            {
                desasignarMateria();
            }
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            if (seHicieronCambios)
            {
                DialogResult result = MessageBox.Show(traducciones["com.td.descartar"], "", MessageBoxButtons.OKCancel);
                if(result == DialogResult.OK)
                {
                    this.seHicieronCambios = false;
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
            
        }

        private void btGuardar_Click(object sender, EventArgs e)
        {
            if (comboNiveles.Items.Count > 0 && comboNiveles.SelectedItem != null)
            {
                Nivel nivelSeleccionado = (Nivel)comboNiveles.SelectedItem;

                    try
                    {
                    this.seHicieronCambios = false;
                        List<Materia> materias = this.asignadas;
                        this.administracion.actualizarMateriasAsignadas(nivelSeleccionado, materias);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }

            }
        }

        private void btExport_Click(object sender, EventArgs e)
        {
            List<Nivel> nivelesParaElReporte = new List<Nivel>();

            foreach (Nivel iter in this.comboNiveles.DataSource as List<Nivel>)
            {
                Nivel nivel = new Nivel();
                nivel.id = iter.id;
                nivel.descripcion = iter.descripcion;
                nivel.orientacion = iter.orientacion;
                nivel.orientacionCodigo = nivel.orientacion.nombre;
                nivel.codigo = iter.codigo;
                List<Materia> materiasDelNivel = this.administracion.traerMateriasPorNivel(nivel);
                nivel.materia = materiasDelNivel;
                nivelesParaElReporte.Add(nivel);
            }
            new ServicioReportes().ejecutarReporte<Nivel>("ReportePlanDeEstudios", nivelesParaElReporte);
        }
    }
}
