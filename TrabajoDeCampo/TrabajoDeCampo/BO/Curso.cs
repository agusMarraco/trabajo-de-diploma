using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoDeCampo
{
    public class Curso
    {
        private long _id;

        public long id
        {
            get { return _id; }
            set { _id = value; }
        }

        private Nivel _nivel;

        public Nivel nivel
        {
            get { return _nivel; }
            set { _nivel = value; }
        }

        private int _capacidad;

        public int capacidad
        {
            get { return _capacidad; }
            set { _capacidad = value; }
        }

        private String _codigo;

        public String codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        private String _turno;

        public String turno
        {
            get { return _turno; }
            set { _turno = value; }
        }

        private String _letra;

        public String letra
        {
            get { return _letra; }
            set { _letra = value; }
        }

        private List<Alumno> _alumnos;

        public List<Alumno> alumnos
        {
            get { return _alumnos; }
            set { _alumnos = value; }
        }






    }
}
