﻿using System;
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
using TrabajoDeCampo.SERVICIO;

namespace TrabajoDeCampo.Pantallas.Seguridad
{
    public partial class Respaldo_Base_de_Datos : Form
    {
        private ServicioSeguridad servicioSeguridad;

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



            String path = this.pathtxt.Text;
            
            if (!String.IsNullOrEmpty(path)){
                try
                {
                    servicioSeguridad.realizarBackup((int)this.partesCB.SelectedItem, path);
                }
                catch (Exception ex)
                {

                    MessageBox.Show("String generico de mensaje de error , la excepcion es :" + ex.Message);
                }
                MessageBox.Show("Completado papu");
            }
            else
            {
                MessageBox.Show("selecciona algo papu");
            }

        }

        private void Respaldo_Base_de_Datos_Load(object sender, EventArgs e)
        {
            
        }
    }
}
