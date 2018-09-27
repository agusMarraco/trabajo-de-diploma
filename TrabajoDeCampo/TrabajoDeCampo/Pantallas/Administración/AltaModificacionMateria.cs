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
    public partial class AltaModificacionMateria : Form
    {
        private ServicioSeguridad servicioSeguridad;
        private ServicioAdministracion administracion;
        private bool esEdit = false;
        private Materia currentMateria = null;

        private Materias callerForm = null;
        public AltaModificacionMateria()
        {
            InitializeComponent();

        }

        public AltaModificacionMateria(Materia materia,Materias materiasForm)
        {
            InitializeComponent();
            callerForm = materiasForm;
            if (materia != null)
            {
                esEdit = true;
                currentMateria = materia;
                this.textBox1.Text = materia.nombre;
                this.textBox2.Text = materia.descripcion;
                if(materia.tipo == "TRONCAL")
                {
                    this.rbTroncal.Checked = true;
                }
                else
                {
                    this.rbExtracurricular.Checked = true;
                }
            }
        }
        private void AltaModificacionMateria_Load(object sender, EventArgs e)
        {
            this.servicioSeguridad = new ServicioSeguridad();
            this.administracion = new ServicioAdministracion();
        }
        //troncal 1 extra 0
        private void button1_Click(object sender, EventArgs e)
        {
            String nombreMateria = this.textBox1.Text;
            String descripcion = this.textBox2.Text;
            Boolean esTroncal = this.rbTroncal.Checked ?  true : false;

            try
            {
                if (!esEdit)
                {
                    Materia mat = new Materia();
                    mat.nombre = nombreMateria;
                    mat.tipo = esTroncal ? "1" : "0";
                    mat.descripcion = descripcion;
                    this.administracion.guardarMateria(mat);
                    this.textBox1.Text = "";
                    this.rbExtracurricular.Checked = false;
                    this.rbTroncal.Checked = false;
                    this.textBox2.Text = "";
                    callerForm.actualizarLista();
                    this.Close();
                }
                else
                {
                    currentMateria.nombre = nombreMateria;
                    currentMateria.tipo = esTroncal ? "1" : "0";
                    currentMateria.descripcion = descripcion;
                    this.administracion.actualizarMateria(currentMateria);
                    this.textBox1.Text = "";
                    this.rbExtracurricular.Checked = false;
                    this.rbTroncal.Checked = false;
                    this.textBox2.Text = "";
                    callerForm.actualizarLista();
                    this.Close();
                }
            }
            catch (Exception ex)
            {

                if(ex.Message == "EXISTE")
                {
                    MessageBox.Show("Existe la materia");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }

            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            callerForm.actualizarLista();
            this.Close();
        }
    }
}
