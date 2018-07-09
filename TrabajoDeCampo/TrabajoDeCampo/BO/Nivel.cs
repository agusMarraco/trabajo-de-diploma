using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoDeCampo
{
    public class Nivel
    {
        private long _id;

        public long id
        {
            get { return _id; }
            set { _id = value; }
        }

        private String _codigo;

        public String codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        private String _descripcion;

        public String descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        private Orientacion _orientacion;

        public Orientacion orientacion
        {
            get { return _orientacion; }
            set { _orientacion = value; }
        }

        private List<Curso> _cursos;

        public List<Curso> cursos
        {
            get { return _cursos; }
            set { _cursos = value; }
        }

        private List<Materia> _materias;

        public List<Materia> materia
        {
            get { return _materias; }
            set { _materias = value; }
        }


    }
}
