using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabajoDeCampo.Pantallas.Reports
{
    public partial class ReporteInasistencias : Form
    {
        private DataSet source = null;
        public ReporteInasistencias(DataSet data)
        {
            InitializeComponent();
            this.source = data;
        }

        private void ReporteInasistencias_Load(object sender, EventArgs e)
        {
            
            this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dataset",new DataTable()));
            this.reportViewer1.RefreshReport();
        }
    }
}
