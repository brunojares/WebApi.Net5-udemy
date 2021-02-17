using S6A0702.Moldel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S6A0702.Repository
{
    public interface IBookRepository
    {
        Book GetById(long id);
        IEnumerable<Book> GetAll();
        bool Exists(long id);
        void Create(ref Book entity);
        void Update(ref Book entity);
        void DeleteById(long id);
    }
}
