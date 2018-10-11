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

namespace TrabajoDeCampo.Pantallas.Reports
{
    public partial class ReportePlanDeEstudios : Form
    {
        private DataSet niveles;
        private List<Nivel> lista;
        public ReportePlanDeEstudios()
        {
            InitializeComponent();
        }
        public ReportePlanDeEstudios(DataSet  niveles, List<Nivel> nivels)
        {
            InitializeComponent();
            this.niveles = niveles;
            this.lista = nivels;
        }
        private void ReportePlanDeEstudios_Load(object sender, EventArgs e)
        {

            this.reportViewer1.Reset();
            ReportDataSource source = new ReportDataSource("DataSet1", this.lista);
          
            
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.ReportPath = @"C:\\Users\\Agustin\\Documents\\Trabajo-De-Campo\trabajo-de-diploma\\TrabajoDeCampo\\TrabajoDeCampo\\Pantallas\\Reports\\PlanDeEstudios.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(source);
            this.reportViewer1.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("nombreDelColegio","Mariano Moreno"));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("nombreDelReporte", "Programa"));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("fecha", DateTime.Now.ToShortDateString()));
            this.reportViewer1.RefreshReport();
            
            
          
            

        }

        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            String pepe = "pepe";
            int nivelId = int.Parse(e.Parameters["nivelId"].Values[0].ToString());
            Nivel nivel = this.lista.Where(x => x.id == nivelId).First();
            ReportDataSource source = new ReportDataSource("DataSet1", nivel.materia);
            e.DataSources.Clear();
            e.DataSources.Add(source);
        }
    }
}
