using System;
using System.Windows.Forms;
using TrabajoDeCampo.Pantallas.Seguridad;
using TrabajoDeCampo.Pantallas.Administración;
using TrabajoDeCampo.Pantallas;
using TrabajoDeCampo.Pantallas.Alumnos;

namespace TrabajoDeCampo
{
    class Program
    {

        public static void Main(string[] args)
        {
            //Pantallas.Seguridad.Menu au = new Pantallas.Seguridad.Menu();

            //au.Show();
            //au.Focus();


            //ListarFamilias fl = new ListarFamilias();
            //fl.Show();

            //Alta_ModificarFamilia af = new Alta_ModificarFamilia();
            //af.Show(); 

            //Modificar_Familia a = new Modificar_Familia();
            //a.Show();
            //a.Focus();

            //AltaModificacionUsuario usu = new AltaModificacionUsuario();
            //usu.Show();
            //usu.Focus();
            //a.Show();
            //a.Focus();

            //Login log = new Login();
            //log.Show();
            //log.Focus();

            //Bitácora b = new Bitácora();
            //b.Show();
            //b.Focus();

            //FalloConexión c = new FalloConexión();
            //c.Show();
            //c.Focus();

            //Respaldo_Base_de_Datos d = new Respaldo_Base_de_Datos();
            //d.Show();
            //d.Focus();

            //Restaurar_Backup e = new Restaurar_Backup();
            //e.Show();
            //e.Focus();

            //AltaModificacionCurso amc = new AltaModificacionCurso();
            //amc.Show();

            //AltaModificacionHorario amh = new AltaModificacionHorario();
            //amh.Show();

            //AltaModificacionMateria amm = new AltaModificacionMateria() ;
            //amm.Show();

            //AsignacionDeMaterias asg = new AsignacionDeMaterias();
            //asg.Show();

            //Cursos cur = new Cursos();
            //cur.Show();

            //Horarios hor = new Horarios();
            //hor.Show();

            //Materias mat = new Materias();
            //mat.Show();

            //PromocionDeAlumnos promo= new PromocionDeAlumnos();
            //promo.Show();

            AltaModificacionAlumno aa = new AltaModificacionAlumno();
            aa.Show();

            AltaModificacionTutor tt = new AltaModificacionTutor();
            tt.Show();

            Alumnos a = new Alumnos();
            a.Show();

            Amonestaciones am = new Amonestaciones();
            am.Show();

            Inasistencias ina = new Inasistencias();
            ina.Show();

            Tutores tu = new Tutores();
            tu.Show();

            Application.Run();

        }
    }
}
