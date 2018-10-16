using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoDeCampo.Properties.DataSources;

namespace TrabajoDeCampo.Pantallas.Reports
{
    public partial class ReporteHorarios : Form
    {
        public ReporteHorarios()
        {
            InitializeComponent();
        }

        private DataTable _horarios;

        public DataTable horarios
        {
            get { return _horarios; }
            set { _horarios = value; }
        }

        private DataSet _info;

        public DataSet info
        {
            get { return _info; }
            set { _info = value; }
        }

        private DataSet _traducciones;

        public DataSet traducciones
        {
            get { return _traducciones; }
            set { _traducciones = value; }
        }



        private void ReporteHorarios_Load(object sender, EventArgs e)
        {
            String code = TrabajoDeCampo.Properties.Settings.Default.Idioma;
            this.Text = (code.Equals("es")) ? "Reporte" : "Report";


            this.reportViewer1.Reset();
            ReportDataSource source = new ReportDataSource("DataSet1", this.horarios);
            ReportDataSource source2 = new ReportDataSource("DataSet2", this.info.Tables[0]);
            ReportDataSource source3 = new ReportDataSource("DataSet3", this.traducciones.Tables[0]);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\\Pantallas\\Reports\\Horarios.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(source);
            this.reportViewer1.LocalReport.DataSources.Add(source2);
            this.reportViewer1.LocalReport.DataSources.Add(source3);
            this.reportViewer1.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;

            this.reportViewer1.RefreshReport();
        }

        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {




        }
    }
}
