using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace TrabajoDeCampo.DAO
{
    public class ConexionSingleton
    {

        private static SqlConnection _conexion;

        private static SqlConnection constructor() {
            return new SqlConnection (TrabajoDeCampo.Properties.Settings.Default.ConnectionString);
        }

        public static SqlConnection obtenerConexion()
        {
            if(_conexion == null)
            {
                _conexion = constructor();
            }
            return _conexion;
        }
        



    }
}
