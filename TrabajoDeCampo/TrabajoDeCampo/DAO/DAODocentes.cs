using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoDeCampo.BO;
namespace TrabajoDeCampo.DAO
{
    public class DAODocentes
    {

        
        public void guardarDocente(Docente docente) { }
        public void modificarDocente(Docente docente) { }
        public void borrarDocente(Docente docente) { }

        public Boolean estaDesasignado(long idDocente) { return true; }//chequea que no tenga horarios a cargo antes de borrarlo

        public Boolean verificarDNI(String dni) { return true; }
        public void listarDocentes(String filtro, String valor, String orden) { }

        public List<InasistenciaDocente>listarInasistenciasPorDocente(Docente docente) { return null; }

        public void guardarInasistencia(InasistenciaDocente inasistencia) { }

        public void modificarInasistencia(InasistenciaDocente inasistencia) { }

    }
}
