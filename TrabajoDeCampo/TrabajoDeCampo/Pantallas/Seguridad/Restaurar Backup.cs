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
using TrabajoDeCampo.SERVICIO;
namespace TrabajoDeCampo.Pantallas.Seguridad
{
    public partial class Restaurar_Backup : Form
    {
        String usersFilePath = null;

        private ServicioSeguridad servicioSeguridad;
        public Restaurar_Backup()
        {
            InitializeComponent();
            servicioSeguridad = new ServicioSeguridad();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if(usersFilePath != null){


                servicioSeguridad.realizarRestore(usersFilePath);
                //Restore restore = new Restore();
                //restore.NoRecovery = false;
                //SqlConnection connection = ConexionSingleton.obtenerConexion();

                //BackupDeviceItem restoreItem = new BackupDeviceItem(usersFilePath, DeviceType.File);
                //restore.Devices.Add(restoreItem);
                //restore.Database = "TRABAJO_DIPLOMA";
                //restore.SqlRestore(new Server());

                MessageBox.Show("Completado papu");

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "BackupFiles|*.bak";

            DialogResult result = dialog.ShowDialog();
            if(result == DialogResult.OK){
                usersFilePath = dialog.FileName;
            }
        }

        private void Restaurar_Backup_Load(object sender, EventArgs e)
        {
            
        }
    }
}
