using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoDeCampo.BO;
using TrabajoDeCampo.SEGURIDAD;
using TrabajoDeCampo.SERVICIO;

namespace TrabajoDeCampo.Pantallas.Seguridad
{
    public partial class AltaModificacionUsuario : Form
    {
        private ServicioSeguridad servicioSeguridad;
        private Dictionary<string, string> traducciones;
        private FormUtils formUtils;
        
        private StringBuilder mensajesDeError = new StringBuilder();
        private ListaDeUsuarios parentForm;
        private Boolean isEdit;
        private Usuario currentUsuario;

        private Regex lettersRegex = new Regex("[a-zA-z]");
        private Regex numbersRegex = new Regex("[0-9]");
        private Regex alphanumericRegex = new Regex("[a-zA-Z0-9]");

        private Boolean valido = false;
        public AltaModificacionUsuario()
        {
        
        }

        public AltaModificacionUsuario(Boolean isEdit, Usuario usuario, ListaDeUsuarios parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            //inicializando propiedades
            this.KeyPreview = true;
            this.servicioSeguridad = new ServicioSeguridad();
            this.dgfamiliapatente.DataSource = null;
            this.dgfamilias.DataSource = null;
            this.dgpatentes.DataSource = null;

            this.nombre.KeyDown += validarLetrasKD;
            this.nombre.KeyPress += validarLetrasKP;
            this.apellido.KeyDown += validarLetrasKD;
            this.apellido.KeyPress += validarLetrasKP;
            this.dni.KeyPress += validarNumerosKP;
            this.dni.KeyDown += validarNumerosKD;
            this.direccion.KeyPress += validarAlphaKP;
            this.direccion.KeyDown += validarAlphaKD;
            this.alias.KeyPress += validarAlphaKP;
            this.alias.KeyDown += validarAlphaKD;
            this.telefono.KeyPress += validarNumerosKP;
            this.telefono.KeyDown += validarNumerosKD;

            this.dgfamiliapatente.AutoGenerateColumns = false ;
            this.dgfamilias.AutoGenerateColumns = false;
            this.dgpatentes.AutoGenerateColumns = false;

            //cargo los dgv
            List<ComponentePermiso> permisos = this.servicioSeguridad.listarFamiliasYPatentes();
            List<Patente> patentes = new List<Patente>();
            List<Familia> familias = new List<Familia>();

            permisos.ForEach(x => {
                ComponentePermiso per = x;
                if (per is Patente)
                {
                    patentes.Add((Patente)per);
                }
                else
                {
                    familias.Add((Familia)per);
                }
            });

            this.dgfamilias.DataSource = familias;
            this.dgpatentes.DataSource = patentes;


            this.dgpatentes.Columns[0].DataPropertyName = "descripcion";
            this.dgpatentes.Columns[2].DataPropertyName = "bloqueada";
            this.dgpatentes.Columns[1].DataPropertyName = "asignada";
            this.dgfamiliapatente.Columns[0].DataPropertyName = "descripcion";
            this.dgfamilias.Columns[0].DataPropertyName = "nombre";
    
            // event handlers
            this.dgfamilias.SelectionChanged += actualizarPatentesMostradas;
            this.dgpatentes.CellMouseUp += mouseLeaveCheckbox;
            this.dgpatentes.CellValueChanged += cellValueChanged;
            this.dgpatentes.CellEndEdit += endEditHandler;
            this.dgpatentes.KeyUp += spaceHandler;



            //si es un edit guardo la referencia al usuario
            this.isEdit = isEdit;
            if(usuario != null)
                this.currentUsuario = this.servicioSeguridad.buscarUsuario(usuario.id);
         
            
        }

      

