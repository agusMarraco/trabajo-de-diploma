using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoDeCampo.BO;
namespace TrabajoDeCampo.DAO
{
    public class DAODocentes
    {

        
        public void guardarDocente(Docente docente) { }
        public void modificarDocente(Docente docente) { }
        public void borrarDocente(Docente docente) { }

        public Boolean estaDesasignado(long idDocente) { return true; }//chequea que no tenga horarios a cargo antes de borrarlo

        public Boolean verificarDNI(String dni) { return true; }
        public List<Docente> listarDocentes(String filtro, String valor, String orden) {
            SqlDataReader reader = null;

            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();

            SqlCommand cmd = new SqlCommand(" SELECT * FROM DOCENTE ",connection,tx);
            List<Docente> docentes = new List<Docente>();
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Docente docente = new Docente();
                    docente.legajo = (long)reader.GetValue(0);
                    docente.apellido = reader.GetValue(1).ToString();
                    docente.nombre = reader.GetValue(2).ToString();
                    docentes.Add(docente);
                }
                reader.Close();
                tx.Commit();
                connection.Close();
            }
            catch (Exception ex)
            {
                reader.Close();
                tx.Rollback();
                connection.Close();
                throw ex;
            }
            return docentes;
        }

        public List<InasistenciaDocente>listarInasistenciasPorDocente(Docente docente) { return null; }

        public void guardarInasistencia(InasistenciaDocente inasistencia) { }

        public void modificarInasistencia(InasistenciaDocente inasistencia) { }

    }
}
