using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DigitalSignage.DTO;
using Serilog;

namespace DigitalSignage.BLL.RSSReader
{
    /// <summary>
    /// Clase que implementa lector de RSS procesando el XML de la fuente.
    /// </summary>
    public class XMLRSSReader : RSSReader
    {
        public XMLRSSReader()
        {
        }

        public override IEnumerable<RSSItemDTO> Read(Uri pUri)
        {
            if (pUri == null)
            {
                throw new ArgumentNullException("pUri");
            }


            // Obtiene el feed en formato XML
            Log.Information("Cargando Url en lector.");
            XmlTextReader mXmlReader = new XmlTextReader(pUri.AbsoluteUri);

            // Genera un nuevo documento
            XmlDocument rSSXmlDocument = new XmlDocument();

            // Carga el feed en el documento
            Log.Information("Cargando fuente xml en el documento.");
            rSSXmlDocument.Load(mXmlReader);


            IList<RSSItemDTO> rSSItems = new List<RSSItemDTO>();

            StringBuilder rSSContent = new StringBuilder();

            Log.Information("Construyendo Items RSS.");
            // Itera entre los items del del archivo RSS y genera RSSItemDTO
            foreach (XmlNode rssNode in rSSXmlDocument.SelectNodes("//channel/item"))
            {
                DateTime result;
                DateTime.TryParse(rssNode.SelectSingleNode("pubDate").InnerText, out result);
                rSSItems.Add(new RSSItemDTO
                {
                    Title = rssNode.SelectSingleNode("title").InnerText,
                    Url = rssNode.SelectSingleNode("link").InnerText,
                    Description = rssNode.SelectSingleNode("description").InnerText,
                    Date = result,
                });
            }

            return rSSItems;
        }
    }
}
