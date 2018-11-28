using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using TrabajoDeCampo.BO;
using TrabajoDeCampo.DAO;
using System.IO;
using System.Windows.Forms;
using TrabajoDeCampo.SEGURIDAD;
using System.Data;

namespace TrabajoDeCampo.SERVICIO
{
    public class ServicioSeguridad
    {


        public ServicioSeguridad()
        {
            this.daoSeguridad = new DAOSeguridad();
        }
        private DAOSeguridad _daoSeguridad;
        
        public DAOSeguridad daoSeguridad
        {
            get { return _daoSeguridad; }
            set { _daoSeguridad = value; }
        }

        //LOGIN
        public void loguear(String user, String pass, String codigoIdioma) {
            
            this.daoSeguridad.loguear(user, pass, null);


        } // ESTE CHEQUEA QUE TENGA X PANTENTE SI EL SISTEMA ESTA BLOQUEADO.


        public Boolean probarConexion() {
            Boolean conectado = true;
            try
            {
                conectado = this.daoSeguridad.probarConexion();
                

            }
            catch (Exception EX)
            {
         
                
                conectado = false;

            }
            return conectado;
        }


        //FAMILIA PATENTE
        public List<ComponentePermiso> listarFamiliasYPatentes() {
            return this.daoSeguridad.listarFamiliasYPatentes();
        }

        public void actualizarFamiliaPatente(List<Patente> pantentes, Familia familia) { } //deprecado

        public Boolean tienePatente(long idUsuario, String codigoPantente) {
            return this.daoSeguridad.tienePatente(idUsuario, codigoPantente);
        }

        public void actualizarPermisosUsuario(Usuario usuario, List<ComponentePermiso> familiasYPatentes) {
            this.daoSeguridad.actualizarPermisosUsuario(usuario, familiasYPatentes);
        }

        public void crearFamilia(Familia familia) {
            this.daoSeguridad.crearFamilia(familia);
        }

        public void modificarFamilia(Familia familia) {
            this.daoSeguridad.modificarFamilia(familia);
        }

        public void borrarFamilia(long IdFamilia) {

            bool estaDesasignada = this.daoSeguridad.chequearFamiliaDesasignada(IdFamilia);
            if (estaDesasignada)
            {
                this.daoSeguridad.borrarFamilia(IdFamilia);
            }
            else
            {
                throw new Exception("ASIGNADA");
            }
            
        }

        public Familia buscarFamilia(long idFamilia) {
           return  this.daoSeguridad.buscarFamilia(idFamilia);
        }
        public List<Familia> listarFamilias() {
            return this.daoSeguridad.listarFamilias();
        }
        public List<Patente> listarPatentes() {
            return this.daoSeguridad.listarPatentes();
        }
        //USUARIOS

        public void cambiarContraseña(long idUsuario, String contraseñaNueva,String contraseñaVieja) {
            Usuario usu = daoSeguridad.buscarUsuario(idUsuario);
            String contraseñaEncriptada = SeguridadUtiles.encriptarMD5(contraseñaNueva);
            if (!SeguridadUtiles.encriptarMD5(contraseñaVieja).Equals(usu.pass))
            {
                throw new Exception("PASS");
            }
            this.daoSeguridad.cambiarContraseña(idUsuario, contraseñaEncriptada);
            enviarMail(contraseñaNueva, usu,false); 
        }

        public void cambiarIdioma(long idUsuario, String codigoIdioma) {
            this.daoSeguridad.cambiarIdioma(idUsuario, codigoIdioma);
        }

        public void regenerarContraseña(Usuario usuario){

            String nuevaContraseña = SeguridadUtiles.generarPassword();
            String passEncriptado = SeguridadUtiles.encriptarMD5(nuevaContraseña);
            this.daoSeguridad.regenerarContraseña(usuario.id, passEncriptado);
            enviarMail(nuevaContraseña, usuario,false);
            
        }

