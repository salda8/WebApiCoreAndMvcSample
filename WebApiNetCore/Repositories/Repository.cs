using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WebApiNetCore.Entities
{
    public abstract class Repository //: IRepository
    {
        protected static Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Repository(IInvoiceContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        public IInvoiceContext Context { get; }

        /// <summary>
        /// Ases the queryable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected IQueryable<T> AsQueryable<T>() where T : class, IEntity
        {
            return Context.DbContext.Set<T>().AsQueryable();
        }

        /// <summary>
        ///     Gets all entities. If maxRows is supplied, the query will throw an exception if more than that number of rows were
        ///     to be returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetAll<T>() where T : class, IEntity
        {
            return GetAllInner<T>();
        }

        /// <summary>
        /// Gets all inner.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected IQueryable<T> GetAllInner<T>() where T : class, IEntity
        {
            return AsQueryable<T>();
        }

        /// <summary>
        /// Finds the entities matching the supplied specification. If maxRows is supplied, the query will throw an exception
        /// if more than that number of rows were to be returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">Search condition.</param>
        /// <returns></returns>
        public IEnumerable<T> Find<T>(Expression<Func<T, bool>> where) where T : class, IEntity
        {
            return FindInner(where);
        }

        /// <summary>
        /// Finds the inner.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        protected virtual IQueryable<T> FindInner<T>(Expression<Func<T, bool>> where) where T : class, IEntity
        {
            return AsQueryable<T>().Where(where);
        }

        /// <summary>
        /// Returns specified single entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">Search condition.</param>
        /// <returns></returns>
        public virtual T SingleOrDefault<T>(Expression<Func<T, bool>> where) where T : class, IEntity
        {
            return AsQueryable<T>().SingleOrDefault(where);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        public void Delete<T>(T entity) where T : class, IEntity
        {
            Context.DbContext.Set<T>().Remove(entity);
        }

        /// <summary>
        ///     Deletes the specified range entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void DeleteRange<T>(IEnumerable<T> list) where T : class, IEntity
        {
            Context.DbContext.Set<T>().RemoveRange(list);
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        public virtual void Add<T>(T entity) where T : class, IEntity
        {
            Context.DbContext.Set<T>().Add(entity);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        public void Update<T>(T entity) where T : class, IEntity
        {
            Context.DbContext.Set<T>().Update(entity);
        }
        /// <summary>
        /// Updates the specified property of entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="propertyName">Name of the property which is updated.</param>
        public void UpdateSingleProperty<T>(T entity, string propertyName) where T : class, IEntity
        {
            Context.DbContext.Set<T>().Attach(entity);
            Context.DbContext.Entry(entity).Property(propertyName).IsModified = true;
        }

        public void SetDeleted<T>(T entity, bool value) where T : class, IEntity
        {
            entity.IsDeleted = value;
            Context.DbContext.Set<T>().Attach(entity);
            Context.DbContext.Entry(entity).Property("IsDeleted").IsModified = true;
        }
        /// <summary>
        /// Check if entity exists in database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        public virtual bool Exists<T>(Expression<Func<T, bool>> where) where T : class, IEntity
        {
            return AsQueryable<T>().Any(where);
        }

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        protected async void SaveChangesAsync()
        {
            try
            {
                await Context.DbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                Logger.Debug(e, e.Message);
            }
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        protected void SaveChanges()
        {
            try
            {
                Context.DbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                Logger.Debug(e, e.Message);
            }
        }
    }
}