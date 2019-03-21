using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DigitalSignage.Domain
{
    /// <summary>
    /// Clase que representa una fuente RSS
    /// </summary>
    public class RSSSource : BannerSource
    {

        /// <summary>
        /// Url de la fuente
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Descripcion de la fuente
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Lista de items de la fuente
        /// </summary>
        public IList<RSSItem> RSSItems { get; set; }
        
        /// <summary>
        /// Obtiene el texto de cada item
        /// </summary>
        /// <returns>Texto con todos los items RSS</returns>
        public override string GetText()
        {
            string text = "";

            using (var enumerator = RSSItems.GetEnumerator())
            {

                while (enumerator.MoveNext())
                {

                    var current = enumerator.Current;
                    text += current.Title + " : " + current.Description + " | ";

                }

            }

            return text;

        }
    }
}