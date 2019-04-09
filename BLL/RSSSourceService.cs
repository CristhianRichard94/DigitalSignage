using DigitalSignage.DAL.EntityFramework;
using DigitalSignage.Domain;
using DigitalSignage.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.BLL
{
    /// <summary>
    /// Clase que implementa la interfaz de IRSSSource que brinda servicios a la vista
    /// </summary>
    public class RSSSourceService : IRSSSourceService
    {
        private UnitOfWork iUnitOfWork;


        /// <summary>
        /// Constructor para usar contexto por defecto
        /// </summary>
        public RSSSourceService()
        {

            this.iUnitOfWork = new UnitOfWork(new DigitalSignageDbContext());

        }


        /// <summary>
        /// Crea una fuente RSS
        /// </summary>
        /// <param name="pRssSourceDTO">fuente RSS que se quiere crear</param>
        public void Create(RSSSourceDTO pRSSSourceDTO)
        {
            ///Mapea el DTO a un objecto RSSSource
            var source = new RSSSource();
            AutoMapper.Mapper.Map(pRSSSourceDTO, source);

            try
            {
                this.iUnitOfWork.RSSSourceRepository.Add(source);
                this.iUnitOfWork.Complete();

            }
            catch (ArgumentException e)
            {
                throw e;

            }

        }


        /// <summary>
        /// Eliminar una fuente RSS
        /// </summary>
        /// <param name="pRssSourceDTO">fuente RSS que se desea eliminar</param>
        public void Remove(RSSSourceDTO pRSSSourceDTO)
        {

                var asociatedBanners = iUnitOfWork.RSSSourceRepository.GetBannersWithSource(pRSSSourceDTO.Id);

                if (asociatedBanners.ToList().Count == 0)
                {

                    RSSSource RSSSource = iUnitOfWork.RSSSourceRepository.Get(pRSSSourceDTO.Id);
                    iUnitOfWork.RSSSourceRepository.Remove(RSSSource);
                    iUnitOfWork.Complete();

                }
                else
                {
                    throw new Exception("No se puede eliminar la fuente RSS ya que esta siendo usada.");

                }

        }

        /// <summary>
        /// Actualiza una fuente RSS
        /// </summary>
        /// <param name="pRssSourceDTO">fuente RSS que se desea actualizar</param>
        public void Update(RSSSourceDTO pRSSSourceDTO)
        {
            ///fuente RSS actualizada
            var source = new RSSSource();
            AutoMapper.Mapper.Map(pRSSSourceDTO, source);
            
                //fuente RSS anterior
                iUnitOfWork.RSSSourceRepository.Update(source);

                //Guardando los cambios
                iUnitOfWork.Complete();


        }

        /// <summary>
        /// Obtiene una fuente RSS por id
        /// </summary>
        /// <param name="pId">id de la fuente RSS que se quiere obtener</param>
        /// <returns></returns>
        public RSSSourceDTO Get(int pId)
        {

                var source = iUnitOfWork.RSSSourceRepository.Get(pId);

                var sourceDTO = new RSSSourceDTO();
                AutoMapper.Mapper.Map(source, sourceDTO);
                return sourceDTO;


        }


        /// <summary>
        /// Obtiene todas las fuentes RSS
        /// </summary>
        public IEnumerable<RSSSourceDTO> GetAll()
        {


                IEnumerable<RSSSource> sources = iUnitOfWork.RSSSourceRepository.GetAll();

                var sourcesDTO = AutoMapper.Mapper.Map<IEnumerable<RSSSource>, IEnumerable<RSSSourceDTO>>(sources);

                return sourcesDTO;


        }
    }
}
