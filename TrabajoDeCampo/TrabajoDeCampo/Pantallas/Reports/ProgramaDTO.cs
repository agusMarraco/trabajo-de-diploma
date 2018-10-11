using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoDeCampo.Pantallas.Reports
{
    public class ProgramaDTO
    {

        private String _nombreDelReporte;

        public String nombreDelReporte
        {
            get { return _nombreDelReporte; }
            set { _nombreDelReporte = value; }
        }

        private String _fecha;

        public String fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        private String _nombreDelColegio;

        public String nombreDelColegio
        {
            get { return _nombreDelColegio; }
            set { _nombreDelColegio = value; }
        }

        private List<Nivel> nivels;

        public List<Nivel> niveles
        {
            get { return nivels; }
            set { nivels = value; }
        }


        public ProgramaDTO(String nombreDelReporte,String Fecha, String NombreDelColegio
            ,List<Nivel> niveles)
        {
            this.nivels = niveles;
            this.nombreDelColegio = NombreDelColegio;
            this.fecha = Fecha;
            this.nombreDelReporte = nombreDelReporte;

        }
    }
}
