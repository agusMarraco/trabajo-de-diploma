using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoDeCampo
{
    public class Orientacion
    {
        private String _codigo;

        public String codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        private String _nombre;

        public String nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public override string ToString()
        {
            return this.nombre;
        }
    }
}
