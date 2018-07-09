using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoDeCampo.BO;
using TrabajoDeCampo.DAO;

namespace TrabajoDeCampo.SERVICIO
{
    public class ServicioSeguridad
    {

        private DAOSeguridad _daoSeguridad;

        public DAOSeguridad daoSeguridad
        {
            get { return _daoSeguridad; }
            set { _daoSeguridad = value; }
        }

        //LOGIN
        public void loguear(String user, String pass, String codigoIdioma) { } // ESTE CHEQUEA QUE TENGA X PANTENTE SI EL SISTEMA ESTA BLOQUEADO.

        public Boolean probarConexion() { return true; }


        //FAMILIA PATENTE
        public List<ComponentePermiso> listarFamiliasYPatentes() { return null; }

        public void actualizarFamiliaPatente(List<Patente> pantentes) { }

        public Boolean tienePatente(long idUsuario, String codigoPantente) { return true; }

        public void actualizarPermisosUsuario(Usuario usuario, List<ComponentePermiso> familiasYPatentes) { }

        public void crearFamilia(Familia familia) { }

        public void modificarFamilia(Familia familia) { }

        public void borrarFamilia(long IdFamilia) { }

        public Familia buscarFamilia(long idFamilia) { return null; }
        public List<Familia> listarFamilias() { return null; }
        public List<Patente> listarPatentes() { return null; }
        //USUARIOS

        public void cambiarContraseña(long idUsuario, String contraseñaNueva) { }

        public void cambiarIdioma(long idUsuario, String codigoIdioma) { }

        public void regenerarContraseña(long idUsuario){ }

        public void bloquearUsuario(long idUsuario) { }
        public void desbloquearUsuario(Usuario usuario) { }

        public void crearUsuario(Usuario usuario) { }

        public Usuario buscarUsuario(long idUsuario) { return null; }
        public void modificarUsuario(Usuario usuario) { }

        public void borrarUsuario(Usuario usuario) { }

        public List<Usuario> listarUsuarios(String filtro, String valor, String orden)
        {
            return null;
        }


        //BITACORA
        //criticidad 1 baja; 2 media; 3 alta;
        public void grabarBitacora(Usuario usuario, String mensaje, int criticidad) { }

        public void listarBitacora(String filtro, String valor, String orden) { }

        //DIGITOS VERIFICADORES

        public void verificarDigitosVerificadores() { }


        public void recalcularDigitosVerificadores() { }

        //BACKUPS

        public void realizarBackup(int partes, String directorio) { }

        public void realizarRestore(String directorio) { }

        //IDIOMA
        public Dictionary<String,String> traerTraducciones(List<String> codigosMensajes, String codigoIdioma) { return null; }


        public void enviarMail(String password, Usuario usuario) { }

        public void actualizaConexión()
        {
            throw new System.NotImplementedException();
        }
    }
}
