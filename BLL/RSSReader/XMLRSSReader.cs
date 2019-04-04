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
    class XMLRSSReader : RSSReader
    {
        public override IEnumerable<RSSItemDTO> Read(Uri pUri)
        {
            if (pUri == null)
            {
                throw new ArgumentNullException("pUri");
            }

            // Se obtiene los datos en formato XML desde la URL, y se transforma el mismo para obtener los diferentes ítems. El modelo de XML
            // utilizado pertenece a http://www.w3schools.com/xml/xml_rss.asp:
            // Se recupera el XML desde la URL, y se parsea el mismo para obtener los diferentes ítems. El modelo de XML
            // utilizado es el siguiente (http://www.w3schools.com/xml/xml_rss.asp):
            //<? xml version = "1.0" encoding = "UTF-8" ?>
            //   < rss version = "2.0" >
            //   < channel >   
            //     < title > W3Schools Home Page</ title >     
            //        < link > https://www.w3schools.com</link>
            //  < description > Free web building tutorials </ description >
            //     < item >  
            //       < title > RSS Tutorial </ title >     
            //          < link > https://www.w3schools.com/xml/xml_rss.asp</link>
            //    < description > New RSS tutorial on W3Schools</ description >  
            //     </ item >
            //     < item >  
            //       < title > XML Tutorial </ title >    
            //          < link > https://www.w3schools.com/xml</link>
            //    < description > New XML tutorial on W3Schools</ description >  
            //     </ item >
            //   </ channel > 
            //   </ rss >

            // Obtiene el feed
            XmlTextReader mXmlReader = new XmlTextReader(pUri.AbsoluteUri);

            // Genera un nuevo documento
            XmlDocument mRssXmlDocument = new XmlDocument();

            // Carga el feed en el documento
            mRssXmlDocument.Load(mXmlReader);


            IList<RSSItemDTO> mRssItems = new List<RSSItemDTO>();

            // Genera un item por cada nodo/item en el documento XML
            foreach (XmlNode bRssXmlItem in mRssXmlDocument.SelectNodes("//channel/item"))
            {
                mRssItems.Add(new RSSItemDTO
                {
                    Title = XMLRSSReader.GetXmlNodeValue<String>(bRssXmlItem, "title"),
                    Description = XMLRSSReader.GetXmlNodeValue<String>(bRssXmlItem, "description"),
                    Url = XMLRSSReader.GetXmlNodeValue<String>(bRssXmlItem, "link"),
                    Date = XMLRSSReader.GetXmlNodeValue<DateTime?>(bRssXmlItem, "pubDate")
                });
            }

            return mRssItems;
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
            TResult mResult = default(TResult);

            // Tipo utilizado para la conversión final, por defecto será el mismo tipo genérico.
            Type mConvertToType = typeof(TResult);

            XmlNode mChildNode = pParentNode.SelectSingleNode(pChildNodeName);

            // Si el nodo existe, se obtiene el valor de texto del mismo para convertirlo al tipo genérico.
            if (mChildNode != null)
            {
                // Se comprueba si el tipo es Nullable, en dicho caso se debe convertir al tipo subyacente.
                if (Nullable.GetUnderlyingType(mConvertToType) != null)
                {
                    mConvertToType = Nullable.GetUnderlyingType(mConvertToType);
                }

                // Se realiza la conversión del texto del nodo al tipo adecuado, ya sea el tipo genérico indicado o bien el tipo subyacente del Nullable.
                mResult = (TResult)Convert.ChangeType(mChildNode.InnerText.Trim(), mConvertToType);
            }

            return mResult;
        }
    }
}
