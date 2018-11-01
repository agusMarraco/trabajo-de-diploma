-- vista bitacora

go

  create  view vistaBitacora as 
  select bi.BIT_FECHA,usu.USU_ALIAS,bi.BIT_CRITICIDAD_ID, bi.BIT_MENSAJE from BITACORA bi
  left join usuario usu on usu.USU_ID = bi.BIT_USUARIO
  inner join CRITICIDAD crit on crit.CRIT_ID = bi.BIT_CRITICIDAD_ID