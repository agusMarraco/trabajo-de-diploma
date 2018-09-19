using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
using TrabajoDeCampo.BO;
using TrabajoDeCampo.SEGURIDAD;

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
        public void loguear(String user, String pass, String codigoIdioma) {

            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();

            Boolean existe = false;
            long idUsuario = 0;
            long intentos = 0;

            SqlCommand queryUser = new SqlCommand("", connection, tx);
            SqlCommand queryPass = new SqlCommand("", connection, tx);
            SqlCommand queryPermisos = new SqlCommand("", connection, tx);
            SqlCommand queryIntentos = new SqlCommand("", connection, tx);
            SqlCommand queryBloqueado = new SqlCommand("", connection, tx);
            SqlCommand queryLoginExitoso = new SqlCommand("", connection, tx);

            queryUser.CommandText = "SELECT USU_ID,USU_ALIAS FROM USUARIO WHERE USU_BAJA = 0";

            queryPass.CommandText = "SELECT COUNT (*) FROM USUARIO WHERE USU_PASS = @PASS AND USU_BAJA = 0 AND USU_ID = @ID";
            String encodeado = SeguridadUtiles.encriptarMD5(pass);
            queryPass.Parameters.Add(new SqlParameter("@PASS", System.Data.SqlDbType.NVarChar)).Value = encodeado;

            queryIntentos.CommandText = " UPDATE USUARIO SET USU_INTENTOS = ((SELECT USU_INTENTOS FROM USUARIO WHERE USU_ID =  @ID) + 1) where usu_id = @ID ";

            queryLoginExitoso.CommandText = " UPDATE USUARIO SET USU_INTENTOS = 0 where usu_id = @ID ";
            queryBloqueado.CommandText = " SELECT USU_INTENTOS FROM USUARIO WHERE USU_ID = @ID ";

            queryPermisos.CommandText = "Select count(*) from PERMISOS_USUARIO WHERE USU_ID = @ID AND PAT_ID IN (16,17,18,19)";


            SqlDataReader reader = null;
            SqlDataReader readerPass;
            //CHEQUEO SI EXISTE
            try
            {
                 reader = queryUser.ExecuteReader();


                //alias
                while (reader.Read())
                {
                    
                    if(!reader.IsDBNull(1) && SeguridadUtiles.desencriptarAES(reader.GetValue(1).ToString()).Equals(user))
                    {
                        existe = true;
                        idUsuario = (long)reader.GetValue(0);
                    }
                    
                }
                reader.Close();
                if (!existe)
                {
                    throw new Exception("ALIAS");
                }

                //pass
                int count = 0;
                queryPass.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = idUsuario;
                reader = queryPass.ExecuteReader();

                while (reader.Read())
                {
                    count = (int)reader.GetValue(0);
                   
                }
                reader.Close();

                //bloqueado
                queryBloqueado.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.NVarChar)).Value = idUsuario;
                reader = queryBloqueado.ExecuteReader();
                while (reader.Read())
                {
                    intentos = (long)reader.GetValue(0);
                  
                }
                reader.Close();


              
                //incremento contador si el pass es incorrecto
                if(count == 0)
                {
                    if(intentos != 3)
                    {
                        queryIntentos.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.NVarChar)).Value = idUsuario;
                        queryIntentos.ExecuteNonQuery();
                   
                    }
                    throw new Exception("PASS");
                }
                //bloqueado
                if (intentos == 3)
                    throw new Exception("BLOQUEADO");


                int sistemaBloqueado = TrabajoDeCampo.Properties.Settings.Default.Bloqueado;

                if (sistemaBloqueado == 1)
                {
                    queryPermisos.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.NVarChar)).Value = idUsuario;
                    reader = queryPermisos.ExecuteReader();
                    int readerReturn = 0;
                    while (reader.Read())
                    {
                       readerReturn = (int)reader.GetValue(0);
                    }
                    reader.Close();
                    if(readerReturn != 4)
                    {
                        throw new Exception("PERMISOS");
                    }
                }


                if(intentos != 0)
                {
                    queryLoginExitoso.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.NVarChar)).Value = idUsuario;
                    queryLoginExitoso.ExecuteNonQuery();
                }

                tx.Commit();

            }
            catch (Exception e)
            {


                reader.Close();
                if(e.Message == "ALIAS" || e.Message == "PASS" || e.Message == "BLOQUEADO" || e.Message == "PERMISOS")
                {
                    tx.Commit();
                    connection.Close();
                }
                else{
                    tx.Rollback();
                    connection.Close();
                }
               
              
                throw e;
            }
            finally{
                connection.Close();
            }
            
        }

        public Boolean chequearSistemaBloqueado() { return true; }

        public Boolean probarConexion() { return true; }

        //FAMILIA PATENTE
        public List<ComponentePermiso> listarFamiliasYPatentes() {
            List<ComponentePermiso> permisos = new List<ComponentePermiso>();

            List<Familia> familias = this.listarFamilias();
            List<Patente> patentes = this.listarPatentes();
            permisos.AddRange(familias);
            permisos.AddRange(patentes);

            return permisos;

        }

        public void actualizarFamiliaPantente(List<Patente> pantentes, Familia familia) { }

        public Boolean tienePatente(long idUsuario, String codigoPantente) { return true; }

        public void actualizarPermisosUsuario(Usuario usuario, List<ComponentePermiso> familiasYPatentes)
        {

            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlCommand query = new SqlCommand("", connection);

            SqlCommand deleteQuery = new SqlCommand(" delete usuario_patente where up_usuario_id = @id delete usuario_familia where uf_usuario_id = @id" , connection);
            deleteQuery.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.BigInt)).Value = usuario.id;
           
            // query
            StringBuilder sb = new StringBuilder();

            // para inserts de permisos
            int i = 0;
            foreach(ComponentePermiso item in usuario.componentePermisos)
            {
                if(item is Familia)
                {
                    sb.Append(" insert into usuario_familia (uf_usuario_id,uf_familia_id) values (@id,@idGenerico" + i + ") ");
                    query.Parameters.Add(new SqlParameter("@idGenerico"+i, System.Data.SqlDbType.BigInt)).Value = ((Familia)item).id;
                }
                else
                {
                    sb.Append(" insert into usuario_patente (up_usuario_id,up_patente_id,up_dvh,up_bloqueada) values (@id,@idGenerico" + i + ",' ',@bloqueadaGenerico"+i+") ");
                    query.Parameters.Add(new SqlParameter("@idGenerico" + i, System.Data.SqlDbType.BigInt)).Value = ((Patente)item).id;
                    query.Parameters.Add(new SqlParameter("@bloqueadaGenerico" + i, System.Data.SqlDbType.BigInt)).Value = ((Patente)item).bloqueada ? 1 : 0;

                }
                i++;
            }
         
            query.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.BigInt)).Value = usuario.id;

            query.CommandText = sb.ToString();
          


            SqlTransaction tx = connection.BeginTransaction();
            query.Transaction = tx;
            deleteQuery.Transaction = tx;
            try
            {

                deleteQuery.ExecuteNonQuery();
                query.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
            }
            catch (Exception exe)
            {
                try
                {
                    tx.Rollback();
                }
                catch (Exception)
                {
                    connection.Close();
                    throw exe;
                }

              
            }
        }

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

        public void borrarFamilia(long IdFamilia) {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            StringBuilder sb = new StringBuilder();
            sb.Append(" DELETE FAMILIA_PATENTE WHERE FP_FAMILIA_ID = @ID ");
            sb.Append(" DELETE FAMILIA WHERE FAM_ID = @ID ");
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand query = new SqlCommand(sb.ToString(), connection, tx);
            query.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = IdFamilia;


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
        public Boolean chequearFamiliaDesasignada(long idFamilia) {

            bool desasignada = false;

            SqlConnection connection = ConexionSingleton.obtenerConexion();
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT COUNT(*) FROM USUARIO_FAMILIA WHERE UF_FAMILIA_ID = @IDFAMILIA ");

            SqlCommand query = new SqlCommand(sb.ToString(), connection);
            query.Parameters.Add(new SqlParameter("@IDFAMILIA", System.Data.SqlDbType.BigInt)).Value = idFamilia;
            SqlDataReader reader = null;
            
            try
            {
                connection.Open();
                reader = query.ExecuteReader();
                while (reader.Read())
                {
                    int relaciones = (int)reader.GetValue(0);
                    desasignada = (relaciones == 0);
                }


                connection.Close();
            }
            catch (Exception exe)
            {
                connection.Close();
                throw exe;
            }

            return desasignada;

        }
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
                        tempUsuario = new Usuario(); tempUsuario.email = reader["USU_EMAIL"].ToString();
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
        public Usuario buscarUsuario(long idUsuario) {

            SqlConnection connection = ConexionSingleton.obtenerConexion();
            SqlCommand query = new SqlCommand("", connection);
            query.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = idUsuario;

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT USU.*,IDI_ID,IDI_CODIGO FROM USUARIO USU inner join IDIOMA ON IDI_ID = USU.USU_IDIOMA WHERE USU.USU_ID = @ID");

            query.CommandText = sb.ToString();

            int idIdioma;

            connection.Open();
            SqlDataReader reader;
            Usuario usu = new Usuario();
            try
            {
                reader = query.ExecuteReader();
                while (reader.Read())
                {
                   
                    usu.dni = reader["USU_DNI"].ToString();
                    usu.email = reader["USU_EMAIL"].ToString();
                    usu.alias = SeguridadUtiles.desencriptarAES(reader["USU_ALIAS"].ToString());
                    usu.nombre = reader["USU_NOMBRE"].ToString();
                    usu.apellido = reader["USU_APELLIDO"].ToString();
                    usu.direccion = reader["USU_DIRECCION"].ToString();
                    usu.telefono = reader["USU_TELEFONO"].ToString();
                    Idioma idi = new Idioma();
                    idi.codigo = reader["IDI_CODIGO"].ToString();
                    idi.id = (long)reader["IDI_ID"];
                    usu.idioma = idi;
                    usu.id = (long)reader["USU_ID"];
                    usu.componentePermisos = new List<ComponentePermiso>();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                throw ex;
            }

            sb.Clear();
            sb.Append(" SELECT * FROM USUARIO_PATENTE UP INNER JOIN PATENTE PAT ON PAT.PAT_ID = UP.UP_PATENTE_ID WHERE UP.UP_USUARIO_ID = @ID ");
            query.CommandText = sb.ToString();
            try
            {
                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    Patente pat = new Patente();
                    pat.bloqueada = (Boolean) reader["UP_BLOQUEADA"];
                    pat.descripcion = SeguridadUtiles.desencriptarAES(reader["PAT_DESC"].ToString());
                    pat.id = (long)reader["PAT_ID"];
                    usu.componentePermisos.Add(pat);
                }
                reader.Close();
            }
            catch (Exception)
            {
                connection.Close();
                throw;
            }


            sb.Clear();
            sb.Append(" SELECT * FROM USUARIO_FAMILIA UF INNER JOIN FAMILIA FAM ON FAM.FAM_ID = UF.UF_FAMILIA_ID WHERE UF.UF_USUARIO_ID = @ID ");
            query.CommandText = sb.ToString();
            try
            {
                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    Familia familia = new Familia();
                    
                    familia.nombre = SeguridadUtiles.desencriptarAES(reader["FAM_NOMBRE"].ToString());
                    familia.id = (long)reader["FAM_ID"];
                    usu.componentePermisos.Add(familia);
                }
                reader.Close();
            }
            catch (Exception)
            {
                connection.Close();
                throw;
            }

            connection.Close();


            return usu;
        }
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
            sb.Append(" OUTPUT Inserted.usu_id values (@dni,@email,@pass,@alias,@nombre,@apellido,@direccion,@telefono,@dvh,@idioma)");
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
                
                SqlDataReader reader  = query.ExecuteReader();
                while (reader.Read())
                {
                    usuario.id = (long)reader.GetValue(0);
                }
                reader.Close();
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

        public void modificarUsuario(Usuario usuario) {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            // query
            StringBuilder sb = new StringBuilder();
            sb.Append(" update usuario set usu_dni = @dni, " +
                "usu_email = @email," +
                "usu_alias = @alias," +
                "usu_nombre = @nombre," +
                "usu_apellido = @apellido," +
                "usu_direccion = @direccion," +
                "usu_telefono = @telefono," +
                "usu_dvh = @dvh," +
                "usu_idioma = @idioma ");
            sb.Append("where usu_id = @id");
            SqlCommand query = new SqlCommand(sb.ToString(), connection);
            query.Transaction = tx;

            //agregando los paramentros 

            query.Parameters.Add(new SqlParameter("@dni", System.Data.SqlDbType.VarChar)).Value = usuario.dni;
            query.Parameters.Add(new SqlParameter("@email", System.Data.SqlDbType.VarChar)).Value = usuario.email;
            query.Parameters.Add(new SqlParameter("@alias", System.Data.SqlDbType.NVarChar)).Value = SeguridadUtiles.encriptarAES(usuario.alias);
            query.Parameters.Add(new SqlParameter("@nombre", System.Data.SqlDbType.VarChar)).Value = usuario.nombre;
            query.Parameters.Add(new SqlParameter("@apellido", System.Data.SqlDbType.VarChar)).Value = usuario.apellido;
            query.Parameters.Add(new SqlParameter("@direccion", System.Data.SqlDbType.VarChar)).Value = usuario.direccion;
            query.Parameters.Add(new SqlParameter("@telefono", System.Data.SqlDbType.VarChar)).Value = usuario.telefono;
            query.Parameters.Add(new SqlParameter("@dvh", System.Data.SqlDbType.VarChar)).Value = " ";
            query.Parameters.Add(new SqlParameter("@idioma", System.Data.SqlDbType.BigInt)).Value = usuario.idioma.id;
            query.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.BigInt)).Value = usuario.id;

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
                catch (Exception)
                {
                }

                connection.Close();
                throw exe;
            }
            actualizarPermisosUsuario(usuario, usuario.componentePermisos);

        }

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
        public void grabarBitacora(Usuario usuario, String mensaje, int criticidad) {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();

            StringBuilder builder = new StringBuilder(" INSERT INTO BITACORA  (");
            if (usuario != null)
            {
                builder.Append("BIT_USUARIO,");
            }
            builder.Append("BIT_MENSAJE,");
            builder.Append("BIT_CRITICIDAD_ID,");
            builder.Append("BIT_FECHA,");
            builder.Append("BIT_DVH )");

            builder.Append(" VALUES (");

            if (usuario != null) { 
                builder.Append(" @USUARIO,");
            }


            builder.Append(" @MENSAJE,");
            builder.Append(" @CRITICIDAD");
            builder.Append(" @FECHA");
            builder.Append(" @DVH");
            builder.Append(" ) ");
            SqlCommand cmd = new SqlCommand(builder.ToString(), connection, tx);
            cmd.Parameters.Add(new SqlParameter("@MENSAJE", System.Data.SqlDbType.Text)).Value = mensaje;
            cmd.Parameters.Add(new SqlParameter("@CRITICIDAD", System.Data.SqlDbType.BigInt)).Value = criticidad;
            cmd.Parameters.Add(new SqlParameter("@FECHA", System.Data.SqlDbType.Date)).Value = new DateTime();
            cmd.Parameters.Add(new SqlParameter("@DVH", System.Data.SqlDbType.VarChar)).Value = "";

            if(usuario!= null)
                cmd.Parameters.Add(new SqlParameter("@USUARIO", System.Data.SqlDbType.BigInt)).Value = usuario.id;

            try
            {
                cmd.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
            }
            catch (Exception)
            {
                try
                {
                    tx.Rollback();
                }
                catch (Exception)
                {

                   
                }
                connection.Close();
                throw;
            }
        }

        public DataSet listarBitacora(String filtro, String valor, String orden) {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            String query = " SELECT * FROM BITACORA ";
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();

            SqlCommand cmd = new SqlCommand(query, connection, tx);
            DataSet set = new DataSet();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(set);
                tx.Commit();
                connection.Close();

            }
            catch (Exception)
            {

                try
                {
                    tx.Rollback();
                }
                catch (Exception)
                {
                    connection.Close();
                  
                }
            }


            return set;

            
        }

        //DIGITOS VERIFICADORES

        public void verificarDigitosVerificadores() {

            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tr = connection.BeginTransaction();
            SqlDataReader reader = null;
            SqlCommand cmd = new SqlCommand("",connection,tr);
            Dictionary<String, String> digitoVerticalCalculado = new Dictionary<string, string>();

            StringBuilder stringParaDVH = new StringBuilder();
            StringBuilder builder = new StringBuilder();

            //usuario
            String query = "SELECT USU_ID,USU_ALIAS,USU_PASS,USU_INTENTOS,USU_DNI,USU_DVH FROM USUARIO ";
            cmd.CommandText = query;

            reader = cmd.ExecuteReader();
            while (reader.Read()) {
                builder.Clear();

                builder.Append(reader.GetValue(1).ToString());
                builder.Append(reader.GetValue(2).ToString());
                builder.Append(reader.GetValue(3).ToString());
                builder.Append(reader.GetValue(4).ToString());
                String md5 = SeguridadUtiles.encriptarMD5(builder.ToString());
                String usuDVH = reader.GetString(5);

                if (!md5.Equals(usuDVH))
                {
                    reader.GetValue(0);
                    throw new Exception("fallo la integridad de datos");
                }

                stringParaDVH.Append(md5);
            }
            
            digitoVerticalCalculado.Add("USUARIO", stringParaDVH.ToString());
            reader.Close();
            stringParaDVH.Clear();



            //mapeoTablaCampo.Add("PATENTE", new KeyValuePair<String, String[]>("PAT_DVH", new String[] { "PAT_ID", "PAT_DESC", "PAT_ID" }));
            //patente

            query = "SELECT PAT_ID, PAT_DESC, PAT_DVH FROM PATENTE ";
            
            cmd.CommandText = query;
            builder = new StringBuilder();

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                builder.Clear();

                builder.Append(reader.GetValue(1).ToString());
                builder.Append(reader.GetValue(0).ToString());
            
                String md5 = SeguridadUtiles.encriptarMD5(builder.ToString());
                String patDVH = reader.GetString(2);

                if (!md5.Equals(patDVH))
                {
                    reader.GetValue(0);
                    throw new Exception("fallo la integridad de datos");
                }

                stringParaDVH.Append(md5);
            }

            digitoVerticalCalculado.Add("PATENTE", stringParaDVH.ToString());
            reader.Close();
            stringParaDVH.Clear();



            //mapeoTablaCampo.Add("BITACORA", new KeyValuePair<String, String[]>("BIT_DVH", new String[] { "BIT_ID", "BIT_FECHA", "BIT_MENSAJE", "BIT_CRITICIDAD_ID" }));

            query = "SELECT BIT_ID, BIT_FECHA, BIT_MENSAJE , BIT_CRITICIDAD_ID, BIT_DVH FROM BITACORA ";

            cmd.CommandText = query;
            builder = new StringBuilder();

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                builder.Clear();

                builder.Append(reader.GetValue(1).ToString());
                builder.Append(reader.GetValue(2).ToString());
                builder.Append(reader.GetValue(3).ToString());

                String md5 = SeguridadUtiles.encriptarMD5(builder.ToString());
                String patDVH = reader.GetString(4);

                if (!md5.Equals(patDVH))
                {
                    reader.GetValue(0);
                    throw new Exception("fallo la integridad de datos");
                }

                stringParaDVH.Append(md5);
            }

            digitoVerticalCalculado.Add("BITACORA", stringParaDVH.ToString());
            reader.Close();
            stringParaDVH.Clear();



            //mapeoTablaCampo.Add("PLANILLA_DE_EVALUACION", new KeyValuePair<String, String[]>("PDE_DVH", new String[] { "PDE_ID", "PDE_TRIMESTRE_1", "PDE_TRIMESTRE_2", "PDE_TRIMESTRE_3", "PDE_ALUMNO_ID" }));

            query = "SELECT PDE_ID, PDE_TRIMESTRE_1, PDE_TRIMESTRE_2 , PDE_TRIMESTRE_3, PDE_ALUMNO_ID, PDE_DVH FROM PLANILLA_DE_EVALUACION ";

            cmd.CommandText = query;
            builder = new StringBuilder();

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                builder.Clear();

                builder.Append(reader.GetValue(1).ToString());
                builder.Append(reader.GetValue(2).ToString());
                builder.Append(reader.GetValue(3).ToString());
                builder.Append(reader.GetValue(4).ToString());

                String md5 = SeguridadUtiles.encriptarMD5(builder.ToString());
                String patDVH = reader.GetString(5);

                if (!md5.Equals(patDVH))
                {
                    reader.GetValue(0);
                    throw new Exception("fallo la integridad de datos");
                }

                stringParaDVH.Append(md5);
            }

            digitoVerticalCalculado.Add("PLANILLA_DE_EVALUACION", stringParaDVH.ToString());
            stringParaDVH.Clear();
            reader.Close();






            //mapeoTablaCampo.Add("FAMILIA", new KeyValuePair<String, String[]>("FAM_DVH", new String[] { "FAM_ID", "FAM_NOMBRE", "FAM_BLOQUEADA" }));


            query = "SELECT FAM_ID, FAM_NOMBRE, FAM_BLOQUEADA , FAM_DVH FROM FAMILIA ";

            cmd.CommandText = query;
            builder = new StringBuilder();

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                builder.Clear();

                builder.Append(reader.GetValue(1).ToString());
                builder.Append(reader.GetValue(2).ToString());


                String md5 = SeguridadUtiles.encriptarMD5(builder.ToString());
                String patDVH = reader.GetString(3);

                if (!md5.Equals(patDVH))
                {
                    reader.GetValue(0);
                    throw new Exception("fallo la integridad de datos");
                }

                stringParaDVH.Append(md5);
            }

            digitoVerticalCalculado.Add("FAMILIA", stringParaDVH.ToString());
            reader.Close();
            stringParaDVH.Clear();


            //// DE ESTOS NO NECESITO EL ID PORQUE SON TABLAS RELACIONALES O ACCEDO POR PK NATURAL.
            //mapeoTablaCampo.Add("INASISTENCIA_DE_ALUMNO", new KeyValuePair<String, String[]>("INA_DVH", new String[] { "INA_ALUMNO_ID", "INA_FECHA", "INA_VALOR", "INA_JUSTIFICADA" }));


            query = "SELECT INA_ALUMNO_ID, INA_FECHA , INA_DVH FROM INASISTENCIA_DE_ALUMNO ";

            cmd.CommandText = query;
            builder = new StringBuilder();

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                builder.Clear();

                builder.Append(reader.GetValue(0).ToString());
                builder.Append(reader.GetValue(1).ToString());


                String md5 = SeguridadUtiles.encriptarMD5(builder.ToString());
                String patDVH = reader.GetString(2);

                if (!md5.Equals(patDVH))
                {
                    reader.GetValue(0);
                    throw new Exception("fallo la integridad de datos");
                }

                stringParaDVH.Append(md5);
            }

            digitoVerticalCalculado.Add("INASISTENCIA_DE_ALUMNO", stringParaDVH.ToString());
            reader.Close();
            stringParaDVH.Clear();

            //mapeoTablaCampo.Add("AMONESTACION", new KeyValuePair<String, String[]>("AMON_DVH", new String[] { "AMON_ALUMNO_ID", "AMON_FECHA", "AMON_MOTIVO" }));

            query = "SELECT AMON_ALUMNO_ID, AMON_FECHA,AMON_DVH FROM AMONESTACION ";

            cmd.CommandText = query;
            builder = new StringBuilder();

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                builder.Clear();

                builder.Append(reader.GetValue(0).ToString());
                builder.Append(reader.GetValue(1).ToString());


                String md5 = SeguridadUtiles.encriptarMD5(builder.ToString());
                String patDVH = reader.GetString(2);

                if (!md5.Equals(patDVH))
                {
                    reader.GetValue(0);
                    throw new Exception("fallo la integridad de datos");
                }

                stringParaDVH.Append(md5);
            }

            digitoVerticalCalculado.Add("AMONESTACION", stringParaDVH.ToString());
            reader.Close();
            stringParaDVH.Clear();
            //mapeoTablaCampo.Add("USUARIO_FAMILIA", new KeyValuePair<String, String[]>("UF_DVH", new String[] { "UF_USUARIO_ID", "UF_FAMILIA_ID" }));


            query = "SELECT UF_FAMILIA_ID, UF_USUARIO_ID, UF_DVH FROM USUARIO_FAMILIA ";

            cmd.CommandText = query;
            builder = new StringBuilder();

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                builder.Clear();

                builder.Append(reader.GetValue(0).ToString());
                builder.Append(reader.GetValue(1).ToString());


                String md5 = SeguridadUtiles.encriptarMD5(builder.ToString());
                String patDVH = reader.GetString(2);

                if (!md5.Equals(patDVH))
                {
                    reader.GetValue(0);
                    throw new Exception("fallo la integridad de datos");
                }

                stringParaDVH.Append(md5);
            }

            digitoVerticalCalculado.Add("USUARIO_FAMILIA", stringParaDVH.ToString());
            reader.Close();
            stringParaDVH.Clear();
            //mapeoTablaCampo.Add("USUARIO_PATENTE", new KeyValuePair<String, String[]>("UP_DVH", new String[] { "UP_USUARIO_ID", "UP_PATENTE_ID", "UP_BLOQUEADA" }));


            query = "SELECT UP_USUARIO_ID, UP_PATENTE_ID,UP_BLOQUEADA,UP_DVH FROM USUARIO_PATENTE ";

            cmd.CommandText = query;
            builder = new StringBuilder();

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                builder.Clear();

                builder.Append(reader.GetValue(0).ToString());
                builder.Append(reader.GetValue(1).ToString());
                Boolean bloqueado = reader.GetBoolean(2);
                builder.Append(bloqueado ? 1 : 0);

                String md5 = SeguridadUtiles.encriptarMD5(builder.ToString());
                String patDVH = reader.GetString(3);

                if (!md5.Equals(patDVH))
                {
                    reader.GetValue(0);
                    throw new Exception("fallo la integridad de datos");
                }

                stringParaDVH.Append(md5);
            }

            digitoVerticalCalculado.Add("USUARIO_PATENTE", stringParaDVH.ToString());
            reader.Close();
            stringParaDVH.Clear();
            //mapeoTablaCampo.Add("FAMILIA_PATENTE", new KeyValuePair<String, String[]>("FP_DVH", new String[] { "FP_PATENTE_ID", "FP_FAMILIA_ID" }));

            query = "SELECT FP_FAMILIA_ID, FP_PATENTE_ID,FP_DVH FROM FAMILIA_PATENTE ";

            cmd.CommandText = query;
            builder = new StringBuilder();

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                builder.Clear();

                builder.Append(reader.GetValue(0).ToString());
                builder.Append(reader.GetValue(1).ToString());


                String md5 = SeguridadUtiles.encriptarMD5(builder.ToString());
                String patDVH = reader.GetString(2);

                if (!md5.Equals(patDVH))
                {
                    reader.GetValue(0);
                    throw new Exception("fallo la integridad de datos");
                }

                stringParaDVH.Append(md5);
            }

            digitoVerticalCalculado.Add("FAMILIA_PATENTE", stringParaDVH.ToString());

            reader.Close();
            stringParaDVH.Clear();

            query = " SELECT DV_NOMBRE_TABLA,DV_DIGITO_CALCULADO FROM DIGITO_VERTICAL ";

            cmd.CommandText = query;
           
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                builder.Clear();

                string tabla = reader.GetValue(0).ToString();
                string md5Base = reader.GetValue(1).ToString();
                string md5Calculado = digitoVerticalCalculado[tabla];

                md5Calculado = SeguridadUtiles.encriptarMD5(md5Calculado);

                if (!md5Base.Equals(md5Calculado))
                {
                    reader.GetValue(1);
                    throw new Exception("fallo la integridad de datos");
                }

            }
            reader.Close();
            connection.Close();
        }


        /// <summary>
        /// recalcula todos los digitos que haya en la base de datos.
        /// </summary>
        public void recalcularDigitosVerificadores() {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand("", connection, tx);

            Dictionary<long, String> clavesAInsertar = new Dictionary<long, string>();
            Dictionary<string, string> clavesCompuestasAInsertar = new Dictionary<string, string>();

            foreach (KeyValuePair<string, KeyValuePair<string, string[]>> item in TablasDvhEnum.mapeoTablaCampo)
            {
                if(item.Key != "DIGITO_VERTICAL")
                {
                    String tabla = item.Key;
                    String campoDeDvh = item.Value.Key;
                    String[] camposASumar = item.Value.Value;
                    String campoId = camposASumar[0];
                    cmd.Parameters.Clear();
                    Boolean tablaSinId = false;
                    if(tabla == "FAMILIA_PATENTE" || tabla == "USUARIO_PATENTE" || tabla == "USUARIO_FAMILIA" || tabla == "AMONESTACION" || tabla == "INASISTENCIA_DE_ALUMNO")
                    {
                        tablaSinId = true;
                    }

                        StringBuilder selectQueryText = new StringBuilder();
                    selectQueryText.Append(" SELECT ");
                    for (int x = 0; x < camposASumar.Length; x++)
                    {
                        if (x == 0)
                        {
                            selectQueryText.Append(" " + camposASumar[x] + " ");
                        }
                        else
                        {
                            selectQueryText.Append("," + camposASumar[x] + " ");
                        }
                        

                    }

                    selectQueryText.Append(" FROM " + tabla);
                    

                    cmd.CommandText = selectQueryText.ToString();
                    long idDeRow = 0;
                    StringBuilder concatenadoAEncriptar = new StringBuilder();
                    String insertQueryText = "" ;
                    try
                    {
                        SqlDataReader reader;
                        if (!tablaSinId)
                        {
                             reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                if (!tablaSinId)
                                {
                                    idDeRow = (long)reader.GetValue(0);
                                    for(int x = 1; x < camposASumar.Length; x++)
                                    {
                                        if (!reader.IsDBNull(x))
                                        {
                                            concatenadoAEncriptar.Append(reader.GetValue(x).ToString());
                                        }
                                    }

                                    clavesAInsertar.Add(idDeRow, concatenadoAEncriptar.ToString());
                                    concatenadoAEncriptar.Clear();
                                }
                            }
                        reader.Close();
                        }

                        cmd.CommandText = "";
                        StringBuilder insertBuilder = new StringBuilder();
                        int iterNumber = 0;
                        cmd.Parameters.Clear();
                        foreach (KeyValuePair<long,string> iter in clavesAInsertar)
                        {
                            iterNumber++;
                            if (!tablaSinId)
                            {
                                insertQueryText = " UPDATE " + tabla + " SET " + campoDeDvh + " = @VALOR" + iterNumber + " WHERE "+ campoId + " =  @VALOR_ID" + iterNumber +" ";
                                insertBuilder.Append(insertQueryText);
                               
                                cmd.Parameters.Add(new SqlParameter("@VALOR"+ iterNumber, System.Data.SqlDbType.VarChar)).Value = SeguridadUtiles.encriptarMD5(iter.Value.ToString());
                                cmd.Parameters.Add(new SqlParameter("@VALOR_ID"+ iterNumber, System.Data.SqlDbType.BigInt)).Value = iter.Key;



                            }
                        }

                        if (tablaSinId)
                        {
                            switch (tabla)
                            {
                                case "INASISTENCIA_DE_ALUMNO":
                                    insertBuilder.Append(" execute generarDVHInasistenciaAlumno ");
                                    break;
                                case "AMONESTACION":
                                    insertBuilder.Append(" execute generarDVHAmonestacion ");
                                    break;
                                case "USUARIO_FAMILIA":
                                    insertBuilder.Append(" execute generarDVHUsuarioFamilia ");
                                    break;
                                case "USUARIO_PATENTE":
                                    insertBuilder.Append(" execute generarDVHUsuarioPatente ");
                                    break;
                                case "FAMILIA_PATENTE":
                                    insertBuilder.Append(" execute generarDVHFamiliaPatente ");
                                    break;

                            }
                        }
                        clavesAInsertar.Clear();
                        cmd.CommandText = insertBuilder.ToString();
                        if(!String.IsNullOrEmpty(cmd.CommandText))
                            cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        connection.Close();

                        throw;
                    }
                    
                    
                    
                
                }
                
                
            }

            tx.Commit();
            connection.Close();
        }


        /// <summary>
        /// calculo el digito horizontal en base a un conjunto de campos(valores) y lo devuelvo para que se produzca
        /// un insert o update.
        /// </summary>
        /// <param name="campos"></param>
        /// <returns></returns>
        public string recalcularDigitoHorizontal(String[] campos) {
            StringBuilder builder = new StringBuilder();
            for(int x = 0; x< campos.Length; x++)
            {
                builder.Append(campos[0]);
            }
            return SeguridadUtiles.encriptarMD5(builder.ToString());
        }

        /// <summary>
        /// calcula el digito vertical de una tabla especifica y lo actualiza en la tabla maestra.
        /// </summary>
        /// <param name="tabla"></param>
        public void recalcularDigitoVertical(String tabla) {

            SqlConnection connection = ConexionSingleton.obtenerConexion();
            String campo = TablasDvhEnum.mapeoTablaCampo[tabla].Key;
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            String updateCommand = " UPDATE DIGITO_VERTICAL SET DV_DIGITO_CALCULADO = @HASH WHERE DV_NOMBRE_TABLA =  @NOMBRE_TABLA ";
            SqlCommand query = new SqlCommand("", connection, tx);
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT STRING_AGG("+ campo +",'') FROM " +tabla );
            

            query.CommandText = sb.ToString();
            SqlDataReader reader;
            String sumaDeDVH = "";
            try
            {
                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        sumaDeDVH = reader.GetString(0);
                    }
                }
                reader.Close();
                if (!String.IsNullOrEmpty(sumaDeDVH))
                {
                    query.Parameters.Clear();
                    query.CommandText = updateCommand;
                    query.Parameters.Add(new SqlParameter("@HASH", System.Data.SqlDbType.VarChar)).Value = SeguridadUtiles.encriptarMD5(sumaDeDVH);
                    query.Parameters.Add(new SqlParameter("@NOMBRE_TABLA", System.Data.SqlDbType.VarChar)).Value = tabla;

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
