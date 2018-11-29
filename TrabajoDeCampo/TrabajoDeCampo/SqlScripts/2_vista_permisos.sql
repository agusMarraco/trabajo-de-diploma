----VISTA DE PERMISOS
GO

CREATE  VIEW PERMISOS_USUARIO
AS 
SELECT DISTINCT USU_ID,PAT.PAT_ID FROM USUARIO USU

LEFT JOIN USUARIO_PATENTE UP ON (
UP.UP_USUARIO_ID = USU.USU_ID AND UP.UP_BLOQUEADA = 0
)
LEFT JOIN USUARIO_FAMILIA UF ON 
UF.UF_USUARIO_ID = USU.USU_ID

LEFT JOIN FAMILIA FAM ON
FAM.FAM_ID = UF.UF_FAMILIA_ID

LEFT JOIN FAMILIA_PATENTE FP ON (
FP_FAMILIA_ID = FAM.FAM_ID AND 0 = (SELECT COUNT(*) FROM USUARIO_PATENTE UP2 WHERE UP2.UP_BLOQUEADA =1 AND UP2.UP_PATENTE_ID = FP.FP_PATENTE_ID AND UP2.UP_USUARIO_ID = USU.USU_ID)
)
INNER JOIN PATENTE PAT ON 
(
(PAT.PAT_ID = FP_PATENTE_ID) OR(
PAT.PAT_ID = UP.UP_PATENTE_ID)
)
WHERE 
USU.USU_BAJA <> 1 AND USU.USU_INTENTOS <> 3
