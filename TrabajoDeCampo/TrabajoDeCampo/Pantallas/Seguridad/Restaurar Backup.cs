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
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Data.SqlClient;
using TrabajoDeCampo.DAO;

namespace TrabajoDeCampo.Pantallas.Seguridad
{
    public partial class Restaurar_Backup : Form
    {
        String usersFilePath = null;
        public Restaurar_Backup()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(usersFilePath != null){
                Restore restore = new Restore();
                restore.NoRecovery = false;
                SqlConnection connection = ConexionSingleton.obtenerConexion();
        
                BackupDeviceItem restoreItem = new BackupDeviceItem(usersFilePath, DeviceType.File);
                restore.Devices.Add(restoreItem);
                restore.Database = "TRABAJO_DIPLOMA";
                restore.SqlRestore(new Server());
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "BackupFiles|*.bak|All Files|*";

            DialogResult result = dialog.ShowDialog();
            if(result == DialogResult.OK){
                usersFilePath = dialog.FileName;
            }
        }
    }
}
