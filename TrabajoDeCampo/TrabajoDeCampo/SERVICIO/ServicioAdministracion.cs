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


        //HORARIOS
        public List<Horario> listarHorarios(String filtro, String valor, String orden) { return null; }

        public void guardarHorario(Horario horario) { }

        public void actualizarHorario(Horario horario) { }

        public void borrarHorario(Horario horario) { }

        //Cursos

        public List<Curso> listarCursos(String filtro, String valor, String orden) { return null; }

        public void guardarCurso(Curso curso) { }

        public void actualizarCurso(Curso curso) { }

        public void borrarCurso(Curso curso) { }

        public List<Curso> listarCursosExcedidos() { return null; }


        //MATERIAS
        public void actualizarMateriasAsignadas(Nivel nivel ,List<Materia> materias) { }
        public List<Materia> listarMaterias(String filtro, String valor, String orden) { return null; }

        public List<Materia> traerMateriasPorNivel(Nivel nivel) { return null; }

        public void guardarMateria(Materia materia) { }

        public void actualizarMateria(Materia materia) { }

        public void borrarMateria(Materia materia) { }

        //NIVELES

        public List<Nivel> listarNiveles(String filtro, String valor, String orden) { return null; }
        //Alumnos
        public List<Alumno> listarAlumnosPorCursoYNivel(Nivel nivel, Curso curso) { return null; }

        public void promocionarAlumno(Alumno alumno, Curso curso) { }




    }
}
