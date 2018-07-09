using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoDeCampo
{
    public class Bitacora
    {
        private long _id;

        public long id
        {
            get { return _id; }
            set { _id = value; }
        }

        private Usuario _usuario;

        public Usuario usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        private String _mensaje;

        public String mensaje
        {
            get { return _mensaje; }
            set { _mensaje = value; }
        }

        private long _criticidad;

        public long criticidad
        {
            get { return _criticidad; }
            set { _criticidad = value; }
        }

        private DateTime _fecha;

        public DateTime fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }





    }
}
