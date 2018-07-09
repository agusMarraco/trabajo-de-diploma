using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoDeCampo.BO;
namespace TrabajoDeCampo
{
    public class Docente
    {
        private long _legajo;

        public long legajo
        {
            get { return _legajo; }
            set { _legajo = value; }
        }

        private String _apellido;

        public String apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }

        private String _nombre;

        public String nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private String  _dni;

        public String  dni
        {
            get { return _dni; }
            set { _dni = value; }
        }

        private DateTime _fechaNacimiento;

        public DateTime fechaNacimiento
        {
            get { return _fechaNacimiento; }
            set { _fechaNacimiento = value; }
        }

        private String _direccion;

        public String direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        private String _telefono;

        public String telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }

        private InasistenciaDocente _inasistencias;

        public InasistenciaDocente inasistencias
        {
            get { return _inasistencias; }
            set { _inasistencias = value; }
        }







    }
}
