using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.Domain
{
    /// <summary>
    /// Clase que representa una imagen
    /// </summary>
    public class Image
    {
        /// <summary>
        /// Id de la imagen
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Bytes que construyen la imagen
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Descripcion de la imagen
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Duracion de la imagen
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Posicion en que debe visualizarse la imagen entre la lista de la campaña
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Id de la campaña a la que esta asociada
        /// </summary>
        public int CampaignId { get; set; }

    }
}
