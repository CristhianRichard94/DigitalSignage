using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.Domain
{
    /// <summary>
    /// Clase que representa un banner
    /// </summary>
    public class Banner
    {
        /// <summary>
        /// Id de banner
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del banner
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descripcion del banner
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Fecha a partir de la cual debería transmitirse
        /// </summary>
        public DateTime InitialDate { get; set; }

        /// <summary>
        /// Fecha desde la cual debería dejar de transmitirse
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Horario a partir del cual debería transmitirse
        /// </summary>
        public TimeSpan InitialTime { get; set; }

        /// <summary>
        /// Horario a partir del cual debería dejar de transmitirse
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// Fuente del banner, puede ser de diferentes tipos
        /// </summary>
        public BannerSource Source { get; set; }

        /// <summary>
        /// Clave foránea para la fuente del banner
        /// </summary>
        public int SourceId { get; set; }

        /// <summary>
        /// Verifica que el banner esté activo en el momento
        /// </summary>
        /// <returns>Verdadero si esta activo, falso en caso contrario</returns>
        public bool IsActiveNow()
        {
            bool isActive;
            var today = DateTime.Now;
            // Se encuentra activo en la fecha
            isActive = this.InitialDate.Date <= today.Date && today.Date <= this.EndDate.Date;
            isActive &= this.InitialTime <= today.TimeOfDay && today.TimeOfDay <= this.EndTime;
            return isActive;

        }

        /// <summary>
        /// Obtiene el texto del banner
        /// </summary>
        /// <returns>Texto del banner</returns>
        public IList<string> GetText()
        {

            return Source.GetText();

        }
    }
}