        public void bloquearUsuario(Usuario usu) {
            Usuario usuario = new Usuario();
            usuario.id = usu.id;
            usuario.componentePermisos = new List<ComponentePermiso>();
            Boolean errorEsenciales = this.daoSeguridad.verificarPermisosEsenciales(usuario);
            if (!errorEsenciales)
            {
                this.daoSeguridad.bloquearUsuario(usu.id);
            }
            else
            {
                throw new Exception("PERMISOS");
            }


        }
        public void desbloquearUsuario(Usuario usuario) {
            this.daoSeguridad.desbloquearUsuario(usuario.id);
        }

        public void crearUsuario(Usuario usuario)
        {
            bool hayRepetidos = this.daoSeguridad.chequearCamposUnicos(usuario);
            String contraseña = SeguridadUtiles.generarPassword();
            String contraseñaEncriptada = SeguridadUtiles.encriptarMD5(contraseña);
            usuario.pass = contraseñaEncriptada;

            if (!hayRepetidos)
            {
                this.daoSeguridad.crearUsuario(ref usuario);
                this.enviarMail(contraseña, usuario,false);

            }
            else
            {
                throw new Exception("REPETIDOS");
            }

            
        }

        public Usuario buscarUsuario(long idUsuario) {
            return this.daoSeguridad.buscarUsuario(idUsuario);
        }
        public void modificarUsuario(Usuario usuario) {
            Boolean errorEsenciales = this.daoSeguridad.verificarPermisosEsenciales(usuario);
            bool hayRepetidos = this.daoSeguridad.chequearCamposUnicos(usuario);
            if (!errorEsenciales)
            {
                if (!hayRepetidos)
                {
                    this.daoSeguridad.modificarUsuario(usuario);
                }
                else
                {
                    throw new Exception("REPETIDOS");
                }

            }
            else
            {
                throw new Exception("PERMISOS");
            }
            
                
        }

        public void borrarUsuario(Usuario usuario) {
            Usuario usu = new Usuario();
            usu.id = usuario.id;
            usu.componentePermisos = new List<ComponentePermiso>();
            Boolean errorEsenciales = this.daoSeguridad.verificarPermisosEsenciales(usu);
            if (!errorEsenciales)
            {
                this.daoSeguridad.borrarUsuario(usuario);
            }
            else
            {
                throw new Exception("PERMISOS");
            }
            
        }

        public List<Usuario> listarUsuarios(String filtro, String valor, String orden)
        {
            return this.daoSeguridad.listarUsuarios(filtro,valor,orden);
        }


        //BITACORA
        //criticidad 1 baja; 2 media; 3 alta;
        public void grabarBitacora(Usuario usuario, String mensaje, int criticidad) {
            this.daoSeguridad.grabarBitacora(usuario, mensaje, criticidad);
        }

        public DataSet listarBitacora(String filtro, String valor, String orden) {
            return this.daoSeguridad.listarBitacora(filtro, valor, orden);

        }

        //DIGITOS VERIFICADORES

        public void verificarDigitosVerificadores() {

            this.daoSeguridad.verificarDigitosVerificadores();
        }


        public void recalcularDigitosVerificadores() {
            try
            {
                this.daoSeguridad.recalcularDigitosVerificadores();
                foreach (KeyValuePair<string, KeyValuePair<string, string[]>> item in TablasDvhEnum.mapeoTablaCampo)
                {
                    this.daoSeguridad.recalcularDigitoVertical(item.Key);
                }
                string idiomaId = TrabajoDeCampo.Properties.Settings.Default.Idioma;
                List<String> tag = new List<string>();
                String code = "com.td.completado";
                tag.Add(code);
                Dictionary<String,String> translation =  this.daoSeguridad.traerTraducciones(tag,idiomaId);
                MessageBox.Show(translation[code]);
            }
            catch (Exception ex)
            {
                string idiomaId = TrabajoDeCampo.Properties.Settings.Default.Idioma;
                List<String> tag = new List<string>();
                
                Dictionary<String, String> translation = this.daoSeguridad.traerTraducciones(tag, idiomaId);
                MessageBox.Show(ex.Message);
            }
        }

