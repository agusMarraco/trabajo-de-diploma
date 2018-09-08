using System;
using System.Windows.Forms;
using TrabajoDeCampo.Pantallas;
using TrabajoDeCampo.Pantallas.Seguridad;
using TrabajoDeCampo.Pantallas.Administración;
using TrabajoDeCampo.Pantallas.Alumnos;
using System.Collections.Generic;

namespace TrabajoDeCampo
{
    class Program
    {
        [STAThreadAttribute]
        public static void Main(string[] args)
        {

            //Pantallas.Seguridad.Menu menu = new Pantallas.Seguridad.Menu();
            //new Pantallas.Seguridad.Menu().Show();

            //ActualizarConexion conec = new ActualizarConexion();
            //conec.Show();

            //FalloConexión fallo = new FalloConexión();
            //fallo.Show();

            //new testeador().Show();
            mostrarTodasLasPantallas();
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
    }
}
