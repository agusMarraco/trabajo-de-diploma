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
    public partial class ReporteCursos : Form
    {
        public ReporteCursos()
        {
            InitializeComponent();
        }

        private DataSet _traducciones;

        public DataSet traducciones
        {
            get { return _traducciones; }
            set { _traducciones = value; }
        }

        private DataSet _info;

        public DataSet info
        {
            get { return _info; }
            set { _info = value; }
        }

        private List<Nivel> _niveles;

        public List<Nivel> niveles
        {
            get { return _niveles; }
            set { _niveles = value; }
        }

        private List<Curso> _cursos;

        public List<Curso> cursos
        {
            get { return _cursos; }
            set { _cursos = value; }
        }






        private void ReporteCursos_Load(object sender, EventArgs e)
        {
            String code = TrabajoDeCampo.Properties.Settings.Default.Idioma;
            this.Text = (code.Equals("es")) ? "Reporte" : "Report";


            this.reportViewer1.Reset();
            ReportDataSource source = new ReportDataSource("DataSet1", this.niveles);
            ReportDataSource source2 = new ReportDataSource("DataSet3", this.traducciones.Tables[0]);
            DataTable table2 = (this.info as InfoColegio).DataTable1;
            ReportDataSource source3 = new ReportDataSource("DataSet2", table2);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\\Pantallas\\Reports\\Cursos.rdlc";
            this.reportViewer1.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;
            this.reportViewer1.LocalReport.DataSources.Add(source);
            this.reportViewer1.LocalReport.DataSources.Add(source2);
            this.reportViewer1.LocalReport.DataSources.Add(source3);
            this.reportViewer1.RefreshReport();
            PageSettings set = this.reportViewer1.GetPageSettings();
            
            set.Margins = new Margins(100, 0, 100, 100);
            set.PaperSize.RawKind = (int)PaperKind.A4;
            this.reportViewer1.SetPageSettings(set);
            this.reportViewer1.RefreshReport();


        }

        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            if (e.ReportPath.Equals("DetalleCurso"))
            {
                long idNivel = long.Parse(e.Parameters["nivel"].Values[0].ToString());
                List<Curso> cursosDelNivel = this.cursos.Where( x => x.nivel.id.Equals(idNivel)).ToList();
                DataTable curso = new TrabajoDeCampo.Properties.DataSources.Curso().DataTable1;
                foreach(Curso cur in cursosDelNivel)
                {
                    DataRow row = curso.NewRow();
                    row.SetField("letra", cur.letra);
                    row.SetField("turno", cur.turno);
                    row.SetField("codigo", cur.codigo);
                    row.SetField("total", cur.capacidad);
                    row.SetField("actual", cur.alumnos.Count);
                    row.SetField("id", cur.id);
                    bool excedido =cur.alumnos.Count > cur.capacidad;
                    row.SetField("excedido", excedido);
                    curso.Rows.Add(row);
                }

                e.DataSources.Clear();        
                e.DataSources.Add(new ReportDataSource("DataSet1", curso));
                e.DataSources.Add(new ReportDataSource("DataSet2", this.traducciones.Tables[0]));

            }
            else if (e.ReportPath.Equals("DetalleAlumnoCurso"))
            {
                e.DataSources.Clear();
                long cursoID = long.Parse(e.Parameters["curso"].Values[0].ToString());
                List<Alumno> alumnos = new List<Alumno>();
                foreach (Curso item in this.cursos)
                {
                    if (item.id.Equals(cursoID))
                    {
                        alumnos.AddRange(item.alumnos);
                    }
                }
                e.DataSources.Add(new ReportDataSource("DataSet1", alumnos));
                e.DataSources.Add(new ReportDataSource("DataSet2", this.traducciones.Tables[0]));
            }
        }
    }
}
