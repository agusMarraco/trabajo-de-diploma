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
    public partial class ListaDeUsuarios : Form
    {
        private ServicioSeguridad servicioSeguridad;
        private Dictionary<string, string> traducciones;
        private FormUtils formUtils;
        private Dictionary<String, String> mensajesDeValidacion = new Dictionary<string, string>();

        public ListaDeUsuarios()
        {
            InitializeComponent();
            servicioSeguridad = new ServicioSeguridad();
            this.comboFiltro.DataSource = null;
            this.gwUsuarios.Columns[0].Tag = "com.td.alias";
            this.gwUsuarios.Columns[1].Tag = "com.td.apellido";
            this.gwUsuarios.Columns[2].Tag = "com.td.nombre";
            this.gwUsuarios.Columns[3].Tag = "com.td.d.n.i.";
            this.gwUsuarios.Columns[4].Tag = "com.td.bloqueado";
            this.gwUsuarios.DataSource = null;
            this.gwUsuarios.AutoGenerateColumns = false;
            this.gwUsuarios.CellFormatting += booleanFormatter;
            this.gwUsuarios.ColumnHeaderMouseClick += customSort;
            desbloquearControles();
        }

        public void desbloquearControles()
        {
            long id = (long)TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            bool crear = servicioSeguridad.tienePatente(id, EnumPatentes.CrearFamilia.ToString());
            bool modificar = servicioSeguridad.tienePatente(id, EnumPatentes.ModificarFamilias.ToString());
            bool borrar = servicioSeguridad.tienePatente(id, EnumPatentes.BorrarFamilia.ToString());
            bool regen = servicioSeguridad.tienePatente(id, EnumPatentes.RegenerarContraseña.ToString());
            bool bloq = servicioSeguridad.tienePatente(id, EnumPatentes.BloquearUsuario.ToString());

            this.btnCrear.Enabled = crear;
            this.btnMod.Enabled = modificar;
            this.btnDel.Enabled = borrar;
            this.btnBloq.Enabled = bloq;
            this.btnRegen.Enabled = regen;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //chequear por null
            if(this.gwUsuarios.CurrentRow.DataBoundItem != null)
            {
                Usuario usu = (Usuario)this.gwUsuarios.CurrentRow.DataBoundItem;
                AltaModificacionUsuario user = new AltaModificacionUsuario(true,usu,this);
                user.Show();
            }
            else
            {
                //mensaje de consistencia
            }
        }

        private void ListaDeUsuarios_Load(object sender, EventArgs e)
        {
            //traduccion
            formUtils = new TraductorIterador();
            List<String> tags = new List<string>();
            formUtils.process(tags, this, null, null);
            tags.AddRange(new string[] {"com.td.d.n.i.","com.td.alias","com.td.apellido","com.td.nombre","com.td.si","com.td.no","com.td.permisos.esenciales"});
            traducciones = servicioSeguridad.traerTraducciones(tags, Properties.Settings.Default.Idioma);
            formUtils = new TraductorReal();
            formUtils.process(null, this, traducciones, null);
            formUtils = new TraductorIterador();

            this.comboFiltro.DataSource = new List<KeyValuePair<String, String>>
            {
                new KeyValuePair<string, string>("dni", traducciones["com.td.d.n.i."]),
                new KeyValuePair<string, string>("alias", traducciones["com.td.alias"]),
                new KeyValuePair<string, string>("apellido", traducciones["com.td.apellido"]),
                new KeyValuePair<string, string>("nombre", traducciones["com.td.nombre"])
            };
            this.comboFiltro.DisplayMember = "value";
            List<Usuario> usuarios = servicioSeguridad.listarUsuarios(null, null, null);
            searchUsuarios(usuarios);


        }

        public void populateDataGrid(List<Usuario> usuarios)
        {
            
            this.gwUsuarios.DataSource = usuarios;
            this.gwUsuarios.Columns[0].DataPropertyName = "alias";
            this.gwUsuarios.Columns[1].DataPropertyName = "apellido";
            this.gwUsuarios.Columns[2].DataPropertyName = "nombre";
            this.gwUsuarios.Columns[3].DataPropertyName = "dni";
            this.gwUsuarios.Columns[4].DataPropertyName = "baja";
            
        }

        public void searchUsuarios(List<Usuario> usuarios)
        {
        
            populateDataGrid(usuarios);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            AltaModificacionUsuario user = new AltaModificacionUsuario(false, null,this);
            user.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            String filtro = ((KeyValuePair<string, string>)this.comboFiltro.SelectedItem).Key;
            String valor = this.textBox1.Text;
            
            List<Usuario> usuarios = this.servicioSeguridad.listarUsuarios(filtro, valor, null);
            searchUsuarios(usuarios);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void booleanFormatter(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.ColumnIndex == 4)
            {
                e.Value = (int)e.Value == 0 ? traducciones["com.td.no"] : traducciones["com.td.si"];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //bloquear
            if(gwUsuarios.CurrentRow != null && gwUsuarios.CurrentRow.DataBoundItem != null)
            {
                Usuario usu = (Usuario)gwUsuarios.CurrentRow.DataBoundItem;
                long id = usu.id;
                if (id != 0)
                {
                    try
                    {
                        servicioSeguridad.bloquearUsuario(usu);
                    }
                    catch (Exception ex)
                    {

                        if (ex.Message == "PERMISOS")
                        {
                            MessageBox.Show(traducciones["com.td.permisos.esenciales"]);
                        }
                        else
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    
                }
                

                searchUsuarios(this.servicioSeguridad.listarUsuarios(null, null, null));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //regenerar password
            if (gwUsuarios.CurrentRow != null && gwUsuarios.CurrentRow.DataBoundItem != null)
            {
                Usuario usu = (Usuario)gwUsuarios.CurrentRow.DataBoundItem;
                servicioSeguridad.regenerarContraseña(usu);
                searchUsuarios(this.servicioSeguridad.listarUsuarios(null, null, null));
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (gwUsuarios.CurrentRow != null && gwUsuarios.CurrentRow.DataBoundItem != null)
            {
                Usuario usu = (Usuario)gwUsuarios.CurrentRow.DataBoundItem;
                try
                {
                    servicioSeguridad.borrarUsuario(usu);
                }
                catch (Exception ex)
                {

                    if (ex.Message == "PERMISOS")
                    {
                        MessageBox.Show(traducciones["com.td.permisos.esenciales"]);
                    }
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                
                searchUsuarios(this.servicioSeguridad.listarUsuarios(null, null, null));
            }
        }

        private void customSort(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach(DataGridViewColumn column in ((DataGridView)sender).Columns)
            {
                if(column.Index != e.ColumnIndex)
                {
                    column.HeaderCell.SortGlyphDirection = SortOrder.None;
                    column.HeaderCell.Style.BackColor = Color.White;
                }
            }
            //initialSorting
            if(this.gwUsuarios.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None || this.gwUsuarios.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                List<Usuario> usuarios = (List<Usuario>)this.gwUsuarios.DataSource;
                string propertyName = this.gwUsuarios.Columns[e.ColumnIndex].Name;
                this.gwUsuarios.DataSource = usuarios.OrderBy(x => x.GetType().GetProperty(propertyName).GetValue(x)).ToList();
                this.gwUsuarios.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                this.gwUsuarios.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.LightSkyBlue;
            }
            else
            {
                List<Usuario> usuarios = (List<Usuario>)this.gwUsuarios.DataSource;
                string propertyName = this.gwUsuarios.Columns[e.ColumnIndex].Name;
                
                this.gwUsuarios.DataSource = usuarios.OrderByDescending(x => x.GetType().GetProperty(propertyName).GetValue(x)).ToList();
                this.gwUsuarios.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                this.gwUsuarios.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.LightSkyBlue;

            }
            
               
        }
        public void  listarDefault()
        {
            searchUsuarios(this.servicioSeguridad.listarUsuarios(null, null, null));
        }

      
    }
}
