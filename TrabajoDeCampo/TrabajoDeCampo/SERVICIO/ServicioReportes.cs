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
using TrabajoDeCampo.SEGURIDAD;

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

            //traduccion
            List<String> tags = new List<string>();
            tags.AddRange(new string[] {
                "com.td.justificada","com.td.fecha","com.td.tipo","com.td.motivo","com.td.descripción","com.td.faltas.totales","com.td.si","com.td.no",
                "com.td.módulo", "com.td.criticidad",
                "com.td.criticidad.alta", "com.td.criticidad.media", "com.td.criticidad.baja",
                "com.td.plan.estudios","com.td.cursos","com.td.amonestaciones","com.td.inasistencias","com.td.horarios","com.td.bitácora",
                "com.td.lunes","com.td.martes","com.td.miercoles","com.td.jueves","com.td.viernes","com.td.desde","com.td.hasta",
                "com.td.mensaje","com.td.nivel","com.td.excedido","com.td.disclaimer","com.td.maximo","com.td.actual",
                "com.td.d.n.i.", "com.td.alias", "com.td.apellido", "com.td.nombre"});
            Dictionary<string,string> traduccionesDeBase = new ServicioSeguridad().traerTraducciones(tags, Properties.Settings.Default.Idioma);
      


            Traducciones traducciones = new Traducciones();

            DataRow row = traducciones.DataTable1.NewDataTable1Row();
            row.SetField("justificadaLbl", traduccionesDeBase["com.td.justificada"]);
            row.SetField("fechaLbl", traduccionesDeBase["com.td.fecha"]);
            row.SetField("tipoLbl", traduccionesDeBase["com.td.tipo"]);
            row.SetField("motivoLbl", traduccionesDeBase["com.td.motivo"]);
            row.SetField("apellidoLbl", traduccionesDeBase["com.td.apellido"]);
            row.SetField("nombreLbl", traduccionesDeBase["com.td.nombre"]);
            row.SetField("dniLbl", traduccionesDeBase["com.td.d.n.i."]);
            row.SetField("faltasTotalesLbl", traduccionesDeBase["com.td.faltas.totales"]);
            row.SetField("descripcionLbl", traduccionesDeBase["com.td.descripción"]);
            row.SetField("siLbl", traduccionesDeBase["com.td.si"]);
            row.SetField("noLbl", traduccionesDeBase["com.td.no"]);
            row.SetField("moduloLbl", traduccionesDeBase["com.td.módulo"]);
            row.SetField("lunesLbl", traduccionesDeBase["com.td.lunes"]);
            row.SetField("martesLbl", traduccionesDeBase["com.td.martes"]);
            row.SetField("miercolesLbl", traduccionesDeBase["com.td.miercoles"]);
            row.SetField("juevesLbl", traduccionesDeBase["com.td.jueves"]);
            row.SetField("viernesLbl", traduccionesDeBase["com.td.viernes"]);
            row.SetField("desdeLbl", traduccionesDeBase["com.td.desde"]);
            row.SetField("hastaLbl", traduccionesDeBase["com.td.hasta"]);
            row.SetField("criticidadLbl", traduccionesDeBase["com.td.criticidad"]);
            row.SetField("usuarioLbl", traduccionesDeBase["com.td.alias"]);
            row.SetField("mensajeLbl", traduccionesDeBase["com.td.mensaje"]);
            row.SetField("nivelLbl", traduccionesDeBase["com.td.nivel"]);
            row.SetField("excedidoLbl", false);
            row.SetField("disclaimerLbl", traduccionesDeBase["com.td.disclaimer"]);
            row.SetField("maximoLbl", traduccionesDeBase["com.td.maximo"]);
            row.SetField("actualLbl", traduccionesDeBase["com.td.actual"]);
            row.SetField("criticidadAltaLbl", traduccionesDeBase["com.td.criticidad.alta"]);
            row.SetField("criticidadMediaLbl", traduccionesDeBase["com.td.criticidad.media"]);
            row.SetField("criticidadBajaLbl", traduccionesDeBase["com.td.criticidad.baja"]);
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
                rowColegio.SetField("nombreReporte", traduccionesDeBase["com.td.plan.estudios"]);
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
                rowColegio.SetField("nombreReporte", traduccionesDeBase["com.td.inasistencias"]);
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
                rowColegio.SetField("nombreReporte", traduccionesDeBase["com.td.horarios"]);
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
                rowColegio.SetField("nombreReporte", traduccionesDeBase["com.td.bitácora"]);
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
                
                rowColegio.SetField("nombreReporte", traduccionesDeBase["com.td.amonestaciones"]);
                rowColegio.SetField("totalFaltas", 0.0);
                reporte.nivel = reporte.alumno.curso.nivel;
                reporte.ShowDialog();
            }

            else if (nombreDelReporte.Equals("ReporteCursos"))
            {
                ReporteCursos reporte = tipo as ReporteCursos;
                List<Curso> cursos = objetos as List<Curso>;
                reporte.info = colegio;
                reporte.traducciones = traducciones;
                List<Nivel> niveles = new List<Nivel>();
                foreach (Curso item in cursos)
                {
                    if(!niveles.Any( niv => niv.id == item.nivel.id))
                        niveles.Add(item.nivel);
                }
                reporte.niveles = niveles;
                reporte.cursos = cursos;
                rowColegio.SetField("nombreReporte", traduccionesDeBase["com.td.cursos"]);
                rowColegio.SetField("totalFaltas", 0.0);
                reporte.ShowDialog();
            }

        }
    }
}