        private void AltaModificacionUsuario_Load(object sender, EventArgs e)
        {
            //inicializo propiedades
            this.dgfamilias.Columns[0].Tag = "com.td.familia";
            this.dgfamilias.Columns[0].ReadOnly = true;
            this.dgfamilias.MultiSelect = false;
            this.dgfamilias.Columns[1].Tag = "com.td.asignada";
            this.dgfamiliapatente.Columns[0].Tag = "com.td.patente";
            this.dgfamiliapatente.Columns[0].ReadOnly= true;
            this.dgpatentes.Columns[0].Tag = "com.td.patente";
            this.dgpatentes.Columns[0].ReadOnly= true;
            this.dgpatentes.Columns[1].Tag = "com.td.asignada";
            this.dgpatentes.Columns[2].Tag = "com.td.bloqueada";


            mostrarPatentesDeFamilia();

            //busco traducciones
            formUtils = new TraductorIterador();
            List<String> tags = new List<string>();
            formUtils.process(tags, this, null, null);
            tags.Add("com.td.ingles");
            tags.Add("com.td.español");
            tags.Add("com.td.completado");
            tags.Add("com.td.permisos.esenciales");
            tags.Add("com.td.permisos.esenciales");
            tags.Add("com.td.complete.campos");
            traducciones = servicioSeguridad.traerTraducciones(tags, Properties.Settings.Default.Idioma);
            formUtils = new TraductorReal();
            formUtils.process(null, this, traducciones, null);
            formUtils = new TraductorIterador();
            List<KeyValuePair<Idioma, String>> comboIdiomas = new List<KeyValuePair<Idioma, string>>();

            comboIdiomas.Add(new KeyValuePair<Idioma, string>(new Idioma(1,"es","Español"), traducciones["com.td.español"]));
            comboIdiomas.Add(new KeyValuePair<Idioma, string>(new Idioma(2, "en", "Ingles"), traducciones["com.td.ingles"]));
            //cargo los combos
            this.comboBox1.DataSource = null;
            this.comboBox1.DataSource = comboIdiomas;
            this.comboBox1.DisplayMember = "value";

           if(currentUsuario != null)
            {
                //populate data si es un edit
                this.nombre.Text = currentUsuario.nombre;
                this.apellido.Text = currentUsuario.apellido;
                this.dni.Text = currentUsuario.dni;
                this.direccion.Text = currentUsuario.direccion;
                this.telefono.Text = currentUsuario.telefono;
                this.alias.Text = currentUsuario.alias;
                this.alias.Enabled = false;
                this.email.Text = currentUsuario.email;
                foreach(KeyValuePair<Idioma, string> item in this.comboBox1.Items)
                {
                    if(item.Key.codigo == currentUsuario.idioma.codigo)
                    {
                        this.comboBox1.SelectedItem = item;
                    }
                }
                 // los permisos que tiene asignado el usuario
                foreach(ComponentePermiso cp in currentUsuario.componentePermisos)
                {
                    if(cp is Familia)
                    {
                        foreach(DataGridViewRow item in dgfamilias.Rows)
                        {
                            if(((Familia)item.DataBoundItem).id == ((Familia)cp).id)
                            {
                                ((DataGridViewCheckBoxCell)item.Cells[1]).Value = true;
                            }
                        }
                    }
                    else
                    {
                        foreach (DataGridViewRow item in dgpatentes.Rows)
                        {
                            if (((Patente)item.DataBoundItem).id == ((Patente)cp).id)
                            {
                                if (((Patente)cp).bloqueada)
                                {
                                    ((DataGridViewCheckBoxCell)item.Cells[2]).Value = true;
                                }
                                else
                                {
                                    ((DataGridViewCheckBoxCell)item.Cells[1]).Value = true;

                                }
                            }

                        }
                    }
                }
            }

        }
        /// <summary>
        /// valida los inputs que ingresan los usuarios y agrega mensajes de error.
        /// </summary>
        /// <returns></returns>
        public Boolean validateInputs()
        {
            mensajesDeError = new StringBuilder();
            mensajesDeError.Append(traducciones["com.td.complete.campos"]).Append(Environment.NewLine);
            Boolean hayErrores = false;
                        if (String.IsNullOrEmpty(this.nombre.Text))
            {
                mensajesDeError.Append(this.nombrelbl.Text);
                mensajesDeError.Append(Environment.NewLine);
                hayErrores = true;
            }
            if (String.IsNullOrEmpty(this.apellido.Text))
            {
                mensajesDeError.Append(this.apellidolbl.Text);
                mensajesDeError.Append(Environment.NewLine);
                hayErrores = true;
            }
            if (String.IsNullOrEmpty(this.dni.Text))
            {
                mensajesDeError.Append(this.dnilbl.Text);
                mensajesDeError.Append(Environment.NewLine);
                hayErrores = true;
            }
            if (String.IsNullOrEmpty(this.direccion.Text))
            {
                mensajesDeError.Append(this.direccionlbl.Text);
                mensajesDeError.Append(Environment.NewLine);
                hayErrores = true;
            }
            if (String.IsNullOrEmpty(this.telefono.Text) || !this.telefono.MaskCompleted)
            {
                mensajesDeError.Append(this.telefonolbl.Text);
                mensajesDeError.Append(Environment.NewLine);
                hayErrores = true;
            }
            if (String.IsNullOrEmpty(this.alias.Text))
            {
                mensajesDeError.Append(this.aliaslbl.Text);
                mensajesDeError.Append(Environment.NewLine);
                hayErrores = true;
            }
            if (String.IsNullOrEmpty(this.email.Text))
            {
                mensajesDeError.Append(this.emaillbl.Text);
                mensajesDeError.Append(Environment.NewLine);
                hayErrores = true;
            }
            else
            {
                var email = new EmailAddressAttribute();
                bool valid;
                valid = email.IsValid(this.email.Text);
                if (!valid)
                {
                    mensajesDeError.Append(this.emaillbl.Text);
                    mensajesDeError.Append(Environment.NewLine);
                    hayErrores = true;
                }
            }

            return hayErrores;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Boolean hayErrores = validateInputs();

            if (hayErrores)
            {
                MessageBox.Show(mensajesDeError.ToString(), "", MessageBoxButtons.OK);
            }
            else
            {
                if (isEdit)
                {//editando un usuario

                    currentUsuario.nombre = this.nombre.Text;
                    currentUsuario.apellido = this.apellido.Text;
                    currentUsuario.dni = this.dni.Text;
                    currentUsuario.direccion = this.direccion.Text;
                    currentUsuario.telefono = this.telefono.Text;
                    currentUsuario.alias = this.alias.Text;
                    currentUsuario.email = this.email.Text;
                    currentUsuario.nombre = this.nombre.Text;
                    currentUsuario.idioma = ((KeyValuePair<Idioma, string>)this.comboBox1.SelectedItem).Key;

                    currentUsuario.componentePermisos = new List<ComponentePermiso>();
                    asociarPermisos(currentUsuario.componentePermisos);
                    bool error = false;
                    try
                    {
                        servicioSeguridad.modificarUsuario(currentUsuario);
                    }
                    catch (Exception exe)
                    {
                        error = true;
                        StringBuilder sb = new StringBuilder();
                   
                   
                        if (exe.Message == "PERMISOS")
                        {
                            sb.Append(traducciones["com.td.permisos.esenciales"]);
                        }
                        else
                        {
                            sb.Append(exe.Message);
                        }
                        MessageBox.Show(sb.ToString(), "", MessageBoxButtons.OK);
                    }
                    if (!error)
                    {
                        MessageBox.Show(traducciones["com.td.completado"], "", MessageBoxButtons.OK);
                        this.parentForm.listarDefault();
                        this.Close();
                    }
                  
                }
                else
                {//creando uno nuevo
                    Usuario usuario = new Usuario();
                    usuario.nombre = this.nombre.Text;
                    usuario.apellido = this.apellido.Text;
                    usuario.dni= this.dni.Text;
                    usuario.direccion = this.direccion.Text;
                    usuario.telefono= this.telefono.Text;
                    usuario.alias= this.alias.Text;
                    usuario.email = this.email.Text;
                    usuario.nombre = this.nombre.Text;
                    usuario.componentePermisos = new List<ComponentePermiso>();
                    asociarPermisos(usuario.componentePermisos);
                    usuario.idioma = ((KeyValuePair<Idioma, string>)this.comboBox1.SelectedItem).Key;
                    bool error = false;
                    try
                    {
                        servicioSeguridad.crearUsuario(usuario);
                    }
                    catch (Exception exe)
                    {
                        error = true;

                        MessageBox.Show(exe.Message, "", MessageBoxButtons.OK);
                    }
                    if (!error)
                    {
                        MessageBox.Show(traducciones["com.td.completado"],"", MessageBoxButtons.OK);
                        this.parentForm.listarDefault();
                        this.Close();
                    }
                }

            }


        }
        /// <summary>
        /// se triggerea cuando cambio la seleccion de familia en el dgv
        /// </summary>
        public void mostrarPatentesDeFamilia()
        {
            List<Patente> currentPatentes = new List<Patente>();
            (this.dgfamilias.CurrentRow.DataBoundItem as Familia).patentes.ForEach(x => {
                ComponentePermiso per = x;
                currentPatentes.Add((Patente)per);

            });
            this.dgfamiliapatente.DataSource = null;
            this.dgfamiliapatente.DataSource = currentPatentes;

        }

