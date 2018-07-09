using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoDeCampo.BO;

namespace TrabajoDeCampo
{
    public class Familia : ComponentePermiso
    {
        private long _id;

        public long id
        {
            get { return _id; }
            set { _id = value; }
        }

        private String _nombre;

        public String nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private List<ComponentePermiso> _patentes;

        public List<ComponentePermiso> patentes
        {
            get { return _patentes; }
            set { _patentes = value; }
        }

        public override void agregarComponente(ComponentePermiso componente) { }

        public override void sacarComponente(ComponentePermiso componente) { }

        public override String MostrarInformacion(ComponentePermiso componente)
        {
            return "";
        }

        private Boolean _bloqueada;

        public Boolean bloqueada
        {
            get { return _bloqueada; }
            set { _bloqueada = value; }
        }


    }
}
