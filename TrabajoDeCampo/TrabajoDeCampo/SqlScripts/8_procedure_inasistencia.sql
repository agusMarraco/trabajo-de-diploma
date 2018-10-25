create  procedure generarDVHInasistenciaAlumno as 
select * into #temporal from INASISTENCIA_DE_ALUMNO 

declare @currentAlumno int
declare @currentFecha date
while exists(select * from #temporal)
begin
	
	SELECT TOP 1 @currentFecha = INA_FECHA, @currentAlumno = INA_ALUMNO_ID from "#temporal"
	update INASISTENCIA_DE_ALUMNO set INA_DVH = (SELECT CONVERT(NVARCHAR(32),HashBytes('MD5', 
	(cast(@currentAlumno as varchar)) + (cast(@currentFecha as varchar))
	),2)) 
	where INA_ALUMNO_ID = @currentAlumno and INA_FECHA = @currentFecha
	delete #temporal where INA_ALUMNO_ID = @currentAlumno and INA_FECHA = @currentFecha
end 
drop table #temporal
