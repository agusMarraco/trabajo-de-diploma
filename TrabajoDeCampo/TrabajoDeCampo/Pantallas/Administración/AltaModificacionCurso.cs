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
    public partial class AltaModificacionCurso : Form
    {
        private ServicioSeguridad servicioSeguridad;
        private ServicioAdministracion servicioAdministracion;
        private Curso currentCurso;
        private Cursos callerForm;

        public AltaModificacionCurso()
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad();
            this.servicioAdministracion = new ServicioAdministracion();

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

        }

        private void AltaModificacionCurso_Load(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
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
                    MessageBox.Show("completado");
                    this.callerForm.cargarCursos(null, null, null);
                    this.Close();
                   

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
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
                    MessageBox.Show("completado");
                    this.callerForm.cargarCursos(null, null, null);
                    this.Close();

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
