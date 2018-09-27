using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoDeCampo.DAO;

namespace TrabajoDeCampo.SERVICIO
{
    public class ServicioAdministracion
    {
        private DAOAdministracion _daoAdministracion;

        public DAOAdministracion daoAdministracion
        {
            get { return _daoAdministracion; }
            set { _daoAdministracion = value; }
        }

        public ServicioAdministracion(){
            this.daoAdministracion = new DAOAdministracion();
        }
        //HORARIOS
        public List<Horario> listarHorarios(String filtro, String valor, String orden) { return null; }

        public void guardarHorario(Horario horario) { }

        public void actualizarHorario(Horario horario) { }

        public void borrarHorario(Horario horario) { }

        //Cursos

        public List<Curso> listarCursos(String filtro, String valor, String orden) {
            return this.daoAdministracion.listarCursos(filtro, valor, orden);

            }

        public void guardarCurso(Curso curso) {
            Boolean codigoUnico = this.daoAdministracion.chequearCodigoUnico(curso.codigo, 0);
            if (codigoUnico)
            {
                this.daoAdministracion.guardarCurso(curso);
            }else
            {
                throw new Exception("CODIGO REPETIDO");
            }
        }

        public void actualizarCurso(Curso curso) {
            Boolean codigoUnico = this.daoAdministracion.chequearCodigoUnico(curso.codigo, curso.id);
            if (codigoUnico)
            {
                this.daoAdministracion.actualizarCurso(curso);
            }else
            {
                throw new Exception("CODIGO REPETIDO");
            }
        }

        public void borrarCurso(Curso curso) {
            Boolean desasignado = this.daoAdministracion.chequearCursoVacio(curso);
            if (desasignado)
            {
                this.daoAdministracion.borrarCurso(curso);
            }else
            {
               // throw new Exception("TIENE ALUMNOS");
            }
        }

        public List<Curso> listarCursosExcedidos() {
            return this.daoAdministracion.listarCursosExcedidos(); }


        //MATERIAS
        public void actualizarMateriasAsignadas(Nivel nivel ,List<Materia> materias) {

            this.daoAdministracion.actualizarMateriasAsignadas(nivel, materias);
        }
        public List<Materia> listarMaterias(String filtro, String valor, String orden) {
            return this.daoAdministracion.listarMaterias(null, null, null);
        }

        public List<Materia> traerMateriasPorNivel(Nivel nivel) {
            return this.daoAdministracion.traerMateriasPorNivel(nivel);
        }

        public void guardarMateria(Materia materia)
        {
            Boolean existe = this.daoAdministracion.verificarMateria(materia);
            if (!existe)
            {
                this.daoAdministracion.guardarMateria(materia);
            }
            else
            {
                throw new Exception("EXISTE");
            }
        }

        public void actualizarMateria(Materia materia) {
            Boolean existe = this.daoAdministracion.verificarMateria(materia);
            if (!existe)
            {
                this.daoAdministracion.actualizarMateria(materia);
            }
            else
            {
                throw new Exception("EXISTE");
            }
        }

        public void borrarMateria(Materia materia) {

            Boolean estaAsignada = this.daoAdministracion.materiaEstaAsignada(materia);
            if (!estaAsignada)
            {
                this.daoAdministracion.borrarMateria(materia);
            }
            else
            {
                throw new Exception("ASIGNADA");
            }
        }

        //NIVELES

        public List<Nivel> listarNiveles(String filtro, String valor, String orden) {
            return this.daoAdministracion.listarNiveles(filtro,valor,orden);
        }
        //Alumnos
        public List<Alumno> listarAlumnosPorCursoYNivel(Nivel nivel, Curso curso) { return null; }

        public void promocionarAlumno(Alumno alumno, Curso curso) { }




    }
}
