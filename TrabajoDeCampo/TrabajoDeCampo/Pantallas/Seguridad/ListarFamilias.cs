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

        private Regex lettersOnly = new Regex("^[a-zA-Z ]+$");
        private Boolean valido = false;
        private Dictionary<string, string> traducciones;

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
            this.dgPatentes.Columns[0].Tag = "com.td.patente";
            this.dgPatentes.Columns[1].Tag = "com.td.asignada";
            this.dgFamilia.ReadOnly = true;
            this.dgPatentes.Columns[0].DataPropertyName = "descripcion";
            this.dgFamilia.Columns[0].DataPropertyName = "nombre";
            this.dgFamilia.Columns[0].Tag = "com.td.familia";
            this.txtNombre.KeyPress += TxtNombre_KeyPress;
            desbloquearControles();

        }

        public void desbloquearControles()
        {
            long id = (long)TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            bool crear = servicioSeguridad.tienePatente(id, EnumPatentes.CrearFamilia.ToString());
            bool modificar = servicioSeguridad.tienePatente(id, EnumPatentes.ModificarFamilias.ToString());
            bool borrar = servicioSeguridad.tienePatente(id, EnumPatentes.BorrarFamilia.ToString());

            this.btnCrear.Enabled = crear;
            this.btnModificar.Enabled = modificar;
            this.btnBorrar.Enabled = borrar;
        }

        private void TxtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!e.KeyChar.Equals('\b'))//tecla borrar
            {
                if (!lettersOnly.IsMatch(e.KeyChar.ToString()))
                {
                    e.Handled = true;
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
                    DialogResult result = MessageBox.Show(traducciones["com.td.seguro"], "", MessageBoxButtons.OKCancel);
                    if (!result.Equals(DialogResult.OK))
                    {
                        return;
                    }
                    this.servicioSeguridad.borrarFamilia(fami.id);
                    MessageBox.Show(traducciones["com.td.completado"], "", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.Equals("ASIGNADA") ? traducciones["com.td.asignada"] : ex.Message);
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
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.helpProvider1.SetHelpKeyword(this, Properties.Settings.Default.Idioma.Equals("es") ? "Permisos.htm" : "Permissions.htm");
            this.helpProvider1.HelpNamespace = Application.StartupPath + @"\\DocumentsDeAyuda.chm";
            listarElementos();

            //traduccion
            FormUtils traductor = new TraductorIterador();
            List<String> tags = new List<string>();
            long id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            tags.Add("com.td.complete.campos");
            tags.Add("com.td.familia.existe");
            tags.Add("com.td.asignada");
            tags.Add("com.td.permisos.esenciales");
            tags.Add("com.td.completado");
            tags.Add("com.td.seguro");

            traductor.process(tags, this, null, null);
            traducciones = servicioSeguridad.traerTraducciones(tags, Properties.Settings.Default.Idioma);
            traductor = new TraductorReal();
            traductor.process(null, this, traducciones, null);
            traductor = new TraductorIterador();

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.txtNombre.Text))
            {
                MessageBox.Show(traducciones["com.td.complete.campos"]);
                return;
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
                bool existe = nombreExiste(familia);
                if (!existe)
                {
                    this.servicioSeguridad.crearFamilia(familia);
                    MessageBox.Show(traducciones["com.td.completado"], "", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show(traducciones["com.td.familia.existe"]);
                }
                
            }
            else
            {
                bool existe = nombreExiste(familia);
                if (!existe)
                {
                    try
                    {
                        this.servicioSeguridad.modificarFamilia(familia);
                        MessageBox.Show(traducciones["com.td.completado"], "", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Equals("PERMISOS"))
                        {
                            MessageBox.Show(traducciones["com.td.permisos.esenciales"]);
                        }
                        else
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    
                }
                else
                {
                    MessageBox.Show(traducciones["com.td.familia.existe"]);
                }
                
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
        
        private Boolean nombreExiste(Familia familia)
        {
            Boolean existe = false;
            List<Familia> familias = this.dgFamilia.DataSource as List<Familia>;
            foreach (Familia fam in familias)
            {
                if(fam.nombre.Equals(familia.nombre,StringComparison.InvariantCultureIgnoreCase) && fam.id != familia.id)
                {
                    existe = true;
                    break;
                }
            }

            return existe;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }

}
