go 
create  procedure generarDVHAmonestacion as 
select * into #temporal from AMONESTACION 

declare @currentAlumno int
declare @currentFecha date
while exists(select * from #temporal)
begin
	
	SELECT TOP 1 @currentAlumno = AMON_ALUMNO_ID, @currentFecha = AMON_FECHA from "#temporal"
	update AMONESTACION set AMON_DVH = (SELECT CONVERT(NVARCHAR(32),HashBytes('MD5', 
	(cast(@currentAlumno as varchar)) + (cast(@currentFecha as varchar))
	),2)) 
	where AMON_ALUMNO_ID = @currentAlumno and AMON_FECHA = @currentFecha
	delete #temporal where  AMON_ALUMNO_ID = @currentAlumno and AMON_FECHA = @currentFecha
end

drop table #temporal
