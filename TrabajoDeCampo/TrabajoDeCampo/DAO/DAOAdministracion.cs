using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoDeCampo.DAO
{
    public class DAOAdministracion
    {
        //HORARIOS
        public List<Horario> listarHorarios(String filtro, String valor, String orden) { return null; }

        //VERIFICAR QUE EL HORARIO SEA VALIDO
        public Boolean verificarRestricciones(long idCurso, long idMateria, long dia, long idDocente, long idModulo) { return true; }

        public void guardarHorario(Horario horario) { }

        public void actualizarHorario(Horario horario) { }

        public void borrarHorario(Horario horario) { }

        //Cursos

        public List<Curso> listarCursos(String filtro, String valor, String orden) { return null; }

        public void guardarCurso(Curso curso) { }

        public void actualizarCurso(Curso curso) { }

        public void borrarCurso(Curso curso) { }

        public Boolean chequearCodigoUnico(String codigoCurso) { return true; }

        public Boolean chequearCantidadCurso(Curso curso) { return true; }

        public List<Curso> listarCursosExcedidos() { return null; }
        public Boolean chequearCursoVacio(Curso curso) { return true; }



        //MATERIAS

        public void actualizarMateriasAsignadas(Nivel nivel, List<Materia> materias) { }
        public List<Materia> traerMateriasPorNivel(Nivel nivel) { return null; }
        public List<Materia> listarMaterias(String filtro, String valor, String orden) { return null; }

        public void guardarMateria(Materia materia) { }

        public void actualizarMateria(Materia materia) { }

        public void borrarMateria(Materia materia) { }

        public Boolean verificarMateria(Materia materia) { return true; } //chequear repetidos

        public Boolean materiaEstaAsignada(Materia materia) { return true;  } // antes de borrar
        //NIVELES
        public List<Nivel> listarNiveles(String filtro, String valor, String orden) { return null; }

        //Alumnos
        public List<Alumno> listarAlumnosPorCursoYNivel(Nivel nivel, Curso curso) { return null; }

        public void promocionarAlumno(Alumno alumno, Curso curso) { }

    }
}
