using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DigitalSignage.Domain
{
    public class RSSSource : BannerSource
    {
        public string Url { get; set; }

        public string Description { get; set; }

        public IList<RSSItem> RSSItems { get; set; }
        
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
/*   try
   {
       XmlTextReader reader = new XmlTextReader(item.Url);
       DataSet ds = new DataSet();
       ds.ReadXml(reader);
   }
   catch (Exception e)
   {
       Console.WriteLine("No hay conexión, mostrando feed anterior");
       item.Title
   }*/
