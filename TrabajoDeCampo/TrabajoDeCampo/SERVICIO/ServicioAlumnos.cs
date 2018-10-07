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

        public ServicioAlumnos()
        {
            this.daoAlumnos = new DAOAlumnos();
        }

        //alumnos
        public void guardarAlumno(Alumno alumno) {
            Boolean dniRepetido = this.daoAlumnos.verificarDNI(alumno.dni, "alumno", alumno.legajo);
            if (!dniRepetido)
            {
                this.daoAlumnos.guardarAlumno(alumno);
            }
            else
            {
                throw new Exception("DNI");
            }
            
        }
        public void actualizarAlumno(Alumno alumno) {
            Boolean dniRepetido = this.daoAlumnos.verificarDNI(alumno.dni, "alumno", alumno.legajo);
            if (!dniRepetido)
            {
                this.daoAlumnos.actualizarAlumno(alumno);
            }
            else
            {
                throw new Exception("DNI");
            }

        }
        public void borrarAlumno(Alumno alumno) {
            this.daoAlumnos.borrarAlumno(alumno);
        }

        public void repetirAlumno(Alumno alumno) {
            this.daoAlumnos.repetirAlumno(alumno);
        }

        public List<Orientacion> listarOrientacion() {
            return this.daoAlumnos.listarOrientacion();
        }
        public List<Alumno> listarAlumno(String filtro, String valor, String orden) {
            return this.daoAlumnos.listarAlumno(filtro, valor, orden);
         }

        public Alumno buscarAlumno(long id) {
            return this.daoAlumnos.buscarAlumno(id);
        }
        //amonestaciones
        public void guardarAmonestacion(Amonestacion amonestacion) {
            this.daoAlumnos.guardarAmonestacion(amonestacion);
        }

        public List<Amonestacion> listarAmonestaciones(String filtro, String valor, String orden) {
            return this.daoAlumnos.listarAmonestaciones(filtro, valor, orden);
        }

        //Inasistencias

        public void guardarInasistencia(InasistenciaAlumno inasistencia) {
            this.daoAlumnos.guardarInasistencia(inasistencia);
        }

        public void modificarInasistencia(InasistenciaAlumno inasistencia) {
            this.daoAlumnos.modificarInasistencia(inasistencia);
        }

        public List<InasistenciaAlumno> listarInasistencias(String filtro, String valor, String orden) {
            return this.daoAlumnos.listarInasistencias(filtro, valor, orden);
        }

        //Tutores
        public void guardarTutor(Tutor tutor) {

            Boolean dniRepetido = this.daoAlumnos.verificarDNI(tutor.dni, "tutor", tutor.id);
            if (dniRepetido)
            {
                throw new Exception("DNI REPETIDO");
            }
            else
            {
                this.daoAlumnos.guardarTutor(tutor);
            }

        }
        public void modificarTutor(Tutor tutor) {

            Boolean dniRepetido = this.daoAlumnos.verificarDNI(tutor.dni, "tutor", tutor.id);
            if (dniRepetido)
            {
                throw new Exception("DNI REPETIDO");
            }
            else
            {
                this.daoAlumnos.modificarTutor(tutor);
            }
        }
        public List<Tutor> listarTutor(String filtro, String valor, String orden) {

            return this.daoAlumnos.listarTutor(filtro, valor, orden);
        }
        public void BorrarTutor(Tutor tutor) {
            Boolean desasignado = this.daoAlumnos.verificarTutor(tutor.id);
            if (!desasignado)
            {
                throw new Exception("TUTOR ASIGNADO");
            }
            else
            {
                this.daoAlumnos.borrarTutor(tutor);
            }
        }


        

    }
}
