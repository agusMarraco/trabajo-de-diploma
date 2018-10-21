using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using Microsoft.SqlServer.Server;
using System.Windows.Forms;
using TrabajoDeCampo.DAO;

using System.Data.SqlClient;
using TrabajoDeCampo.SERVICIO;
using TrabajoDeCampo.SEGURIDAD;

namespace TrabajoDeCampo.Pantallas.Seguridad
{
    public partial class Respaldo_Base_de_Datos : Form
    {
        private ServicioSeguridad servicioSeguridad;
        private Dictionary<string, string> traducciones;

        public Respaldo_Base_de_Datos()
        {
            this.servicioSeguridad = new ServicioSeguridad();
            InitializeComponent();
            int[] ints = { 1,2,3,4,5,6,7,8,9,10};
            this.partesCB.DataSource = ints;

        }


        
        private void btnExaminar_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult resultado = dialog.ShowDialog();
            if( resultado == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath)){
                this.pathtxt.Text = dialog.SelectedPath;
            }
        }

        private void btnRespaldar_Click(object sender, EventArgs e)
        {


            if (String.IsNullOrEmpty(this.pathtxt.Text))
            {
                MessageBox.Show(traducciones["com.td.complete.campos"]);
                return;
            }
            String path = this.pathtxt.Text;
            
            if (!String.IsNullOrEmpty(path)){
                try
                {
                    servicioSeguridad.realizarBackup((int)this.partesCB.SelectedItem, path);
                    MessageBox.Show(traducciones["com.td.completado"]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
               
            }
            else
            {
                MessageBox.Show(traducciones["com.td.path"]);
            }

        }

        private void Respaldo_Base_de_Datos_Load(object sender, EventArgs e)
        {
            this.helpProvider1.SetHelpKeyword(this, Properties.Settings.Default.Idioma.Equals("es") ? "Backup.htm" : "BackupEN.htm");
            this.helpProvider1.HelpNamespace = Application.StartupPath + @"\\DocumentsDeAyuda.chm";
            //traduccion
            FormUtils traductor = new TraductorIterador();
            List<String> tags = new List<string>();
            long id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            traductor.process(tags, this, null, null);
            tags.Add("com.td.complete.campos");
            tags.Add("com.td.completado");
            tags.Add("com.td.path");
            traducciones = servicioSeguridad.traerTraducciones(tags, Properties.Settings.Default.Idioma);
            traductor = new TraductorReal();
            traductor.process(null, this, traducciones, null);
            traductor = new TraductorIterador();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
