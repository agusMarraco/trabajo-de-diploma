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
            string alias = "";
            string savedPass = "";
            string dni = "";
            string idioma = "es";
            SqlCommand queryUser = new SqlCommand("", connection, tx);
            SqlCommand queryPass = new SqlCommand("", connection, tx);
            SqlCommand queryPermisos = new SqlCommand("", connection, tx);
            SqlCommand queryIntentos = new SqlCommand("", connection, tx);
            SqlCommand queryBloqueado = new SqlCommand("", connection, tx);
            SqlCommand queryLoginExitoso = new SqlCommand("", connection, tx);

            queryUser.CommandText = "SELECT USU_ID,USU_ALIAS,USU_PASS, USU_DNI, (select idi_codigo from idioma where idi_id = usu_idioma)" +
                " FROM USUARIO WHERE USU_BAJA = 0";

            queryPass.CommandText = "SELECT COUNT (*) FROM USUARIO WHERE USU_PASS = @PASS AND USU_BAJA = 0 AND USU_ID = @ID";
            String encodeado = SeguridadUtiles.encriptarMD5(pass);
            queryPass.Parameters.Add(new SqlParameter("@PASS", System.Data.SqlDbType.NVarChar)).Value = encodeado;

            queryIntentos.CommandText = " UPDATE USUARIO SET USU_INTENTOS = ((SELECT USU_INTENTOS FROM USUARIO WHERE USU_ID =  @ID) + 1) where usu_id = @ID ";

            queryLoginExitoso.CommandText = " UPDATE USUARIO SET USU_INTENTOS = 0 where usu_id = @ID ";
            queryBloqueado.CommandText = " SELECT USU_INTENTOS FROM USUARIO WHERE USU_ID = @ID ";
            //que tenga , para entrar con el sistema bloqueado, los permisos de 
            /*
             *  GenerarBackups = 16;
                RestaurarBackup = 17;
                RecalcularDígitosVerificadores = 18;
                VerBitácora = 19;
             */
            queryPermisos.CommandText = "Select count(*) from PERMISOS_USUARIO WHERE USU_ID = @ID AND PAT_ID IN (16,17,18,19)";
            SqlDataReader reader = null;
            
            //CHEQUEO SI EXISTE
            try
            {
                 reader = queryUser.ExecuteReader();


                //alias
                while (reader.Read())
                {
                    //tengo que levantar los usuarios de la base por la forma en la cual manejo el AES, es mas seguro, pero menos performante
                    if(!reader.IsDBNull(1) && SeguridadUtiles.desencriptarAES(reader.GetValue(1).ToString()).Equals(user))
                    {
                        existe = true;
                        idUsuario = (long)reader.GetValue(0);
                        alias = !reader.IsDBNull(1) ? reader.GetValue(1).ToString() : "" ;
                        savedPass = !reader.IsDBNull(2) ? reader.GetValue(2).ToString() : "";
                        dni = !reader.IsDBNull(3) ? reader.GetValue(3).ToString() : "";
                        idioma = !reader.IsDBNull(4) ? reader.GetValue(4).ToString() : "es";
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
                queryBloqueado.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = idUsuario;
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
                        queryIntentos.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = idUsuario;
                        queryIntentos.ExecuteNonQuery();
                        intentos++;
                        String digito = this.recalcularDigitoHorizontal(new String[] { alias, savedPass, intentos.ToString(), dni });
                        queryIntentos.CommandText = " UPDATE USUARIO SET USU_DVH = @DVH WHERE USU_ID = @ID ";
                        queryIntentos.Parameters.Add(new SqlParameter("@DVH", System.Data.SqlDbType.NVarChar)).Value = digito;
                        queryIntentos.ExecuteNonQuery();
                    }
                    throw new Exception("PASS");
                }
                //bloqueado

                int sistemaBloqueado = TrabajoDeCampo.Properties.Settings.Default.Bloqueado;
                int permisosEsencialesCount = 0;
                if (sistemaBloqueado == 1)
                {
                    queryPermisos.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = idUsuario;
                    reader = queryPermisos.ExecuteReader();
                    int readerReturn = 0;
                    while (reader.Read())
                    {
                       readerReturn = (int)reader.GetValue(0);
                    }
                    reader.Close();
                    if(readerReturn != 4)
                    {//SI NO TIENE LOS PERMISOS MINIMOS NO ENTRA.
                        throw new Exception("PERMISOS");
                    }
                    permisosEsencialesCount = readerReturn;
                }

                // intentos de usuario bloqueado
                if (intentos == 3)
                    throw new Exception("BLOQUEADO");


                queryLoginExitoso.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = idUsuario;
                if (intentos != 0)
                {
                    //SI ENTRA OK LE RESETEO EL CONTADOR
                    queryLoginExitoso.ExecuteNonQuery();
                    intentos = 0;
                }
                String dvh = this.recalcularDigitoHorizontal(new String[] { alias, savedPass, intentos.ToString(), dni });
                queryLoginExitoso.CommandText = " UPDATE USUARIO SET USU_DVH = @DVH WHERE USU_ID = @ID ";
                
                queryLoginExitoso.Parameters.Add(new SqlParameter("@DVH", System.Data.SqlDbType.NVarChar)).Value = dvh;
                queryLoginExitoso.ExecuteNonQuery();

                tx.Commit();
                TrabajoDeCampo.Properties.Settings.Default.SessionUser = idUsuario;
                TrabajoDeCampo.Properties.Settings.Default.Idioma = idioma;
            }
            catch (Exception e)
            {

                //MANEJO DE EXCEPCIONES
                reader.Close();
                if(e.Message == "ALIAS" || e.Message == "PASS" || e.Message == "BLOQUEADO" || e.Message == "PERMISOS")
                {
                    tx.Commit();
                    connection.Close();
                    if(e.Message == "PASS")
                    {
                        this.recalcularDigitoVertical("USUARIO");
                    }
                    Usuario usu = null;
                    switch (e.Message)
                    {
                        case "ALIAS":
                            usu = new Usuario();
                            usu.id = 1L;
                            this.grabarBitacora(usu, "Alguien se intento loguear con un alias incorrecto " + user, CriticidadEnum.MEDIA);
                            break;
                        case "PASS":
                           usu = new Usuario();
                            usu.id = idUsuario;
                            this.grabarBitacora(usu, "Alguien se intento loguear con un pass incorrecto " + user, CriticidadEnum.BAJA);
                            break;
                        case "BLOQUEADO":
                            usu = new Usuario();
                            usu.id = idUsuario;
                            this.grabarBitacora(usu, "Un usuario bloqueado se intento loguear  " + user, CriticidadEnum.ALTA);
                            break;
                        case "PERMISOS":
                            usu = new Usuario();
                            usu.id = idUsuario;
                            this.grabarBitacora(usu, "Alguien se intento loguear estando el sistema bloqueado" + user, CriticidadEnum.ALTA);
                            break;
                        default:
                            break;
                    }
                }
                else{
                    try
                    {
                        tx.Rollback();
                       
                    }
                    catch (Exception)
                    {

                        throw e;
                    }
                    finally
                    {
                        connection.Close();
                    }
                    
                }
               
              
                throw e;
            }
            finally{
                connection.Close();
                this.recalcularDigitoVertical("USUARIO");
                if(TrabajoDeCampo.Properties.Settings.Default.SessionUser != 0)
                {
                    //AL EJECUTARSE EN UN FINALLY TENGO QUE VERIFICAR QUE EL ID DE USUARIO EXISTA, ES DECIR != 0
                  Usuario usu = new Usuario();
                  usu.id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
                  this.grabarBitacora(usu, "El usuario se logueo exitosamente", CriticidadEnum.BAJA);

                }
            }
            
        }
        /// <summary>
        /// FLAG QUE SE SETEA AL INICIO DE SESION
        /// </summary>
        /// <returns></returns>
        public Boolean chequearSistemaBloqueado() {
            return TrabajoDeCampo.Properties.Settings.Default.Bloqueado == 1;
        }
        /// <summary>
        /// QUERY PARA PROBAR QUE LA CONEXION ESTE OK, LE EXCEPCION SE AGARRA MAS ARRIBA
        /// </summary>
        /// <returns></returns>
        public Boolean probarConexion() {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            Boolean sePudoConectar = false;
            connection.Open();
                SqlCommand query = new SqlCommand(" SELECT * FROM USUARIO", connection);
                query.ExecuteReader();
                connection.Close();
                sePudoConectar = true;
                
            return sePudoConectar;
          



        }

        //FAMILIA PATENTE
        public List<ComponentePermiso> listarFamiliasYPatentes() {
            List<ComponentePermiso> permisos = new List<ComponentePermiso>();

            List<Familia> familias = this.listarFamilias();
            List<Patente> patentes = this.listarPatentes();
            permisos.AddRange(familias);
            permisos.AddRange(patentes);

            return permisos;

        }

        public void actualizarFamiliaPantente(List<Patente> pantentes, Familia familia) { } //deprecado
        /// <summary>
        /// Se usa para chequear los permisos a la hora de habilitar controles, vista dinamica en base de datos.
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="codigoPantente"></param>
        /// <returns></returns>
        public Boolean tienePatente(long idUsuario, String codigoPantente)
        {

            bool tienePantente = false;

            SqlConnection connection = ConexionSingleton.obtenerConexion();
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT COUNT(*) FROM PERMISOS_USUARIO WHERE USU_ID = @ID AND PAT_ID = @PAT ");

            SqlCommand query = new SqlCommand(sb.ToString(), connection);
            query.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = idUsuario;
            query.Parameters.Add(new SqlParameter("@PAT", System.Data.SqlDbType.BigInt)).Value = long.Parse(codigoPantente);
            SqlDataReader reader = null;

            try
            {
                connection.Open();
                reader = query.ExecuteReader();
                while (reader.Read())
                {
                    int relaciones = (int)reader.GetValue(0);
                    tienePantente = (relaciones == 1);
                }


                connection.Close();
            }
            catch (Exception exe)
            {
                connection.Close();
                throw exe;
            }

            return tienePantente;
        }

        public void actualizarPermisosUsuario(Usuario usuario, List<ComponentePermiso> familiasYPatentes)
        {

            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlCommand query = new SqlCommand("", connection);

            SqlCommand deleteQuery = new SqlCommand(" delete usuario_patente where up_usuario_id = @id delete usuario_familia where uf_usuario_id = @id" , connection);
            deleteQuery.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.BigInt)).Value = usuario.id;
           
            // query
            StringBuilder sb = new StringBuilder();

            // para inserts de permisos en las tablas intermedias
            int i = 0;
            foreach(ComponentePermiso item in usuario.componentePermisos)
            {
                if (item is Familia)
                {
                    sb.Append(" insert into usuario_familia (uf_usuario_id,uf_familia_id,uf_dvh) values (@id,@idGenerico" + i + ",@dvh" + i + ") ");
                    query.Parameters.Add(new SqlParameter("@idGenerico" + i, System.Data.SqlDbType.BigInt)).Value = ((Familia)item).id;
                    query.Parameters.Add(new SqlParameter("@dvh" + i, System.Data.SqlDbType.NVarChar)).Value = this.recalcularDigitoHorizontal(new String[] { ((Familia)item).id.ToString(), usuario.id.ToString() });
                }
                else
                {
                    sb.Append(" insert into usuario_patente (up_usuario_id,up_patente_id,up_dvh,up_bloqueada) values (@id,@idGenerico" + i + ",@dvh"+ i +",@bloqueadaGenerico"+i+") ");
                    query.Parameters.Add(new SqlParameter("@idGenerico" + i, System.Data.SqlDbType.BigInt)).Value = ((Patente)item).id;
                    query.Parameters.Add(new SqlParameter("@dvh" + i, System.Data.SqlDbType.NVarChar)).Value = this.recalcularDigitoHorizontal(new String[] { usuario.id.ToString(),
                        ((Patente)item).id.ToString(), (((Patente)item).bloqueada ? 1 : 0).ToString() });
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
                //borro las relaciones viejas
                deleteQuery.ExecuteNonQuery();
                //inserto las nuevas si corresponse
                if (usuario.componentePermisos.Count > 0)
                    query.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
                this.recalcularDigitoVertical("USUARIO_FAMILIA");
                this.recalcularDigitoVertical("USUARIO_PATENTE");
            }
            catch (Exception exe)
            {
                    tx.Rollback();
                    connection.Close();
                    throw exe;
                
              
            }
        }

        public Boolean verificarPermisosEsenciales(Usuario usuario) {
            //al cambiar permisos chequear que quede un usuario distinto a este con permisos clave.
            //logica de chequeo
            List<Patente> patentes = new List<Patente>();
            List<Patente> filtradas = new List<Patente>();
            List<Patente> negadas = new List<Patente>();

            foreach (ComponentePermiso item in usuario.componentePermisos)
            {
                if(item.GetType() == typeof(Patente))
                {
                    Patente pat = (Patente)item;
                    if (pat.bloqueada)
                    {
                        negadas.Add(pat);
                    }
                    else
                    {
                        patentes.Add(pat);
                    }
                   
                }
                else
                {
                    foreach (Patente patente in ((Familia)item).patentes)
                    {
                        patentes.Add(patente);
                    }
                }
            }
            bool estaNegada;
            foreach(Patente pat in patentes)
            {
                estaNegada = false;
                foreach (Patente negada in negadas)
                {
                    if(negada.id == pat.id)
                    {
                        estaNegada = true;
                        break;
                    }
                    if (!estaNegada)
                    {
                        filtradas.Add(pat);

                    }
                }
            }
            if(negadas.Count == 0)
            {
                filtradas.AddRange(patentes);
            }
            // que patentes esenciales va a tener
            bool CrearUsuario = false;
            bool ModificarUsuario = false;
            bool ListarUsuarios = false;
            bool GenerarBackups = false;
            bool RestaurarBackup = false;
            bool RecalcularDígitosVerificadores = false;
            bool ModificarFamilias = false;
            bool ListarFamilias = false;
            bool CrearFamilia = false;


            foreach (Patente pat in filtradas)
            {
                if(pat.id == EnumPatentes.CrearUsuario)
                {
                    CrearUsuario = true;
                }
                if (pat.id == EnumPatentes.ModificarUsuario)
                {
                    ModificarUsuario = true;
                }
                if (pat.id == EnumPatentes.ListarUsuarios)
                {
                    ListarUsuarios = true;
                }
                if (pat.id == EnumPatentes.GenerarBackups)
                {
                    GenerarBackups = true;
                }
                if (pat.id == EnumPatentes.RestaurarBackup)
                {
                    RestaurarBackup = true;
                }
                if (pat.id == EnumPatentes.RecalcularDígitosVerificadores)
                {
                    RecalcularDígitosVerificadores = true;
                }
                if (pat.id == EnumPatentes.ModificarFamilias)
                {
                    ModificarFamilias = true;
                }
                if (pat.id == EnumPatentes.ListarFamilias)
                {
                    ListarFamilias = true;
                }
                if (pat.id == EnumPatentes.CrearFamilia)
                {
                    CrearFamilia = true;
                }
            }
            //consultas sobre base
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            SqlCommand cmd = new SqlCommand("SELECT count(*) FROM PERMISOS_USUARIO WHERE PAT_ID = @PAT AND USU_ID <> @ID");
            
            cmd.Connection = connection;
            SqlDataReader reader;
            connection.Open();
            Boolean seguirBuscando = true;
            Boolean errorEsenciales = false;
            //va por cada permiso hasta que encuentro una falla o llego al final
            if (!CrearUsuario && seguirBuscando)
            {
                
                cmd.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = usuario.id;
                cmd.Parameters.Add(new SqlParameter("@PAT", System.Data.SqlDbType.BigInt)).Value = EnumPatentes.CrearUsuario;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int value = (int)reader.GetValue(0);
                    if(value == 0)
                    {
                        errorEsenciales = true;
                        seguirBuscando = false;

                    }
                }
                reader.Close();
            }
            if (!ModificarUsuario && seguirBuscando)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = usuario.id;
                cmd.Parameters.Add(new SqlParameter("@PAT", System.Data.SqlDbType.BigInt)).Value = EnumPatentes.ModificarUsuario;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int value = (int)reader.GetValue(0);
                    if (value == 0)
                    {
                        errorEsenciales = true;
                        seguirBuscando = false;

                    }
                }
                reader.Close();

            }
            if (!ListarUsuarios && seguirBuscando)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = usuario.id;
                cmd.Parameters.Add(new SqlParameter("@PAT", System.Data.SqlDbType.BigInt)).Value = EnumPatentes.ListarUsuarios;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int value = (int)reader.GetValue(0);
                    if (value == 0)
                    {
                        errorEsenciales = true;
                        seguirBuscando = false;

                    }
                }
                reader.Close();
            }
            if (!GenerarBackups && seguirBuscando)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = usuario.id;
                cmd.Parameters.Add(new SqlParameter("@PAT", System.Data.SqlDbType.BigInt)).Value = EnumPatentes.GenerarBackups;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int value = (int)reader.GetValue(0);
                    if (value == 0)
                    {
                        errorEsenciales = true;
                        seguirBuscando = false;

                    }
                }
                reader.Close();
            }
            if (!RestaurarBackup && seguirBuscando)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = usuario.id;
                cmd.Parameters.Add(new SqlParameter("@PAT", System.Data.SqlDbType.BigInt)).Value = EnumPatentes.RestaurarBackup;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int value = (int)reader.GetValue(0);
                    if (value == 0)
                    {
                        errorEsenciales = true;
                        seguirBuscando = false;

                    }
                }
                reader.Close();
            }
            if (!RecalcularDígitosVerificadores && seguirBuscando)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = usuario.id;
                cmd.Parameters.Add(new SqlParameter("@PAT", System.Data.SqlDbType.BigInt)).Value = EnumPatentes.RecalcularDígitosVerificadores;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int value = (int)reader.GetValue(0);
                    if (value == 0)
                    {
                        errorEsenciales = true;
                        seguirBuscando = false;

                    }
                }
                reader.Close();
            }
            if (!ModificarFamilias && seguirBuscando)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = usuario.id;
                cmd.Parameters.Add(new SqlParameter("@PAT", System.Data.SqlDbType.BigInt)).Value = EnumPatentes.ModificarFamilias;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int value = (int)reader.GetValue(0);
                    if (value == 0)
                    {
                        errorEsenciales = true;
                        seguirBuscando = false;

                    }
                }
                reader.Close();
            }
            if (!ListarFamilias && seguirBuscando)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = usuario.id;
                cmd.Parameters.Add(new SqlParameter("@PAT", System.Data.SqlDbType.BigInt)).Value = EnumPatentes.ListarFamilias;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int value = (int)reader.GetValue(0);
                    if (value == 0)
                    {
                        errorEsenciales = true;
                        seguirBuscando = false;

                    }
                }
                reader.Close();

            }
            if (!CrearFamilia && seguirBuscando)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = usuario.id;
                cmd.Parameters.Add(new SqlParameter("@PAT", System.Data.SqlDbType.BigInt)).Value = EnumPatentes.CrearFamilia;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int value = (int)reader.GetValue(0);
                    if (value == 0)
                    {
                        errorEsenciales = true;
                        seguirBuscando = false;

                    }
                }
                reader.Close();

            }

            connection.Close();

            return errorEsenciales;
        }

        public Boolean chequearNegada(long idUsuario, String codigoPatente) { return true; } //deprecado se usa la vista.
        /// <summary>
        /// creacion de familia
        /// </summary>
        /// <param name="familia"></param>
        public void crearFamilia(Familia familia) {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            StringBuilder sb = new StringBuilder();
            sb.Append(" INSERT INTO FAMILIA (FAM_NOMBRE, FAM_DVH) OUTPUT Inserted.FAM_ID VALUES(@NOMBRE, @DVHFAMILIA) ");
            
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand query = new SqlCommand(sb.ToString(), connection, tx);
            String nombreAES = SeguridadUtiles.encriptarAES(familia.nombre);
            query.Parameters.Add(new SqlParameter("@NOMBRE", System.Data.SqlDbType.NVarChar)).Value = nombreAES;
            query.Parameters.Add(new SqlParameter("@DVHFAMILIA", System.Data.SqlDbType.NVarChar)).Value = this.recalcularDigitoHorizontal(new string[] { nombreAES, familia.bloqueada.ToString() });

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
                    query.CommandText = " INSERT INTO FAMILIA_PATENTE (FP_PATENTE_ID,FP_FAMILIA_ID,FP_DVH) VALUES(@PATENTE,@FAMILIA,@DVH) ";
                    foreach (Patente item in familia.patentes)
                    {
                        query.Parameters.Clear();
                        query.Parameters.Add(new SqlParameter("@PATENTE", System.Data.SqlDbType.BigInt)).Value = item.id;
                        query.Parameters.Add(new SqlParameter("@FAMILIA", System.Data.SqlDbType.BigInt)).Value = id;
                        query.Parameters.Add(new SqlParameter("@DVH", System.Data.SqlDbType.NVarChar)).Value = this.recalcularDigitoHorizontal(new String[] { id.ToString(), item.id.ToString() });
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
                connection.Close();
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            this.recalcularDigitoVertical("FAMILIA");
            this.recalcularDigitoVertical("FAMILIA_PATENTE");

            Usuario usu = new Usuario();
            usu.id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            this.grabarBitacora(usu, "Se creó una familia", CriticidadEnum.ALTA);


        }
        /// <summary>
        /// modificacion de familia con chequeo de permisos esenciales.
        /// </summary>
        /// <param name="familia"></param>
        public void modificarFamilia(Familia familia) {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            StringBuilder sb = new StringBuilder();
            sb.Append(" UPDATE FAMILIA SET FAM_NOMBRE = @NOMBRE, FAM_DVH = @DVH WHERE FAM_ID = @ID ");

            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand query = new SqlCommand(sb.ToString(), connection, tx);
            String nombreAES = SeguridadUtiles.encriptarAES(familia.nombre);
            query.Parameters.Add(new SqlParameter("@NOMBRE", System.Data.SqlDbType.NVarChar)).Value = nombreAES;
            query.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = familia.id;
            query.Parameters.Add(new SqlParameter("@DVH", System.Data.SqlDbType.NVarChar)).Value = this.recalcularDigitoHorizontal(new String[] { nombreAES, familia.bloqueada.ToString() });

            //hago todo dentro la misma transaccion, si encuentro que 
            //hay problemas con los permisos esenciales, la cancelo
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

                    query.CommandText = " INSERT INTO FAMILIA_PATENTE (FP_PATENTE_ID,FP_FAMILIA_ID,FP_DVH) VALUES (@PATENTE,@FAMILIA,@DVH) ";
                    query.Parameters.Add(new SqlParameter("@PATENTE", System.Data.SqlDbType.BigInt)).Value = item.id;
                    query.Parameters.Add(new SqlParameter("@FAMILIA", System.Data.SqlDbType.BigInt)).Value = familia.id;
                    query.Parameters.Add(new SqlParameter("@DVH", System.Data.SqlDbType.NVarChar)).Value = this.recalcularDigitoHorizontal(new String[] { familia.id.ToString(), item.id.ToString() });
                    query.ExecuteNonQuery();
                }
                SqlCommand cmd = new SqlCommand("SELECT count(*) FROM PERMISOS_USUARIO WHERE PAT_ID = @PAT ");

                cmd.Connection = connection;
                cmd.Transaction = tx;
                SqlDataReader reader;
                Boolean seguirBuscando = true;
                int cantidad = 0;
                Boolean errorEsenciales = false;
                if (seguirBuscando)
                {
                    cmd.Parameters.Add(new SqlParameter("@PAT", System.Data.SqlDbType.BigInt)).Value = EnumPatentes.CrearUsuario;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cantidad = (int)reader.GetValue(0);
                        if (cantidad == 0)
                        {
                            errorEsenciales = true;
                            seguirBuscando = false;

                        }
                    }
                    reader.Close();
                }
                if (seguirBuscando)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@PAT", System.Data.SqlDbType.BigInt)).Value = EnumPatentes.ModificarUsuario;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cantidad = (int)reader.GetValue(0);
                        if (cantidad == 0)
                        {
                            errorEsenciales = true;
                            seguirBuscando = false;

                        }
                    }
                    reader.Close();

                }
                if (seguirBuscando)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@PAT", System.Data.SqlDbType.BigInt)).Value = EnumPatentes.ListarUsuarios;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cantidad=(int)reader.GetValue(0);
                        if (cantidad == 0)
                        {
                            errorEsenciales = true;
                            seguirBuscando = false;

                        }
                    }
                    reader.Close();
                }
                if (seguirBuscando)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@PAT", System.Data.SqlDbType.BigInt)).Value = EnumPatentes.GenerarBackups;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cantidad = (int)reader.GetValue(0);
                        if (cantidad == 0)
                        {
                            errorEsenciales = true;
                            seguirBuscando = false;

                        }
                    }
                    reader.Close();
                }
                if (seguirBuscando)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@PAT", System.Data.SqlDbType.BigInt)).Value = EnumPatentes.RestaurarBackup;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cantidad = (int)reader.GetValue(0);
                        if (cantidad == 0)
                        {
                            errorEsenciales = true;
                            seguirBuscando = false;

                        }
                    }
                    reader.Close();
                }
                if (seguirBuscando)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@PAT", System.Data.SqlDbType.BigInt)).Value = EnumPatentes.RecalcularDígitosVerificadores;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cantidad = (int)reader.GetValue(0);
                        if (cantidad == 0)
                        {
                            errorEsenciales = true;
                            seguirBuscando = false;

                        }
                    }
                    reader.Close();
                }
                if (seguirBuscando)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@PAT", System.Data.SqlDbType.BigInt)).Value = EnumPatentes.ModificarFamilias;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cantidad = (int)reader.GetValue(0);
                        if (cantidad == 0)
                        {
                            errorEsenciales = true;
                            seguirBuscando = false;

                        }
                    }
                    reader.Close();
                }
                if (seguirBuscando)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@PAT", System.Data.SqlDbType.BigInt)).Value = EnumPatentes.ListarFamilias;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cantidad = (int)reader.GetValue(0);
                        if (cantidad == 0)
                        {
                            errorEsenciales = true;
                            seguirBuscando = false;

                        }
                    }
                    reader.Close();

                }
                if (seguirBuscando)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@PAT", System.Data.SqlDbType.BigInt)).Value = EnumPatentes.CrearFamilia;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cantidad = (int)reader.GetValue(0);
                        if (cantidad == 0)
                        {
                            errorEsenciales = true;
                            seguirBuscando = false;

                        }
                    }
                    reader.Close();

                }
                if (errorEsenciales || cantidad == 0)
                {
                    throw new Exception("PERMISOS");
                }
                tx.Commit();
                connection.Close();

                this.recalcularDigitoVertical("FAMILIA");
                this.recalcularDigitoVertical("FAMILIA_PATENTE");
                Usuario usu = new Usuario();
                usu.id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
                this.grabarBitacora(usu, "Se modificó una familia ", CriticidadEnum.ALTA);
              
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
                connection.Close();
                throw ex;
            }
            finally
            {
              
              
            }

        }
        /// <summary>
        /// borrado de familia
        /// </summary>
        /// <param name="IdFamilia"></param>
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
                connection.Close();
                throw ex;
            }
            finally
            {
                connection.Close();
                this.recalcularDigitoVertical("FAMILIA");
                this.recalcularDigitoVertical("FAMILIA_PATENTE");
                Usuario usu = new Usuario();
                usu.id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
                this.grabarBitacora(usu, "Se borró una familia ", CriticidadEnum.ALTA);
            }

        }

        public Familia buscarFamilia(long idFamilia) { return null; } //deprecado
        /// <summary>
        /// listado de familias
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// listado de patentes
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// ver si la familia esta desasignada antes de borrarla.
        /// </summary>
        /// <param name="idFamilia"></param>
        /// <returns></returns>
        public Boolean chequearFamiliaDesasignada(long idFamilia) {

            bool desasignada = false;

            SqlConnection connection = ConexionSingleton.obtenerConexion();
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT COUNT(*) FROM USUARIO_FAMILIA inner join USUARIO USU ON USU.USU_ID = UF_USUARIO_ID WHERE UF_FAMILIA_ID = @IDFAMILIA  AND USU.USU_BAJA = 0");

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
        //cambio de contraseña
        public void cambiarContraseña(long idUsuario, String contraseñaNueva) {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            StringBuilder sb = new StringBuilder();
            sb.Append(" UPDATE USUARIO SET USU_PASS = @PASS, USU_INTENTOS = 0 WHERE USU_ID = @ID");

            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand query = new SqlCommand(sb.ToString(), connection, tx);
            query.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = idUsuario;
            query.Parameters.Add(new SqlParameter("@PASS", System.Data.SqlDbType.NVarChar)).Value = contraseñaNueva;

            SqlCommand dataQuery = new SqlCommand(" SELECT USU_ALIAS, USU_PASS, USU_INTENTOS,USU_DNI FROM USUARIO WHERE USU_ID = @ID", connection);
            dataQuery.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = idUsuario;

            SqlDataReader reader;
            try
            {
                query.ExecuteNonQuery();
                tx.Commit();
                String dvh = "";
                reader = dataQuery.ExecuteReader();
                while (reader.Read())
                {
                    string alias = reader.GetValue(0).ToString();
                    string pass = reader.GetValue(1).ToString();
                    string intentos = reader.GetValue(2).ToString();
                    string dni = reader.GetValue(3).ToString();
                    dvh = this.recalcularDigitoHorizontal(new string[] { alias, pass, intentos, dni });
                }
                reader.Close();
                tx = connection.BeginTransaction();
                SqlCommand updateDVH = new SqlCommand(" UPDATE USUARIO SET USU_DVH = @DVH WHERE USU_ID = @ID", connection, tx);
                updateDVH.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = idUsuario;
                updateDVH.Parameters.Add(new SqlParameter("@DVH", System.Data.SqlDbType.NVarChar)).Value = dvh;
                updateDVH.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                connection.Close();
                throw ex;
            }
            Usuario usu = new Usuario();
            usu.id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            this.grabarBitacora(usu, "Cambio de contraseña", CriticidadEnum.BAJA);
            this.recalcularDigitoVertical("USUARIO");
        }
        /// <summary>
        /// cambio del idioma del usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="codigoIdioma"></param>
        public void cambiarIdioma(long idUsuario, String codigoIdioma) {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();

            StringBuilder sb = new StringBuilder();
            sb.Append(" UPDATE USUARIO SET USU_IDIOMA = ( SELECT IDI_ID FROM IDIOMA WHERE IDI_CODIGO = @CODIGO) WHERE USU_ID = @ID");

            SqlCommand query = new SqlCommand(sb.ToString(), connection, tx);
            query.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = idUsuario;
            query.Parameters.Add(new SqlParameter("@CODIGO", System.Data.SqlDbType.VarChar)).Value = codigoIdioma;
            

            try
            {
                query.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
            }
            catch (Exception exe)
            {
                tx.Rollback();
                connection.Close();
                throw exe;
            }

        }
        /// <summary>
        /// chequeo de los campos unicos del usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Boolean chequearCamposUnicos(Usuario usuario)
        {
            bool repetido = false;

            SqlConnection connection = ConexionSingleton.obtenerConexion();
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT USU.USU_EMAIL , USU.USU_DNI, USU.USU_ALIAS ,USU.USU_ID FROM USUARIO USU WHERE USU_BAJA <> 1");
           
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
            //tengo que levantar los usuario por la manera en la que manejo aes, donde cada encriptacion es unica.
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
        /// <summary>
        /// regenero contraseña y seteo el contador de bloqueo en 1.
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="passEncriptado"></param>
        public void regenerarContraseña(long idUsuario, string passEncriptado) {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            StringBuilder sb = new StringBuilder();
            sb.Append(" UPDATE USUARIO SET USU_PASS = @PASS WHERE USU_ID = @ID");

            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand query = new SqlCommand(sb.ToString(), connection, tx);
            query.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = idUsuario;
            query.Parameters.Add(new SqlParameter("@PASS", System.Data.SqlDbType.NVarChar)).Value = passEncriptado;

            SqlCommand dataQuery = new SqlCommand(" SELECT USU_ALIAS, USU_PASS, USU_INTENTOS,USU_DNI FROM USUARIO WHERE USU_ID = @ID", connection);
            dataQuery.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = idUsuario;

            SqlDataReader reader;
            try
            {
                query.ExecuteNonQuery();
                tx.Commit();
                String dvh = "";
                reader = dataQuery.ExecuteReader();
                while (reader.Read())
                {
                    string alias = reader.GetValue(0).ToString();
                    string pass = reader.GetValue(1).ToString();
                    string intentos = reader.GetValue(2).ToString();
                    string dni = reader.GetValue(3).ToString();

                    dvh = this.recalcularDigitoHorizontal(new string[] { alias, pass, intentos, dni });

                }
                reader.Close();
                tx = connection.BeginTransaction();
                SqlCommand updateDVH = new SqlCommand(" UPDATE USUARIO SET USU_DVH = @DVH WHERE USU_ID = @ID",connection, tx);
                updateDVH.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = idUsuario;
                updateDVH.Parameters.Add(new SqlParameter("@DVH", System.Data.SqlDbType.NVarChar)).Value = dvh;
                updateDVH.ExecuteNonQuery();
                tx.Commit();

            }
            catch (Exception ex)
            {

                try
                {
                    tx.Rollback();

                }
                catch (Exception )
                {
                   
                }
                connection.Close();
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            Usuario usu = new Usuario();
            usu.id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            this.grabarBitacora(usu, "Se regeneró una contraseña", CriticidadEnum.MEDIA);
            this.recalcularDigitoVertical("USUARIO");
            //La regeneracion de la contraseña no implica un desbloqueo del usuario
            //desbloquearUsuario(idUsuario);


        }
        public Usuario buscarUsuario(long idUsuario) {

            SqlConnection connection = ConexionSingleton.obtenerConexion();
            SqlCommand query = new SqlCommand("", connection);
            query.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = idUsuario;

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT USU.*,IDI_ID,IDI_CODIGO FROM USUARIO USU inner join IDIOMA ON IDI_ID = USU.USU_IDIOMA WHERE USU.USU_ID = @ID");

            query.CommandText = sb.ToString();


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
            //traigo tambien los permisos del usuario.
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
        // bloqueo un usuario en el sistema
        public void bloquearUsuario(long idUsuario)
        {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            StringBuilder sb = new StringBuilder();
            sb.Append(" UPDATE USUARIO SET USU_INTENTOS = 3 WHERE USU_ID = @ID");

            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand query = new SqlCommand(sb.ToString(), connection, tx);
            query.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = idUsuario;

            SqlCommand dataQuery = new SqlCommand(" SELECT USU_ALIAS, USU_PASS, USU_INTENTOS,USU_DNI FROM USUARIO WHERE USU_ID = @ID", connection);
            dataQuery.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = idUsuario;

            SqlDataReader reader;
            try
            {
                query.ExecuteNonQuery();
                tx.Commit();
                String dvh = "";
                reader = dataQuery.ExecuteReader();
                while (reader.Read())
                {
                    string alias = reader.GetValue(0).ToString();
                    string pass = reader.GetValue(1).ToString();
                    string intentos = reader.GetValue(2).ToString();
                    string dni = reader.GetValue(3).ToString();

                    dvh = this.recalcularDigitoHorizontal(new string[] { alias, pass, intentos, dni });

                }
                reader.Close();
                tx = connection.BeginTransaction();
                SqlCommand updateDVH = new SqlCommand(" UPDATE USUARIO SET USU_DVH = @DVH WHERE USU_ID = @ID", connection, tx);
                updateDVH.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = idUsuario;
                updateDVH.Parameters.Add(new SqlParameter("@DVH", System.Data.SqlDbType.NVarChar)).Value = dvh;
                updateDVH.ExecuteNonQuery();
                tx.Commit();

            }
            catch (Exception ex)
            {
                
                try
                {
                    tx.Rollback();

                }
                catch (Exception )
                {
            
                }
                connection.Close();
                throw ex;
            }
            finally
            {
                connection.Close();
                Usuario usu = new Usuario();
                usu.id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
                this.grabarBitacora(usu, "Se bloquea un usuario ", CriticidadEnum.ALTA);
                this.recalcularDigitoVertical("USUARIO");
            }



        }

        /// <summary>
        /// desbloqueo un usuario, lo llamo despues del regenerar contraseña
        /// </summary>
        /// <param name="idUsuario"></param>
        public void desbloquearUsuario(long idUsuario) {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            StringBuilder sb = new StringBuilder();
            sb.Append(" UPDATE USUARIO SET USU_INTENTOS = 0 WHERE USU_ID = @ID");

            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand query = new SqlCommand(sb.ToString(), connection, tx);
            query.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = idUsuario;

            SqlCommand dataQuery = new SqlCommand(" SELECT USU_ALIAS, USU_PASS, USU_INTENTOS,USU_DNI FROM USUARIO WHERE USU_ID = @ID", connection);
            dataQuery.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = idUsuario;
            SqlDataReader reader;
           


            try
            {
                query.ExecuteNonQuery();
                tx.Commit();

                String dvh = "";
                reader = dataQuery.ExecuteReader();
                while (reader.Read())
                {
                    string alias = reader.GetValue(0).ToString();
                    string pass = reader.GetValue(1).ToString();
                    string intentos = reader.GetValue(2).ToString();
                    string dni = reader.GetValue(3).ToString();

                    dvh = this.recalcularDigitoHorizontal(new string[] { alias, pass, intentos, dni });

                }
                reader.Close();
                tx = connection.BeginTransaction();
                SqlCommand updateDVH = new SqlCommand(" UPDATE USUARIO SET USU_DVH = @DVH WHERE USU_ID = @ID", connection, tx);
                updateDVH.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = idUsuario;
                updateDVH.Parameters.Add(new SqlParameter("@DVH", System.Data.SqlDbType.NVarChar)).Value = dvh;
                updateDVH.ExecuteNonQuery();
                tx.Commit();

            }
            catch (Exception ex)
            {

                try
                {
                    tx.Rollback();

                }
                catch (Exception )
                {

                }
                connection.Close();
                throw ex;
            }
            finally
            {
                connection.Close();

            }
            Usuario usu = new Usuario();
            usu.id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            this.grabarBitacora(usu, "Se desbloquea un usuario ", CriticidadEnum.ALTA);
            this.recalcularDigitoVertical("USUARIO");
        }

        /// <summary>
        /// crear usuario
        /// </summary>
        /// <param name="usuario"></param>
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
            string pass = usuario.pass;
            string alias = SeguridadUtiles.encriptarAES(usuario.alias);
            query.Parameters.Add(new SqlParameter("@pass", System.Data.SqlDbType.VarChar)).Value = pass;
            query.Parameters.Add(new SqlParameter("@alias", System.Data.SqlDbType.NVarChar)).Value = alias;
            query.Parameters.Add(new SqlParameter("@nombre", System.Data.SqlDbType.VarChar)).Value = usuario.nombre;
            query.Parameters.Add(new SqlParameter("@apellido", System.Data.SqlDbType.VarChar)).Value = usuario.apellido;
            query.Parameters.Add(new SqlParameter("@direccion", System.Data.SqlDbType.VarChar)).Value = usuario.direccion;
            query.Parameters.Add(new SqlParameter("@telefono", System.Data.SqlDbType.VarChar)).Value = usuario.telefono;
            query.Parameters.Add(new SqlParameter("@dvh", System.Data.SqlDbType.VarChar)).Value = this.recalcularDigitoHorizontal(new string[] { alias, pass, 0.ToString(), usuario.dni });
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
                Usuario usu = new Usuario();
                usu.id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
                this.grabarBitacora(usu, "Crear usuario", CriticidadEnum.ALTA);
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
            this.recalcularDigitoVertical("USUARIO");
            actualizarPermisosUsuario(usuario, usuario.componentePermisos);
        }
        /// <summary>
        /// modificacion de un usuario
        /// </summary>
        /// <param name="usuario"></param>
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
            //trayendo el pass y los intentos.
            SqlCommand dataQuery = new SqlCommand(" SELECT USU_PASS, USU_INTENTOS FROM USUARIO WHERE USU_ID = @ID", connection,tx);
            dataQuery.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = usuario.id;
            SqlDataReader reader;


            string savedPass = "";
            string savedIntentos = "";
            reader = dataQuery.ExecuteReader();
            while (reader.Read())
            {
                savedPass = reader.GetValue(0).ToString();
                savedIntentos = reader.GetValue(1).ToString();
            }
            reader.Close();
            //agregando los paramentros 

            query.Parameters.Add(new SqlParameter("@dni", System.Data.SqlDbType.VarChar)).Value = usuario.dni;
            query.Parameters.Add(new SqlParameter("@email", System.Data.SqlDbType.VarChar)).Value = usuario.email;
            string alias = SeguridadUtiles.encriptarAES(usuario.alias);
            query.Parameters.Add(new SqlParameter("@alias", System.Data.SqlDbType.NVarChar)).Value = alias;
            query.Parameters.Add(new SqlParameter("@nombre", System.Data.SqlDbType.VarChar)).Value = usuario.nombre;
            query.Parameters.Add(new SqlParameter("@apellido", System.Data.SqlDbType.VarChar)).Value = usuario.apellido;
            query.Parameters.Add(new SqlParameter("@direccion", System.Data.SqlDbType.VarChar)).Value = usuario.direccion;
            query.Parameters.Add(new SqlParameter("@telefono", System.Data.SqlDbType.VarChar)).Value = usuario.telefono;
            query.Parameters.Add(new SqlParameter("@dvh", System.Data.SqlDbType.VarChar)).Value = this.recalcularDigitoHorizontal(new string[] { alias, savedPass, savedIntentos, usuario.dni });
            query.Parameters.Add(new SqlParameter("@idioma", System.Data.SqlDbType.BigInt)).Value = usuario.idioma.id;
            query.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.BigInt)).Value = usuario.id;

           
            try
            {

                int resultados = query.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
                Usuario usu = new Usuario();
                usu.id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
                this.grabarBitacora(usu, "Se modificó un usuario", CriticidadEnum.MEDIA);
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
            this.recalcularDigitoVertical("USUARIO");
            actualizarPermisosUsuario(usuario, usuario.componentePermisos);

        }
        /// <summary>
        /// borro un usuario del sistema.
        /// </summary>
        /// <param name="usuario"></param>
        public void borrarUsuario(Usuario usuario)
        {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            StringBuilder sb = new StringBuilder();
            ///tengo que borrar los datos de la familia porque me bloquea el borrado de la familia, porque el delete de la familia no es logico, sino fisico.
            sb.Append(" UPDATE  USUARIO SET USU_BAJA = 1 WHERE USU_ID = @ID DELETE FROM USUARIO_FAMILIA WHERE UF_USUARIO_ID =  @ID");

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
                connection.Close();
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            this.recalcularDigitoVertical("USUARIO_FAMILIA");

        }
        /// <summary>
        /// listado de usuario
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="valor"></param>
        /// <param name="orden"></param>
        /// <returns></returns>
        public List<Usuario> listarUsuarios(String filtro, String valor, String orden)
        {   
            
            List<Usuario> usuarios = new List<Usuario>();
            SqlConnection connection = ConexionSingleton.obtenerConexion();

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT usu_dni,usu_alias,usu_nombre,usu_apellido, usu_intentos, usu_id, idi_id, idi_codigo FROM USUARIO  ");
            sb.Append(" inner join IDIOMA on usu_idioma = idi_id");
            sb.Append(" where usu_baja <> 1 ");

            StringBuilder queryVista = new StringBuilder();
            queryVista.Append("SELECT * FROM PERMISOS_USUARIO WHERE USU_ID = @ID ");
            SqlCommand query = new SqlCommand("", connection);
            SqlCommand cmd = new SqlCommand(queryVista.ToString(), connection);
            query.CommandText = sb.ToString();

            

            connection.Open();
            SqlDataReader reader = null;
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
                reader.Close();
                foreach (Usuario user in usuarios)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt)).Value = user.id;
                    reader = cmd.ExecuteReader();
                    user.componentePermisos = new List<ComponentePermiso>();
                    while (reader.Read())
                    {
                        Patente pat = new Patente();
                        pat.id = (long)reader["PAT_ID"];
                        user.componentePermisos.Add(pat);
                    }
                    reader.Close();
                }
                
            }
           

            catch (Exception ex)
            {
                if(reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                connection.Close();
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
            mensaje = SeguridadUtiles.encriptarAES(mensaje);
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
            builder.Append(" @CRITICIDAD,");
            builder.Append(" @FECHA,");
            builder.Append(" @DVH");
            builder.Append(" ) ");
            SqlCommand cmd = new SqlCommand(builder.ToString(), connection, tx);
            DateTime fecha = DateTime.Now;
            cmd.Parameters.Add(new SqlParameter("@MENSAJE", System.Data.SqlDbType.Text)).Value = mensaje;
            cmd.Parameters.Add(new SqlParameter("@CRITICIDAD", System.Data.SqlDbType.BigInt)).Value = (long)criticidad;
            cmd.Parameters.Add(new SqlParameter("@FECHA", System.Data.SqlDbType.DateTime)).Value = fecha;
            cmd.Parameters.Add(new SqlParameter("@DVH", System.Data.SqlDbType.VarChar)).Value = this.recalcularDigitoHorizontal(new string[] { fecha.ToString(), mensaje, criticidad.ToString()});

            if(usuario!= null)
                cmd.Parameters.Add(new SqlParameter("@USUARIO", System.Data.SqlDbType.BigInt)).Value = usuario.id;

            try
            {
                cmd.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
                this.recalcularDigitoVertical("BITACORA");
            }
            catch (Exception ex)
            {
                try
                {
                    tx.Rollback();
                }
                catch (Exception)
                {

                   
                }
                connection.Close();
                throw ex;
            }
        }
        /// <summary>
        /// listo las cosas de la bitacora
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="valor"></param>
        /// <param name="orden"></param>
        /// <returns></returns>
        public DataSet listarBitacora(String filtro, String valor, String orden) {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand("", connection, tx);
            String query = " SELECT * FROM vistaBitacora";
            if(filtro != null)
            {
                switch (filtro)
                {
                    case "FECHA":
                        query += " where bit_fecha between @from and @to";
                        cmd.Parameters.Add(new SqlParameter("@from", System.Data.SqlDbType.VarChar)).Value = valor.Split(';')[0];
                        cmd.Parameters.Add(new SqlParameter("@to", System.Data.SqlDbType.VarChar)).Value = valor.Split(';')[1];
                        break;
                    case "CRITICIDAD":
                        query += " where bit_criticidad_id = @criticidad";
                        cmd.Parameters.Add(new SqlParameter("@criticidad", System.Data.SqlDbType.Int)).Value = int.Parse(valor);
                        break;

                }
            }

            cmd.CommandText = query;

            
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
                connection.Close();
                throw;
            }
            foreach (DataRow item in set.Tables[0].Rows)
            {
    
                    item.BeginEdit();
                
                if(!item.ItemArray[1].GetType().Equals(typeof(DBNull)))
                {
                    String usuario = SeguridadUtiles.desencriptarAES(item.ItemArray[1].ToString());
                    item.ItemArray[1] = usuario;
                    item.SetField(1, usuario);
                }
                item.SetField(3, SeguridadUtiles.desencriptarAES(item.ItemArray[3].ToString()));
                item.EndEdit();
                
            }


            if(filtro == "ALIAS")
            {
                DataSet filteredSet = new DataSet();
                filteredSet = set.Clone();
                filteredSet.Tables[0].Rows.Clear();
                DataTable table = filteredSet.Tables[0];

                foreach (DataRow item in set.Tables[0].Rows)
                {
                    if (item.ItemArray[1].ToString().Equals(valor))
                    {
                        table.Rows.Add(item.ItemArray);
  
                    }
                }
                set = filteredSet;
            }
            return set;

            
        }

        //DIGITOS VERIFICADORES
        /// <summary>
        /// verificar digitos verificadores en el startup de la applicacion.
        /// </summary>
        public void verificarDigitosVerificadores() {

            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tr = connection.BeginTransaction();
            SqlDataReader reader = null;
            SqlCommand cmd = new SqlCommand("",connection,tr);
            Dictionary<String, String> digitoVerticalCalculado = new Dictionary<string, string>();

            StringBuilder stringParaDVH = new StringBuilder();
            StringBuilder builder = new StringBuilder();
            List<String> mensajesDeError = new List<string>(); // la lista en donde voy a cargar todos los mensajes de error
            //por validacion de digitos verificadores.
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
                    long id = (long) reader.GetValue(0);
                    mensajesDeError.Add("Falló la integridad de datos en usuario en el id " + id.ToString());
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
                    long id =  (long)reader.GetValue(0);
                    mensajesDeError.Add("Falló la integridad de datos en patente en el id " + id.ToString());

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
                    long id = (long) reader.GetValue(0);
                    mensajesDeError.Add("Falló la integridad de datos en bitácora en el id " + id.ToString());
                    
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
                if(!reader.IsDBNull(1))
                    builder.Append(reader.GetValue(1).ToString());
                if (!reader.IsDBNull(2))
                    builder.Append(reader.GetValue(2).ToString());
                if (!reader.IsDBNull(3))
                    builder.Append(reader.GetValue(3).ToString());
                builder.Append(reader.GetValue(4).ToString());

                String md5 = SeguridadUtiles.encriptarMD5(builder.ToString());
                String patDVH = reader.GetString(5);

                if (!md5.Equals(patDVH))
                {
                    long id = (long)reader.GetValue(0);
                    mensajesDeError.Add("Falló la integridad de datos en planilla en el id " + id.ToString());
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
                    long id = (long)reader.GetValue(0);
                    mensajesDeError.Add("Falló la integridad de datos en familia en el id " + id.ToString());
                }

                stringParaDVH.Append(md5);
            }

            digitoVerticalCalculado.Add("FAMILIA", stringParaDVH.ToString());
            reader.Close();
            stringParaDVH.Clear();


            //// DE ESTOS NO NECESITO EL ID PORQUE SON TABLAS RELACIONALES O ACCEDO POR PK NATURAL.
            //mapeoTablaCampo.Add("INASISTENCIA_DE_ALUMNO", new KeyValuePair<String, String[]>("INA_DVH", new String[] { "INA_ALUMNO_ID", "INA_FECHA", "INA_VALOR", "INA_JUSTIFICADA" }));


            query = "SELECT INA_ALUMNO_ID, INA_FECHA, INA_DVH , ROW_NUMBER() over ( order by (select 1)) as rowid FROM INASISTENCIA_DE_ALUMNO ";

            cmd.CommandText = query;
            builder = new StringBuilder();

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                builder.Clear();

                builder.Append(reader.GetValue(0).ToString());
                builder.Append(DateTime.Parse(reader.GetValue(1).ToString()).ToString("yyyy-MM-dd"));

                String md5 = SeguridadUtiles.encriptarMD5(builder.ToString());
                String patDVH = reader.GetString(2);

                if (!md5.Equals(patDVH))
                {
                    long id = (long) reader["rowid"];
                    mensajesDeError.Add("Falló la integridad de datos en inasistencia alumno en el row " + id.ToString());
                }

                stringParaDVH.Append(md5);
            }

            digitoVerticalCalculado.Add("INASISTENCIA_DE_ALUMNO", stringParaDVH.ToString());
            reader.Close();
            stringParaDVH.Clear();

            //mapeoTablaCampo.Add("AMONESTACION", new KeyValuePair<String, String[]>("AMON_DVH", new String[] { "AMON_ALUMNO_ID", "AMON_FECHA", "AMON_MOTIVO" }));

            query = "SELECT AMON_ALUMNO_ID, AMON_FECHA,AMON_DVH, ROW_NUMBER() over ( order by (select 1)) as rowid  FROM AMONESTACION ";

            cmd.CommandText = query;
            builder = new StringBuilder();

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                builder.Clear();

                builder.Append(reader.GetValue(0).ToString());
                builder.Append(DateTime.Parse(reader.GetValue(1).ToString()).ToString("yyyy-MM-dd"));


                String md5 = SeguridadUtiles.encriptarMD5(builder.ToString());
                String patDVH = reader.GetString(2);

                if (!md5.Equals(patDVH))
                {
                    long id = (long)reader["rowid"];
                    mensajesDeError.Add("Falló la integridad de datos en amonestacion en el row " + id.ToString());
                }

                stringParaDVH.Append(md5);
            }

            digitoVerticalCalculado.Add("AMONESTACION", stringParaDVH.ToString());
            reader.Close();
            stringParaDVH.Clear();
            //mapeoTablaCampo.Add("USUARIO_FAMILIA", new KeyValuePair<String, String[]>("UF_DVH", new String[] { "UF_USUARIO_ID", "UF_FAMILIA_ID" }));


            query = "SELECT UF_FAMILIA_ID, UF_USUARIO_ID, UF_DVH, ROW_NUMBER() over ( order by (select 1)) as rowid  FROM USUARIO_FAMILIA ";

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
                    long id = (long)reader["rowid"];
                    mensajesDeError.Add("Falló la integridad de datos en unsuario familia en el row " + id.ToString());
                }

                stringParaDVH.Append(md5);
            }

            digitoVerticalCalculado.Add("USUARIO_FAMILIA", stringParaDVH.ToString());
            reader.Close();
            stringParaDVH.Clear();
            //mapeoTablaCampo.Add("USUARIO_PATENTE", new KeyValuePair<String, String[]>("UP_DVH", new String[] { "UP_USUARIO_ID", "UP_PATENTE_ID", "UP_BLOQUEADA" }));


            query = "SELECT UP_USUARIO_ID, UP_PATENTE_ID,UP_BLOQUEADA,UP_DVH, ROW_NUMBER() over ( order by (select 1)) as rowid  FROM USUARIO_PATENTE ";

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
                    long id = (long)reader["rowid"];
                    mensajesDeError.Add("Falló la integridad de datos en usuario_patente en el row " + id.ToString());
                }

                stringParaDVH.Append(md5);
            }

            digitoVerticalCalculado.Add("USUARIO_PATENTE", stringParaDVH.ToString());
            reader.Close();
            stringParaDVH.Clear();
            //mapeoTablaCampo.Add("FAMILIA_PATENTE", new KeyValuePair<String, String[]>("FP_DVH", new String[] { "FP_PATENTE_ID", "FP_FAMILIA_ID" }));

            query = "SELECT FP_FAMILIA_ID, FP_PATENTE_ID,FP_DVH, ROW_NUMBER() over ( order by (select 1)) as rowid  FROM FAMILIA_PATENTE ";

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
                    long id = (long)reader["rowid"];
                    mensajesDeError.Add("Falló la integridad de datos en familia patente en el row " + id.ToString());
                }

                stringParaDVH.Append(md5);
            }

            digitoVerticalCalculado.Add("FAMILIA_PATENTE", stringParaDVH.ToString());

            reader.Close();
            stringParaDVH.Clear();

            query = " SELECT DV_NOMBRE_TABLA,DV_DIGITO_CALCULADO,dv_id FROM DIGITO_VERTICAL ";

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
                    long id = (long) reader.GetValue(2);
                    mensajesDeError.Add("Falló la integridad de datos en digito vertical en el row " + id.ToString());
                }

            }
            reader.Close();
            connection.Close();

            //chequeo si hubo errores, si los hubo tiro la excepcion y logueo todo

            if(mensajesDeError.Count > 0)
            {
                foreach (String item in mensajesDeError)
                {
                    //usuario custom para logueo de digitos verificadores
                    Usuario usu = new Usuario();
                    usu.id = 2L;
                    this.grabarBitacora(usu, item, CriticidadEnum.ALTA);
                }
                throw new Exception("Falló la integridad de datos.");
            }
        }


        /// <summary>
        /// recalcula todos los digitos que haya en la base de datos.
        /// tengo 2 tipos de tablas , las que no tiene clave propia o son intermedias, y las que tienen id secuencial
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
                        //las tablas sin id las trato con procedures en base de datos.
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
                        if (!String.IsNullOrEmpty(cmd.CommandText))
                            cmd.ExecuteNonQuery();
                       
                    }
                    catch (Exception ex)
                    {
                        
                        tx.Rollback();
                        connection.Close();

                        throw ex;
                    }
                    
                    
                    
                
                }
                
                
            }

            tx.Commit();
            connection.Close();
            Usuario usu = new Usuario();
            usu.id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            this.grabarBitacora(usu, "Se recalcularon los dígitos verificadores ", CriticidadEnum.ALTA);

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
                builder.Append(campos[x]);
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

            sb.Append(" SELECT "+ campo +" FROM " +tabla );
            

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
                        sumaDeDVH += reader.GetString(0);
                    }
                }
                reader.Close();
                
                    query.Parameters.Clear();
                    query.CommandText = updateCommand;
                    query.Parameters.Add(new SqlParameter("@HASH", System.Data.SqlDbType.VarChar)).Value = SeguridadUtiles.encriptarMD5(sumaDeDVH);
                    query.Parameters.Add(new SqlParameter("@NOMBRE_TABLA", System.Data.SqlDbType.VarChar)).Value = tabla;

                    query.ExecuteNonQuery();
                
                tx.Commit();
                connection.Close();
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
                connection.Close();
                throw ex;
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
            directorio = directorio.Replace("//", "\\");
            queryText.Append(" USE MASTER ");
            queryText.Append(" BACKUP DATABASE TRABAJO_DIPLOMA ");

            for (int i = 0; i < partes; i++)
            {
                if (i == 0)
                {
                    queryText.Append(" TO DISK = '" + directorio + (i+1) +".bak '");
                }
                else
                {
                    queryText.Append(" , DISK = '" + directorio + (i + 1) + ".bak '");
                }
            }

            queryText.Append(" WITH init ");
            SqlCommand query = new SqlCommand(queryText.ToString(), connection);
            try
            {
                query.ExecuteNonQuery();
                connection.Close();
                Usuario usu = new Usuario();
                usu.id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
                this.grabarBitacora(usu, "Se realizó un backup", CriticidadEnum.MEDIA);

            }
            catch (Exception e)
            {
                Usuario usu = new Usuario();
                usu.id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
                connection.Close();
                this.grabarBitacora(usu, "Falló un backup", CriticidadEnum.ALTA);
                throw e;
            }
        }

        public void realizarRestore(String directorio)

        {
            String cs = Encoding.UTF8.GetString(Convert.FromBase64String(TrabajoDeCampo.Properties.Settings.Default.MasterString));
            SqlConnection connection = new SqlConnection(cs);
            connection.Open();
            StringBuilder queryText = new StringBuilder();
            queryText.Append(" USE MASTER ");

            queryText.Append(" alter database [TRABAJO_DIPLOMA]  ");
            queryText.Append(" set offline with rollback immediate ");
            queryText.Append(" RESTORE DATABASE TRABAJO_DIPLOMA ");
            //FROM  DISK = '" + directorio + "' WITH REPLACE");
            queryText.Append(directorio);
            queryText.Append(" WITH REPLACE ");
            queryText.Append(" alter database [TRABAJO_DIPLOMA]  ");
            queryText.Append(" set online with rollback immediate ");
            SqlCommand query = new SqlCommand(queryText.ToString(), connection);
            try
            {
                query.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {

                Usuario usuario = new Usuario();
                usuario.id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;

                connection.Close();
                this.grabarBitacora(usuario, "Falló un restore", CriticidadEnum.ALTA);
                throw e;
            }

            Usuario usu = new Usuario();
            usu.id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            this.grabarBitacora(usu, "Se realizó un restore", CriticidadEnum.ALTA);

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

        public Boolean actualizaConexión() // deprecado
        {
            throw new System.NotImplementedException();
        }
    }
}
