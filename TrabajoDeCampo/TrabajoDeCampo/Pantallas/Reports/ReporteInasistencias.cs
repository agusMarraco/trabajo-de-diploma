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
    public partial class ReporteInasistencias : Form
    {
        private DataSet source = null;
        private List<InasistenciaAlumno> _inasistencias;

        public List<InasistenciaAlumno> inasistencias
        {
            get { return _inasistencias; }
            set { _inasistencias = value; }
        }

        private Alumno _alumno;

        public Alumno alumno
        {
            get { return _alumno; }
            set { _alumno = value; }
        }

        private Nivel _nivel;

        public Nivel nivel
        {
            get { return _nivel; }
            set { _nivel = value; }
        }

        private Traducciones _traducciones;

        public Traducciones traducciones
        {
            get { return _traducciones; }
            set { _traducciones = value; }
        }


        private InfoColegio _info;

        public InfoColegio info
        {
            get { return _info; }
            set { _info = value; }
        }


        public ReporteInasistencias()
        {
            InitializeComponent();
        }


        public ReporteInasistencias(DataSet data)
        {
            InitializeComponent();
        }

        private void ReporteInasistencias_Load(object sender, EventArgs e)
        {
            String code = TrabajoDeCampo.Properties.Settings.Default.Idioma;
            this.Text = (code.Equals("es")) ? "Reporte" : "Report";

            this.reportViewer1.Reset();
            ReportDataSource source = new ReportDataSource("DataSet1", this.inasistencias);
            DataTable dt = this.traducciones.DataTable1;
            ReportDataSource source2 = new ReportDataSource("DataSet2", dt);
            DataTable table2 = (this.info as InfoColegio).DataTable1;
            ReportDataSource source5 = new ReportDataSource("DataSet3", table2);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\\Pantallas\\Reports\\Inasistencias.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(source);
            this.reportViewer1.LocalReport.DataSources.Add(source2);
            this.reportViewer1.LocalReport.DataSources.Add(source5);
            this.reportViewer1.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;
            this.reportViewer1.RefreshReport();



        }

        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {

            if (e.ReportPath != "Header")
            {
                List<Alumno> list = new List<Alumno>() { this.alumno };
                List<Curso> cursos = new List<Curso>() { this.alumno.curso };
                this.nivel.orientacionCodigo = this.alumno.orientacion.nombre;
                List<Nivel> niveles = new List<Nivel>() { this.nivel};
                ReportDataSource source = new ReportDataSource("DataSet1", list);
                DataTable table = (this.traducciones as Traducciones).DataTable1;
                ReportDataSource source2 = new ReportDataSource("DataSet4", table);
                ReportDataSource source3 = new ReportDataSource("DataSet3", cursos);
                ReportDataSource source4 = new ReportDataSource("DataSet2", niveles);
                DataTable table2 = (this.info as InfoColegio).DataTable1;
                ReportDataSource source5 = new ReportDataSource("DataSet5", table2);
                e.DataSources.Clear();
                e.DataSources.Add(source);
                e.DataSources.Add(source2);
                e.DataSources.Add(source3);
                e.DataSources.Add(source4);
                e.DataSources.Add(source5);
            }
            else
            {
                DataTable table = (this.info as InfoColegio).DataTable1;
                ReportDataSource source = new ReportDataSource("DataSet1", table);
                e.DataSources.Clear();
                e.DataSources.Add((source));
            }


        }
    }
}
