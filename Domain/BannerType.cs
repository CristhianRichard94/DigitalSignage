using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.Domain
{
    public abstract class BannerType
    {
        /// <summary>
        /// Id del tipo
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Todo banner debe implementar este método para devolver su texto
        /// </summary>
        /// <returns>texto que será mostrado del banner en pantalla</returns>
        public abstract string GetText();
    }
}
