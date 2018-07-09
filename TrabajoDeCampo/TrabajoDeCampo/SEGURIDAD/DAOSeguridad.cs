using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoDeCampo.BO;

namespace TrabajoDeCampo.DAO
{
    public class DAOSeguridad
    {
        private String[] _permisosClave;

        public String[] permisosClave
        {
            get { return _permisosClave; }
            set { _permisosClave = value; }
        }

        //LOGIN
        public void loguear(String user, String pass, String codigoIdioma) { }

        public Boolean chequearSistemaBloqueado() { return true; }

        public Boolean probarConexion() { return true; }

        //FAMILIA PATENTE
        public List<ComponentePermiso> listarFamiliasYPatentes() { return null; }

        public void actualizarFamiliaPantente(List<Patente> pantentes,Familia familia) { }

        public Boolean tienePatente(long idUsuario, String codigoPantente) { return true; }

        public void actualizarPermisosUsuario(Usuario usuario, List<ComponentePermiso> familiasYPatentes) { }

        public Boolean verificarPermisosEsenciales(long idUsuario) { return true; } //al cambiar permisos chequear que quede un usuario distinto a este con permisos clave.

        public Boolean chequearNegada(long idUsuario, String codigoPatente) { return true; }

        public void crearFamilia(Familia familia) { }

        public void modificarFamilia(Familia familia) { }

        public void borrarFamilia(long IdFamilia) { }

        public Familia buscarFamilia(long idFamilia) { return null; }

        public List<Familia> listarFamilias() { return null; }
        public List<Patente> listarPatentes() { return null; }
        public Boolean chequearFamiliaDesasignada() { return true; }
        //USUARIOS

        public void cambiarContraseña(long idUsuario, String contraseñaNueva) { }

        public void cambiarIdioma(long idUsuario, String codigoIdioma) { }

        public Boolean chequearCamposUnicos(Usuario usuario) { return false; } // para chequear que no se repitan dni mail alias 

        public String regenerarContraseña(long idUsuario) { return ""; }
        public Usuario buscarUsuario(long idUsuario) { return null; }
        public void bloquearUsuario(long idUsuario) { }
        public void desbloquearUsuario(long idUsuario) { }

        public void crearUsuario(Usuario usuario) { }

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

        public void recalcularDigitoHorizontal(String[] campos) { }

        public void recalcularDigitoVertical(String tabla) { }

        //BACKUPS

        public void realizarBackup(int partes, String directorio) { }

        public void realizarRestore(String directorio) { }
       
        
        //IDIOMA
        public Dictionary<String, String> traerTraducciones(List<String> codigosMensajes, String codigoIdioma) { return null; }

        public void actualizaConexión()
        {
            throw new System.NotImplementedException();
        }
    }
}
