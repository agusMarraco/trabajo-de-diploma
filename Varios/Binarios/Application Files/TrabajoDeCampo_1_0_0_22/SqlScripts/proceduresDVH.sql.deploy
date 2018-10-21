


-- procedures para regenerar los dvh de las tablas sin id autonumerico  crearlos de a uno

create or alter procedure generarDVHFamiliaPatente as 

select * into #temporal from FAMILIA_PATENTE 

declare @currentFamilia int
declare @currentPatente int
while exists(select * from #temporal)
begin
	
	SELECT TOP 1 @currentFamilia = fp_familia_id, @currentPatente = fp_patente_id from "#temporal"
	update FAMILIA_PATENTE set FP_DVH = (SELECT CONVERT(NVARCHAR(32),HashBytes('MD5', (cast(@currentFamilia as varchar)) + (cast(@currentPatente as varchar))),2)) 
	where FP_FAMILIA_ID = @currentFamilia and FP_PATENTE_ID = @currentPatente
	delete #temporal where FP_FAMILIA_ID = @currentFamilia and FP_PATENTE_ID = @currentPatente
end 
drop table #temporal
go 

--------------------------------------------------------------
create or alter procedure generarDVHUsuarioPatente as 

select * into #temporal from USUARIO_PATENTE 

declare @currentUsuario int
declare @currentPatente int
declare @bloqueado int
while exists(select * from #temporal)
begin
	
	SELECT TOP 1 @currentUsuario = UP_USUARIO_ID, @currentPatente = UP_PATENTE_ID, @bloqueado = UP_BLOQUEADA from "#temporal"
	update USUARIO_PATENTE set UP_DVH = (SELECT CONVERT(NVARCHAR(32),HashBytes('MD5', 
	(cast(@currentUsuario as varchar)) + (cast(@currentPatente as varchar)) + (cast(@bloqueado as varchar))
	),2)) 
	where UP_USUARIO_ID = @currentUsuario and UP_PATENTE_ID = @currentPatente
	delete #temporal where UP_USUARIO_ID = @currentUsuario and UP_PATENTE_ID = @currentPatente
end 
drop table #temporal
go


-------------------------------------------------------------
create or alter procedure generarDVHUsuarioFamilia as 

select * into #temporal from USUARIO_FAMILIA 

declare @currentFamilia int
declare @currentUsuario int
while exists(select * from #temporal)
begin
	
	SELECT TOP 1 @currentFamilia = UF_FAMILIA_ID, @currentUsuario = UF_USUARIO_ID from "#temporal"
	update USUARIO_FAMILIA set UF_DVH = (SELECT CONVERT(NVARCHAR(32),HashBytes('MD5', 
	(cast(@currentFamilia as varchar)) + (cast(@currentUsuario as varchar))
	),2)) 
	where UF_FAMILIA_ID = @currentFamilia and UF_USUARIO_ID = @currentUsuario
	delete #temporal where UF_FAMILIA_ID = @currentFamilia and UF_USUARIO_ID = @currentUsuario
end 
drop table #temporal
----------------------------------------------------------------------

go 
create or alter procedure generarDVHAmonestacion as 
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

go
--------------------------------------------------------------------------------------------------
create or alter procedure generarDVHInasistenciaAlumno as 
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

go