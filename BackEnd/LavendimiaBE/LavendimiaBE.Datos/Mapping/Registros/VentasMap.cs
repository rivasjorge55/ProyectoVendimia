using LavendimiaBE.Entidades.Registros;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LavendimiaBE.Datos.Mapping.Registros
{
    public class VentasMap : IEntityTypeConfiguration<Venta> 
    {
        public void Configure(EntityTypeBuilder<Venta> builder)
        {
            builder.ToTable("ventas").HasKey(v => v.idVenta);
        }
    }
}
