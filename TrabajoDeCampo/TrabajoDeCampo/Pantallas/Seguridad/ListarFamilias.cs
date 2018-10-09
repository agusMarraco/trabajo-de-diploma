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

namespace TrabajoDeCampo.Pantallas.Seguridad
{
    public partial class ListarFamilias : Form
    {
        private ServicioSeguridad servicioSeguridad;
        FormUtils utils;
        private Familia currentFamilia;

        private Regex lettersOnly = new Regex("[a-zA-z]");
        private Boolean valido = false;
        public ListarFamilias()
        {
            InitializeComponent();
            servicioSeguridad = new ServicioSeguridad();
            utils = new Bloqueador();
            this.dgFamilia.SelectionChanged += cambioDeFamiliaSeleccionada;
            List<Control> controlesABloquear = new List<Control> { this.btnGuardar, this.txtNombre };
            utils.process(null,null,null, controlesABloquear);
            this.dgFamilia.MultiSelect = false;
            this.dgPatentes.DataSource = null;
            this.dgFamilia.DataSource = null;
            this.dgFamilia.AutoGenerateColumns = false;
            this.dgPatentes.AutoGenerateColumns = false;
            this.dgPatentes.Columns[0].ReadOnly = true;
            this.dgPatentes.Columns[1].ReadOnly = true;
            this.dgFamilia.ReadOnly = true;
            this.dgPatentes.Columns[0].DataPropertyName = "descripcion";
            this.dgFamilia.Columns[0].DataPropertyName = "nombre";
            this.txtNombre.KeyDown += TxtNombre_KeyDown;
            this.txtNombre.KeyPress += TxtNombre_KeyPress;


        }

        private void TxtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!valido)
            {
                e.Handled = true;
            }
        }

        private void TxtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            valido = true;
            if (!e.KeyValue.Equals(8))//tecla borrar
            {
                if (!lettersOnly.IsMatch(e.KeyData.ToString()) || e.KeyData.ToString().Contains("Oem"))
                {
                    valido = false;
                }
            }
        }

        /// <summary>
        /// lista las familias y las patentes. 
        /// </summary>
        public void listarElementos()
        {

            this.dgPatentes.DataSource = this.servicioSeguridad.listarPatentes();
            this.dgFamilia.DataSource = this.servicioSeguridad.listarFamilias();
            if (this.dgFamilia.Rows.Count > 0)
            {
                    Familia fami = (Familia)dgFamilia.Rows[0].DataBoundItem;
                    mostrarPatentesMarcadas(fami);
            }
        }

        /// <summary>
        /// marca las patentes en el dg de patentes depediendo de la familia.
        /// </summary>
        /// <param name="fami"></param>
        public void mostrarPatentesMarcadas(Familia fami)
        {
            foreach (Patente pat in fami.patentes)
            {
                foreach (DataGridViewRow patrow in dgPatentes.Rows)
                {
                    Patente current = (Patente)patrow.DataBoundItem;
                    if (current.id == pat.id)
                    {
                        ((DataGridViewCheckBoxCell)patrow.Cells[1]).Value = true;
                    }
                }
            }

        }
        private void button3_Click(object sender, EventArgs e)
        {

            if(dgFamilia.CurrentRow != null)
            {
                Familia fami = (Familia)dgFamilia.CurrentRow.DataBoundItem;
                try
                {
                    this.servicioSeguridad.borrarFamilia(fami.id);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                
                listarElementos();
                if (dgFamilia.Rows.Count < 1)
                    this.limpiarPatentesSeleccionadas();
            }

           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(this.dgFamilia.CurrentRow != null)
            {
                limpiarPatentesSeleccionadas();
                Familia fami = (Familia)this.dgFamilia.CurrentRow.DataBoundItem;
                mostrarPatentesMarcadas(fami);
                this.txtNombre.Text = fami.nombre;
                this.currentFamilia = fami;
                utils = new Desbloqueador();
                utils.process(null, null, null, new List<Control> { this.txtNombre, this.btnGuardar });
                utils = new Bloqueador();
                this.dgPatentes.Columns[1].ReadOnly = false;
                utils.process(null, null, null, new List<Control> { this.btnCrear, this.btnModificar, this.btnBorrar });
            }
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dgPatentes.Columns[1].ReadOnly = false;
            limpiarPatentesSeleccionadas();
             utils = new Desbloqueador();
            utils.process(null, null, null, new List<Control> { this.txtNombre, this.btnGuardar });
            utils = new Bloqueador();
            this.txtNombre.Text = "";
            utils.process(null, null, null, new List<Control> { this.btnCrear, this.btnModificar, this.btnBorrar });
        }

        private void ListarFamilias_Load(object sender, EventArgs e)
        {

            listarElementos();
            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.txtNombre.Text))
            {
                MessageBox.Show("Complete los campos requeridos");
            }
            String nombreFamilia = this.txtNombre.Text;
            Familia familia = currentFamilia;
            if (familia== null)
            {
                //create
                familia = new Familia();
            }

            familia.nombre = txtNombre.Text;                
            List<Patente> patentes = new List<Patente>();
            familia.patentes = new List<BO.ComponentePermiso>();
            foreach (DataGridViewRow item in dgPatentes.Rows)
            {
                if (((DataGridViewCheckBoxCell)item.Cells[1]).Value != null && (bool)((DataGridViewCheckBoxCell)item.Cells[1]).Value == true)
                {
                    Patente temp = new Patente();
                    temp.id = ((Patente)item.DataBoundItem).id;
                    familia.patentes.Add(temp);
                }
            }
            if(currentFamilia == null)
            {
                this.servicioSeguridad.crearFamilia(familia);
            }
            else
            {
                this.servicioSeguridad.modificarFamilia(familia);
            }
            currentFamilia = null;
            this.dgPatentes.Columns[1].ReadOnly = true;
            this.txtNombre.Text = "";
            utils = new Bloqueador();
            utils.process(null, null, null, new List<Control> { this.txtNombre, this.btnGuardar });
            this.txtNombre.Text = "";
            utils = new Desbloqueador();
            utils.process(null, null, null, new List<Control> { this.btnCrear, this.btnModificar, this.btnBorrar });
            listarElementos();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.currentFamilia = null;
            if (this.txtNombre.Enabled)
            {
                this.dgPatentes.Columns[1].ReadOnly = true;
                utils = new Bloqueador();
                utils.process(null, null, null, new List<Control> { this.txtNombre, this.btnGuardar });
                this.txtNombre.Text = "";
                utils = new Desbloqueador();
                utils.process(null, null, null, new List<Control> { this.btnCrear, this.btnModificar, this.btnBorrar});
                listarElementos();


            }
            else
            {
                this.Close();
            }
        }

        private void cambioDeFamiliaSeleccionada(object sender, EventArgs e)
        {
            if(this.dgFamilia.CurrentRow!= null)
            {
                limpiarPatentesSeleccionadas();
                Familia fami = (Familia)this.dgFamilia.CurrentRow.DataBoundItem;
                mostrarPatentesMarcadas(fami);
            }
        }

        private void limpiarPatentesSeleccionadas()
        {
            foreach (DataGridViewRow item in this.dgPatentes.Rows)
            {
                ((DataGridViewCheckBoxCell)item.Cells[1]).Value = false;
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }

}
