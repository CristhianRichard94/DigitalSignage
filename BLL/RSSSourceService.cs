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
            ///Mapea el DTO a un objecto RssSource
            var source = new RSSSource();
            AutoMapper.Mapper.Map(pRSSSourceDTO, source);

            try
            {
                this.iUnitOfWork.RSSSourceRepository.Add(source);
                this.iUnitOfWork.Complete();

            }
            catch (ArgumentException e)
            {
                throw new ArgumentException();

            }

        }

        /// <summary>
        /// Eliminar una fuente RSS
        /// </summary>
        /// <param name="pRssSourceDTO">fuente RSS que se desea eliminar</param>
        public void Remove(RSSSourceDTO pRSSSourceDTO)
        {

            try
            {
                var asociatedBanners = iUnitOfWork.RSSSourceRepository.GetBannersWithSource(pRSSSourceDTO.Id);

                if (asociatedBanners.ToList().Count == 0)
                {

                    RSSSource RssSource = iUnitOfWork.RSSSourceRepository.Get(pRSSSourceDTO.Id);
                    iUnitOfWork.RSSSourceRepository.Remove(RssSource);
                    iUnitOfWork.Complete();

                }
                else
                {
                    throw new Exception("No se puede eliminar la fuente RSS ya que esta siendo usada por banners");


                }


            }
            catch (ArgumentNullException e)
            {

                throw new ArgumentException();

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

            try
            {
                //fuente RSS anterior
                iUnitOfWork.RSSSourceRepository.Update(source);

                //Guardando los cambios
                iUnitOfWork.Complete();

            }
            catch (Exception e)
            {

                throw new Exception();

            }

        }

        /// <summary>
        /// Obtiene una fuente RSS por su id
        /// </summary>
        /// <param name="pId">id de la fuente RSS que se quiere obtener</param>
        /// <returns></returns>
        public RSSSourceDTO Get(int pId)
        {

            try
            {

                var source = iUnitOfWork.RSSSourceRepository.Get(pId);

                var sourceDTO = new RSSSourceDTO();
                AutoMapper.Mapper.Map(source, sourceDTO);
                return sourceDTO;

            }
            catch (Exception e)
            {

                throw new Exception();

            }

        }


        /// <summary>
        /// Obtiene todas las fuente RSSs
        /// </summary>
        public IEnumerable<RSSSourceDTO> GetAll()
        {

            try
            {

                IEnumerable<RSSSource> sources = iUnitOfWork.RSSSourceRepository.GetAll();

                var sourcesDTO = AutoMapper.Mapper.Map<IEnumerable<RSSSource>, IEnumerable<RSSSourceDTO>>(sources);

                return sourcesDTO;

            }
            catch (Exception e)
            {

                throw new Exception();

            }

        }
    }
}
