using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using S6A0702.Moldel.Entities;
using S6A0702.Moldels.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S6A0702.Repository.Implementation
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : EntityBase
    {
        protected WebApi001Context Context { get; }
        protected abstract DbSet<TEntity> Entities { get; }

        protected RepositoryBase(WebApi001Context context)
        {
            Context = context;
        }

        public IEnumerable<TEntity> GetAll() =>
            Entities
        ;
        public TEntity GetById(long id) =>
            Entities.FirstOrDefault(item => item.Id == id)
        ;
        public bool Exists(long id) =>
            Entities.Count(item => item.Id == id) > 0
        ;
        public virtual void Create(ref TEntity entity)
        {
            Entities.Add(entity);
            Context.SaveChanges();
        }
        public virtual void Update(ref TEntity entity)
        {
            var _databaseEntity = GetById(entity.Id);
            Context.Entry(_databaseEntity).CurrentValues.SetValues(entity);
            Context.SaveChanges();
        }
        public virtual void DeleteById(long id)
        {
            if (Exists(id))
            {
                var _entity = GetById(id);
                Entities.Remove(_entity);
                Context.SaveChanges();
            }
        }
    }
}
