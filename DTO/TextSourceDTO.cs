using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DTO
{
    /// <summary>
    /// Clase que representa el DTO de un texto
    /// </summary>
    public class TextSourceDTO : BannerSourceDTO
    {
        public string Data { get; set; }
    }
}
