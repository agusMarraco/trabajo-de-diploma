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
            String cs = Encoding.UTF8.GetString(Convert.FromBase64String(TrabajoDeCampo.Properties.Settings.Default.ConnectionString));
            return new SqlConnection (cs);
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
