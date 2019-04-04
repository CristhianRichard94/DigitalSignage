using DigitalSignage.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.BLL.RSSReader
{
    /// <summary>
    /// Interfaz para servicio de lectura de RSS.
    /// </summary>
    public interface IRSSReader
    {


        IEnumerable<RSSItemDTO> Read(Uri pUri);

        IEnumerable<RSSItemDTO> Read(String pUrl);

    }
}
