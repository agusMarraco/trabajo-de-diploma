using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoDeCampo.SEGURIDAD
{
    public static class TablasDvhEnum
    {
        public static Dictionary<String, KeyValuePair<String,String[]>> mapeoTablaCampo = new Dictionary<String, KeyValuePair<String, String[]>>();

        static TablasDvhEnum(){

            mapeoTablaCampo.Add("USUARIO", new KeyValuePair<String, String[]>("USU_DVH",new String[] { "USU_ID","USU_ALIAS", "USU_PASS", "USU_INTENTOS","USU_DNI" }));
            mapeoTablaCampo.Add("PATENTE", new KeyValuePair<String, String[]>("PAT_DVH", new String[] { "PAT_ID","PAT_DESC", "PAT_ID"}));
            mapeoTablaCampo.Add("BITACORA", new KeyValuePair<String, String[]>("BIT_DVH", new String[] { "BIT_ID","BIT_FECHA", "BIT_MENSAJE", "BIT_CRITICIDAD_ID" }));
            mapeoTablaCampo.Add("PLANILLA_DE_EVALUACION", new KeyValuePair<String, String[]>("PDE_DVH", new String[] { "PDE_ID","PDE_TRIMESTRE_1", "PDE_TRIMESTRE_2", "PDE_TRIMESTRE_3", "PDE_ALUMNO_ID" }));
            mapeoTablaCampo.Add("FAMILIA", new KeyValuePair<String, String[]>("FAM_DVH", new String[] { "FAM_ID","FAM_NOMBRE", "FAM_BLOQUEADA" }));

            // DE ESTOS NO NECESITO EL ID PORQUE SON TABLAS RELACIONALES O ACCEDO POR PK NATURAL.
            mapeoTablaCampo.Add("INASISTENCIA_DE_ALUMNO", new KeyValuePair<String, String[]>("INA_DVH", new String[] { "INA_ALUMNO_ID","INA_FECHA", "INA_VALOR", "INA_JUSTIFICADA" }));
            mapeoTablaCampo.Add("AMONESTACION", new KeyValuePair<String, String[]>("AMON_DVH", new String[] { "AMON_ALUMNO_ID","AMON_FECHA", "AMON_MOTIVO"}));
            mapeoTablaCampo.Add("USUARIO_FAMILIA", new KeyValuePair<String, String[]>("UF_DVH", new String[] { "UF_USUARIO_ID", "UF_FAMILIA_ID" }));
            mapeoTablaCampo.Add("USUARIO_PATENTE", new KeyValuePair<String, String[]>("UP_DVH", new String[] { "UP_USUARIO_ID", "UP_PATENTE_ID", "UP_BLOQUEADA" }));
            mapeoTablaCampo.Add("FAMILIA_PATENTE", new KeyValuePair<String, String[]>("FP_DVH", new String[] { "FP_PATENTE_ID", "FP_FAMILIA_ID"}));




            mapeoTablaCampo.Add("DIGITO_VERTICAL", new KeyValuePair<String, String[]>("DV_DIGITO_CALCULADO", new String[] {}));
        }
    }
}
