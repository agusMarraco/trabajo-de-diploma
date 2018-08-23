using System;
using System.Windows.Forms;
using TrabajoDeCampo.Pantallas;
using TrabajoDeCampo.Pantallas.Seguridad;

namespace TrabajoDeCampo
{
    class Program
    {

        public static void Main(string[] args)
        {

            Pantallas.Seguridad.Menu menu = new Pantallas.Seguridad.Menu();
            menu.Show();

            ActualizarConexion conec = new ActualizarConexion();
            conec.Show();

            FalloConexión fallo = new FalloConexión();
            fallo.Show();

            Application.Run();
        }
    }
}
