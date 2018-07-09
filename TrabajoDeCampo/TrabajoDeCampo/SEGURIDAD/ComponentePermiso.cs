using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoDeCampo.BO
{
    public abstract class ComponentePermiso
    {
        public virtual void agregarComponente(ComponentePermiso componente) { }

        public virtual void sacarComponente(ComponentePermiso componente) { }

        public virtual String MostrarInformacion(ComponentePermiso componente) {
            return "";
        }
    }
}
