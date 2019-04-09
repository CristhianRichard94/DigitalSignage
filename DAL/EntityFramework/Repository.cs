using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL.EntityFramework
{

    /// <summary>
    /// Clase abstracta que implementa el patron Repository
    /// </summary>
    /// <typeparam name="TEntity">Entidades que manejará</typeparam>
    /// <typeparam name="TDbContext">Contexto de sesion de la base de datos</typeparam>
    public abstract class Repository<TEntity, TDbContext> : IRepository<TEntity>
        where TEntity : class
        where TDbContext : DbContext
    {
        /// <summary>
        /// Variable de instancia del contexto
        /// </summary>
        protected readonly TDbContext iDbContext;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pContext">Contexto</param>
        public Repository(TDbContext pContext)
        {
            if (pContext == null)
            {
                throw new ArgumentNullException(nameof(pContext));
            }

            this.iDbContext = pContext;
        }


        /// <summary>
        /// Agrega una nueva entidad, lanza una excepción si esta es nula
        /// </summary>
        /// <param name="pEntity">Entidad</param>
        public void Add(TEntity pEntity)
        {
            if (pEntity == null)
            {
                throw new ArgumentNullException(nameof(pEntity));
            }

            this.iDbContext.Set<TEntity>().Add(pEntity);
        }


        /// <summary>
        /// Elimina la entidad pasada como parámetro, lanza una excepción si esta es nula
        /// </summary>
        /// <param name="pEntity">Entidad</param>
        public void Remove(TEntity pEntity)
        {
            if (pEntity == null)
            {
                throw new ArgumentNullException(nameof(pEntity));
            }
            this.iDbContext.Set<TEntity>().Remove(pEntity);
        }

        /// <summary>
        /// Obtiene una entidad de la base de datos con el Id pasado como parámetro
        /// </summary>
        /// <param name="pId"></param>
        /// <returns>Devuelve la Entidad con el Id pasado como parámetro</returns>
        public virtual TEntity Get(int pId)
        {
            return this.iDbContext.Set<TEntity>().Find(pId);
        }


        /// <summary>
        /// Devuelve todas las entidades del repositorio
        /// </summary>
        /// <returns>Lista de entidades</returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return this.iDbContext.Set<TEntity>().ToList();
        }
    }
}