        public void actualizarPatentesMostradas(Object sender, EventArgs e)
        {
            this.mostrarPatentesDeFamilia();
        }

        //marco los permisos del usuario
        public void asociarPermisos( List<ComponentePermiso> permisos)
        {
            foreach (DataGridViewRow item in dgfamilias.Rows)
            {
                if (((DataGridViewCheckBoxCell)item.Cells[1]).Value != null && (bool)((DataGridViewCheckBoxCell)item.Cells[1]).Value == true)
                {
                    permisos.Add((Familia)item.DataBoundItem);
                }
            }
            foreach (DataGridViewRow item in dgpatentes.Rows)
            {
                if ((((DataGridViewCheckBoxCell)item.Cells[1]).Value != null && (bool)((DataGridViewCheckBoxCell)item.Cells[1]).Value == true) || (((DataGridViewCheckBoxCell)item.Cells[2]).Value != null && (bool)((DataGridViewCheckBoxCell)item.Cells[2]).Value == true))
                {
                    Patente patente = (Patente)item.DataBoundItem;
                    //chequeo si esta bloqueandose , si no esta bloqueada entra por el otro y la interpreto como un insert, no se puede tener los 2 estados al mismo tiempo.
                    patente.bloqueada = (((DataGridViewCheckBoxCell)item.Cells[2]).Value != null && (bool)((DataGridViewCheckBoxCell)item.Cells[2]).Value == true) ? true : false;
                    permisos.Add(patente);

                }
            }

        }

