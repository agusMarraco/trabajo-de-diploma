using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoDeCampo
{
    public class Amonestacion
    {
        private Alumno _alumno;

        public Alumno alumno
        {
            get { return _alumno; }
            set { _alumno = value; }
        }

        private DateTime _fecha;

        public DateTime fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        private string _motivo;

        public string motivo
        {
            get { return _motivo; }
            set { _motivo = value; }
        }
        




    }
}
