using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using TrabajoDeCampo.BO;
using TrabajoDeCampo.DAO;
using System.IO;
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

        public void realizarBackup(int partes, String directorio) {
            String date = DateTime.Now.Date.ToShortDateString();
            date = date.Replace("/", "-");
            String nombreDelBack = "TRABAJO-DIPLOMA-" + date + "-";

            foreach (String item in Directory.GetFiles(directorio, nombreDelBack+"*"))
            {
                File.Delete(item);
            }

            partes = partes - 1;
            this.daoSeguridad.realizarBackup(partes, directorio);
            //consulto filesize
            FileInfo informacionDelArchivo = new FileInfo(directorio + "\\tempBackup.bak");

            long size = informacionDelArchivo.Length;
            //calculo el max size por parte
            Double chunk = size / partes;

            byte[] buffer = new byte[(int)Math.Round(chunk)];
            

            string archivoTemporal = directorio + "\\tempBackup.bak";
            Stream tempBak = File.OpenRead(archivoTemporal);
            Stream destino;
            int index = 1;
            while (tempBak.Position < tempBak.Length)
            {
                String nombreNuevo = nombreDelBack + index;
                index++;
                File.Create(directorio + "\\" + nombreNuevo + ".bak").Close();
                destino = File.OpenWrite(directorio + "\\" + nombreNuevo +".bak");
                    

                while( destino.Position < chunk)
                {
                    int leerHasta= (int)Math.Min(chunk, buffer.Length);
                    int cantidadLeida =  tempBak.Read(buffer, 0, leerHasta);
                    destino.Write(buffer, 0, cantidadLeida);

                    if (cantidadLeida < Math.Min(chunk, buffer.Length))
                        break;
                }

                destino.Close();


            }

            tempBak.Close();
            File.Delete(archivoTemporal);


        }

        public void realizarRestore(String directorio) {
            String[] partesPath = directorio.Split('-');
            String nombreBasico = "TRABAJO-DIPLOMA" + "-" + partesPath[2] + "-" + partesPath[3] + "-" + partesPath[4];
            String regex = (nombreBasico +"-??"+".bak");



            String directorioActual = partesPath[0].Replace("TRABAJO", "");
            List<String> todosLosArchivos = Directory.GetFiles(directorioActual, regex).ToList();
            todosLosArchivos.Sort((String a, String b) =>
            { return long.Parse(a.Split('-')[5].Replace(".bak","")).CompareTo(long.Parse(b.Split('-')[5].Replace(".bak", ""))); });

            
            String directorioTemporal = directorioActual + "\\tempRestoreFile.bak";

            Stream stream = File.Create(directorioTemporal);

            foreach (String archivo in todosLosArchivos)
            {
                Stream parte = File.OpenRead(archivo);
                parte.CopyTo(stream);
                parte.Close();
            }

            stream.Close();
            daoSeguridad.realizarRestore(directorioTemporal);

            File.Delete(directorioTemporal);

        }

        //IDIOMA
        public Dictionary<String,String> traerTraducciones(List<String> codigosMensajes, String codigoIdioma) { return null; }


        public void enviarMail(String password, Usuario usuario) { }

        public void actualizaConexión()
        {
            throw new System.NotImplementedException();
        }
    }
}
