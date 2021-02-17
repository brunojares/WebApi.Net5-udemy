using Microsoft.EntityFrameworkCore;
using S6A0702.Moldel.Entities;
using S6A0702.Moldels.Context;
using System.Collections.Generic;
using System.Linq;

namespace S6A0702.Repository.Implementation
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        protected override DbSet<Book> Entities => Context.Books;
        public BookRepository(WebApi001Context context)
            : base(context) { }

    }
}
