﻿using DigitalSignage.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL.EntityFramework.Mappings
{
    /// <summary>
    /// Clase de mapeo de Textos de banners
    /// </summary>
    class TextSourceMap : EntityTypeConfiguration<TextSource>
    {
        public TextSourceMap()
        {
            // Tabla en la que se mapea la entidad
            this.ToTable("TextSources");

            // Propiedad requerida
            this.Property(pText => pText.Data)
                .IsRequired();

        }

    }
}
