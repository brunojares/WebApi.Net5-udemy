using S6A0702.Moldel.Entities;
using S6A0702.Moldels.Context;
using System.Collections.Generic;
using System.Linq;

namespace S6A0702.Repository.Implementation
{
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        public PersonRepository(WebApi001Context context)
            : base(context) { }
        public IEnumerable<Person> GetByFilter(string filter)
        {
            var _result = Context.People.AsQueryable();
            if(!string.IsNullOrWhiteSpace(filter))
            {
                _result = _result.Where(p =>
                    p.FirstName.Contains(filter) ||
                    p.LastName.Contains(filter) ||
                    p.Address.Contains(filter)
                );
            }
            return _result;
        }
        public Person SetEnabled(long id, bool enabled)
        {
            var _entity = GetById(id);
            Context.Entry(_entity).CurrentValues[nameof(Person.Enabled)] = enabled;
            Context.SaveChanges();
            return _entity;
        }
    }
}
