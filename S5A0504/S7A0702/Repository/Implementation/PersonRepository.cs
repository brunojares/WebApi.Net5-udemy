using Microsoft.EntityFrameworkCore;
using S6A0702.Moldel.Entities;
using S6A0702.Moldels.Context;
using System.Collections.Generic;
using System.Linq;

namespace S6A0702.Repository.Implementation
{
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        protected override DbSet<Person> Entities => Context.People;
        public PersonRepository(WebApi001Context context)
            : base(context) { }
    }
}
