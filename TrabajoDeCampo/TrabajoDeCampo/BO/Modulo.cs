using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoDeCampo
{
    public class Modulo
    {
        private long _id;

        public long id
        {
            get { return _id; }
            set { _id = value; }
        }

        private DateTime _horaInicio;

        public DateTime horaInicio
        {
            get { return _horaInicio; }
            set { _horaInicio = value; }
        }

        private DateTime _horaFin;

        public DateTime horaFin
        {
            get { return _horaFin; }
            set { _horaFin = value; }
        }



    }
}