        //manejadores de eventos para garantizar que solo se puede bloquear o agregar una patente, no las 2 al mismo tiempo.

        public void mouseLeaveCheckbox(object obj, DataGridViewCellMouseEventArgs eventArgs)
        {
            int rowIndex = eventArgs.RowIndex;
            int columnIndex = eventArgs.ColumnIndex;
            if ((columnIndex == 2 || columnIndex == 1) && rowIndex != -1)
            {
                this.dgpatentes.EndEdit();
             
            }


        }

        public void spaceHandler(object obj, KeyEventArgs eventArgs)
        {
            if(eventArgs.KeyCode == Keys.Space)
            {
                this.dgpatentes.EndEdit();
            }

        }


        public void endEditHandler(object obj, DataGridViewCellEventArgs eventArgs)
        {
             int rowIndex = eventArgs.RowIndex;
            int columnIndex = eventArgs.ColumnIndex;
            if ((columnIndex == 2 || columnIndex == 1) && rowIndex != -1)
            {
                if (columnIndex == 2)
                {
                    DataGridViewCell currentCell = this.dgpatentes.Rows[rowIndex].Cells[columnIndex];
                    if (currentCell.Value != null && (bool)currentCell.Value)
                    {
                        DataGridViewCell theOtherCell = this.dgpatentes.Rows[rowIndex].Cells[columnIndex - 1];
                        theOtherCell.Value = false;
                        InvokeLostFocus(this.dgpatentes, null);
                    }
                }
                else
                {
                    DataGridViewCell currentCell = this.dgpatentes.Rows[rowIndex].Cells[columnIndex];
                    if (currentCell.Value != null && (bool)currentCell.Value)
                    {
                        DataGridViewCell theOtherCell = this.dgpatentes.Rows[rowIndex].Cells[columnIndex + 1];
                        theOtherCell.Value = false;

                    }

                }

            }
        }
        

        public void cellValueChanged(object obj, DataGridViewCellEventArgs eventArgs)
        {
            int rowIndex = eventArgs.RowIndex;
            int columnIndex = eventArgs.ColumnIndex;
            if ((columnIndex == 2 || columnIndex == 1) && rowIndex != -1)
            {

                this.dgpatentes.EndEdit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void validarNumerosKD(object sender, KeyEventArgs e)
        {
            valido = true;
            if (!e.KeyValue.Equals(8))//tecla borrar
            {
                if (!numbersRegex.IsMatch(e.KeyData.ToString()) || e.KeyData.ToString().Contains("Oem"))
                {
                    valido = false;
                }
            }
        }

        private void validarNumerosKP(object sender, KeyPressEventArgs e)
        {
            if (!valido)
            {
                e.Handled = true;
            }
        }

        private void validarAlphaKD(object sender, KeyEventArgs e)
        {
            valido = true;
            if (!e.KeyValue.Equals(8))//tecla borrar
            {
                if (!alphanumericRegex.IsMatch(e.KeyData.ToString()) || e.KeyData.ToString().Contains("Oem"))
                {
                    valido = false;
                }
            }
        }

        private void validarAlphaKP(object sender, KeyPressEventArgs e)
        {
            if (!valido)
            {
                e.Handled = true;
            }
        }

        private void validarLetrasKP(object sender, KeyPressEventArgs e)
        {
            if (!valido)
            {
                e.Handled = true;
            }
        }

        private void validarLetrasKD(object sender, KeyEventArgs e)
        {
            valido = true;
            if (!e.KeyValue.Equals(8))//tecla borrar
            {
                if (!lettersRegex.IsMatch(e.KeyData.ToString()) || e.KeyData.ToString().Contains("Oem"))
                {
                    valido = false;
                }
            }
        }
    }

    
}
