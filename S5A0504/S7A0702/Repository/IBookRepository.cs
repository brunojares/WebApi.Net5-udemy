using S6A0702.Moldel.Entities;
using System.Collections.Generic;

namespace S6A0702.Repository
{
    public interface IBookRepository : IRepository<Book>
    {
        IEnumerable<Book> GetByFilter(string filter);
    }
}