        //BACKUPS

        public void realizarBackup(int partes, String directorio) {
            String date = DateTime.Now.Date.ToShortDateString();
            date = date.Replace("/", "_");
            String nombreDelBack = "TRABAJO_DIPLOMA_" + date + "_";
            directorio += "//" + nombreDelBack; 
            this.daoSeguridad.realizarBackup(partes, directorio);

        }

        public void realizarRestore(String directorio) {
            String[] partesPath = directorio.Split('_');
            String nombreBasico = "TRABAJO_DIPLOMA" + "_" + partesPath[2] + "_" + partesPath[3] + "_" + partesPath[4];
            String regex = (nombreBasico +"_??"+".bak");



            String directorioActual = partesPath[0].Replace("TRABAJO", "");
            List<String> todosLosArchivos = Directory.GetFiles(directorioActual, regex).ToList();
            todosLosArchivos.Sort((String a, String b) =>
            { return long.Parse(a.Split('_')[5].Replace(".bak","")).CompareTo(long.Parse(b.Split('_')[5].Replace(".bak", ""))); });

            StringBuilder bs = new StringBuilder();

            for (int i = 0; i < todosLosArchivos.Count; i++)
            {
                if (i == 0)
                {
                    bs.Append(" FROM DISK = '" + todosLosArchivos[i] + "' ");
                }
                else
                {
                    bs.Append(" ,DISK = '" + todosLosArchivos[i] + "' ");
                }

            }

            daoSeguridad.realizarRestore(bs.ToString());
        }

        //IDIOMA
        public Dictionary<String, String> traerTraducciones(List<String> codigosMensajes, String codigoIdioma)
        {
            return daoSeguridad.traerTraducciones(codigosMensajes, codigoIdioma);
        }
        

        public void enviarMail(String password, Usuario usuario,Boolean mailRecuperacion) {
            List<String> codigos = new List<string> {"com.td.email.header", "com.td.email.body", "com.td.email.header.recover", "com.td.email.body.recover" };

            String disco = Path.GetPathRoot(Environment.CurrentDirectory);
            String currentUser = Environment.UserName;

            String desktopPath = disco + "Users\\" + currentUser + "\\Desktop\\pass.txt";
            usuario.idioma.codigo = "es";
            Dictionary<String, String> traducciones = this.daoSeguridad.traerTraducciones(codigos, usuario.idioma.codigo);
            StringBuilder builder = new StringBuilder();
            if (!mailRecuperacion)
            {
                builder.Append(usuario.apellido + " " + usuario.nombre);
                builder.Append(Environment.NewLine);
                builder.Append(traducciones[codigos[0]]);
                builder.Append(Environment.NewLine);
                builder.Append(Environment.NewLine);
                builder.Append(Environment.NewLine);
                builder.Append("Alias: ");
                builder.Append(usuario.alias);
                builder.Append(Environment.NewLine);
                builder.Append(traducciones[codigos[1]] + " ");
                builder.Append(password);
            }
            else
            {
                builder.Append(usuario.alias);
                builder.Append(Environment.NewLine);
                builder.Append(traducciones[codigos[2]]);
                builder.Append(Environment.NewLine);
                builder.Append(traducciones[codigos[3]] + " ");
                builder.Append(password);
            }

            try
            {
                FileStream mail = File.Create(desktopPath);
                mail.Write(Encoding.UTF8.GetBytes(builder.ToString()),0, Encoding.UTF8.GetBytes(builder.ToString()).Length);
                mail.Close();
            }
            catch (Exception ex )
            {

                throw ex;
            }
            


        }

        public void actualizaConexión()
        {
            throw new System.NotImplementedException();
        }
    }
}
