using LavendimiaBE.Entidades.Catalogos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LavendimiaBE.Datos.Mapping.Catalogos
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente> 
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("cliente").HasKey(c => c.idCliente);
        }
    }
}
