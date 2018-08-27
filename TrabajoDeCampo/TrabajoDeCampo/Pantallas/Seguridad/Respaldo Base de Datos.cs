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
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Server;
using System.Windows.Forms;
using TrabajoDeCampo.DAO;
using Microsoft.SqlServer.Management.Common;
using System.Data.SqlClient;

namespace TrabajoDeCampo.Pantallas.Seguridad
{
    public partial class Respaldo_Base_de_Datos : Form
    {
        public Respaldo_Base_de_Datos()
        {
            InitializeComponent();
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
            Backup backup = new Backup();
            backup.Action = BackupActionType.Database;
            backup.BackupSetDescription = "haciendo un backup de la base de datos";
            backup.BackupSetName = "BackupBaseDeDatos" + System.DateTime.Now.ToLongDateString();
            backup.Database = "TRABAJO_DIPLOMA";
            string path = this.pathtxt.Text + "\\bk1.bak";
            FileStream stream = File.Create(path);
            stream.Close();
            BackupDeviceItem deviceItem = new BackupDeviceItem(path, DeviceType.File);
            backup.Devices.Add(deviceItem);
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            ServerConnection Serverconnection = new ServerConnection(connection);
            Server server = new Server(Serverconnection);
            
            backup.SqlBackup(server);
            


        }
    }
}
