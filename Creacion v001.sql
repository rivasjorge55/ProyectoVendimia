if not exists  (select * from sysobjects where name ='configuracion' and xtype ='U')
begin
	create table configuracion -- drop table configuracion
	(
		idConfiguracion int not null primary key identity(1,1),
		tazaFinanciamiento decimal(5,2) not null,
		porcentajeEnganche decimal(5,2) not null,
		plazoMaximo int not null
	)
	end
go
insert into configuracion select 15.6,25,60
go


if not exists  (select * from sysobjects where name ='articulo' and xtype ='U')
begin -- drop table articulo
	create table articulo
	(
		idArticulo int not null primary key identity(1,1),
		descripcion varchar(200) not null,
		modelo varchar(50) not null,
		precio decimal (12,2) not null,
		existencia int not null,
		activo bit not null
	)
	end
go


if not exists  (select * from sysobjects where name ='cliente' and xtype ='U')
begin -- drop table cliente
	create table cliente
	(
		idCliente int not null primary key identity(1,1),
		nombre varchar(50) not null, 
		primerapellido varchar(50) not null, 
		segundoapellido varchar(50) not null, 
		rfc varchar(13) not null, 
	)
	end
go


if not exists  (select * from sysobjects where name ='ventas' and xtype ='U')
begin -- drop table ventas
	create table ventas
	(
		idVenta int not null primary key identity(1,1),
		idCliente int not null,
		total decimal(15,2) not null,
		fechaVenta date not null
	)
	end
go
-- insert into ventas select 1,4160.84,'04/08/2020'


if exists (select * from sysobjects where name ='store_OpcionesVenta')
begin
	drop procedure store_OpcionesVenta
end
go


create procedure store_OpcionesVenta
	@monto int	
as 
begin
	declare @plazoMaximo int
	declare @tazaFinanciamiento decimal(5,2)
	declare @porcentajeEnganche decimal(5,2)

	declare @precioTotal decimal(15,2)
	declare @enganche decimal(15,2)
	declare @bonificacionEnganche decimal(15,2)
	declare @totalAdeudo decimal(15,2)
	declare @precioContado decimal(15,2)

	select  @plazoMaximo  = plazoMaximo, 
			@tazaFinanciamiento = tazaFinanciamiento,  
			@porcentajeEnganche =porcentajeEnganche  from configuracion

	

	declare @opcionesCompra as table
	(
		plazo int,
		totalPagar decimal(15,2),
		importeAbono decimal(15,2),
		ahorro decimal(15,2)
	)

	insert into @opcionesCompra (plazo) values (@plazoMaximo/4)
	insert into @opcionesCompra (plazo) values (@plazoMaximo/2)
	insert into @opcionesCompra (plazo) values (@plazoMaximo/4*3)
	insert into @opcionesCompra (plazo) values (@plazoMaximo)

	select @precioTotal = @monto * (1+( @tazaFinanciamiento * @plazoMaximo )/100)
	--select '@precioTotal ' , @precioTotal 
	
	select @enganche = @precioTotal* @porcentajeEnganche
	--select '@enganche ' ,@enganche

	select @bonificacionEnganche = @enganche *((@tazaFinanciamiento * @plazoMaximo)/100)/@plazoMaximo/12
	--select '@bonificacionEnganche ' ,@bonificacionEnganche

	select @totalAdeudo = @precioTotal -@enganche -@bonificacionEnganche
	--select '@totalAdeudo ' ,@totalAdeudo

	select @precioContado = @totalAdeudo /(1+ ((@tazaFinanciamiento *  @plazoMaximo))/100)
	--select '@precioContado ' ,@precioContado

	update @opcionesCompra  set totalPagar = @precioContado * (1+ (@tazaFinanciamiento * plazo)/100)
	update @opcionesCompra  set importeAbono = totalPagar  / plazo , 
								 ahorro = @totalAdeudo-totalPagar
	--select * from @opcionesCompra		
	
	select	cast(plazo as char(2))+ ' ABONOS DE' as [DESplazo], 
			'$ '+CONVERT(varchar, CAST(importeAbono AS money), 1) as[DESimporteAbono],			
			'TOTAL A PAGAR $ ' +CONVERT(varchar, CAST(totalPagar AS money), 1) as [DEStotalPagar], 
			'SE AHORRA $ '+CONVERT(varchar, CAST(ahorro AS money), 1) as [DESahorro] ,
			plazo ,
			totalPagar ,
			importeAbono ,
			ahorro 
	from @opcionesCompra		

end
go
-- exec store_OpcionesVenta 4250

if exists (select * from sysobjects where name ='vw_ventas' )
begin 
	drop view vw_ventas
end
go

create view vw_ventas as
	select v.idVenta, c.idCliente, c.nombre+' '+c.primerapellido+ ' '+ c.segundoapellido as nombreCompleto, v.total,  convert(VARCHAR(10), fechaVenta, 103) fechaVenta
	from ventas v inner join cliente c on (v.idCliente = c.idCliente)	

go
-- select * from vw_ventas

----en dbcontext
--modelBuilder.Query<vw_trabajadores>().ToView("vw_trabajadores").Property(v => v.id).HasColumnName("id");



----en entidades


--	 // GET: api/Trabajadores/vwtrabajadores
--        [HttpGet("[action]")]
--        public async Task<IEnumerable<vw_trabajadores>> vwtrabajadores()
--        {
--            var trabajador = await _context.vw_Trabajadores.ToListAsync();
--            return trabajador;
--        }