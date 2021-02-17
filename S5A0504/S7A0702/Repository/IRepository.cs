using S6A0702.Moldel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S6A0702.Repository
{
    public interface IRepository<TEntity>
        where TEntity: EntityBase
    {
        TEntity GetById(long id);
        IEnumerable<TEntity> GetAll();
        bool Exists(long id);
        bool Create(ref TEntity entity);
        bool Update(ref TEntity entity);
        bool DeleteById(long id);
    }
}
