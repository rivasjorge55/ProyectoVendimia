using LavendimiaBE.Entidades.Catalogos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LavendimiaBE.Datos.Mapping.Catalogos
{
    public class ArticuloMap : IEntityTypeConfiguration<Articulo> 
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Articulo> builder)
        {
            builder.ToTable("articulo").HasKey(c => c.idArticulo);
        }
    }
}
