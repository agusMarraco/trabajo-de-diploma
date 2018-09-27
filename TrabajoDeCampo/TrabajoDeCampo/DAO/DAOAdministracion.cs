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
        public List<Horario> listarHorarios(String filtro, String valor, String orden) { return null; }

        //VERIFICAR QUE EL HORARIO SEA VALIDO
        public Boolean verificarRestricciones(long idCurso, long idMateria, long dia, long idDocente, long idModulo) { return true; }

        public void guardarHorario(Horario horario) { }

        public void actualizarHorario(Horario horario) { }

        public void borrarHorario(Horario horario) { }

        //Cursos

        public List<Curso> listarCursos(String filtro, String valor, String orden) {
            SqlDataReader reader = null;
            List<Curso> cursos = new List<Curso>();
            String query = " SELECT NIV.NIV_ID,NIV.NIV_CODIGO, CUR.* FROM CURSO CUR INNER JOIN NIVEL NIV ON NIV.NIV_ID = CUR.CUR_NIVEL_ID WHERE CUR.CUR_BORRADO IS NULL ";
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
                    if(reader.IsDBNull(3)){
                        Orientacion ori = new Orientacion();
                        ori.codigo = reader.GetValue(3).ToString();
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
        public List<Alumno> listarAlumnosPorCursoYNivel(Nivel nivel, Curso curso) { return null; }

        public void promocionarAlumno(Alumno alumno, Curso curso) { }

    }
}
