using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoDeCampo.BO;
using TrabajoDeCampo.DAO;
using System.Data;
using System.Data.SqlClient;
namespace TrabajoDeCampo.SERVICIO
{
    public class ServicioDocentes
    {
        private DAODocentes _daoDocentes;

        public DAODocentes daoDocentes
        {
            get { return _daoDocentes; }
            set { _daoDocentes = value; }
        }

        public ServicioDocentes()
        {
            this.daoDocentes = new DAODocentes();
        }
        public void guardarDocente(Docente docente) { }
        public void modificarDocente(Docente docente) { }
        public void borrarDocente(Docente docente) { }

        public List<Docente> listarDocentes(String filtro, String valor, String orden) {
       
            return daoDocentes.listarDocentes(filtro,valor,orden);
        }

        public List<InasistenciaDocente> listarInasistenciasPorDocente(Docente docente) { return null; }

        public void guardarInasistencia(InasistenciaDocente inasistencia) { }

        public void modificarInasistencia(InasistenciaDocente inasistencia) { }
    }
}
