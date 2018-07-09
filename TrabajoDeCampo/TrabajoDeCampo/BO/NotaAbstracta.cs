using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoDeCampo.BO
{
    public abstract class NotaAbstracta
    {
        private long _id;

        public long id
        {
            get { return _id; }
            set { _id = value; }
        }

        private Docente _docente;

        public Docente docente
        {
            get { return _docente; }
            set { _docente = value; }
        }

        private Alumno _alumno;

        public Alumno alumno
        {
            get { return _alumno; }
            set { _alumno = value; }
        }

        private Curso _curso;

        public Curso curso
        {
            get { return _curso; }
            set { _curso = value; }
        }

        private int _nota;

        public int nota
        {
            get { return _nota; }
            set { _nota = value; }
        }

        private Materia _materia;

        public Materia materia
        {
            get { return _materia; }
            set { _materia = value; }
        }








    }
}
