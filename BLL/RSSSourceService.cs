﻿using DigitalSignage.DAL.EntityFramework;
using DigitalSignage.Domain;
using DigitalSignage.DTO;
using Serilog;
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

        /// <summary>
        /// Instancia de unidad de trabajo
        /// </summary>
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
            try
            {
                Log.Information("Creando fuente RSS.");
                var source = new RSSSource();

                Campaign campaign = new Campaign();
                AutoMapper.Mapper.Map(pRSSSourceDTO, source);
                this.iUnitOfWork.RSSSourceRepository.Add(source);

                iUnitOfWork.Complete();
                Log.Information("Fuente RSS creada con exito.");

            }
            catch (Exception)
            {
                Log.Error("Error al crear la fuente RSS.");

                throw;
            }

        }


        /// <summary>
        /// Elimina una fuente RSS
        /// </summary>
        /// <param name="pRssSourceDTO">fuente RSS que se desea eliminar</param>
        public void Remove(RSSSourceDTO pRSSSourceDTO)
        {

            try
            {
                // Verifica si existen banners que tengan asignada la fuente
                var asociatedBanners = iUnitOfWork.RSSSourceRepository.GetBannersWithSource(pRSSSourceDTO.Id);

                if (asociatedBanners.ToList().Count == 0)
                {
                    Log.Information(String.Format("Eliminando fuente RSS con Id {0}.", pRSSSourceDTO.Id));
                    RSSSource RSSSource = iUnitOfWork.RSSSourceRepository.Get(pRSSSourceDTO.Id);
                    iUnitOfWork.RSSSourceRepository.Remove(RSSSource);
                    iUnitOfWork.Complete();
                    Log.Information("Fuente RSS Eliminada con exito.");

                }
                else
                {
                    throw new Exception("No se puede eliminar la fuente RSS ya que esta siendo usada.");
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error al eliminar fuente RSS con Id {0}. "+ ex.Message, pRSSSourceDTO.Id));
                throw;
            }
        }

        /// <summary>
        /// Actualiza una fuente RSS
        /// </summary>
        /// <param name="pRssSourceDTO">fuente RSS que se desea actualizar</param>
        public void Update(RSSSourceDTO pRSSSourceDTO)
        {
            try
            {
                Log.Information(String.Format("Actualizando fuente RSS con Id {0}.", pRSSSourceDTO.Id));
                // Fuente RSS actualizada
                var source = new RSSSource();
                AutoMapper.Mapper.Map(pRSSSourceDTO, source);

                // Fuente RSS anterior
                iUnitOfWork.RSSSourceRepository.Update(source);

                iUnitOfWork.Complete();
                Log.Information("Fuente RSS actualizada con exito.");

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error al actualizar la fuente RSS con Id {0}. "+ ex.Message , pRSSSourceDTO.Id));
                throw;
            }



        }

        /// <summary>
        /// Obtiene una fuente RSS por id
        /// </summary>
        /// <param name="pId">id de la fuente RSS que se quiere obtener</param>
        /// <returns></returns>
        public RSSSourceDTO Get(int pId)
        {
            try
            {
                Log.Information(String.Format("Obteniendo fuente RSS con Id {0}.", pId));
                var source = iUnitOfWork.RSSSourceRepository.Get(pId);
                Log.Information("Fuente RSS obtenida con exito.");
                if (source == null)
                {
                    return null;
                } else
                {
                    var sourceDTO = new RSSSourceDTO();
                    AutoMapper.Mapper.Map(source, sourceDTO);
                    return sourceDTO;
                }

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error al obtener la fuente RSS con id: {0}. "+ ex.Message, pId));
                throw;
            }



        }


        /// <summary>
        /// Obtiene todas las fuentes RSS
        /// </summary>
        public IEnumerable<RSSSourceDTO> GetAll()
        {

            try
            {
                Log.Information("Obteniendo todas las fuentes RSS.");
                IEnumerable<RSSSource> sources = iUnitOfWork.RSSSourceRepository.GetAll();
                Log.Information("Fuentes RSS obtenidas con exito.");

                return AutoMapper.Mapper.Map<IEnumerable<RSSSource>, IEnumerable<RSSSourceDTO>>(sources);
            }
            catch (Exception)
            {
                Log.Error("Error al obtener todas las fuentes RSS.");

                throw;
            }
        }


        /// <summary>
        /// Obtiene las fuentes RSS que contengan en su URL la especificada
        /// </summary>
        /// <param name="pURL"></param>
        /// <returns></returns>
        public IEnumerable<RSSSourceDTO> GetSourcesByURL(string pURL)
        {
            try
            {
                Log.Information(String.Format("Obteniendo fuentes que contengan '{0}' en su URL.", pURL));
                IEnumerable<RSSSource> sources = iUnitOfWork.RSSSourceRepository.GetSourcesByURL(pURL);
                Log.Information("Fuentes obtenidas con exito.");
                return AutoMapper.Mapper.Map<IEnumerable<RSSSourceDTO>>(sources);
            }
            catch (Exception)
            {
                Log.Error("Error al obtener las fuentes por URL.");
                throw;
            }
        }
    }
}
