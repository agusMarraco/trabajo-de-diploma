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

        public void actualizarFamiliaPantente(List<Patente> pantentes, Familia familia) { }

        public Boolean tienePatente(long idUsuario, String codigoPantente) { return true; }

        public void actualizarPermisosUsuario(Usuario usuario, List<ComponentePermiso> familiasYPatentes) { }

        public Boolean verificarPermisosEsenciales(long idUsuario) { return true; } //al cambiar permisos chequear que quede un usuario distinto a este con permisos clave.

        public Boolean chequearNegada(long idUsuario, String codigoPatente) { return true; }

        public void crearFamilia(Familia familia) {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            StringBuilder sb = new StringBuilder();
            sb.Append(" INSERT INTO FAMILIA (FAM_NOMBRE, FAM_DVH) OUTPUT Inserted.FAM_ID VALUES(@NOMBRE, 0) ");
            
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand query = new SqlCommand(sb.ToString(), connection, tx);
            query.Parameters.Add(new SqlParameter("@NOMBRE", System.Data.SqlDbType.NVarChar)).Value = SeguridadUtiles.encriptarAES(familia.nombre);
            

            try
            {
                SqlDataReader reader = query.ExecuteReader();
                long id = 0;
                if (reader.Read())
                {
                    id = (long)reader[0];
                }

                if(id != 0)
                {
                    reader.Close();
                    query.CommandText = " INSERT INTO FAMILIA_PATENTE (FP_PATENTE_ID,FP_FAMILIA_ID) VALUES(@PATENTE,@FAMILIA) ";
                    foreach (Patente item in familia.patentes)
                    {
                        query.Parameters.Clear();
                        query.Parameters.Add(new SqlParameter("@PATENTE", System.Data.SqlDbType.BigInt)).Value = item.id;
                        query.Parameters.Add(new SqlParameter("@FAMILIA", System.Data.SqlDbType.BigInt)).Value = id;
                        query.ExecuteNonQuery();
                    }
                }

                tx.Commit();

            }
            catch (Exception ex)
            {

                try
                {
                    tx.Rollback();
                }
                catch (Exception ex2)
                {
                    connection.Close();
                    throw ex2;

                }
            }
            finally
            {
                connection.Close();
            }
            


        }

        public void modificarFamilia(Familia familia) {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            StringBuilder sb = new StringBuilder();
            sb.Append(" UPDATE FAMILIA SET FAM_NOMBRE = @NOMBRE WHERE FAM_ID = @ID ");

            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand query = new SqlCommand(sb.ToString(), connection, tx);
            query.Parameters.Add(new SqlParameter("@NOMBRE", System.Data.SqlDbType.NVarChar)).Value = SeguridadUtiles.encriptarAES(familia.nombre);
            query.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = familia.id;


            try
            {
                query.ExecuteNonQuery();
                query.Parameters.Clear();

                query.CommandText = " DELETE FROM FAMILIA_PATENTE WHERE FP_FAMILIA_ID = @ID ";
                query.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = familia.id;

                query.ExecuteNonQuery();

                foreach (Patente item in familia.patentes)
                {
                    query.Parameters.Clear();

                    query.CommandText = " INSERT INTO FAMILIA_PATENTE (FP_PATENTE_ID,FP_FAMILIA_ID) VALUES (@PATENTE,@FAMILIA) ";
                    query.Parameters.Add(new SqlParameter("@PATENTE", System.Data.SqlDbType.BigInt)).Value = item.id;
                    query.Parameters.Add(new SqlParameter("@FAMILIA", System.Data.SqlDbType.BigInt)).Value = familia.id;
                    query.ExecuteNonQuery();
                }

                tx.Commit();

            }
            catch (Exception ex)
            {

                try
                {
                    tx.Rollback();
                }
                catch (Exception ex2)
                {
                    connection.Close();
                    throw ex2;

                }
            }
            finally
            {
                connection.Close();
            }

        }

        public void borrarFamilia(long IdFamilia) { }

        public Familia buscarFamilia(long idFamilia) { return null; }

        public List<Familia> listarFamilias() {
            
            List<Familia> familias = new List<Familia>();
            SqlConnection connection = ConexionSingleton.obtenerConexion();

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT * FROM FAMILIA WHERE FAM_BLOQUEADA = 0  ");
            SqlCommand query = new SqlCommand("", connection);
            query.CommandText = sb.ToString();

            connection.Open();
            SqlDataReader reader;
            Familia familia;
            try
            {
                reader = query.ExecuteReader();
                while (reader.Read())
                {
                    familia = new Familia();
                    familia.id = (long)reader["FAM_ID"];
                    familia.nombre = SeguridadUtiles.desencriptarAES(reader["FAM_NOMBRE"].ToString());
                    familias.Add(familia);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            connection.Open();

                try
                {
            
                    foreach(Familia fam in familias)
                    {
                            query.Parameters.Clear();
                            query.CommandText = " SELECT PAT_ID,PAT_DESC FROM PATENTE INNER JOIN FAMILIA_PATENTE ON FP_PATENTE_ID = PAT_ID WHERE FP_FAMILIA_ID = @ID";
                            query.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = fam.id;

                            reader = query.ExecuteReader();

                            while (reader.Read())
                            {
                                Patente pat = new Patente();
                                pat.id = (long)reader["PAT_ID"];
                                pat.descripcion = SeguridadUtiles.desencriptarAES(reader["PAT_DESC"].ToString());
                                fam.patentes.Add(pat);
                            }
                             reader.Close();

                        }
                    }
                catch (Exception ex)
                {
                    connection.Close();
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }



            return familias;
        }
        public List<Patente> listarPatentes() {
            List<Patente> patentes = new List<Patente>();
            SqlConnection connection = ConexionSingleton.obtenerConexion();

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT * FROM PATENTE  ");
            SqlCommand query = new SqlCommand("", connection);
            query.CommandText = sb.ToString();

            connection.Open();
            SqlDataReader reader;
            Patente patente;
            try
            {
                reader = query.ExecuteReader();
                while (reader.Read())
                {
                    patente = new Patente();
                    patente.id = (long)reader["PAT_ID"];
                    patente.descripcion = SeguridadUtiles.desencriptarAES(reader["PAT_DESC"].ToString());
                    patentes.Add(patente);
                }
            }
            catch (Exception ex)
            {
                connection.Close();
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            
            return patentes;


        }
        public Boolean chequearFamiliaDesasignada() { return true; }
        //USUARIOS

        public void cambiarContraseña(long idUsuario, String contraseñaNueva) { }

        public void cambiarIdioma(long idUsuario, String codigoIdioma) { }

        public Boolean chequearCamposUnicos(Usuario usuario)// para chequear que no se repitan dni mail alias 
        {
            bool repetido = false;

            SqlConnection connection = ConexionSingleton.obtenerConexion();
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT USU.USU_EMAIL , USU.USU_DNI, USU.USU_ALIAS ,USU.USU_ID FROM USUARIO USU ");
           
            SqlCommand query = new SqlCommand(sb.ToString(), connection);
            SqlDataReader reader = null;
            Usuario tempUsuario;
            List<Usuario> usuarios = new List<Usuario>();


            try
            {
                connection.Open();
                reader = query.ExecuteReader();
                    while (reader.Read())
                    {
                        tempUsuario = new Usuario();
                        tempUsuario.email = reader["USU_EMAIL"].ToString();
                        tempUsuario.dni = reader["USU_DNI"].ToString();
                        tempUsuario.alias = SeguridadUtiles.desencriptarAES(reader["USU_ALIAS"].ToString());
                        tempUsuario.id = long.Parse(reader["USU_ID"].ToString());
                        usuarios.Add(tempUsuario);
                    }
                

                connection.Close();
            }
            catch (Exception exe)
            {
                connection.Close();
                throw exe;
            }
            foreach (Usuario item in usuarios)
            {
                if((item.dni == usuario.dni && item.id != usuario.id) || (item.email == usuario.email && item.id != usuario.id) 
                    || (item.alias == usuario.alias && item.id != usuario.id))
                {
                    repetido = true;
                    break;
                }
            }

            return repetido;
        } 

        public void regenerarContraseña(long idUsuario, string passEncriptado) {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            StringBuilder sb = new StringBuilder();
            sb.Append(" UPDATE USUARIO SET USU_PASS = @PASS WHERE USU_ID = @ID");

            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand query = new SqlCommand(sb.ToString(), connection, tx);
            query.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = idUsuario;
            query.Parameters.Add(new SqlParameter("@PASS", System.Data.SqlDbType.NVarChar)).Value = passEncriptado;

            try
            {
                query.ExecuteNonQuery();
                tx.Commit();

            }
            catch (Exception ex)
            {

                try
                {
                    tx.Rollback();
                }
                catch (Exception ex2)
                {
                    connection.Close();
                    throw ex2;

                }
            }
            finally
            {
                connection.Close();
            }
            desbloquearUsuario(idUsuario);

        }
        public Usuario buscarUsuario(long idUsuario) { return null; }
        public void bloquearUsuario(long idUsuario)
        {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            StringBuilder sb = new StringBuilder();
            sb.Append(" UPDATE USUARIO SET USU_INTENTOS = 3 WHERE USU_ID = @ID");

            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand query = new SqlCommand(sb.ToString(), connection, tx);
            query.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = idUsuario;

            try
            {
                query.ExecuteNonQuery();
                tx.Commit();

            }
            catch (Exception ex)
            {
                
                try
                {
                    tx.Rollback();
                }
                catch (Exception ex2)
                {
                    connection.Close();
                    throw ex2;
                    
                }
            }
            finally
            {
                connection.Close();
            }



        }
        public void desbloquearUsuario(long idUsuario) {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            StringBuilder sb = new StringBuilder();
            sb.Append(" UPDATE USUARIO SET USU_INTENTOS = 0 WHERE USU_ID = @ID");

            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand query = new SqlCommand(sb.ToString(), connection, tx);
            query.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = idUsuario;

            try
            {
                query.ExecuteNonQuery();
                tx.Commit();

            }
            catch (Exception ex)
            {

                try
                {
                    tx.Rollback();
                }
                catch (Exception ex2)
                {
                    connection.Close();
                    throw ex2;

                }
            }
            finally
            {
                connection.Close();
            }

        }

        public void crearUsuario(ref Usuario usuario) {

            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            // query
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into usuario(usu_dni,usu_email,usu_pass,usu_alias,usu_nombre,usu_apellido,usu_direccion,usu_telefono,usu_dvh,usu_idioma) ");
            sb.Append(" values (@dni,@email,@pass,@alias,@nombre,@apellido,@direccion,@telefono,@dvh,@idioma)");
            SqlCommand query = new SqlCommand(sb.ToString(), connection);
            query.Transaction = tx;

            //agregando los paramentros 

            query.Parameters.Add(new SqlParameter("@dni", System.Data.SqlDbType.VarChar)).Value = usuario.dni;
            query.Parameters.Add(new SqlParameter("@email", System.Data.SqlDbType.VarChar)).Value = usuario.email;
            query.Parameters.Add(new SqlParameter("@pass", System.Data.SqlDbType.VarChar)).Value = SeguridadUtiles.encriptarMD5(usuario.pass);
            query.Parameters.Add(new SqlParameter("@alias", System.Data.SqlDbType.NVarChar)).Value = SeguridadUtiles.encriptarAES(usuario.alias);
            query.Parameters.Add(new SqlParameter("@nombre", System.Data.SqlDbType.VarChar)).Value = usuario.nombre;
            query.Parameters.Add(new SqlParameter("@apellido", System.Data.SqlDbType.VarChar)).Value = usuario.apellido;
            query.Parameters.Add(new SqlParameter("@direccion", System.Data.SqlDbType.VarChar)).Value = usuario.direccion;
            query.Parameters.Add(new SqlParameter("@telefono", System.Data.SqlDbType.VarChar)).Value = usuario.telefono;
            query.Parameters.Add(new SqlParameter("@dvh", System.Data.SqlDbType.VarChar)).Value = " ";
            query.Parameters.Add(new SqlParameter("@idioma", System.Data.SqlDbType.BigInt)).Value = usuario.idioma.id;

            try
            {
                
                int resultados = query.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
            }
            catch (Exception exe)
            {
                try
                {
                    tx.Rollback();
                }
                catch (Exception )
                {
                }
                
                connection.Close();
                throw exe;
            }

        }

        public void modificarUsuario(Usuario usuario) { }

        public void borrarUsuario(Usuario usuario)
        {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            StringBuilder sb = new StringBuilder();
            sb.Append(" DELETE FROM USUARIO WHERE USU_ID = @ID");

            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand query = new SqlCommand(sb.ToString(), connection, tx);
            query.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = usuario.id;

            try
            {
                query.ExecuteNonQuery();
                tx.Commit();

            }
            catch (Exception ex)
            {

                try
                {
                    tx.Rollback();
                }
                catch (Exception ex2)
                {
                    connection.Close();
                    throw ex2;

                }
            }
            finally
            {
                connection.Close();
            }

        }

        public List<Usuario> listarUsuarios(String filtro, String valor, String orden)
        {   
            
            List<Usuario> usuarios = new List<Usuario>();
            SqlConnection connection = ConexionSingleton.obtenerConexion();

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT usu_dni,usu_alias,usu_nombre,usu_apellido, usu_intentos, usu_id, idi_id, idi_codigo FROM USUARIO  ");
            sb.Append(" inner join IDIOMA on usu_idioma = idi_id");
            sb.Append(" where usu_baja <> 1 ");

            SqlCommand query = new SqlCommand("", connection);

            query.CommandText = sb.ToString();

            connection.Open();
            SqlDataReader reader;
            Usuario usu;
            try
            {
                reader = query.ExecuteReader();
                while (reader.Read())
                {
                    usu = new Usuario();
                    usu.dni = reader["USU_DNI"].ToString();
                    usu.alias = SeguridadUtiles.desencriptarAES(reader["USU_ALIAS"].ToString());
                    usu.nombre = reader["USU_NOMBRE"].ToString();
                    usu.apellido = reader["USU_APELLIDO"].ToString();
                    Idioma idi = new Idioma();
                    idi.codigo = reader["IDI_CODIGO"].ToString();
                    idi.id = (long)reader["IDI_ID"];
                    usu.idioma = idi;
                    long intentos = (long)reader["USU_INTENTOS"];
                    if (intentos == 3)
                    {
                        usu.baja = 1;
                    }
                    else
                    {
                        usu.baja = 0;
                    }
                    usu.id = (long)reader["USU_ID"];
                    usuarios.Add(usu);
                }
            }
            catch (Exception ex)
            {

                throw  ex;
            }
            finally
            {
                connection.Close();
            }

            List<Usuario> usuariosFiltrados = new List<Usuario>();
            if (!String.IsNullOrEmpty(filtro) && !String.IsNullOrEmpty(valor))
            {
                foreach (Usuario current in usuarios)
                {
                    if (current.GetType().GetProperty(filtro).GetValue(current).ToString().Equals(valor, StringComparison.InvariantCultureIgnoreCase)){
                        usuariosFiltrados.Add(current);
                    }
                }

            }

            return usuariosFiltrados.Count > 0 ? usuariosFiltrados : usuarios;
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

        public void realizarBackup(int partes, String directorio)
        {
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
            catch (Exception e)
            {

                connection.Close();
                throw e;
            }
        }

        public void realizarRestore(String directorio)
        {
            SqlConnection connection = new SqlConnection("Data Source="+Environment.MachineName+";Initial Catalog=master;Integrated Security=True");
            connection.Open();
            StringBuilder queryText = new StringBuilder();
            queryText.Append(" USE MASTER ");
            queryText.Append(" RESTORE DATABASE TRABAJO_DIPLOMA FROM  DISK = '" + directorio + "' WITH REPLACE");
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
                query.Parameters.Add(new SqlParameter("@CODIGO", System.Data.SqlDbType.VarChar)).Value = tag;
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
                            if (!traducciones.ContainsKey(tag))
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
