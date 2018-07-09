using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoDeCampo.BO
{
    public class NotaDeMesa : NotaAbstracta
    {
        private Docente _adjunto;

        public Docente adjunto
        {
            get { return _adjunto; }
            set { _adjunto = value; }
        }

        private DateTime _fecha;

        public DateTime fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        private String _libro;

        public String libro
        {
            get { return _libro; }
            set { _libro = value; }
        }

        private String _folio;

        public String folio
        {
            get { return _folio; }
            set { _folio = value; }
        }




    }
}
