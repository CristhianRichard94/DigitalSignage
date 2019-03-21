using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.Domain
{

    /// <summary>
    /// Clase abstracta que representa una fuente de banner
    /// </summary>
    public abstract class BannerSource
    {
        /// <summary>
        /// Id de la fuente
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Todo banner debe implementar este método para devolver su texto
        /// </summary>
        /// <returns>Texto que será mostrado en pantalla</returns>
        public abstract string GetText();
    }
}
