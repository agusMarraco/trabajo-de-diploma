using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoDeCampo
{
    public class Tutor
    {
        private long _id;

        public long id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _nombre;

        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private string _apellido;

        public string apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }

        private string _telefono1;

        public string telefono1
        {
            get { return _telefono1; }
            set { _telefono1 = value; }
        }

        private string _telefono2;

        public string telefono2
        {
            get { return _telefono2; }
            set { _telefono2 = value; }
        }


        private string _dni;

        public string dni
        {
            get { return _dni; }
            set { _dni = value; }
        }


        private string _email;

        public string email
        {
            get { return _email; }
            set { _email = value; }
        }

        private bool _asignado;

        public bool asignado
        {
            get { return _asignado; }
            set { _asignado = value; }
        }



    }
}
