create procedure SalvaTipoUsuario
(@Tipo varchar(50))
as 
begin
insert into [dbo].[TipoUsuario] values(@Tipo)
end