using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoDeCampo.BO;

namespace TrabajoDeCampo
{
    public class Usuario
    {
        private long _id;

        public long id
        {
            get { return _id; }
            set { _id = value; }
        }

        private String _dni;

        public String dni
        {
            get { return _dni; }
            set { _dni = value; }
        }

        private String _email;

        public String email
        {
            get { return _email; }
            set { _email = value; }
        }

        private String _pass;

        public String pass
        {
            get { return _pass; }
            set { _pass = value; }
        }

        private String _alias;

        public String alias
        {
            get { return _alias; }
            set { _alias = value; }
        }

        private int _intentos;

        public int intentos
        {
            get { return _intentos; }
            set { _intentos = value; }
        }

        private int _baja;

        public int baja
        {
            get { return _baja; }
            set { _baja = value; }
        }

        private String _nombre;

        public String nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private String _apellido;

        public String apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }

        private String _direccion;

        public String direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        private String _telefono;

        public String telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }

        private Idioma _idioma;

        public Idioma idioma
        {
            get { return _idioma; }
            set { _idioma = value; }
        }


        private List<ComponentePermiso> _componentePermisos;

        public List<ComponentePermiso> componentePermisos
        {
            get { return _componentePermisos; }
            set { _componentePermisos = value; }
        }








    }
}
