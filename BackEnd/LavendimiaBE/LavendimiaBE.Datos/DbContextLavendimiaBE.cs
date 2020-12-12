using LavendimiaBE.Datos.Mapping.Catalogos;
using LavendimiaBE.Datos.Mapping.Registros;
using LavendimiaBE.Entidades.Catalogos;
using LavendimiaBE.Entidades.Registros;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace LavendimiaBE.Datos
{
    public class DbContextLavendimiaBE :DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Configuracion> configuracion { get; set; }

        public DbSet<Venta> Ventas { get; set; } 
        public DbQuery<vw_ventas> vwVentas { get; set; }

        public DbContextLavendimiaBE    (DbContextOptions<DbContextLavendimiaBE> options) :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new ArticuloMap());
            modelBuilder.ApplyConfiguration(new ConfiguracionMap());
            modelBuilder.ApplyConfiguration(new VentasMap());

            modelBuilder.Query<opcionesCompra>();
            modelBuilder.Query<vw_ventas>().ToView("vw_ventas").Property(v => v.idVenta).HasColumnName("idVenta");
        }


    }
}

