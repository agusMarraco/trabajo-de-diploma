using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoDeCampo
{
    public class PlanillaDeEvaluacion
    {
        private long _id;

        public long id
        {
            get { return _id; }
            set { _id = value; }
        }

        private Alumno _alumno;

        public Alumno Alumno
        {
            get { return _alumno; }
            set { _alumno = value; }
        }

        private Nivel _nivel;

        public Nivel nivel
        {
            get { return _nivel; }
            set { _nivel = value; }
        }

        private int _trimestre1;

        public int trimestre1
        {
            get { return _trimestre1; }
            set { _trimestre1 = value; }
        }

        private int _trimestre2;

        public int trimestre2
        {
            get { return _trimestre2; }
            set { _trimestre2 = value; }
        }

        private int _trimestre3;

        public int trimestre3
        {
            get { return _trimestre3; }
            set { _trimestre3 = value; }
        }

        private int _notaFinal;

        public int notaFinal
        {
            get { return _notaFinal; }
            set { _notaFinal = value; }
        }

        private Boolean _condicion;

        public Boolean condicion
        {
            get { return _condicion; }
            set { _condicion = value; }
        }

        private Materia _materia;

        public Materia materia
        {
            get { return _materia; }
            set { _materia = value; }
        }








    }
}
