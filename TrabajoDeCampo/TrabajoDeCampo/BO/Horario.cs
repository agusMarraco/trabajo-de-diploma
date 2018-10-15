using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoDeCampo
{
    public class Horario
    {
        private long _id;

        public long id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _dia;

        public int dia
        {
            get { return _dia; }
            set { _dia = value; }
        }

        private Curso _curso;

        public Curso curso
        {
            get { return _curso; }
            set { _curso = value; }
        }

        private Materia _materia;

        public Materia materia
        {
            get { return _materia; }
            set { _materia = value; }
        }

        private Docente _docente;

        public Docente docente
        {
            get { return _docente; }
            set { _docente = value; }
        }

        private Modulo _modulo;

        public Modulo modulo
        {
            get { return _modulo; }
            set { _modulo = value; }
        }

        public override string ToString()
        {
            return new StringBuilder(this.curso.codigo).Append(Environment.NewLine).Append(this.materia.nombre).
                                Append(Environment.NewLine).Append(this.docente.apellido + ", " + this.docente.nombre).ToString();
        }




    }
}
