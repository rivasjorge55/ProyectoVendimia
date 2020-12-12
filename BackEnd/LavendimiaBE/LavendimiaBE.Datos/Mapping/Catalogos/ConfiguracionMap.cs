using System;
using System.Collections.Generic;
using System.Text;
using LavendimiaBE.Entidades.Catalogos;
using Microsoft.EntityFrameworkCore;

namespace LavendimiaBE.Datos.Mapping.Catalogos
{
    public class ConfiguracionMap : IEntityTypeConfiguration<Configuracion> 
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Configuracion> builder)
        {
            builder.ToTable("configuracion").HasKey(c => c.idConfiguracion);
        }
    }
}
