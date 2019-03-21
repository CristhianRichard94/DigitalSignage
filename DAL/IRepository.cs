using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL
{

    /// <summary>
    /// Interfaz de repositorio generico que implementa el patron Repository
    /// </summary>
    public interface IRepository<TEntity> where TEntity : class
    {

        /// <summary>
        /// Agrega una nueva entidad, lanza una excepción si esta es nula
        /// </summary>
        /// <param name="pEntity">Entidad</param>
        void Add(TEntity pEntity);


        /// <summary>
        /// Elimina la entidad pasada como parámetro, lanza una excepción si esta es nula
        /// </summary>
        /// <param name="pEntity">Entidad</param>
        void Remove(TEntity pEntity);


        /// <summary>
        /// Obtiene una entidad de la base de datos con el Id pasado como parámetro
        /// </summary>
        /// <param name="pId"></param>
        /// <returns>Devuelve la Entidad con el Id pasado como parámetro</returns
        TEntity Get(int pId);


        /// <summary>
        /// Devuelve todas las entidades del repositorio
        /// </summary>
        /// <returns>Lista de entidades</returns>
        IEnumerable<TEntity> GetAll();

    }
}
