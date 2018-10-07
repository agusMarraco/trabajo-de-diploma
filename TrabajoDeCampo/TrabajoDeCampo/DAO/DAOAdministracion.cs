using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoDeCampo.DAO
{
    public class DAOAdministracion
    {
        //HORARIOS
        public List<Horario> listarHorarios(String filtro, String valor, String orden) {

            List<Horario> horarios = new List<Horario>();
            SqlDataReader reader = null;

            StringBuilder sb = new StringBuilder();
            sb. Append(" select hor.HOR_ID,hor.HOR_DIA,mo.MOD_ID, ").
                Append(" mo.MOD_HORA_INICIO, mo.MOD_HORA_FINAL, ").
                Append(" cur.CUR_ID, cur.CUR_CODIGO, ").
                Append(" mat.MAT_ID, mat.MAT_NOMBRE, ").
                Append(" doc.DOC_LEGAJO, doc.DOC_NOMBRE, doc.DOC_APELLIDO, niv.NIV_ID ").
                Append(" from horario hor ").
                Append(" inner join CURSO cur  ").
                Append(" on cur.CUR_ID = hor.HOR_CURSO ").
                Append(" inner join NIVEL niv  ").
                Append(" on niv.NIV_ID = cur.CUR_NIVEL_ID ").
                Append(" inner join MATERIA mat  ").
                Append(" on mat.MAT_ID = hor.HOR_MATERIA_ID ").
                Append(" inner join docente doc  ").
                Append(" on doc.DOC_LEGAJO = hor.HOR_DOCENTE_ID ").
                Append(" inner join MODULO mo  ").
                Append(" on mo.MOD_ID = hor.HOR_MODULO_ID ");

            if (!String.IsNullOrEmpty(filtro) && !String.IsNullOrEmpty(valor))
            {
                // nivel curso
                switch (filtro)
                {
                    case "nivel":
                        sb.Append( " where niv.niv_id = @VALOR ");
                        break;
                    case "curso":
                        sb.Append(" where cur_id = @VALOR ");
                        break;
                    
                }
            }
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();

            //chequeando docentes
            SqlCommand cmd = new SqlCommand(sb.ToString(), connection, tx);
            if (!String.IsNullOrEmpty(filtro) && !String.IsNullOrEmpty(valor))
            {
                cmd.Parameters.Add(new SqlParameter("@VALOR", SqlDbType.BigInt)).Value = long.Parse(valor);
            }

            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Modulo mod = new Modulo();
                    mod.id = (long)reader["MOD_ID"];
                    mod.horaInicio = DateTime.Parse(reader["MOD_HORA_INICIO"].ToString());
                    mod.horaFin = DateTime.Parse(reader["MOD_HORA_FINAL"].ToString());
                    Docente doc = new Docente();
                    doc.nombre = reader["DOC_NOMBRE"].ToString();
                    doc.apellido = reader["DOC_APELLIDO"].ToString();
                    doc.legajo = (long)reader["DOC_LEGAJO"];
                    Materia mat = new Materia();
                    mat.id = (long)reader["MAT_ID"];
                    mat.nombre = reader["MAT_NOMBRE"].ToString();
                    Curso curso = new Curso();
                    curso.id = (long)reader["CUR_ID"];
                    curso.codigo = reader["CUR_CODIGO"].ToString();
                    Nivel nivel = new Nivel();
                    nivel.id = (long)reader["NIV_ID"];
                    curso.nivel = nivel;
                    Horario hor = new Horario();
                    hor.modulo = mod;
                    hor.docente = doc;
                    hor.materia = mat;
                    hor.curso = curso;
                    hor.id = (long)reader["HOR_ID"];
                    hor.dia = (int)(long)reader["HOR_DIA"];
                    horarios.Add(hor);
                }
                reader.Close();
                tx.Commit();
                connection.Close();


            }
            catch (Exception ex)
            {
             
                tx.Rollback();
                connection.Close();
                throw ex;
            }
           return horarios;
        }

        //VERIFICAR QUE EL HORARIO SEA VALIDO
        public Boolean verificarRestricciones(long idCurso, long idMateria, long dia, long idDocente, long idModulo,long idHorario) {
            Boolean disponibles = false ;
            Boolean docenteDisponible = false;
            Boolean cursoDisponible = false;

            SqlDataReader reader = null;

            String queryDocentes = " select count(*) from horario where hor_docente_id = @docente and hor_modulo_id = @modulo" +
                " and hor_dia = @dia ";

            String queryCurso = " select count(*) from horario where hor_curso = @curso and hor_modulo_id = @modulo" +
                " and hor_dia = @dia ";

            if(idHorario != 0)
            {
                queryCurso += " and hor_id <> @id";
                queryDocentes += " and hor_id <> @id";
            }
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();

            //chequeando docentes
            SqlCommand cmd = new SqlCommand(queryDocentes, connection, tx);
            cmd.Parameters.Add(new SqlParameter("@docente",SqlDbType.BigInt)).Value = idDocente;
            cmd.Parameters.Add(new SqlParameter("@modulo", SqlDbType.BigInt)).Value = idModulo;
            cmd.Parameters.Add(new SqlParameter("@dia", SqlDbType.BigInt)).Value = dia;
            cmd.Parameters.Add(new SqlParameter("@curso", SqlDbType.BigInt)).Value = idCurso;
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.BigInt)).Value = idHorario;
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int count = (int)reader.GetValue(0);
                    if(count == 0)
                    {
                        docenteDisponible = true;
                    }

                }
                reader.Close();

                //chequeando cursos
                cmd.CommandText = queryCurso;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int count = (int)reader.GetValue(0);
                    if (count == 0)
                    {
                        cursoDisponible = true;
                    }

                }
                reader.Close();

                if(docenteDisponible && cursoDisponible)
                {
                    disponibles = true;
                }
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




            return disponibles ;
        }

        public void guardarHorario(Horario horario) {
            StringBuilder query = new StringBuilder(" insert into horario ");
            query.Append(" (")
                .Append(" hor_dia, ")
                .Append(" hor_curso, ")
                .Append(" hor_materia_id, ")
                .Append(" hor_docente_id ,")
                .Append(" hor_modulo_id ) ");

            query.Append(" values  (")
                .Append(" @dia,     ")
                .Append(" @curso,   ")
                .Append(" @materia, ")
                .Append(" @docente, ")
                .Append(" @modulo)  ");

            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query.ToString(), connection, tx);
            cmd.Parameters.Add(new SqlParameter("@dia", SqlDbType.BigInt)).Value = horario.dia;
            cmd.Parameters.Add(new SqlParameter("@curso", SqlDbType.BigInt)).Value = horario.curso.id;
            cmd.Parameters.Add(new SqlParameter("@materia", SqlDbType.BigInt)).Value = horario.materia.id;
            cmd.Parameters.Add(new SqlParameter("@docente", SqlDbType.BigInt)).Value = horario.docente.legajo;
            cmd.Parameters.Add(new SqlParameter("@modulo", SqlDbType.BigInt)).Value = horario.modulo.id;
            try
            {
                cmd.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                connection.Close();
                throw ex;
            }
        }

        public void actualizarHorario(Horario horario) {
            StringBuilder query = new StringBuilder(" update horario set ");
            query.Append(" hor_dia = @dia, ")
                .Append(" hor_curso = @curso, ")
                .Append(" hor_materia_id = @materia, ")
                .Append(" hor_docente_id = @docente,")
                .Append(" hor_modulo_id = @modulo ");

            query.Append(" where hor_id = @id ");
            
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query.ToString(), connection, tx);
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.BigInt)).Value = horario.id;
            cmd.Parameters.Add(new SqlParameter("@dia", SqlDbType.BigInt)).Value = horario.dia;
            cmd.Parameters.Add(new SqlParameter("@curso", SqlDbType.BigInt)).Value = horario.curso.id;
            cmd.Parameters.Add(new SqlParameter("@materia", SqlDbType.BigInt)).Value = horario.materia.id;
            cmd.Parameters.Add(new SqlParameter("@docente", SqlDbType.BigInt)).Value = horario.docente.legajo;
            cmd.Parameters.Add(new SqlParameter("@modulo", SqlDbType.BigInt)).Value = horario.modulo.id;
            try
            {
                cmd.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                connection.Close();
                throw ex;
            }
        }

        public void borrarHorario(Horario horario) {
            StringBuilder query = new StringBuilder(" delete horario  ");
            query.Append(" where hor_id = @id ");

            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query.ToString(), connection, tx);
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.BigInt)).Value = horario.id;
            
            try
            {
                cmd.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                connection.Close();
                throw ex;
            }




        }

        //Cursos

        public List<Curso> listarCursos(String filtro, String valor, String orden) {
            SqlDataReader reader = null;
            List<Curso> cursos = new List<Curso>();
            String query = " SELECT NIV.NIV_ID,NIV.NIV_CODIGO, CUR.* , ORI_CODIGO FROM CURSO CUR INNER JOIN NIVEL NIV ON NIV.NIV_ID = CUR.CUR_NIVEL_ID " +
                " LEFT JOIN ORIENTACION ON ORI_CODIGO = NIV.NIV_ORIENTACION " +
                " WHERE CUR.CUR_BORRADO IS NULL ";
            if (!String.IsNullOrEmpty(filtro) && !String.IsNullOrEmpty(valor))
            {
                // nivel codigo capacidad turno
                switch (filtro)
                {
                    case "nivel":
                        query += " AND NIV_CODIGO = @VALOR ";
                        break;
                    case "codigo":
                        query += " AND CUR_CODIGO = @VALOR ";
                        break;
                    case "capacidad":
                        query += " AND CUR_CAPACIDAD = @VALOR ";
                        break;
                    case "turno":
                        query += " AND CUR_TURNO = @VALOR ";
                        break;
                }
            }
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query, connection, tx);
            if (!String.IsNullOrEmpty(filtro) && !String.IsNullOrEmpty(valor))
            {
                if (filtro == "capacidad")
                {
                    cmd.Parameters.Add(new SqlParameter("@VALOR", SqlDbType.BigInt)).Value = long.Parse(valor);
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VALOR", SqlDbType.VarChar)).Value = valor;
                }

                
            }




            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Curso cur = new Curso();
                    Nivel niv = new Nivel();
                    niv.id = (long)reader.GetValue(0);  
                    niv.codigo = reader.GetValue(1).ToString();
                    if (!reader.IsDBNull(9))
                    {
                        Orientacion orientacion = new Orientacion();
                        orientacion.codigo = reader["ORI_CODIGO"].ToString();
                        niv.orientacion = orientacion;
                    }
                    cur.id = (long)reader.GetValue(2);
                    cur.capacidad = Convert.ToInt32(reader.GetValue(4));
                    cur.codigo = reader.GetValue(5).ToString();
                    cur.turno = reader.GetValue(6).ToString();
                    cur.letra = reader.GetValue(7).ToString();
                    cur.nivel = niv;
                    cursos.Add(cur);
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
            return cursos;
            }

        public void guardarCurso(Curso curso) {
            
            String query = " INSERT INTO CURSO(CUR_NIVEL_ID,CUR_CAPACIDAD, CUR_CODIGO, CUR_TURNO, CUR_LETRA) " +
                " VALUES (@NIVEL,@CAPACIDAD,@CODIGO,@TURNO,@LETRA) ";
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query, connection, tx);
            cmd.Parameters.Add(new SqlParameter("@NIVEL", SqlDbType.BigInt)).Value = curso.nivel.id;
            cmd.Parameters.Add(new SqlParameter("@CAPACIDAD", SqlDbType.BigInt)).Value = curso.capacidad;
            cmd.Parameters.Add(new SqlParameter("@CODIGO", SqlDbType.VarChar)).Value = curso.codigo;
            cmd.Parameters.Add(new SqlParameter("@TURNO", SqlDbType.Char)).Value = curso.turno;
            cmd.Parameters.Add(new SqlParameter("@LETRA", SqlDbType.Char)).Value = curso.letra;


            try
            {
                cmd.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                connection.Close();
                throw ex;
            }

        }

        public void actualizarCurso(Curso curso) {
            String query = " UPDATE CURSO SET " +
                " CUR_NIVEL_ID = @NIVEL, " +
                " CUR_CAPACIDAD = @CAPACIDAD, " +
                " CUR_CODIGO = @CODIGO, " +
                " CUR_TURNO = @TURNO, " +
                " CUR_LETRA = @LETRA  " +
                " WHERE CUR_ID = @ID";
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query, connection, tx);
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt)).Value = curso.id;
            cmd.Parameters.Add(new SqlParameter("@NIVEL", SqlDbType.BigInt)).Value = curso.nivel.id;
            cmd.Parameters.Add(new SqlParameter("@CAPACIDAD", SqlDbType.BigInt)).Value = curso.capacidad;
            cmd.Parameters.Add(new SqlParameter("@CODIGO", SqlDbType.VarChar)).Value = curso.codigo;
            cmd.Parameters.Add(new SqlParameter("@TURNO", SqlDbType.Char)).Value = curso.turno;
            cmd.Parameters.Add(new SqlParameter("@LETRA", SqlDbType.Char)).Value = curso.letra;


            try
            {
                cmd.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                connection.Close();
                throw ex;
            }


        }

        public void borrarCurso(Curso curso) {
            String query = " UPDATE CURSO SET " +
              " CUR_BORRADO = 1 " +
              " WHERE CUR_ID = @ID";
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query, connection, tx);
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt)).Value = curso.id;

            try
            {
                cmd.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                connection.Close();
                throw ex;
            }

        }

        public Boolean chequearCodigoUnico(String codigoCurso,long cursoId) {
            Boolean codigoUnico = false;

            SqlDataReader reader = null;
            String query = " SELECT COUNT(*) FROM CURSO WHERE CUR_CODIGO = @CODIGO and CUR_BORRADO IS NULL  ";
            if(cursoId != 0)
            {
                query += " AND CUR_ID <> @ID ";
            }
                
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query, connection, tx);
            cmd.Parameters.Add(new SqlParameter("@CODIGO", SqlDbType.VarChar)).Value = codigoCurso;
            if (cursoId != 0)
            {
                cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt)).Value = cursoId;
            }


            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int repetidos = (int)reader.GetValue(0);
                    if(repetidos==0)
                    {
                        codigoUnico = true;
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
            return codigoUnico;


        }

        public Boolean chequearCantidadCurso(Curso curso) {

            Boolean excedido = false;

            SqlDataReader reader = null;
            String query = " SELECT COUNT(*) FROM ALUMNO WHERE ALU_CURSO = @ID AND ALU_BORRADO IS NULL";
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query, connection, tx);
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt)).Value = curso.id;
            

            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int cantidad = (int)reader.GetValue(0);
                    if (cantidad > 40)
                    {
                        excedido = true;
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
            return excedido;


        }

        public List<Curso> listarCursosExcedidos() {
            SqlDataReader reader = null;
            List<Curso> cursos = new List<Curso>();
            String query = " SELECT * FROM CURSO WHERE CUR_BORRADO IS NULL AND " +
                "(SELECT COUNT(*) FROM ALUMNO WHERE ALU_BORRADO IS NULL AND ALU_CURSO = CUR_ID) > CUR_CAPACIDAD";
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query, connection, tx);

            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Curso cur = new Curso();
                    Nivel niv = new Nivel();
                    niv.id = (long)reader.GetValue(0);
                    niv.codigo = reader.GetValue(1).ToString();
                    cur.id = (long)reader.GetValue(2);
                    cur.capacidad = ((int)reader.GetValue(4));
                    cur.codigo = reader.GetValue(5).ToString();
                    cur.turno = reader.GetValue(6).ToString();
                    cur.letra = reader.GetValue(7).ToString();
                    cur.nivel = niv;
                    cursos.Add(cur);
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
            return cursos;
        
        }
        public Boolean chequearCursoVacio(Curso curso) {
            Boolean vacio = false;

            SqlDataReader reader = null;
            String query = " SELECT COUNT(*) FROM ALUMNO WHERE ALU_CURSO = @ID AND ALU_BORRADO IS NULL";
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query, connection, tx);
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt)).Value = curso.id;


            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int cantidad = (int)reader.GetValue(0);
                    if (cantidad == 0)
                    {
                        vacio = true;
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
            return vacio;

        }



        //MATERIAS

        public void actualizarMateriasAsignadas(Nivel nivel, List<Materia> materias) {
            

            SqlConnection connection = ConexionSingleton.obtenerConexion();
            StringBuilder sb = new StringBuilder(); 
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand command = new SqlCommand("", connection, tx);
            command.Parameters.Add(new SqlParameter("@IDNIVEL", SqlDbType.BigInt)).Value = nivel.id;


            sb.Append(" DELETE  MATERIA_NIVEL WHERE MN_NIVEL_ID = @IDNIVEL ");
            int x = 1;
            foreach (Materia materia  in materias)
            {
                sb.Append(" INSERT INTO MATERIA_NIVEL (MN_MATERIA_ID,MN_NIVEL_ID) VALUES (@MATERIA" + x + ",@IDNIVEL) ");
                command.Parameters.Add(new SqlParameter("@MATERIA" + x, SqlDbType.BigInt)).Value = materia.id;
                x++;
            }

            command.CommandText = sb.ToString();
            try
            {

                command.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                connection.Close();
                throw ex;
            }
        }
        public List<Materia> traerMateriasPorNivel(Nivel nivel) {

            List<Materia> materias = new List<Materia>();

            SqlConnection connection = ConexionSingleton.obtenerConexion();
            String query = " SELECT MAT_ID, MAT_NOMBRE, MAT_DESCRIPCION, MAT_TIPO FROM MATERIA_NIVEL INNER JOIN MATERIA ON MAT_ID = MN_MATERIA_ID " +
                "WHERE MN_NIVEL_ID = @IDNIVEL ";

            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand command = new SqlCommand(query,connection, tx);
            command.Parameters.Add(new SqlParameter("@IDNIVEL", SqlDbType.BigInt)).Value = nivel.id;
            SqlDataReader reader = null;

            try
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    String nombre = reader.GetValue(1).ToString();
                    String desc = reader.GetValue(2).ToString();
                    bool tipo = (bool)reader.GetValue(3);
                    Materia mat = new Materia();
                    mat.id = (long)reader.GetValue(0);
                    mat.nombre = nombre;
                    mat.descripcion = desc;
                    mat.tipo = (tipo) ? "TRONCAL" : "EXTRA";
                    materias.Add(mat);
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

            return materias;
        }

        public List<Materia> listarMaterias(String filtro, String valor, String orden) {

            List<Materia> materias = new List<Materia>();

            SqlCommand cmd = new SqlCommand();
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();

            SqlTransaction tx = connection.BeginTransaction();
            cmd.Connection = connection;
            cmd.Transaction = tx;
            cmd.CommandText = " SELECT * FROM MATERIA WHERE MAT_BORRADA is null ";
            SqlDataReader reader =  null;
            try
            {
                 reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    String nombre = reader.GetValue(1).ToString();
                    String desc = reader.GetValue(2).ToString();
                    bool tipo = (bool) reader.GetValue(3);
                    Materia mat = new Materia();
                    mat.id = (long)reader.GetValue(0);
                    mat.nombre = nombre;
                    mat.descripcion = desc;
                    mat.tipo = (tipo) ? "TRONCAL" : "EXTRA";
                    materias.Add(mat);
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

            return materias;

        }

        public void guardarMateria(Materia materia) {
            SqlCommand cmd = new SqlCommand();
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();

            SqlTransaction tx = connection.BeginTransaction();
            cmd.Connection = connection;
            cmd.Transaction = tx;
            cmd.CommandText = " INSERT INTO MATERIA (MAT_NOMBRE,MAT_DESCRIPCION,MAT_TIPO) VALUES (@NOMBRE,@DESC,@TIPO) ";

            cmd.Parameters.Add(new SqlParameter("@NOMBRE", SqlDbType.VarChar)).Value = materia.nombre;
            cmd.Parameters.Add(new SqlParameter("@DESC", SqlDbType.VarChar)).Value = materia.descripcion;
            cmd.Parameters.Add(new SqlParameter("@TIPO", SqlDbType.Bit)).Value = long.Parse(materia.tipo);


            try
            {
                cmd.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
            }
            catch (Exception)
            {
                tx.Rollback();
                connection.Close();
                throw;
            }
        }

        public void actualizarMateria(Materia materia) {

            SqlConnection connection = ConexionSingleton.obtenerConexion();
            String query = "UPDATE MATERIA SET MAT_NOMBRE = @NOMBRE , MAT_DESCRIPCION = @DESC, MAT_TIPO = @TIPO  WHERE MAT_ID = @ID";

            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();

            SqlCommand cmd = new SqlCommand(query, connection, tx);
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt)).Value = materia.id;
            cmd.Parameters.Add(new SqlParameter("@NOMBRE", SqlDbType.VarChar)).Value = materia.nombre;
            cmd.Parameters.Add(new SqlParameter("@DESC", SqlDbType.VarChar)).Value = materia.descripcion;
            cmd.Parameters.Add(new SqlParameter("@TIPO", SqlDbType.Bit)).Value = long.Parse(materia.tipo); ;
            try
            {
                cmd.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                connection.Close();
                throw ex;
            }

        }

        public void borrarMateria(Materia materia) {

     
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            String query = "UPDATE MATERIA SET MAT_BORRADA = 1 WHERE MAT_ID = @ID";

            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();

            SqlCommand cmd = new SqlCommand(query, connection, tx);
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt)).Value = materia.id;
            try
            {
                cmd.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                connection.Close();
                throw ex;
            }
        }

        //chequear repetidos
        public Boolean verificarMateria(Materia materia) {

            Boolean esEdit = materia.id != 0 ? true : false;
            Boolean existe = false;
            SqlDataReader reader;
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            String query = "SELECT COUNT(*) FROM MATERIA WHERE MAT_NOMBRE = @NOMBRE AND MAT_BORRADA is null ";

            if (esEdit)
            {
                query += " AND MAT_ID <> @ID ";
            }
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();

            SqlCommand cmd = new SqlCommand(query, connection, tx);
            cmd.Parameters.Add(new SqlParameter("@NOMBRE", SqlDbType.VarChar)).Value = materia.nombre;
            if (esEdit)
            {
                cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.VarChar)).Value = materia.id;
            }
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int resultadoCount = (int) reader.GetValue(0);
                    existe = (resultadoCount > 0);
                }
                reader.Close();
                tx.Commit();
                connection.Close();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                connection.Close();
                throw ex;
            }

            return existe;
        }

        // antes de borrar
        public Boolean materiaEstaAsignada(Materia materia) {
            Boolean estaAsignada = false;
            SqlDataReader reader;
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            String query = "SELECT COUNT(*) FROM MATERIA_NIVEL WHERE MN_MATERIA_ID = @ID ";

            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();

            SqlCommand cmd = new SqlCommand(query, connection, tx);
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt)).Value = materia.id;
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int resultadoCount = (int)reader.GetValue(0);
                    estaAsignada = (resultadoCount > 0);
                }
                reader.Close();
                tx.Commit();
                connection.Close();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                connection.Close();
                throw ex;
            }

            return estaAsignada;
        } 
        //NIVELES
        public List<Nivel> listarNiveles(String filtro, String valor, String orden) {

            List<Nivel> niveles = new List<Nivel>();
            SqlDataReader reader = null;
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand(" SELECT * FROM NIVEL ", connection, tx);

            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Nivel nivel = new Nivel();
                    nivel.id = (long)reader.GetValue(0);
                    nivel.codigo = reader.GetValue(1).ToString();
                    nivel.descripcion = reader.GetValue(2).ToString();
                    if(!reader.IsDBNull(3)){
                        Orientacion ori = new Orientacion();
                        ori.codigo = reader.GetValue(3).ToString();
                        nivel.orientacion = ori;
                    }
                    else
                    {
                        Orientacion ori = new Orientacion();
                        ori.codigo = "null";
                        nivel.orientacion = ori;
                    }
                    niveles.Add(nivel);
                    
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

            return niveles;

        }

        //Alumnos

        public void generarPlanillasDeEvaluacion(Alumno alumno)
        {
            Nivel nivel = alumno.curso.nivel;
         
            List<Materia> materias = this.traerMateriasPorNivel(nivel);
            if(materias.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                SqlConnection connection = ConexionSingleton.obtenerConexion();
                connection.Open();
                SqlTransaction tx = connection.BeginTransaction();

                SqlCommand cmd = new SqlCommand("", connection, tx);
                Random rand = new Random();
                int iter = 0;
                cmd.Parameters.Add(new SqlParameter("@nivel", SqlDbType.BigInt)).Value = nivel.id;
                cmd.Parameters.Add(new SqlParameter("@alumno", SqlDbType.BigInt)).Value = alumno.legajo;
                foreach (Materia materia in materias)
                {
                    iter++;
                    sb.Append(" insert into planilla_de_evaluacion " +
                        "(pde_alumno_id,pde_nivel_id,pde_trimestre_1,pde_trimestre_2,pde_trimestre_3,pde_nota_final,pde_condicion,pde_dvh,pde_materia_id) ");
                    sb.Append(" values(@alumno,@nivel, ");
                    int tri1 = rand.Next(1, 10);
                    int tri2 = rand.Next(1, 10);
                    int tri3 = rand.Next(1, 10);
                    Double promedio = (tri1 + tri2 + tri3) / 3;
                    int condicion = promedio >= 7 ? 1 : 0;
                    sb.Append("@tri1" + iter + ", " + "@tri2" + iter + ", " + "@tri3" + iter + ", " + "@promedio" + iter + ", " + "@condicion" + iter + ", ");
                    sb.Append(" @dvh" + iter + " , @materia" + iter + ")");
                    String trim1 = SeguridadUtiles.encriptarAES(tri1.ToString());
                    String trim2 = SeguridadUtiles.encriptarAES(tri2.ToString());
                    String trim3 = SeguridadUtiles.encriptarAES(tri3.ToString());
                    cmd.Parameters.Add(new SqlParameter("@tri1" + iter, SqlDbType.NVarChar)).Value = trim1;
                    cmd.Parameters.Add(new SqlParameter("@tri2" + iter, SqlDbType.NVarChar)).Value = trim2;
                    cmd.Parameters.Add(new SqlParameter("@tri3" + iter, SqlDbType.NVarChar)).Value = trim3;
                    cmd.Parameters.Add(new SqlParameter("@promedio" + iter, SqlDbType.NVarChar)).Value = SeguridadUtiles.encriptarAES(promedio.ToString());
                    cmd.Parameters.Add(new SqlParameter("@condicion" + iter, SqlDbType.BigInt)).Value = condicion;
                    cmd.Parameters.Add(new SqlParameter("@dvh" + iter, SqlDbType.NVarChar)).Value = new DAOSeguridad().recalcularDigitoHorizontal(new String[] { trim1, trim2, trim3, alumno.legajo.ToString() });
                    cmd.Parameters.Add(new SqlParameter("@materia" + iter, SqlDbType.BigInt)).Value = materia.id;
                }
                cmd.CommandText = sb.ToString();

                try
                {
                    cmd.ExecuteNonQuery();
                    tx.Commit();
                    connection.Close();
                    new DAOSeguridad().recalcularDigitoVertical("PLANILLA_DE_EVALUACION");
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    connection.Close();
                    throw;
                }

            }
           
        }

        public List<Alumno> listarAlumnosPorCursoYNivel(Nivel nivel, Curso curso) {
            List<Alumno> alumnos = new List<Alumno>();
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            String queryAlumnosPromocionables = " (select count(*) from planilla_de_evaluacion where pde_alumno_id = alu.alu_legajo and pde_condicion = 0) < 3 ";
            StringBuilder sb = new StringBuilder();
            sb.Append(" select alu.alu_legajo, alu.alu_apellido, alu.alu_nombre, alu.alu_dni, alu.alu_curso, " +
                " alu.alu_orientacion, " +
                " ori.ori_codigo, cur.cur_codigo , cur.cur_id, cur.cur_nivel_id, niv.niv_codigo, ori.ori_nombre ");
            sb.Append(" from alumno alu ");
            sb.Append(" left join orientacion ori on ori.ori_codigo = alu.alu_orientacion ");
            sb.Append(" inner join curso cur on cur.cur_id = alu.alu_curso ");
            sb.Append(" inner join nivel niv  on cur.cur_nivel_id = niv.niv_id ");
            sb.Append(" where alu.alu_borrado is null and " + queryAlumnosPromocionables);
            if(curso!= null)
                sb.Append(" and alu.alu_curso = @curso ");
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand(sb.ToString(), connection, tx);
            if (curso != null)
                cmd.Parameters.Add(new SqlParameter("@curso", SqlDbType.BigInt)).Value = curso.id;
            SqlDataReader reader = null;
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Alumno alu = new Alumno();
                    Orientacion ori = new Orientacion();
                    Curso cur = new Curso();
                    alu.legajo          = (long)reader["ALU_LEGAJO"];
                    alu.apellido        = reader["ALU_APELLIDO"].ToString();
                    alu.nombre          = reader["ALU_NOMBRE"].ToString();
                    alu.dni             = reader["ALU_DNI"].ToString();
                    cur.id              = (long)reader["CUR_ID"];
                    cur.codigo          = reader["CUR_CODIGO"].ToString();
                    cur.nivel           = new Nivel();
                    cur.nivel.codigo        = reader["NIV_CODIGO"].ToString();
                    cur.nivel.id        = (long)reader["CUR_NIVEL_ID"];
                    alu.curso           = cur;
                    if (reader.IsDBNull(5))
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
            return alumnos;

        }

        public void promocionarAlumno(Alumno alumno, Curso curso) {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            SqlTransaction tx = connection.BeginTransaction();

            SqlCommand cmd = new SqlCommand(" UPDATE ALUMNO SET ALU_CURSO = @CURSO, ALU_ORIENTACION = @ORIENTACION WHERE ALU_LEGAJO = @LEGAJO ",connection,tx);
            cmd.Parameters.Add(new SqlParameter("@CURSO", SqlDbType.BigInt)).Value = curso.id;
            cmd.Parameters.Add(new SqlParameter("@LEGAJO", SqlDbType.BigInt)).Value = alumno.legajo;
            cmd.Parameters.Add(new SqlParameter("@ORIENTACION", SqlDbType.BigInt)).Value = curso.nivel.orientacion != null ? Convert.DBNull : long.Parse(curso.nivel.orientacion.codigo);

            try
            {
                cmd.ExecuteNonQuery();
                tx.Commit();
                connection.Close();

                alumno.curso = curso;
                this.generarPlanillasDeEvaluacion(alumno);
            }
            catch (Exception)
            {
                tx.Rollback();
                connection.Close();
                throw;
            }


        }

    }
}
