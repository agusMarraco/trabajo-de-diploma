using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoDeCampo.DAO;
using System.IO;

namespace TrabajoDeCampo.SERVICIO
{
    public class ServicioReportes
    {
        private String[] _reportes;

        public String[] reportes
        {
            get { return _reportes; }
            set { _reportes = value; }
        }

        private DAOReportes _daoReportes;

        public DAOReportes daoReportes
        {
            get { return _daoReportes; }
            set { _daoReportes = value; }
        }


        public FileStream ejecutarReporte(String NombreReporte) { return null; }
    }
}
