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
    public partial class FalloConexión : Form
    {
        private Regex regex = new Regex("([A-Za-z0-9\\\\.-])");
        private Boolean valido = false;
        public FalloConexión()
        {
            System.Diagnostics.Debugger.Launch();
            InitializeComponent();
            this.textBox1.KeyPress += validateKP;
            this.textBox2.KeyPress += validateKP;
            this.textBox3.KeyPress += validateKP;

        }

    
        private void validateKP(object sender, KeyPressEventArgs e)
        {
            if (!e.KeyChar.Equals('\b'))//tecla borrar
            {
                if (!regex.IsMatch(e.KeyChar.ToString()))
                {
                    e.Handled = true;
                }
            }
        }

        private void FalloConexión_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.helpProvider1.SetHelpKeyword(this, "Fallo_Conexión.htm");
            this.helpProvider1.HelpNamespace = Application.StartupPath + @"\\DocumentsDeAyuda.chm";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Por favor ingrese un servidor o nombre de base de datos.");
            }
            else
            {
                if(!String.IsNullOrEmpty(this.textBox2.Text) && !String.IsNullOrEmpty(this.textBox3.Text))
                {
                    String prevConnectionString = TrabajoDeCampo.Properties.Settings.Default.ConnectionString; // encriptado
                    String connectionString = "Data Source = "  + this.textBox1.Text + "; Initial Catalog = TRABAJO_DIPLOMA; User ID = " + this.textBox2.Text + " ; Password = " + this.textBox3.Text;

                    TrabajoDeCampo.Properties.Settings.Default.ConnectionString = Convert.ToBase64String(Encoding.UTF8.GetBytes(connectionString));
                    TrabajoDeCampo.Properties.Settings.Default.MasterString =     Convert.ToBase64String(Encoding.UTF8.GetBytes(connectionString.Replace("TRABAJO_DIPLOMA", "master"))); 
                    ServicioSeguridad servicioSeguridad = new ServicioSeguridad();

                    Boolean  conecto = servicioSeguridad.probarConexion();
                    if(!conecto){
                        TrabajoDeCampo.Properties.Settings.Default.ConnectionString = prevConnectionString;
                        MessageBox.Show("No se pudo establecer una conexión con la base de datos");
                    }
                    else
                    {
                        MessageBox.Show("Conexión exitosa");
                        servicioSeguridad.grabarBitacora(null, "Se regeneró el string de conexión", CriticidadEnum.ALTA);
                        Login login = new Login();

                        login.ShowDialog();
                        this.Close();
                        
                    }
                }
                else
                {

                    String prevConnectionString = TrabajoDeCampo.Properties.Settings.Default.ConnectionString; // encriptado

                    String cs = "Data Source = " + this.textBox1.Text + " ; Initial Catalog = TRABAJO_DIPLOMA ; Integrated Security = True";
                    String mcs = "Data Source = " + this.textBox1.Text + " ; Initial Catalog = master ; Integrated Security = True";
                    TrabajoDeCampo.Properties.Settings.Default.ConnectionString = Convert.ToBase64String(Encoding.UTF8.GetBytes(cs));
                    TrabajoDeCampo.Properties.Settings.Default.MasterString = Convert.ToBase64String(Encoding.UTF8.GetBytes(mcs));

                    ServicioSeguridad servicioSeguridad = new ServicioSeguridad();

                    Boolean conecto = servicioSeguridad.probarConexion();
                    if (!conecto)
                    {
                        TrabajoDeCampo.Properties.Settings.Default.ConnectionString = prevConnectionString;
                        MessageBox.Show("No se pudo establecer una conexión con la base de datos");
                    }
                    else
                    {
                        MessageBox.Show("Conexión exitosa");
                        Login login = new Login();
                        servicioSeguridad.grabarBitacora(null, "Se regeneró el string de conexión", CriticidadEnum.ALTA);

                        login.ShowDialog();
                        this.Close();

                    }

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
