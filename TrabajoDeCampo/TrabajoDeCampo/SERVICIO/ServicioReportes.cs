using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoDeCampo.DAO;
using System.IO;
using System.Data;

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


        public DataSet ejecutarReporte<T>(String nombreDelReporte, List<T> objetos) {
            if (nombreDelReporte.Equals("materias"))
            {
                DataSet set = new DataSet();
                int it = 1;
                foreach (var item in objetos)
                {
                    DataTable dataTable = new DataTable();
                    dataTable.TableName = "PROGRAMA" + ++it;
                    dataTable.Columns.Add(new DataColumn()
                    {
                        ColumnName = "nombre",
                        DataType = typeof(String),
                        

                    });
                    dataTable.Columns.Add(new DataColumn()
                    {
                        ColumnName = "tipo",
                        DataType = typeof(String)
                    });
                    dataTable.Columns.Add(new DataColumn()
                    {
                        ColumnName = "descripcion",
                        DataType = typeof(String)
                    });

                    Nivel nivel = item as Nivel;

                    foreach (Materia materia in nivel.materia)
                    {
                        DataRow row = dataTable.NewRow();
                        row.BeginEdit();
                        row.SetField(0, materia.nombre);
                        row.SetField(1, materia.tipo);
                        row.SetField(2, materia.descripcion);
                        row.EndEdit();
                        dataTable.Rows.Add(row);
                    }
                    set.Tables.Add(dataTable);
                   
                }
                return set;
            }
            return null;
        }
    }
}
