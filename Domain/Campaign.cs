using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.Domain
{

    /// <summary>
    /// Clase que representa una Campaña
    /// </summary>
    public class Campaign
    {
        /// <summary>
        /// Id de la campaña
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la campaña
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descripcion de la campaña
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
        /// Lista de imagenes que debe mostrar
        /// </summary>
        public IList<Image> Images {get; set;}

        /// <summary>
        /// Verifica si la campaña se encuentra activa en ese instante de tiempo
        /// </summary>
        /// <returns>Verdadero o falso si esta activa</returns>
        public bool IsActiveNow()
        {
            bool isActive;
            var today = DateTime.Now;
            //se encuentra activo en la fecha
            isActive = this.InitialDate.Date <= today.Date && today.Date <= this.EndDate.Date;
            isActive &= this.InitialTime <= today.TimeOfDay && today.TimeOfDay <= this.EndTime;
            return isActive;

        }
    }
}
