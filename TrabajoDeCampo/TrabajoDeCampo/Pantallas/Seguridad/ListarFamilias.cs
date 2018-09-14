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

namespace TrabajoDeCampo.Pantallas.Seguridad
{
    public partial class ListarFamilias : Form
    {
        //List<Control> controlesABloquear = new List<Control> { this.btnCrear, this.btnBorrar, this.btnCancelar, this.btnGuardar, this.btnModificar, this.txtNombre };
        private ServicioSeguridad servicioSeguridad;
        FormUtils utils;
        private Familia currentFamilia;
        public ListarFamilias()
        {
            InitializeComponent();
            servicioSeguridad = new ServicioSeguridad();
            utils = new Bloqueador();

            List<Control> controlesABloquear = new List<Control> { this.btnGuardar, this.txtNombre };
            utils.process(null,null,null, controlesABloquear);
            this.dgPatentes.DataSource = null;
            this.dgFamilia.DataSource = null;
            this.dgFamilia.AutoGenerateColumns = false;
            this.dgPatentes.AutoGenerateColumns = false;
            this.dgPatentes.Columns[0].ReadOnly = true;
            this.dgPatentes.Columns[1].ReadOnly = true;
            this.dgFamilia.ReadOnly = true;
            this.dgPatentes.Columns[0].DataPropertyName = "descripcion";
            this.dgFamilia.Columns[0].DataPropertyName = "nombre";


        }
          
        public void listarElementos()
        {

            this.dgPatentes.DataSource = this.servicioSeguridad.listarPatentes();
            this.dgFamilia.DataSource = this.servicioSeguridad.listarFamilias();
            if (this.dgFamilia.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in this.dgFamilia.Rows)
                {
                    Familia fami = (Familia)row.DataBoundItem;

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
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(this.dgFamilia.CurrentRow != null)
            {
                Familia fami = (Familia)this.dgFamilia.CurrentRow.DataBoundItem;

                this.txtNombre.Text = fami.nombre;
                this.currentFamilia = fami;
            }
            utils = new Desbloqueador();
            utils.process(null, null, null, new List<Control> { this.txtNombre, this.btnGuardar });
            utils = new Bloqueador();
            this.dgPatentes.Columns[1].ReadOnly = false;
            utils.process(null, null, null, new List<Control> { this.btnCrear, this.btnModificar, this.btnBorrar });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dgPatentes.Columns[1].ReadOnly = false;
            foreach (DataGridViewRow item in this.dgPatentes.Rows)
            {
                ((DataGridViewCheckBoxCell)item.Cells[1]).Value = false;
            } 
            utils = new Desbloqueador();
            utils.process(null, null, null, new List<Control> { this.txtNombre, this.btnGuardar });
            utils = new Bloqueador();    
            utils.process(null, null, null, new List<Control> { this.btnCrear, this.btnModificar, this.btnBorrar });
        }

        private void ListarFamilias_Load(object sender, EventArgs e)
        {

            listarElementos();
            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
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
            listarElementos();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.currentFamilia = null;
            if (this.txtNombre.Enabled)
            {
                utils = new Bloqueador();
                utils.process(null, null, null, new List<Control> { this.txtNombre, this.btnGuardar });
                this.txtNombre.Text = "";
                utils = new Desbloqueador();
                utils.process(null, null, null, new List<Control> { this.btnCrear, this.btnModificar, this.btnBorrar});

            }
            else
            {
                this.Close();
            }
        }
    }

}
