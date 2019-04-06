using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DigitalSignage.DTO;

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
            XmlTextReader mXmlReader = new XmlTextReader(pUri.AbsoluteUri);

            // Genera un nuevo documento
            XmlDocument mRssXmlDocument = new XmlDocument();

            // Carga el feed en el documento
            mRssXmlDocument.Load(mXmlReader);


            IList<RSSItemDTO> rSSItems = new List<RSSItemDTO>();

            // Genera un item por cada nodo/item en el documento XML
            foreach (XmlNode bRssXmlItem in mRssXmlDocument.SelectNodes("//channel/item"))
            {
                rSSItems.Add(new RSSItemDTO
                {
                    Title = XMLRSSReader.GetXmlNodeValue<String>(bRssXmlItem, "title"),
                    Description = XMLRSSReader.GetXmlNodeValue<String>(bRssXmlItem, "description"),
                    Url = XMLRSSReader.GetXmlNodeValue<String>(bRssXmlItem, "link"),
                    Date = XMLRSSReader.GetXmlNodeValue<DateTime?>(bRssXmlItem, "pubDate")
                });
            }

            return rSSItems;
        }

        /// <summary>
        /// Obtiene el valor del nodo XML
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="pParentNode">Nodo padre, referencia a un item</param>
        /// <param name="pChildNodeName">Nodo hijo, referencia un atributo del item</param>
        /// <returns>Valor del nodo hijo especificado</returns>
        private static TResult GetXmlNodeValue<TResult>(XmlNode pParentNode, String pChildNodeName)
        {
            if (pParentNode == null)
            {
                throw new ArgumentNullException("pParentNode");
            }

            if (String.IsNullOrWhiteSpace(pChildNodeName))
            {
                throw new ArgumentException("pChildNodeName");
            }

            // Inicialmente se devuelve el valor por defecto del tipo genérico. Si es un objeto, devuelve null, sino depende del tipo indicado.
            TResult result = default(TResult);

            // Tipo utilizado para la conversión final, por defecto será el mismo tipo genérico.
            Type convertToType = typeof(TResult);

            XmlNode childNode = pParentNode.SelectSingleNode(pChildNodeName);

            // Si el nodo existe, se obtiene el valor de texto del mismo para convertirlo al tipo genérico.
            if (childNode != null)
            {
                // Se comprueba si el tipo es Nullable, en dicho caso se debe convertir al tipo subyacente.
                if (Nullable.GetUnderlyingType(convertToType) != null)
                {
                    convertToType = Nullable.GetUnderlyingType(convertToType);
                }

                // Se realiza la conversión del texto del nodo al tipo adecuado, ya sea el tipo genérico indicado o bien el tipo subyacente del Nullable.
                result = (TResult)Convert.ChangeType(childNode.InnerText.Trim(), convertToType);
            }

            return result;
        }
    }
}
