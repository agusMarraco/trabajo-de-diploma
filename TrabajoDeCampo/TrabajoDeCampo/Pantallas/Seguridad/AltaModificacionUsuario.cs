using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
        private FormUtils formUtils;
        private Dictionary<String, String> mensajesDeValidacion = new Dictionary<string, string>();
        private StringBuilder mensajesDeError = new StringBuilder();
        private Boolean isEdit;
        public AltaModificacionUsuario()
        {
            
        }

        public AltaModificacionUsuario(Boolean isEdit, Usuario usuario)
        {
            InitializeComponent();
            this.servicioSeguridad = new ServicioSeguridad();
            this.dgfamilias.DataSource = null;
            this.dgpatentes.DataSource = null;
            this.isEdit = isEdit;
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void AltaModificacionUsuario_Load(object sender, EventArgs e)
        {
            this.dgfamilias.Columns[0].Tag = "com.td.familia";
            this.dgfamilias.Columns[0].ReadOnly = true;
            this.dgfamilias.Columns[1].Tag = "com.td.asignada";
            this.dgfamiliapatente.Columns[0].Tag = "com.td.patente";
            this.dgfamiliapatente.Columns[0].ReadOnly= true;
            this.dgpatentes.Columns[0].Tag = "com.td.patente";
            this.dgpatentes.Columns[0].ReadOnly= true;
            this.dgpatentes.Columns[1].Tag = "com.td.asignada";
            this.dgpatentes.Columns[2].Tag = "com.td.bloqueada";
            formUtils = new TraductorIterador();
            List<String> tags = new List<string>();
            formUtils.process(tags, this, null, null);
            tags.Add("com.td.ingles");
            tags.Add("com.td.español");
            Dictionary<String, String> traducciones = servicioSeguridad.traerTraducciones(tags, Properties.Settings.Default.Idioma);
            formUtils = new TraductorReal();
            formUtils.process(null, this, traducciones, null);
            formUtils = new TraductorIterador();
            List<KeyValuePair<Idioma, String>> comboIdiomas = new List<KeyValuePair<Idioma, string>>();

            comboIdiomas.Add(new KeyValuePair<Idioma, string>(new Idioma(1,"es","Español"), traducciones["com.td.español"]));
            comboIdiomas.Add(new KeyValuePair<Idioma, string>(new Idioma(2, "en", "Ingles"), traducciones["com.td.ingles"]));
            
            this.comboBox1.DataSource = null;
            this.comboBox1.DataSource = comboIdiomas;
            this.comboBox1.DisplayMember = "value";

        }

        public Boolean validateInputs()
        {
            mensajesDeError = new StringBuilder();
            Boolean hayErrores = false;
            if (mensajesDeValidacion.Count == 0)
            {
                String[] array = { "com.td.validacion.alerta","com.td.validacion.error","com.td.error.generico","com.td.completado.generico" };
                List<String> tags = new List<string>(array);
                mensajesDeValidacion = servicioSeguridad.traerTraducciones(tags, Properties.Settings.Default.Idioma);

            }

            mensajesDeError.Append(mensajesDeValidacion["com.td.validacion.alerta"]);
            mensajesDeError.Append(Environment.NewLine);
            if (this.nombre.Text == null || this.nombre.Text == "")
            {
                mensajesDeError.Append(this.nombrelbl.Text);
                mensajesDeError.Append(Environment.NewLine);
                hayErrores = true;
            }
            if (this.apellido.Text == null || this.apellido.Text == "")
            {
                mensajesDeError.Append(this.apellidolbl.Text);
                mensajesDeError.Append(Environment.NewLine);
                hayErrores = true;
            }
            if (this.dni.Text == null || this.dni.Text == "")
            {
                mensajesDeError.Append(this.dnilbl.Text);
                mensajesDeError.Append(Environment.NewLine);
                hayErrores = true;
            }
            if (this.direccion.Text == null || this.direccion.Text == "")
            {
                mensajesDeError.Append(this.direccionlbl.Text);
                mensajesDeError.Append(Environment.NewLine);
                hayErrores = true;
            }
            if (this.telefono.Text == null || this.telefono.Text == "" || !this.telefono.MaskCompleted)
            {
                mensajesDeError.Append(this.telefonolbl.Text);
                mensajesDeError.Append(Environment.NewLine);
                hayErrores = true;
            }
            if (this.alias.Text == null || this.alias.Text == "")
            {
                mensajesDeError.Append(this.aliaslbl.Text);
                mensajesDeError.Append(Environment.NewLine);
                hayErrores = true;
            }
            if (this.email.Text == null || this.email.Text == "")
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
                try
                {
                    var addr = new System.Net.Mail.MailAddress(this.email.Text);
                    var trues = addr.Address == this.email.Text;
                }
                catch
                {
                    var falses = false;
                }

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
                MessageBox.Show(mensajesDeError.ToString(), mensajesDeValidacion["com.td.validacion.error"], MessageBoxButtons.OK);
            }
            else
            {
                if (isEdit)
                {//editando un usuario

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

                    usuario.idioma = ((KeyValuePair<Idioma, string>)this.comboBox1.SelectedItem).Key;
                    bool error = false;
                    try
                    {
                        servicioSeguridad.crearUsuario(usuario);
                    }
                    catch (Exception exe)
                    {
                        error = true;
                        StringBuilder sb = new StringBuilder();
                        sb.Append(mensajesDeValidacion["com.td.error.generico"]);
                        sb.Append(Environment.NewLine);
                        sb.Append(exe.Message);
                        MessageBox.Show(sb.ToString(), mensajesDeValidacion["com.td.validacion.error"], MessageBoxButtons.OK);
                    }
                    if(!error)
                    MessageBox.Show(mensajesDeValidacion["com.td.completado.generico"], mensajesDeValidacion["com.td.validacion.error"], MessageBoxButtons.OK);
                }

            }


        }
    }

    
}
