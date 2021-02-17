using S6A0702.Moldel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S6A0702.Business
{
    public interface IBookBusiness
    {
        Book GetById(long id);
        IEnumerable<Book> GetAll();
        void Create(ref Book entity);
        void Update(ref Book entity);
        void DeleteById(long id);
    }
}
