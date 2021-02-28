using S6A0702.Moldel.Entities;
using S6A0702.Moldels.Context;
using System.Collections.Generic;
using System.Linq;

namespace S6A0702.Repository.Implementation
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(WebApi001Context context)
            : base(context) { }

        public IEnumerable<Book> GetByFilter(string filter)
        {
            var _result = Context.Books.AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                _result = _result.Where(b =>
                    b.Title.Contains(filter) ||
                    b.Author.Contains(filter)
                );
            }
            return _result;
        }
    }
}
