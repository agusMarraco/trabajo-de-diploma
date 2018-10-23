using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using TrabajoDeCampo.DAO;
using TrabajoDeCampo.SERVICIO;
using TrabajoDeCampo.SEGURIDAD;

namespace TrabajoDeCampo.Pantallas.Seguridad
{
    public partial class Restaurar_Backup : Form
    {
        String usersFilePath = null;

        private ServicioSeguridad servicioSeguridad;
        private Dictionary<string, string> traducciones;

        public Restaurar_Backup()
        {
            InitializeComponent();
            servicioSeguridad = new ServicioSeguridad();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(traducciones["com.td.seguro"], "", MessageBoxButtons.OKCancel);
            if (!result.Equals(DialogResult.OK))
            {
                return;
            }

            if (String.IsNullOrEmpty(this.usersFilePath))
            {
                MessageBox.Show(traducciones["com.td.complete.campos"]);
                return;
            }
            if (usersFilePath != null){

                try
                {
                    servicioSeguridad.realizarRestore(usersFilePath);
                    MessageBox.Show(traducciones["com.td.completado"]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
               

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "BackupFiles|*.bak";

            DialogResult result = dialog.ShowDialog();
            if(result == DialogResult.OK){
                usersFilePath = dialog.FileName;
                this.textBox1.Text = usersFilePath;
            }
        }

        private void Restaurar_Backup_Load(object sender, EventArgs e)
        {
            this.helpProvider1.SetHelpKeyword(this, Properties.Settings.Default.Idioma.Equals("es") ? "Backup.htm" : "BackupEN.htm");
            this.helpProvider1.HelpNamespace = Application.StartupPath + @"\\DocumentsDeAyuda.chm";
            this.textBox1.ReadOnly = true;

            //traduccion
            FormUtils traductor = new TraductorIterador();
            List<String> tags = new List<string>();
            long id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            traductor.process(tags, this, null, null);
            tags.Add("com.td.complete.campos");
            tags.Add("com.td.completado");
            tags.Add("com.td.seguro");
            traducciones = servicioSeguridad.traerTraducciones(tags, Properties.Settings.Default.Idioma);
            traductor = new TraductorReal();
            traductor.process(null, this, traducciones, null);
            traductor = new TraductorIterador();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
