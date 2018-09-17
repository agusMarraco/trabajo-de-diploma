using System;
using System.Windows.Forms;
using TrabajoDeCampo.Pantallas;
using TrabajoDeCampo.Pantallas.Seguridad;
using TrabajoDeCampo.Pantallas.Administración;
using TrabajoDeCampo.Pantallas.Alumnos;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using TrabajoDeCampo.DAO;

namespace TrabajoDeCampo
{
    class Program
    {
        [STAThreadAttribute]
        public static void Main(string[] args)
        {


            TrabajoDeCampo.Properties.Settings.Default.ConnectionString = "Data Source = " + Environment.MachineName + " ; Initial Catalog = TRABAJO_DIPLOMA ; Integrated Security = True";
            Pantallas.Seguridad.Menu menu = new Pantallas.Seguridad.Menu();
            new Pantallas.Seguridad.Menu().Show();

            //ActualizarConexion conec = new ActualizarConexion();
            //conec.Show();

            //FalloConexión fallo = new FalloConexión();
            //fallo.Show();
            //   encriptarUsuarios();
            //ListarFamilias familias = new ListarFamilias();
            //familias.Show();

            //new testeador().Show();
            //    mostrarTodasLasPantallas();
            Login login = new Login();
            login.Show();
            Application.Run();
                
        }

        public static void mostrarTodasLasPantallas()
        {
            List<Form> pantallas = new List<Form>();
            pantallas.Add(new AltaModificacionCurso());
            pantallas.Add(new AltaModificacionHorario());
            pantallas.Add(new AltaModificacionMateria());
            pantallas.Add(new AsignacionDeMaterias());
            pantallas.Add(new Cursos());
            pantallas.Add(new Horarios());
            pantallas.Add(new Materias());
            pantallas.Add(new PromocionDeAlumnos());
            pantallas.Add(new AltaModificacionAlumno());
            pantallas.Add(new AltaModificacionTutor());
            pantallas.Add(new Alumnos());
            pantallas.Add(new Amonestaciones());
            pantallas.Add(new Inasistencias());
            pantallas.Add(new Tutores());
            pantallas.Add(new AltaModificacionUsuario());
            pantallas.Add(new Bitácora());
            pantallas.Add(new CambiarContraseña());
            pantallas.Add(new FalloConexión());
            pantallas.Add(new ListaDeUsuarios());
            pantallas.Add(new ListarFamilias());
            pantallas.Add(new Login());
            pantallas.Add(new Respaldo_Base_de_Datos());
            pantallas.Add(new Restaurar_Backup());
            pantallas.Add(new Pantallas.Seguridad.Menu());

            foreach (Form item in pantallas)
            {
                item.Show();
            }
        }

        public static void insertsDePatentes()
        {
            
            SqlConnection connection = ConexionSingleton.obtenerConexion();

            StringBuilder sb = new StringBuilder();
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Crear Alumno',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Modificar Alumno',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Ver el listado de alumnos',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Borrar un Alumno',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Registrar Inasistencia',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Registrar Amonestación',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Crear Tutor',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Modificar Tutor',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Borrar Tutor',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Listar Tutores',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Crear Usuario',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Modificar Usuario',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Borrar un Usuario',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Listar Usuarios',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Regenerar Contraseña',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Generar Backups',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Restaurar desde Backup',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Recalcular Dígitos Verificadores',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Ver Bitácora',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Bloquear Usuario',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Modificar Familias',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Listar Familias',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Crear Familia',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Borrar Familia',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Crear Horario',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Listar Horarios',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Modificar Horario',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Borrar Horario',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Crear Curso',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Borrar Curso',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Modificar Curso',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Listar Cursos',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Promocionar Alumnos',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Crear Materia',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Modificar Materia',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Listar Materia',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Borrar Materia',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Asignar Materia a Nivel',0)");
            sb.Append("insert into patente(pat_desc,pat_dvh) values('Generar Reportes',0)");

            SqlCommand query = new SqlCommand("", connection);

            query.CommandText = sb.ToString();

            connection.Open();

            query.ExecuteNonQuery();
            List<Patente> patentes = new List<Patente>();
            SqlDataReader reader;

            query.CommandText = " SELECT * FROM PATENTE";
                reader = query.ExecuteReader();
                while (reader.Read())
                {
                Patente pat = new Patente();

                    pat.descripcion= reader["PAT_DESC"].ToString();
                    pat.id = (long)reader["PAT_ID"];
                patentes.Add(pat);
                }

            reader.Close();
                foreach(Patente item in patentes){
                SqlCommand query2 = new SqlCommand("UPDATE PATENTE SET PAT_DESC = @DESC WHERE PAT_ID = @ID", connection);
                query2.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = item.id;
                query2.Parameters.Add(new SqlParameter("@DESC", System.Data.SqlDbType.NVarChar)).Value = SeguridadUtiles.encriptarAES(item.descripcion);

                query2.ExecuteNonQuery();

            }
                connection.Close();
        }

        public static void encriptarUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            SqlConnection connection = ConexionSingleton.obtenerConexion();

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT usu_alias,usu_id, usu_pass FROM USUARIO  ");


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

                    usu.alias = reader["USU_ALIAS"].ToString();
                    usu.id = (long)reader["USU_ID"];
                    usu.pass = reader["USU_PASS"].ToString();

                    usuarios.Add(usu);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {

            }
            reader.Close();
            foreach (Usuario item in usuarios)
            {
                SqlCommand query2 = new SqlCommand("UPDATE USUARIO SET USU_ALIAS = @ALIAS , USU_PASS=@PASS WHERE USU_ID = @ID", connection);
                query2.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.BigInt)).Value = item.id;
                query2.Parameters.Add(new SqlParameter("@PASS", System.Data.SqlDbType.VarChar)).Value = SeguridadUtiles.encriptarMD5(item.pass);
                query2.Parameters.Add(new SqlParameter("@ALIAS", System.Data.SqlDbType.NVarChar)).Value = SeguridadUtiles.encriptarAES(item.alias);

                query2.ExecuteNonQuery();
            }

            connection.Close();
        }
    }
}
