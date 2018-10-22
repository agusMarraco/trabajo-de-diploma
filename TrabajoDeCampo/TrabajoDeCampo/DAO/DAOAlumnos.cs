using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using TrabajoDeCampo.SEGURIDAD;

namespace TrabajoDeCampo.DAO
{
    public class DAOAlumnos
    {
        //recibe TUTOR o ALUMNO
        public Boolean verificarDNI(String dni, String tipo, long entityId) {
            StringBuilder query = new StringBuilder();
            query.Append(" select count(*) from ");
            if (tipo == "tutor")
            {
                query.Append(" tutor where tut_dni = @dni ");
                if(entityId!= 0)
                {
                    query.Append(" and tut_id <> @id ");
                }
            }
            else
            {
                query.Append(" alumno where alu_dni = @dni and alu_borrado is null ");
                if (entityId != 0)
                {
                    query.Append(" and alu_legajo <> @id ");
                }
            }

            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();

            Boolean repetido = true;   

            SqlCommand cmd = new SqlCommand(query.ToString(), connection, tx);
            cmd.Parameters.Add(new SqlParameter("@dni", SqlDbType.VarChar)).Value = dni;
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.BigInt)).Value = entityId;

            SqlDataReader reader = null;

            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int count = (int)reader.GetValue(0);
                    if(count == 0)
                    {
                        repetido = false;
                    }
                }
                reader.Close();
                tx.Commit();
                connection.Close();
            }
            catch (Exception ex )
            {
                reader.Close();
                tx.Rollback();
                connection.Close();
                throw ex;
            }
            return repetido;
        }


        // listar orientaciones
        public List<Orientacion> listarOrientacion() {

            SqlConnection connection = ConexionSingleton.obtenerConexion();
            List<Orientacion> orientaciones = new List<Orientacion>();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand(" Select * from Orientacion", connection, tx);
            
            SqlDataReader reader = null;
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Orientacion orientacion = new Orientacion();
                    orientacion.codigo = reader["ORI_CODIGO"].ToString();
                    orientacion.nombre = reader["ORI_NOMBRE"].ToString();
                    orientaciones.Add(orientacion);
                }

                reader.Close();
                tx.Commit();
                connection.Close();
            }
            catch (Exception ex)
            {
                if (reader != null)
                    reader.Close();
                tx.Rollback();
                connection.Close();
                throw ex;
            }
            return orientaciones;
        }
        
        //alumnos
        public void guardarAlumno(Alumno alumno) {

            SqlConnection connection = ConexionSingleton.obtenerConexion();

            StringBuilder sb = new StringBuilder();
            sb.Append(" insert into alumno (alu_apellido,alu_nombre,alu_fecha_nacimiento,alu_dni,alu_curso,alu_domicilio ");
            if (alumno.orientacion != null)
                sb.Append(" ,alu_orientacion");
            sb.Append(" ) ");
            sb.Append(" OUTPUT Inserted.alu_legajo values( ");
            sb.Append(" @apellido, ");
            sb.Append(" @nombre, ");
            sb.Append(" @fecha, ");
            sb.Append(" @dni, ");
            sb.Append(" @curso, ");
            sb.Append(" @domicilio ");
            if (alumno.orientacion != null)
                sb.Append(" ,@orientacion");
            sb.Append(" ) ");

            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand("", connection, tx);
            cmd.Parameters.Add(new SqlParameter("@apellido", SqlDbType.VarChar)).Value = alumno.apellido;
            cmd.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar)).Value = alumno.nombre;
            cmd.Parameters.Add(new SqlParameter("@fecha", SqlDbType.DateTime)).Value = alumno.fechaNacimiento;
            cmd.Parameters.Add(new SqlParameter("@dni", SqlDbType.VarChar)).Value = alumno.dni;
            cmd.Parameters.Add(new SqlParameter("@curso", SqlDbType.BigInt)).Value = alumno.curso.id;
            cmd.Parameters.Add(new SqlParameter("@domicilio", SqlDbType.VarChar)).Value = SeguridadUtiles.encriptarAES(alumno.domicilio);
            if(alumno.orientacion != null)
                cmd.Parameters.Add(new SqlParameter("@orientacion", SqlDbType.VarChar)).Value = alumno.orientacion.codigo ;

            cmd.CommandText = sb.ToString();
            SqlDataReader reader = null;
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    alumno.legajo = (long)reader.GetValue(0);
                }
                reader.Close();
                

                
                sb.Clear();
                int cantidad = 0;
                foreach (Tutor tutor in alumno.tutores)
                {
                    cantidad++;
                    cmd.Parameters.Add(new SqlParameter("@tut" + cantidad, SqlDbType.BigInt)).Value = tutor.id;
                    sb.Append(" insert into alumno_tutor (at_alumno_id,at_tutor_id) values (@alumno,@tut" + cantidad + ") ");
                }

                cmd.Parameters.Add(new SqlParameter("@alumno", SqlDbType.BigInt)).Value = alumno.legajo;

                cmd.CommandText = sb.ToString();
                cmd.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
                Usuario usu = new Usuario();
                usu.id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
                new DAOSeguridad().grabarBitacora(usu, "Se creó alumno", CriticidadEnum.MEDIA);
                new DAOAdministracion().generarPlanillasDeEvaluacion(alumno);
            }
            catch (Exception ex)
            {
                tx.Rollback();
                connection.Close();
                throw;
            }
        }
        public void actualizarAlumno(Alumno alumno) {
            SqlConnection connection = ConexionSingleton.obtenerConexion();

            String deleteQuery = " delete from alumno_tutor where at_alumno_id = @id ";

            StringBuilder sb = new StringBuilder();
            sb.Append(" update alumno set " +
                "alu_apellido           = @apellido," +
                "alu_nombre             = @nombre," +
                "alu_fecha_nacimiento   = @fecha," +
                "alu_dni                = @dni," +
                "alu_curso              = @curso," +
                "alu_domicilio          = @domicilio," +
                "alu_orientacion        = @orientacion ");

            sb.Append(" where alu_legajo = @id ");
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand("", connection, tx);
            cmd.Parameters.Add(new SqlParameter("@apellido", SqlDbType.VarChar)).Value = alumno.apellido;
            cmd.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar)).Value = alumno.nombre;
            cmd.Parameters.Add(new SqlParameter("@fecha", SqlDbType.DateTime)).Value = alumno.fechaNacimiento;
            cmd.Parameters.Add(new SqlParameter("@dni", SqlDbType.VarChar)).Value = alumno.dni;
            cmd.Parameters.Add(new SqlParameter("@curso", SqlDbType.BigInt)).Value = alumno.curso.id;
            cmd.Parameters.Add(new SqlParameter("@domicilio", SqlDbType.VarChar)).Value = SeguridadUtiles.encriptarAES(alumno.domicilio);
            cmd.Parameters.Add(new SqlParameter("@orientacion", SqlDbType.VarChar)).Value = alumno.orientacion != null ? alumno.orientacion.codigo : Convert.DBNull;
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.VarChar)).Value = alumno.legajo;
            cmd.CommandText = sb.ToString();
          
            try
            {

                cmd.ExecuteNonQuery();
                cmd.CommandText = deleteQuery;
                cmd.ExecuteNonQuery();

                sb.Clear();
                int cantidad = 0;
                foreach (Tutor tutor in alumno.tutores)
                {
                    cantidad++;
                    cmd.Parameters.Add(new SqlParameter("@tut" + cantidad, SqlDbType.BigInt)).Value = tutor.id;
                    sb.Append(" insert into alumno_tutor (at_alumno_id,at_tutor_id) values (@id,@tut" + cantidad + ") ");
                }
                cmd.CommandText = sb.ToString();
                if(sb.ToString().Length > 0)
                    cmd.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
                Usuario usu = new Usuario();
                usu.id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
                new DAOSeguridad().grabarBitacora(usu, "Se modificó un alumno", CriticidadEnum.MEDIA);
            }
            catch (Exception ex)
            {
                tx.Rollback();
                connection.Close();
                throw;
            }

        }
        public void borrarAlumno(Alumno alumno) {
            SqlConnection connection = ConexionSingleton.obtenerConexion();

            String deleteQuery = " update alumno set alu_borrado = 1 where alu_legajo = @id ";
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand(deleteQuery, connection, tx);
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.VarChar)).Value = alumno.legajo;
            try
            {
                cmd.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
                Usuario usu = new Usuario();
                usu.id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
                new DAOSeguridad().grabarBitacora(usu, "Se borró alumno", CriticidadEnum.ALTA);
            }
            catch (Exception ex)
            {
                tx.Rollback();
                connection.Close();
                throw;
            }

        }

        public void repetirAlumno(Alumno alumno) {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            DateTime principioDeAño = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime finDeAño = new DateTime(DateTime.Now.Year, 12, 31);

            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();

            SqlCommand cmd = new SqlCommand(" update planilla_de_evaluacion set " +
                " pde_trimestre_1 = @valor, pde_trimestre_2 = @valor, pde_trimestre_3 = @valor, " +
                " pde_condicion = @valor, pde_nota_final = @valor, pde_dvh =@dvh " +
                " where pde_alumno_id = @alumno and pde_nivel_id = @nivel ",connection, tx);

            cmd.Parameters.Add(new SqlParameter("@valor", SqlDbType.NVarChar)).Value = Convert.DBNull;
            cmd.Parameters.Add(new SqlParameter("@dvh", SqlDbType.NVarChar)).Value = SeguridadUtiles.encriptarMD5(alumno.legajo.ToString());
            cmd.Parameters.Add(new SqlParameter("@alumno", SqlDbType.BigInt)).Value = alumno.legajo;
            cmd.Parameters.Add(new SqlParameter("@nivel", SqlDbType.BigInt)).Value = alumno.curso.nivel.id;
            cmd.Parameters.Add(new SqlParameter("@principio", SqlDbType.Date)).Value = principioDeAño;
            cmd.Parameters.Add(new SqlParameter("@fin", SqlDbType.Date)).Value = finDeAño;

            String updateInasistencias = " delete inasistencia_de_alumno where ina_alumno_id = @alumno and ina_fecha between @principio and @fin ";
            cmd.CommandText += updateInasistencias;
            try
            {
                cmd.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
                DAOSeguridad dao = new DAOSeguridad();
                dao.recalcularDigitoVertical("PLANILLA_DE_EVALUACION");
                dao.recalcularDigitoVertical("INASISTENCIA_DE_ALUMNO");
                Usuario usu = new Usuario();
                usu.id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
                new DAOSeguridad().grabarBitacora(usu, "Se marcó un alumno como repetidor", CriticidadEnum.ALTA);
            }
            catch (Exception ex)
            {
                tx.Rollback();
                connection.Close();
                throw;
            }


        }

        public List<Alumno> listarAlumno(String filtro, String valor, String orden) {

            List<Alumno> alumnos = new List<Alumno>();
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            DateTime principioDeAño = new DateTime(DateTime.Now.Year,1,1);
            DateTime finDeAño = new DateTime(DateTime.Now.Year, 12, 31);


            String queryInasistencias = " (select SUM(INA_VALOR) as inasistencias from inasistencia_de_alumno where ina_alumno_id = alu.alu_legajo and " +
                "ina_justificada <> 1 and ina_fecha between @princio and @fin " +
                "group by ina_alumno_id) , ";
            StringBuilder sb = new StringBuilder();
            sb.Append(" select alu.alu_legajo, alu.alu_apellido, alu.alu_nombre, alu.alu_fecha_nacimiento, alu.alu_dni, alu.alu_curso, " +
                " alu.alu_domicilio, alu.alu_orientacion, " +
                " ori.ori_codigo, cur.cur_codigo , cur.cur_id, cur.cur_nivel_id, nivel.niv_codigo,ORI.ORI_NOMBRE, " +
                queryInasistencias +
                " (select count(*) from planilla_de_evaluacion where pde_alumno_id = alu.alu_legajo and pde_condicion = 0) as desaprobadas ");
            sb.Append(" from alumno alu ");
            sb.Append(" left join orientacion ori on ori.ori_codigo = alu.alu_orientacion ");
            sb.Append(" inner join curso cur on cur.cur_id = alu.alu_curso ");
            sb.Append(" inner join nivel nivel on cur.cur_nivel_id = nivel.niv_id");
            sb.Append(" where alu.alu_borrado is null ");
            if(!String.IsNullOrEmpty(filtro) && !String.IsNullOrEmpty(valor))
            {
                switch (filtro)
                {
                    case "nombre":
                        sb.Append(" and alu.alu_nombre = @valor ");
                        break;
                    case "apellido":
                        sb.Append(" and alu.alu_apellido = @valor ");
                        break;
                    case "dni":
                        sb.Append(" and alu.alu_dni = @valor ");
                        break;
                    case "curso":
                        sb.Append(" and cur.cur_codigo = @valor ");
                        break;

                }
            }
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand(sb.ToString(), connection, tx);
            cmd.Parameters.Add(new SqlParameter("@princio", SqlDbType.Date)).Value = principioDeAño;
            cmd.Parameters.Add(new SqlParameter("@fin", SqlDbType.Date)).Value = finDeAño;
            if (!String.IsNullOrEmpty(filtro) && !String.IsNullOrEmpty(valor))
            {
                cmd.Parameters.Add(new SqlParameter("@valor", SqlDbType.VarChar)).Value = valor;
            }

                SqlDataReader reader = null;
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Alumno alu = new Alumno();
                    Orientacion ori = new Orientacion();
                    Curso cur = new Curso();
                    alu.legajo = (long)reader["ALU_LEGAJO"];
                    alu.apellido = reader["ALU_APELLIDO"].ToString();
                    alu.nombre = reader["ALU_NOMBRE"].ToString();
                    alu.fechaNacimiento = DateTime.Parse(reader["ALU_FECHA_NACIMIENTO"].ToString());
                    alu.dni = reader["ALU_DNI"].ToString();
                    cur.id = (long)reader["CUR_ID"];
                    cur.codigo = reader["CUR_CODIGO"].ToString();
                    cur.nivel = new Nivel();
                    cur.nivel.id = (long)reader["CUR_NIVEL_ID"];
                    cur.nivel.codigo = reader["NIV_CODIGO"].ToString();
                    alu.curso = cur;
                    alu.domicilio = SeguridadUtiles.desencriptarAES(reader["ALU_DOMICILIO"].ToString());
                    decimal faltas = reader.IsDBNull(14) ? 0 : (decimal)reader[14];
                    alu.puedeRepetir = (1 < faltas) || 2 < (int)reader["desaprobadas"];
                    if (reader.IsDBNull(8))
                    {
                        ori.codigo = "null";
                    }
                    else
                    {
                         ori.codigo = reader["ORI_CODIGO"].ToString();
                        ori.nombre = reader["ORI_NOMBRE"].ToString();
                    }
                   
                    alu.orientacion = ori;
                    alumnos.Add(alu);
                }

                reader.Close();

                String tutoresQuery = " select tut_id from alumno_tutor inner join tutor on at_tutor_id = tut_id where at_alumno_id = @id ";
                cmd.CommandText = tutoresQuery;

                foreach (Alumno alu in alumnos)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.BigInt)).Value = alu.legajo;
                    reader = cmd.ExecuteReader();
                    alu.tutores = new List<Tutor>();
                    while (reader.Read())
                    {
                        Tutor tut = new Tutor();
                        tut.id = (long)reader["TUT_ID"];
                        alu.tutores.Add(tut);
                    }
                    reader.Close();
                    

                }
                reader.Close();
                
                tx.Commit();
                connection.Close();
            }
            catch (Exception ex)
            {
                if (reader != null)
                    reader.Close();
                tx.Rollback();
                connection.Close();
                throw ex;
            }
            return alumnos; }

        public Alumno buscarAlumno(long id) { return new Alumno(); }
        //amonestaciones
        public void guardarAmonestacion(Amonestacion amonestacion) {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();

            StringBuilder sb = new StringBuilder();
            sb.Append(" insert into amonestacion ");
            sb.Append(" (amon_alumno_id,amon_fecha,amon_motivo,amon_dvh) ");
            sb.Append(" values(@alu,@fecha,@motivo,@dvh) ");

            String dvh = new DAOSeguridad().recalcularDigitoHorizontal(new String[] { amonestacion.alumno.legajo.ToString(),
                amonestacion.fecha.ToString("yyyy-MM-dd")});

            SqlCommand cmd = new SqlCommand(sb.ToString(), connection, tx);
            cmd.Parameters.Add(new SqlParameter("@alu", SqlDbType.BigInt)).Value = amonestacion.alumno.legajo;
            cmd.Parameters.Add(new SqlParameter("@fecha", SqlDbType.Date)).Value = amonestacion.fecha;
            cmd.Parameters.Add(new SqlParameter("@motivo", SqlDbType.Text)).Value =  SeguridadUtiles.encriptarAES(amonestacion.motivo);
            cmd.Parameters.Add(new SqlParameter("@dvh", SqlDbType.VarChar)).Value = dvh;
            

            try
            {
                String check = " Select count(*) from amonestacion where amon_alumno_id = @alu and amon_fecha = @fecha";
                cmd.CommandText = check;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int count = (int)reader.GetValue(0);
                    if (count > 0)
                    {
                        reader.Close();
                        throw new Exception("FECHA");
                    }

                }
                reader.Close();
                cmd.CommandText = sb.ToString();
                cmd.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
                Usuario usu = new Usuario();
                usu.id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
                new DAOSeguridad().grabarBitacora(usu, "Se registro una amonestación", CriticidadEnum.MEDIA);
            }
            catch (Exception)
            {
                tx.Rollback();
                connection.Close();
                throw;
            }

        }

        public List<Amonestacion> listarAmonestaciones(String filtro, String valor, String orden) {
            SqlDataReader reader = null;
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            List<Amonestacion> amonestaciones = new List<Amonestacion>();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand("SELECT * FROM AMONESTACION WHERE AMON_ALUMNO_ID = @ID ", connection, tx);
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt)).Value = long.Parse(valor);

            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Amonestacion amonestacion = new Amonestacion();
                    amonestacion.fecha = DateTime.Parse(reader.GetValue(1).ToString());
                    amonestacion.motivo = SeguridadUtiles.desencriptarAES(reader.GetValue(2).ToString());
                    amonestaciones.Add(amonestacion);
                }
                reader.Close();
                tx.Commit();
                connection.Close();
                new DAOSeguridad().recalcularDigitoVertical("AMONESTACION");
            }
            catch (Exception)
            {
                if (reader != null)
                    reader.Close();
                tx.Rollback();
                connection.Close();
                throw;
            }

            return amonestaciones;


        }

        //Inasistencias

        public void guardarInasistencia(InasistenciaAlumno inasistencia) {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            StringBuilder sb = new StringBuilder();
            sb.Append(" insert into inasistencia_de_alumno ");
            sb.Append(" (ina_alumno_id,ina_fecha,ina_valor,ina_dvh,ina_justificada) ");
            sb.Append(" values(@alu,@fecha,@valor,@dvh,@justificada) ");

            String dvh = new DAOSeguridad().recalcularDigitoHorizontal(new String[] { inasistencia.Alumno.legajo.ToString(),
                inasistencia.fecha.ToString("yyyy-MM-dd")});

            SqlCommand cmd = new SqlCommand(sb.ToString(), connection, tx);
            cmd.Parameters.Add(new SqlParameter("@alu", SqlDbType.BigInt)).Value = inasistencia.Alumno.legajo;
            cmd.Parameters.Add(new SqlParameter("@fecha", SqlDbType.DateTime)).Value = inasistencia.fecha.ToString("yyyy-MM-dd");
            cmd.Parameters.Add(new SqlParameter("@valor", SqlDbType.Decimal)
            { Precision = 2
            }).Value =inasistencia.valor;
            cmd.Parameters.Add(new SqlParameter("@dvh", SqlDbType.VarChar)).Value = dvh;
            cmd.Parameters.Add(new SqlParameter("@justificada", SqlDbType.Bit)).Value = inasistencia.Justificada;

            try
            {
                cmd.CommandText = sb.ToString();


                cmd.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
                Usuario usu = new Usuario();
                usu.id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
                new DAOSeguridad().grabarBitacora(usu, "Se guardó una inasistencia", CriticidadEnum.BAJA);
                new DAOSeguridad().recalcularDigitoVertical("INASISTENCIA_DE_ALUMNO");
            }
            catch (Exception)
            {
                tx.Rollback();
                connection.Close();
                throw;
            }
        }

        public void modificarInasistencia(InasistenciaAlumno inasistencia) {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();

            StringBuilder sb = new StringBuilder();
            sb.Append(" update inasistencia_de_alumno set ");
            sb.Append(" ina_justificada        = @justificada ");
            sb.Append(" where ina_alumno_id = @alu  and ina_fecha = @fecha");
            
            SqlCommand cmd = new SqlCommand(sb.ToString(), connection, tx);
            cmd.Parameters.Add(new SqlParameter("@alu", SqlDbType.BigInt)).Value = inasistencia.Alumno.legajo;
            cmd.Parameters.Add(new SqlParameter("@fecha", SqlDbType.DateTime)).Value = inasistencia.fecha.ToString("yyyy-MM-dd");
            cmd.Parameters.Add(new SqlParameter("@justificada", SqlDbType.Bit)).Value = inasistencia.Justificada;

            try
            {
                cmd.CommandText = sb.ToString();
                cmd.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
                Usuario usu = new Usuario();
                usu.id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
                new DAOSeguridad().grabarBitacora(usu, "Se modificó una inasistencia", CriticidadEnum.BAJA);
                new DAOSeguridad().recalcularDigitoVertical("INASISTENCIA_DE_ALUMNO");
            }
            catch (Exception)
            {
                tx.Rollback();
                connection.Close();
                throw;
            }
        }

        public List<InasistenciaAlumno> listarInasistencias(String filtro, String valor, String orden) {
            SqlDataReader reader = null;
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            List<InasistenciaAlumno> inasistenciaAlumnos = new List<InasistenciaAlumno>();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand("SELECT * FROM INASISTENCIA_DE_ALUMNO WHERE INA_ALUMNO_ID = @ID ",connection,tx);
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt)).Value = long.Parse(valor);
            
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    InasistenciaAlumno inas = new InasistenciaAlumno();
                    inas.fecha = DateTime.Parse(reader.GetValue(1).ToString());
                    inas.valor = (double)reader.GetDecimal(2);
                    inas.Justificada = reader.GetBoolean(4);
                    inasistenciaAlumnos.Add(inas);
                }
                reader.Close();
                tx.Commit();
                connection.Close();
            }
            catch (Exception)
            {
                if (reader != null)
                    reader.Close();
                tx.Rollback();
                connection.Close();
                throw;
            }

            return inasistenciaAlumnos;

        }

        //Tutores
        public void guardarTutor(Tutor tutor) {

            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();

            SqlCommand cmd = new SqlCommand("", connection, tx);

            StringBuilder query = new StringBuilder(" INSERT INTO TUTOR (TUT_NOMBRE,TUT_APELLIDO,TUT_TELEFONO_PRIMARIO,TUT_DNI,TUT_EMAIL");
            if (!String.IsNullOrEmpty(tutor.telefono2))
            {
                query.Append(",TUT_TELEFONO_SECUNDARIO) ");
                
            }
            else
            {
                query.Append(") ");
            }

            query.Append(" VALUES(@NOMBRE,@APELLIDO,@TEL1,@DNI,@MAIL");
            if (!String.IsNullOrEmpty(tutor.telefono2))
            {
                query.Append(",@TEL2) ");
                cmd.Parameters.Add(new SqlParameter("@TEL2", SqlDbType.NVarChar)).Value = SeguridadUtiles.encriptarAES(tutor.telefono2);
            }
            else
            {
                query.Append(") ");
            }

            cmd.Parameters.Add(new SqlParameter("@NOMBRE", SqlDbType.VarChar)).Value = tutor.nombre;
            cmd.Parameters.Add(new SqlParameter("@APELLIDO", SqlDbType.VarChar)).Value =tutor.apellido;
            cmd.Parameters.Add(new SqlParameter("@TEL1", SqlDbType.NVarChar)).Value = SeguridadUtiles.encriptarAES(tutor.telefono1);
            cmd.Parameters.Add(new SqlParameter("@DNI", SqlDbType.VarChar)).Value = tutor.dni;
            cmd.Parameters.Add(new SqlParameter("@MAIL", SqlDbType.VarChar)).Value = tutor.email;

            cmd.CommandText = query.ToString();

            try
            {
                cmd.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
                Usuario usu = new Usuario();
                usu.id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
                new DAOSeguridad().grabarBitacora(usu, "Se creó un tutor", CriticidadEnum.MEDIA);

            }
            catch (Exception ex)
            {
                tx.Rollback();
                connection.Close();
                throw ex;
            }
        }
        public void modificarTutor(Tutor tutor) {


            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();

            SqlCommand cmd = new SqlCommand("", connection, tx);

            StringBuilder query = new StringBuilder(" UPDATE TUTOR SET " +
                "TUT_NOMBRE = @NOMBRE," +
                "TUT_APELLIDO = @APELLIDO, " +
                "TUT_TELEFONO_PRIMARIO = @TEL1, " +
                "TUT_DNI = @DNI, " +
                "TUT_EMAIL = @MAIL ");
            if (!String.IsNullOrEmpty(tutor.telefono2))
            {
                query.Append(", TUT_TELEFONO_SECUNDARIO = @TEL2 ");
                cmd.Parameters.Add(new SqlParameter("@TEL2", SqlDbType.NVarChar)).Value = SeguridadUtiles.encriptarAES(tutor.telefono2);
            }

            query.Append(" WHERE TUT_ID = @ID ");
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt)).Value = tutor.id;
            cmd.Parameters.Add(new SqlParameter("@NOMBRE", SqlDbType.VarChar)).Value = tutor.nombre;
            cmd.Parameters.Add(new SqlParameter("@APELLIDO", SqlDbType.VarChar)).Value = tutor.apellido;
            cmd.Parameters.Add(new SqlParameter("@TEL1", SqlDbType.NVarChar)).Value = SeguridadUtiles.encriptarAES(tutor.telefono1);
            cmd.Parameters.Add(new SqlParameter("@DNI", SqlDbType.VarChar)).Value = tutor.dni;
            cmd.Parameters.Add(new SqlParameter("@MAIL", SqlDbType.VarChar)).Value = tutor.email;
            cmd.CommandText = query.ToString();

            try
            {
                cmd.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
                Usuario usu = new Usuario();
                usu.id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
                new DAOSeguridad().grabarBitacora(usu, "Se modificó un tutor", CriticidadEnum.BAJA);

            }
            catch (Exception ex)
            {
                tx.Rollback();
                connection.Close();
                throw ex;
            }


        }
        public List<Tutor> listarTutor(String filtro, String valor, String orden) {
           
           
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();

            List<Tutor> tutores = new List<Tutor>();
            StringBuilder query = new StringBuilder(" select * from TUTOR ");
            if (!String.IsNullOrEmpty(filtro) && !String.IsNullOrEmpty(valor))
            {
                // nombre apellido dni
                switch (filtro)
                {
                    case "nombre":
                        query.Append( "WHERE TUT_NOMBRE = @VALOR ");
                        break;
                    case "apellido":
                        query.Append(" WHERE TUT_APELLIDO = @VALOR ");
                        break;
                    case "dni":
                        query.Append(" WHERE TUT_DNI = @VALOR ");
                        break;
                }
            }






            SqlCommand cmd = new SqlCommand(query.ToString(), connection, tx);
            SqlDataReader reader = null ;

            if (!String.IsNullOrEmpty(valor))
            {
                cmd.Parameters.Add(new SqlParameter("@VALOR", SqlDbType.VarChar)).Value = valor;
            }
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Tutor tut = new Tutor();
                    tut.id = (long)reader.GetValue(0);
                    tut.nombre = reader.GetValue(1).ToString();
                    tut.apellido = reader.GetValue(2).ToString();
                    tut.telefono1 =  SeguridadUtiles.desencriptarAES(reader.GetValue(3).ToString());
                    if (reader.IsDBNull(4))
                    {
                        tut.telefono2 = "";
                    }
                    else
                    {
                        tut.telefono2 = SeguridadUtiles.desencriptarAES(reader.GetValue(4).ToString());
                    }
                    
                    tut.dni = reader.GetValue(5).ToString();
                    tut.email = reader.GetValue(6).ToString();
                    tutores.Add(tut);
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

            return tutores;


        }
        public void borrarTutor(Tutor tutor) {

            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();

            SqlCommand cmd = new SqlCommand("", connection, tx);
            StringBuilder query = new StringBuilder();
            query.Append(" DELETE TUTOR ");
            query.Append(" WHERE TUT_ID = @ID ");
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt)).Value = tutor.id;
            cmd.CommandText = query.ToString();

            try
            {
                cmd.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
                Usuario usu = new Usuario();
                usu.id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
                new DAOSeguridad().grabarBitacora(usu, "Se borró un tutor", CriticidadEnum.MEDIA);
            }
            catch (Exception ex)
            {
                tx.Rollback();
                connection.Close();
                throw ex;
            }


        }
        /// <summary>
        /// VERIFICA QUE EL TUTOR NO ESTE ASIGNADO A NINGUN ALUMNO.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Boolean verificarTutor(long id) {

            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            
            string query = " SELECT COUNT(*) FROM ALUMNO_TUTOR WHERE AT_TUTOR_ID = @ID";
            SqlCommand cmd = new SqlCommand(query, connection, tx);
            SqlDataReader reader = null;
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt)).Value = id;

            Boolean desasignado = false;    
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int cantidad = (int)reader.GetValue(0);
                    if(cantidad == 0)
                    {
                        desasignado = true;
                    }
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

            return desasignado;
        }


    }
}
