using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoDeCampo.BO;
using TrabajoDeCampo.DAO;

namespace TrabajoDeCampo.SERVICIO
{
    public class ServicioNotas
    {
        private DAONotas _daoNotas;

        public DAONotas daoNotas
        {
            get { return _daoNotas; }
            set { _daoNotas = value; }
        }

        //notas de mesa

        public List<NotaAbstracta> listarNotas(String filtro, String valor, String orden) { return null; }


        //notas regulares
        public List<Alumno> buscarAlumnosPorNivCurMat(long idNivel, long idCurso, long idMateria) { return null; }

        public void guardarNota(Nota nota) { }

        public void modificarNota(Nota nota) { }


        //planillas de evaluacion

        public List<PlanillaDeEvaluacion> listarPlanillas(String filtro, String valor, String orden) { return null; }
        public void CerrarTrimestre(long numeroTrimestre, PlanillaDeEvaluacion planilla) { }

        public void calcularNotaFinal(PlanillaDeEvaluacion planilla) { }

        public List<PlanillaDeEvaluacion> listarMateriasDesaprobadasPorAlumno(Alumno alumno) { return null; }

    }
}
