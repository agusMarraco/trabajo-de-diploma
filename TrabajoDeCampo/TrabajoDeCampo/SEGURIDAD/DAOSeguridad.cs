using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
using TrabajoDeCampo.BO;

namespace TrabajoDeCampo.DAO
{
    public class DAOSeguridad
    {
        private String[] _permisosClave;

        public String[] permisosClave
        {
            get { return _permisosClave; }
            set { _permisosClave = value; }
        }

        //LOGIN
        public void loguear(String user, String pass, String codigoIdioma) { }

        public Boolean chequearSistemaBloqueado() { return true; }

        public Boolean probarConexion() { return true; }

        //FAMILIA PATENTE
        public List<ComponentePermiso> listarFamiliasYPatentes() { return null; }

        public void actualizarFamiliaPantente(List<Patente> pantentes,Familia familia) { }

        public Boolean tienePatente(long idUsuario, String codigoPantente) { return true; }

        public void actualizarPermisosUsuario(Usuario usuario, List<ComponentePermiso> familiasYPatentes) { }

        public Boolean verificarPermisosEsenciales(long idUsuario) { return true; } //al cambiar permisos chequear que quede un usuario distinto a este con permisos clave.

        public Boolean chequearNegada(long idUsuario, String codigoPatente) { return true; }

        public void crearFamilia(Familia familia) { }

        public void modificarFamilia(Familia familia) { }

        public void borrarFamilia(long IdFamilia) { }

        public Familia buscarFamilia(long idFamilia) { return null; }

        public List<Familia> listarFamilias() { return null; }
        public List<Patente> listarPatentes() { return null; }
        public Boolean chequearFamiliaDesasignada() { return true; }
        //USUARIOS

        public void cambiarContraseña(long idUsuario, String contraseñaNueva) { }

        public void cambiarIdioma(long idUsuario, String codigoIdioma) { }

        public Boolean chequearCamposUnicos(Usuario usuario) { return false; } // para chequear que no se repitan dni mail alias 

        public String regenerarContraseña(long idUsuario) { return ""; }
        public Usuario buscarUsuario(long idUsuario) { return null; }
        public void bloquearUsuario(long idUsuario) { }
        public void desbloquearUsuario(long idUsuario) { }

        public void crearUsuario(Usuario usuario) { }

        public void modificarUsuario(Usuario usuario) { }

        public void borrarUsuario(Usuario usuario) { }

        public List<Usuario> listarUsuarios(String filtro, String valor, String orden)
        {
            return null;
        }


        //BITACORA
        //criticidad 1 baja; 2 media; 3 alta;
        public void grabarBitacora(Usuario usuario, String mensaje, int criticidad) { }

        public void listarBitacora(String filtro, String valor, String orden) { }

        //DIGITOS VERIFICADORES

        public void verificarDigitosVerificadores() { }

        public void recalcularDigitosVerificadores() { }

        public void recalcularDigitoHorizontal(String[] campos) { }

        public void recalcularDigitoVertical(String tabla) { }

        //BACKUPS

        public void realizarBackup(int partes, String directorio) {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            StringBuilder queryText = new StringBuilder();
            
            queryText.Append(" USE MASTER ");
            queryText.Append(" BACKUP DATABASE TRABAJO_DIPLOMA TO DISK = '" + directorio + "\\tempBackup.bak' WITH init");
            SqlCommand query = new SqlCommand(queryText.ToString(), connection);
            try
            {
                query.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e )
            {

                connection.Close();
                throw e;
            }  
        }

        public void realizarRestore(String directorio) {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            StringBuilder queryText = new StringBuilder();
            queryText.Append(" USE MASTER ");
            queryText.Append(" RESTORE DATABASE TRABAJO_DIPLOMA FROM  DISK = '"+ directorio +"' WITH REPLACE");
            SqlCommand query = new SqlCommand(queryText.ToString(), connection);
            try
            {
                query.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {

                connection.Close();
                throw e;
            }
        }
       
        
        //IDIOMA
        public Dictionary<String, String> traerTraducciones(List<String> codigosMensajes, String codigoIdioma)
        {
            Dictionary<String, String> traducciones = new Dictionary<string, string>();

            SqlConnection connection = ConexionSingleton.obtenerConexion();

            StringBuilder sb = new StringBuilder();
            connection.Open();
            foreach (String tag in codigosMensajes)
            {
                sb.Clear();
                sb.Append(" SELECT MSJ.MSJ_TEXTO FROM MENSAJE MSJ INNER JOIN IDIOMA IDI ON IDI.IDI_ID = MSJ.MSJ_IDIOMA_ID ");
                sb.Append(" WHERE MSJ.MSJ_CODIGO = @CODIGO AND IDI.IDI_CODIGO = @LANGCODE");
                //sb.Append("select * from IDIOMA");
                SqlCommand query = new SqlCommand(sb.ToString(), connection);
                SqlDataAdapter adapter = new SqlDataAdapter(query);
                
                query.CommandType = System.Data.CommandType.Text;                
                query.Parameters.Add(new SqlParameter("@CODIGO",System.Data.SqlDbType.VarChar)).Value = tag;
                query.Parameters.Add(new SqlParameter("@LANGCODE", System.Data.SqlDbType.VarChar)).Value = codigoIdioma;

                try
                {
                    SqlDataReader reader = query.ExecuteReader();
                    DataSet ds = new DataSet();
                    //adapter.Fill(ds);
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        { 
                            if(!traducciones.ContainsKey(tag))
                            traducciones.Add(tag, reader.GetValue(0).ToString());
                        }

                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    throw ex;
                }
                
            }

            connection.Close();



            return traducciones;
        }

        public void actualizaConexión()
        {
            throw new System.NotImplementedException();
        }
    }
}
