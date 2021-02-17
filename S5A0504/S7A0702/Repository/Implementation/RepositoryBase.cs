using Microsoft.EntityFrameworkCore;
using S6A0702.Moldel.Entities;
using S6A0702.Moldels.Context;
using System.Collections.Generic;
using System.Linq;

namespace S6A0702.Repository.Implementation
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : EntityBase
    {
        protected WebApi001Context Context { get; }
        private readonly DbSet<TEntity> _entities;

        protected RepositoryBase(WebApi001Context context)
        {
            Context = context;
            _entities = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll() =>
            _entities
        ;
        public TEntity GetById(long id) =>
            _entities.FirstOrDefault(item => item.Id == id)
        ;
        public bool Exists(long id) =>
            _entities.Count(item => item.Id == id) > 0
        ;
        public virtual bool Create(ref TEntity entity)
        {
            _entities.Add(entity);
            return Context.SaveChanges() > 0;
        }
        public virtual bool Update(ref TEntity entity)
        {
            var _databaseEntity = GetById(entity.Id);
            Context.Entry(_databaseEntity).CurrentValues.SetValues(entity);
            return Context.SaveChanges() > 0;
        }
        public virtual bool DeleteById(long id)
        {
            var _entity = GetById(id);
            _entities.Remove(_entity);
            return Context.SaveChanges() > 0;
        }
    }
}
