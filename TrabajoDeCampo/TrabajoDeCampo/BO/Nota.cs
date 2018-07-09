using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoDeCampo.BO
{
    public class Nota : NotaAbstracta
    {
        private int _trimestre;

        public int trimestre
        {
            get { return _trimestre; }
            set { _trimestre = value; }
        }


    }
}
