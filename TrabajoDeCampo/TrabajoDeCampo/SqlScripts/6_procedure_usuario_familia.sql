create  procedure generarDVHUsuarioFamilia as 

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
