using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.Domain
{
    public class Banner
    {
        public int Id { get; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime InitialDate { get; set; }

        public DateTime EndDate { get; set; }

        public TimeSpan InitialTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public virtual BannerSource Source { get; set; }

        /// <summary>
        /// Clave foránea para la fuente del banner
        /// </summary>
        public int SourceId { get; set; }

        /// <summary>
        /// Verifica que el banner esté activo en este momento
        /// </summary>
        /// <returns>Verdadero si esta activo, falso en caso contrario</returns>
        public bool IsActiveNow()
        {
            bool isActive;
            var today = DateTime.Now;
            //se encuentra activo en la fecha
            isActive = this.InitialDate.Date <= today.Date && today.Date <= this.EndDate.Date;
            isActive &= this.InitialTime <= today.TimeOfDay && today.TimeOfDay <= this.EndTime;
            return isActive;

        }

        /// <summary>
        /// Obtiene el texto del banner
        /// </summary>
        /// <returns>Texto del banner</returns>
        public string GetText()
        {

            return Source.GetText();

        }
    }
}
