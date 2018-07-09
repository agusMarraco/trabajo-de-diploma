using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoDeCampo
{
    public class Alumno
    {
        private long _legajo;

        public long legajo
        {
            get { return _legajo; }
            set { _legajo = value; }
        }

        private string _nombre;

        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private string _apellido;

        public string apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }
        private string _dni;

        public string dni
        {
            get { return _dni; }
            set { _dni = value; }
        }

        private string _domicilio;

        public string domicilio
        {
            get { return _domicilio; }
            set { _domicilio = value; }
        }

        private Curso _curso;

        public Curso curso
        {
            get { return _curso; }
            set { _curso = value; }
        }

        private DateTime _fechaNacimiento;

        public DateTime fechaNacimiento
        {
            get { return _fechaNacimiento; }
            set { _fechaNacimiento = value; }
        }

        private Orientacion _orientacion;

        public Orientacion orientacion
        {
            get { return _orientacion; }
            set { _orientacion = value; }
        }

        private List<Tutor> _tutores;

        public List<Tutor> tutores
        {
            get { return _tutores; }
            set { _tutores = value; }
        }

        private List<InasistenciaAlumno> _inasistencias;

        public List<InasistenciaAlumno> inasistencias
        {
            get { return _inasistencias; }
            set { _inasistencias = value; }
        }

        private List<Amonestacion> _amonestaciones;

        public List<Amonestacion> amonestaciones
        {
            get { return _amonestaciones; }
            set { _amonestaciones = value; }
        }

        private Boolean _puedeRepetir;

        public Boolean puedeRepetir
        {
            get { return _puedeRepetir; }
            set { _puedeRepetir = value; }
        }




    }
}
