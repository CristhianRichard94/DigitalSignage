using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.Domain
{
    /// <summary>
    /// Clase que representa un Item RSS
    /// </summary>
    public class RSSItem
    {
        /// <summary>
        /// Id del item
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Url del item
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Titulo del item
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Fecha de publicación del item
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Descripcion del item
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Id de la fuente a la que pertenece el item
        /// </summary>
        public int RSSSourceId { get; set; }
    }
}
