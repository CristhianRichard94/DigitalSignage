﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.Domain
{
    /// <summary>
    /// Clase que representa una fuente de Texto Fijo
    /// </summary>
    public class Text : BannerSource
    {
        /// <summary>
        /// Texto a mostrar
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Obtiene el texto de la fuente
        /// </summary>
        /// <returns>Texto a mostrar</returns>
        public override string GetText()
        {
            return Data;
        }
    }
}
