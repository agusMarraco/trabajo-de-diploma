using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoDeCampo.BO;

namespace TrabajoDeCampo
{
    public class Patente : ComponentePermiso
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

        private ComponentePermiso _familia;

        public ComponentePermiso familia
        {
            get { return _familia; }
            set { _familia = value; }
        }

        public override String MostrarInformacion(ComponentePermiso componente)
        {
            return this.descripcion;
        }

        private Boolean _bloqueada;

        public Boolean bloqueada
        {
            get { return _bloqueada; }
            set { _bloqueada = value; }
        }

        private Boolean _asignada;

        public Boolean asignada
        {
            get { return _asignada; }
            set { _asignada = value; }
        }



    }
}
