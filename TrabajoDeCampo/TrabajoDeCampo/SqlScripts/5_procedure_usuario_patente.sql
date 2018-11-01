create  procedure generarDVHUsuarioPatente as 

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
