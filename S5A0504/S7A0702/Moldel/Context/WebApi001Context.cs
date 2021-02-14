using Microsoft.EntityFrameworkCore;
using S6A0702.Moldel.Entities;

namespace S6A0702.Moldels.Context
{
    public class WebApi001Context : DbContext
    {
        public WebApi001Context() { }
        public WebApi001Context(DbContextOptions<WebApi001Context> context)
            : base(context) { }

        public DbSet<Person> People { get; set; }
    }
}
