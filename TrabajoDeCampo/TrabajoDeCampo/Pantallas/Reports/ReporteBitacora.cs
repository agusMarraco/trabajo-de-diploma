using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoDeCampo.Properties.DataSources;

namespace TrabajoDeCampo.Pantallas.Reports
{
    public partial class ReporteBitacora : Form
    {
        public ReporteBitacora()
        {
            InitializeComponent();
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

        private String _desde;

        public String desde
        {
            get { return _desde; }
            set { _desde = value; }
        }

        private String _hasta;

        public String hasta
        {
            get { return _hasta; }
            set { _hasta = value; }
        }


        private DataTable _bitacora;

        public DataTable bitacora
        {
            get { return _bitacora; }
            set { _bitacora = value; }
        }




        private void ReporteBitacora_Load(object sender, EventArgs e)
        {

            String code = TrabajoDeCampo.Properties.Settings.Default.Idioma;
            this.Text = (code.Equals("es")) ? "Reporte" : "Report";
            this.reportViewer1.Reset();
            ReportDataSource source = new ReportDataSource("DataSet1", this.bitacora);
            DataTable dt = this.traducciones.Tables[0];
            ReportDataSource source2 = new ReportDataSource("DataSet2", dt);
            ReportDataSource source3 = new ReportDataSource("DataSet3", (this.info as InfoColegio).DataTable1 as DataTable);

            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\\Pantallas\\Reports\\Bitacora.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(source);
            this.reportViewer1.LocalReport.DataSources.Add(source2);
            this.reportViewer1.LocalReport.DataSources.Add(source3);
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("desde", desde));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("hasta", hasta));
            this.reportViewer1.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;


            this.reportViewer1.RefreshReport();
            PageSettings set = this.reportViewer1.GetPageSettings();
            set.Landscape = true;
            set.Margins = new Margins(100, 0, 100, 100);
            set.PaperSize.RawKind = (int)PaperKind.A4;
            this.reportViewer1.SetPageSettings(set);
            this.reportViewer1.RefreshReport();

        }

    
        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
        }
    }
}
