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
using TrabajoDeCampo.SERVICIO;

namespace TrabajoDeCampo.Pantallas.Seguridad
{
    public partial class FalloConexión : Form
    {
        private Regex regex = new Regex("([A-Za-z0-9\\.-])");
        private Boolean valido = false;
        public FalloConexión()
        {
            InitializeComponent();
            this.textBox1.KeyPress += validateKP;
            this.textBox1.KeyDown += validateKD;
            this.textBox2.KeyPress += validateKP;
            this.textBox2.KeyDown += validateKD;
            this.textBox3.KeyPress += validateKP;
            this.textBox3.KeyDown += validateKD;

        }

        private void validateKD(object sender, KeyEventArgs e)
        {
            valido = true;
            if (!e.KeyValue.Equals(8))//tecla borrar
            {
                if (!regex.IsMatch(e.KeyData.ToString()) || (e.KeyData.ToString().Contains("Oem") && !e.KeyData.ToString().Contains("OemMinus")))
                {
                    valido = false;
                }

            }
        }
        private void validateKP(object sender, KeyPressEventArgs e)
        {
            if (!valido)
            {
                e.Handled = true;
            }
        }

        private void FalloConexión_Load(object sender, EventArgs e)
        {
            
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
                    String prevConnectionString = TrabajoDeCampo.Properties.Settings.Default.ConnectionString;
                    String connectionString = "Data Source = "  + this.textBox1.Text + "; Initial Catalog = TRABAJO_DIPLOMA; User ID = " + this.textBox2.Text + " ; Password = " + this.textBox3.Text;
                    TrabajoDeCampo.Properties.Settings.Default.ConnectionString = connectionString;
                    TrabajoDeCampo.Properties.Settings.Default.MasterString = connectionString.Replace("TRABAJO_DIPLOMA","master");
                    ServicioSeguridad servicioSeguridad = new ServicioSeguridad();

                    Boolean  conecto = servicioSeguridad.probarConexion();
                    if(!conecto){
                        TrabajoDeCampo.Properties.Settings.Default.ConnectionString = prevConnectionString;
                        MessageBox.Show("No se pudo establecer una conexión con la base de datos");
                    }
                    else
                    {
                        MessageBox.Show("Conexión exitosa");
                        Login login = new Login();

                        login.ShowDialog();
                        this.Close();
                        
                    }
                }
                else
                {

                    String prevConnectionString = TrabajoDeCampo.Properties.Settings.Default.ConnectionString;
                    
                    TrabajoDeCampo.Properties.Settings.Default.ConnectionString = "Data Source = " + this.textBox1.Text + " ; Initial Catalog = TRABAJO_DIPLOMA ; Integrated Security = True";
                    TrabajoDeCampo.Properties.Settings.Default.MasterString = "Data Source = " + this.textBox1.Text + " ; Initial Catalog = master ; Integrated Security = True";

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
