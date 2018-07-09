using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoDeCampo.BO;
namespace TrabajoDeCampo.DAO
{
    public class DAONotas
    {
        //ObjetoComun para notas regulares y de mesa
        public List<NotaAbstracta> listarNotas(String filtro, String valor, String orden,String tipoDeNota) { return null; }

        public List<Alumno> buscarAlumnosPorNivCurMat(long idNivel, long idCurso, long idMateria) { return null; }

        public void guardarNota(NotaAbstracta nota,String tipoDeNota) { }

        public void modificarNota(NotaAbstracta nota, String tipoDeNota) { }

        //planillas de evaluacion

        public List<PlanillaDeEvaluacion> listarPlanillas(String filtro, String valor, String orden) { return null; }
        public void CerrarTrimestre(long numeroTrimestre, PlanillaDeEvaluacion planilla) { }

        public Boolean verificarCantidadDeNotas(long numeroTrimestre, PlanillaDeEvaluacion planilla) { return true; }
        public void calcularNotaFinal(PlanillaDeEvaluacion planilla) { }

        public List<PlanillaDeEvaluacion> listarMateriasDesaprobadasPorAlumno(Alumno alumno) { return null; }

        public void actualizarEstadoPlanilla(PlanillaDeEvaluacion planilla) { }
    }
}
