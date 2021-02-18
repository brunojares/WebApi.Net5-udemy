using S6A0702.Moldel.Entities;
using S6A0702.Moldels.Context;

namespace S6A0702.Repository.Implementation
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(WebApi001Context context)
            : base(context) { }

    }
}
