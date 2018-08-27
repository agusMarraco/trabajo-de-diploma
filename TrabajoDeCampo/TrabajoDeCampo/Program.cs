using System;
using System.Windows.Forms;
using TrabajoDeCampo.Pantallas;
using TrabajoDeCampo.Pantallas.Seguridad;

namespace TrabajoDeCampo
{
    class Program
    {
        [STAThreadAttribute]
        public static void Main(string[] args)
        {

            Pantallas.Seguridad.Menu menu = new Pantallas.Seguridad.Menu();
            menu.Show();

            //ActualizarConexion conec = new ActualizarConexion();
            //conec.Show();

            //FalloConexión fallo = new FalloConexión();
            //fallo.Show();

            new testeador().Show();

            Application.Run();
        }
    }
}
