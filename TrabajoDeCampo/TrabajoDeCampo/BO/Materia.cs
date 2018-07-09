using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoDeCampo
{
    public class Materia
    {

        private long _id;

        public long id
        {
            get { return _id; }
            set { _id = value; }
        }

        private String _descripcion;

        public String descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        private String _tipo;

        public String tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        private Nivel _nivel;

        public Nivel nivel
        {
            get { return _nivel; }
            set { _nivel = value; }
        }




    }
}
