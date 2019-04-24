using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.Domain
{
    /// <summary>
    /// Interfaz a cumplir por las fuentes de banner
    /// </summary>
    public interface IBannerSource
    {
        /// <summary>
        /// Id de la fuente de banner
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Devuelve el texto a mostrar de la fuente del banner
        /// </summary>
        /// <returns></returns>
        IList<string> GetText();

    }
}
