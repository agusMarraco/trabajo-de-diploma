using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoDeCampo.BO
{
    public class Idioma
    {
        private long _id;

        public long id
        {
            get { return _id; }
            set { _id = value; }
        }

        private String _codigo;

        public String codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        private String _descripcion;

        public String descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }



    }
}
