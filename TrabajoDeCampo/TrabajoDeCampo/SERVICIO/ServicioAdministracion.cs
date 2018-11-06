using System;
using System.Collections.Generic;
using System.Data;
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
        public DataTable listarHorarios(String filtro, String valor, String orden) {
            List<Horario> horarios = this.daoAdministracion.listarHorarios(filtro, valor, orden);

            DataTable datatable = new DataTable("horarios");
            datatable.Columns.Add(new DataColumn()
            {
                ColumnName = "modulo",
                DataType = typeof(Modulo),
                
            });
            datatable.Columns.Add(new DataColumn()
            {
                ColumnName = "lunes",
                DataType = typeof(Horario)
            });
            datatable.Columns.Add(new DataColumn()
            {
                ColumnName = "martes",
                DataType = typeof(Horario)
            });
            datatable.Columns.Add(new DataColumn()
            {
                ColumnName = "miercoles",
                DataType = typeof(Horario)
            });
            datatable.Columns.Add(new DataColumn()
            {
                ColumnName = "jueves",
                DataType = typeof(Horario)
            });
            datatable.Columns.Add(new DataColumn()
            {
                ColumnName = "viernes",
                DataType = typeof(Horario)
            });


            Dictionary<long, List<Horario>> horariosPorCurso = new Dictionary<long, List<Horario>>();
            foreach (Horario hor in horarios)
            {
                if (horariosPorCurso.ContainsKey(hor.curso.id))
                {
                    horariosPorCurso[(int)hor.curso.id].Add(hor);
                }
                else
                {
                    List<Horario> temp = new List<Horario>();
                    temp.Add(hor);
                    horariosPorCurso.Add(hor.curso.id, temp);
                }
            }

            foreach (KeyValuePair<long,List<Horario>> iter in horariosPorCurso)
            {
                Dictionary<long, List<Horario>> horariosPorModulo = new Dictionary<long, List<Horario>>();
                horariosPorModulo.Add(1,new List<Horario>());
                horariosPorModulo.Add(2, new List<Horario>());
                horariosPorModulo.Add(3, new List<Horario>());
                horariosPorModulo.Add(4, new List<Horario>());
                horariosPorModulo.Add(5, new List<Horario>());

                foreach (Horario horario in iter.Value)
                {
                    horariosPorModulo[horario.modulo.id].Add(horario);
                }

                foreach (KeyValuePair<long, List<Horario>> item in horariosPorModulo)
                {
                    DataRow row = datatable.NewRow();
                    row.BeginEdit();
                    foreach (Horario hor in item.Value)
                    {

                        row.SetField(hor.dia, hor);
                        //DataRowExtensions.SetField(row, hor.dia, hor);
                        
                        
                    }
                    row.EndEdit();
                    if(item.Value.Count > 0)
                    {

                        row.BeginEdit();
                        row.SetField(0, item.Value.First().modulo);
                        row.EndEdit();
                        datatable.Rows.Add(row);
                    }
                        
                }
                    
            }

            return datatable;

        }

        public void guardarHorario(Horario horario) {
            Boolean disponible = this.daoAdministracion.verificarRestricciones(horario.curso.id, horario.materia.id, 
                horario.dia, horario.docente.legajo , horario.modulo.id,horario.id);
            if (disponible)
            {
                this.daoAdministracion.guardarHorario(horario);
            }
            else
            {
                throw new Exception("Horario No Disponible");
            }
        }

        public void actualizarHorario(Horario horario) {
            //long idCurso, long idMateria, long dia, long idDocente, long idModulo,long idHorario
            Boolean disponible = this.daoAdministracion.verificarRestricciones(horario.curso.id, horario.materia.id, horario.dia ,horario.docente.legajo, 
               horario.modulo.id,horario.id);
            if (disponible)
            {
                this.daoAdministracion.actualizarHorario(horario);
            }
            else
            {
                throw new Exception("Horario No Disponible");
            }
        }

        public void borrarHorario(Horario horario) {
            this.daoAdministracion.borrarHorario(horario);
        }

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
                throw new Exception("TIENE ALUMNOS");
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
        public List<Alumno> listarAlumnosPorCursoYNivel(Nivel nivel, Curso curso) {

            return daoAdministracion.listarAlumnosPorCursoYNivel(nivel,curso);
        }

        public Boolean promocionarAlumno(Alumno alumno, Curso curso) {
            this.daoAdministracion.promocionarAlumno(alumno, curso);
            return this.daoAdministracion.chequearCantidadCurso(curso);
        }




    }
}
