using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoDeCampo.Properties.DataSources;

namespace TrabajoDeCampo.Pantallas.Reports
{
    public partial class ReportePlanDeEstudios : Form
    {
        private DataSet _informacion;

        private DataSet _colegio;

        public DataSet colegio
        {
            get { return _colegio; }
            set { _colegio = value; }
        }


        public DataSet informacion
        {
            get { return _informacion; }
            set { _informacion = value; }
        }


        public ReportePlanDeEstudios()
        {
            InitializeComponent();
        }
        public ReportePlanDeEstudios(List<Nivel> nivels)
        {
            InitializeComponent();
            this.lista = nivels;
        }
     

        private List<Nivel> _lista;

        public List<Nivel> lista
        {
            get { return _lista; }
            set { _lista = value; }
        }

        private void ReportePlanDeEstudios_Load(object sender, EventArgs e)
        {
            String code = TrabajoDeCampo.Properties.Settings.Default.Idioma;
            this.Text = (code.Equals("es")) ? "Reporte" : "Report";

            this.reportViewer1.Reset();
            ReportDataSource source = new ReportDataSource("DataSet1", this.lista);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\\Pantallas\\Reports\\PlanDeEstudios.rdlc";
            DataTable table = (this.colegio as InfoColegio).DataTable1;
            ReportDataSource source2 = new ReportDataSource("DataSet2", table);
            
            this.reportViewer1.LocalReport.DataSources.Add(source);
            this.reportViewer1.LocalReport.DataSources.Add(source2);

            this.reportViewer1.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("nombreDelReporte", table.Rows[0].ItemArray[0].ToString()));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("nombreDelColegio",table.Rows[0].ItemArray[1].ToString()));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("fecha", table.Rows[0].ItemArray[2].ToString()));
            this.reportViewer1.RefreshReport();
            
            
          
            

        }

        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {

            if(e.ReportPath != "Header")
            {
                int nivelId = int.Parse(e.Parameters["nivelId"].Values[0].ToString());
                Nivel nivel = this.lista.Where(x => x.id == nivelId).First();
                ReportDataSource source = new ReportDataSource("DataSet1", nivel.materia);
                DataTable table = (this.informacion as Traducciones).DataTable1;
                ReportDataSource source2 = new ReportDataSource("DataSet2", table);
                e.DataSources.Clear();
                e.DataSources.Add(source);
                e.DataSources.Add(source2);
            }

        }
    }
}
