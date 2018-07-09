using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoDeCampo.DAO;
namespace TrabajoDeCampo.SERVICIO
{
    public class ServicioAlumnos
    {
        private DAOAlumnos _daoAlumnos;

        public DAOAlumnos daoAlumnos
        {
            get { return _daoAlumnos; }
            set { _daoAlumnos = value; }
        }


        //alumnos
        public void guardarAlumno(Alumno alumno) { }
        public void actualizarAlumno(Alumno alumno) { }
        public void borrarAlumno(Alumno alumno) { }

        public void repetirAlumno(Alumno alumno) { }

        public List<Orientacion> listarOrientacion() { return null; }
        public List<Alumno> listarAlumno(String filtro, String valor, String orden) { return new List<Alumno>(); }

        public Alumno buscarAlumno(long id) { return new Alumno(); }
        //amonestaciones
        public void guardarAmonestacion() { }

        public List<Amonestacion> listarAmonestaciones(Alumno alumno) { return null; }

        //Inasistencias

        public void guardarInasistencia(InasistenciaAlumno inasistencia) { }

        public void modificarInasistencia(InasistenciaAlumno inasistencia) { }

        public List<InasistenciaAlumno> listarInasistencias() { return null; }

        //Tutores
        public void guardarTutor(Tutor tutor) { }
        public void modificarTutor(Tutor tutor) { }
        public List<Tutor> listarTutor(String filtro, String valor, String orden) { return null; }
        public void BorrarTutor(Tutor tutor) { }


        

    }
}
