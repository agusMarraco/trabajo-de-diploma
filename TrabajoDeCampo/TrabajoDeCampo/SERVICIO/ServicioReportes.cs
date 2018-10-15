using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoDeCampo.DAO;
using System.IO;
using System.Data;
using TrabajoDeCampo.Pantallas.Reports;
using System.Reflection;
using TrabajoDeCampo.Properties.DataSources;

namespace TrabajoDeCampo.SERVICIO
{
    public class ServicioReportes
    {
        private String[] _reportes;

        public String[] reportes
        {
            get { return _reportes; }
            set { _reportes = value; }
        }

        private DAOReportes _daoReportes;

        public DAOReportes daoReportes
        {
            get { return _daoReportes; }
            set { _daoReportes = value; }
        }


        public void ejecutarReporte<T>(String nombreDelReporte, List<T> objetos) {

            String fullname = "TrabajoDeCampo.Pantallas.Reports." + nombreDelReporte;
            object tipo = Type.GetType(fullname).GetConstructor(new Type[0]).Invoke(null);

            Traducciones traducciones = new Traducciones();

            DataRow row = traducciones.DataTable1.NewDataTable1Row();
            row.SetField("justificadaLbl", "Justificada");
            row.SetField("fechaLbl", "Fecha");
            row.SetField("tipoLbl", "Tipo");
            row.SetField("motivoLbl", "Motivo");
            row.SetField("apellidoLbl", "Apellido");
            row.SetField("nombreLbl", "Nombre");
            row.SetField("dniLbl", "DNI");
            row.SetField("faltasTotalesLbl", "Faltas Totales");
            row.SetField("descripcionLbl", "Decripcion");
            row.SetField("siLbl", "SI");
            row.SetField("noLbl", "NO");
            row.SetField("moduloLbl", "Modulo");
            row.SetField("lunesLbl", "Lunes");
            row.SetField("martesLbl", "Martes");
            row.SetField("miercolesLbl", "Miercoles");
            row.SetField("juevesLbl", "Jueves");
            row.SetField("viernesLbl", "Viernes");
            row.SetField("desdeLbl", "Desde");
            row.SetField("hastaLbl", "Hasta");
            row.SetField("criticidadLbl", "Criticidad");
            row.SetField("usuarioLbl", "Usuario");
            row.SetField("mensajeLbl", "Mensaje");
            traducciones.DataTable1.Rows.Add(row);

            InfoColegio colegio = new InfoColegio();
            DataRow rowColegio = colegio.DataTable1.NewDataTable1Row();
            rowColegio.SetField("nombreColegio", "Mariano Moreno");
          
            rowColegio.SetField("fecha", DateTime.Now.ToShortDateString());

            colegio.DataTable1.Rows.Add(rowColegio);

            if (nombreDelReporte.Equals("ReportePlanDeEstudios"))
            {
                ReportePlanDeEstudios reporte = tipo as ReportePlanDeEstudios;
                reporte.lista = objetos as List<Nivel>;
                reporte.informacion = traducciones;
                rowColegio.SetField("nombreReporte", "Plan de Estudios");
                reporte.colegio = colegio;
                reporte.ShowDialog();

            }
            else if (nombreDelReporte.Equals("ReporteInasistencias"))
            {
                ReporteInasistencias reporte = tipo as ReporteInasistencias;
                reporte.alumno = (objetos as List<Alumno>).First();
                reporte.info = colegio;
                reporte.traducciones = traducciones;
                reporte.inasistencias = reporte.alumno.inasistencias;
                Double faltas = 0.0;
                foreach (InasistenciaAlumno item in reporte.alumno.inasistencias)
                {
                    faltas += item.valor;
                }
                rowColegio.SetField("nombreReporte", "Reporte de Inasistencias");
                rowColegio.SetField("totalFaltas",faltas);
                reporte.nivel = reporte.alumno.curso.nivel;
                reporte.ShowDialog();
            }else if(nombreDelReporte.Equals("ReporteHorarios")){
                DataTable table = (objetos as List<DataTable>).First();
                ReporteHorarios horarios = tipo as ReporteHorarios;

                //pasando a objetos planos

                DataTable datatable = new DataTable("horarios");
                datatable.Columns.Add(new DataColumn()
                {
                    ColumnName = "modulo",
                    DataType = typeof(String)

                });
                datatable.Columns.Add(new DataColumn()
                {
                    ColumnName = "lunes",
                    DataType = typeof(String)
                });
                datatable.Columns.Add(new DataColumn()
                {
                    ColumnName = "martes",
                    DataType = typeof(String)
                });
                datatable.Columns.Add(new DataColumn()
                {
                    ColumnName = "miercoles",
                    DataType = typeof(String)
                });
                datatable.Columns.Add(new DataColumn()
                {
                    ColumnName = "jueves",
                    DataType = typeof(String)
                });
                datatable.Columns.Add(new DataColumn()
                {
                    ColumnName = "viernes",
                    DataType = typeof(String)
                });

                foreach (DataRow rowIter in table.Rows)
                {
                    String modulo       = rowIter.ItemArray[0].ToString();
                    String lunes        = rowIter.ItemArray[1].ToString();
                    String martes       = rowIter.ItemArray[2].ToString();
                    String miercoles    = rowIter.ItemArray[3].ToString();
                    String jueves       = rowIter.ItemArray[4].ToString();
                    String viernes      = rowIter.ItemArray[5].ToString();
                    DataRow temp =datatable.NewRow();
                    temp.SetField("modulo", modulo);
                    temp.SetField("lunes", lunes);
                    temp.SetField("martes", martes);
                    temp.SetField("miercoles", miercoles);
                    temp.SetField("jueves", jueves);
                    temp.SetField("viernes", viernes);
                    datatable.Rows.Add(temp);
                }
                rowColegio.SetField("nombreReporte", "Reporte de Horarios");
                horarios.traducciones = traducciones;
                horarios.info = colegio;
                horarios.horarios = datatable;
                horarios.ShowDialog();


            }else if (nombreDelReporte.Equals("ReporteBitacora"))
            {
                ReporteBitacora bitacora = tipo as ReporteBitacora;
                DataTable setBitacora = new Properties.DataSources.Bitacora().DataTable1;

                DataTable temp = objetos.ElementAt(0) as DataTable;
                foreach (DataRow rowIter in temp.Rows)
                {
                    String fecha = rowIter.ItemArray[0].ToString();
                    String usuario = rowIter.ItemArray[1].ToString();
                    String criticidad = rowIter.ItemArray[2].ToString();
                    String mensaje = rowIter.ItemArray[3].ToString();
                    DataRow tempRow = setBitacora.NewRow();
                    tempRow.SetField("fecha", fecha);
                    tempRow.SetField("criticidad", criticidad);
                    tempRow.SetField("usuario", usuario);
                    tempRow.SetField("mensaje", mensaje);
                    setBitacora.Rows.Add(tempRow);
                }
                bitacora.bitacora = setBitacora;
                bitacora.desde = objetos.ElementAt(1) as String;
                bitacora.hasta = objetos.ElementAt(2) as String;

                bitacora.info = colegio;
                bitacora.traducciones = traducciones;
                rowColegio.SetField("nombreReporte", "Bitácora");
                rowColegio.SetField("totalFaltas", 0);
                bitacora.ShowDialog();


            }
            else if (nombreDelReporte.Equals("ReporteAmonestaciones"))
            {
                ReporteAmonestaciones reporte = tipo as ReporteAmonestaciones;
                reporte.alumno = (objetos as List<Alumno>).First();
                reporte.info = colegio;
                reporte.traducciones = traducciones;
                reporte.amonestaciones = reporte.alumno.amonestaciones;
                rowColegio.SetField("nombreReporte", "Reporte de Amonestaciones");
                rowColegio.SetField("totalFaltas", 0);
                reporte.nivel = reporte.alumno.curso.nivel;
                reporte.ShowDialog();
            }
            
        }
    }
}
