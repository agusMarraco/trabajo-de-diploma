using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoDeCampo.SEGURIDAD
{
    public class EnumPatentes
    {
        //esenciales 
        public static readonly long CrearUsuario = 11;
        public static readonly long ModificarUsuario = 12;
        public static readonly long ListarUsuarios = 14;
        public static readonly long GenerarBackups = 16;
        public static readonly long RestaurarBackup = 17;
        public static readonly long RecalcularDígitosVerificadores = 18;
        public static long VerBitácora = 19;
        public static readonly long ModificarFamilias = 21;
        public static readonly long ListarFamilias = 22;
        public static readonly long CrearFamilia = 23;

        //no esenciales
        public static long CrearAlumno = 1;
        public static long ModificarAlumno = 2;
        public static long ListadoAlumnos = 3;
        public static long BorrarAlumno = 4;
        public static long RegistrarInasistencia = 5;
        public static long RegistrarAmonestación = 6;
        public static long CrearTutor = 7;
        public static long ModificarTutor = 8;
        public static long BorrarTutor = 9;
        public static long ListarTutores = 10;
        public static long BorrarUsuario = 13;
        public static long RegenerarContraseña = 15;
        public static long BloquearUsuario = 20;
        public static long BorrarFamilia = 24;
        public static long CrearHorario = 25;
        public static long ListarHorarios = 26;
        public static long ModificarHorario = 27;
        public static long BorrarHorario = 28;
        public static long CrearCurso = 29;
        public static long BorrarCurso = 30;
        public static long ModificarCurso = 31;
        public static long ListarCursos = 32;
        public static long PromocionarAlumnos = 33;
        public static long CrearMateria = 34;
        public static long ModificarMateria = 35;
        public static long ListarMateria = 36;
        public static long BorrarMateria = 37;
        public static long AsignarMateriaNivel = 38;
        public static long GenerarReportes = 39;
    }
}
