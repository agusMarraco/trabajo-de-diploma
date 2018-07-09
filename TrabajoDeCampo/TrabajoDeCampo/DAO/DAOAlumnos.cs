using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoDeCampo.DAO
{
    public class DAOAlumnos
    {
        //recibe TUTOR o ALUMNO
        public Boolean verificarDNI(String dni, String tipo) { return true; }
        // listar orientaciones
        public List<Orientacion> listarOrientacion() { return null; }
        
        //alumnos
        public void guardarAlumno(Alumno alumno) { }
        public void actualizarAlumno(Alumno alumno) { }
        public void borrarAlumno(Alumno alumno) { }

        public void repetirAlumno(Alumno alumno) { }

        public List<Alumno> listarAlumno(String filtro, String valor, String orden) { return new List<Alumno>(); }

        public Alumno buscarAlumno(long id) { return new Alumno(); }
        //amonestaciones
        public void guardarAmonestacion(Amonestacion amonestacion) { }

        public List<Amonestacion> listarAmonestaciones(Alumno alumno) { return null; }

        //Inasistencias

        public void guardarInasistencia(InasistenciaAlumno inasistencia) { }

        public void modificarInasistencia(InasistenciaAlumno inasistencia) { }

        public List<InasistenciaAlumno> listarInasistencias() { return null; }

        //Tutores
        public void guardarTutor(Tutor tutor) { }
        public void modificarTutor(Tutor tutor) { }
        public List<Tutor> listarTutor(String filtro, String valor, String orden) { return null; }
        public void borrarTutor(Tutor tutor) { }

        public Boolean verificarTutor(long id) { return true; }


    }
}
