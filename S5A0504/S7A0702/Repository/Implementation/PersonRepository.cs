using S6A0702.Moldel.Entities;
using S6A0702.Moldels.Context;

namespace S6A0702.Repository.Implementation
{
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        public PersonRepository(WebApi001Context context)
            : base(context) { }

        public Person SetEnabled(long id, bool enabled)
        {
            var _entity = GetById(id);
            Context.Entry(_entity).CurrentValues[nameof(Person.Enabled)] = enabled;
            Context.SaveChanges();
            return _entity;
        }
    }
}
