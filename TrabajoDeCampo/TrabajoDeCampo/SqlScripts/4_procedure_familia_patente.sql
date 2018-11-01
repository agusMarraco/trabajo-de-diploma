
create  procedure generarDVHFamiliaPatente as 

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