using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoDeCampo
{
    public class InasistenciaAlumno
    {

        private Alumno _alumno;

        public Alumno Alumno
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

        private Double _valor;

        public Double valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

        private Boolean _justificada;

        public Boolean Justificada
        {
            get { return _justificada; }
            set { _justificada = value; }
        }




    }
}
