using S6A0702.Moldel.Entities;
using S6A0702.Moldels.Context;
using System.Collections.Generic;
using System.Linq;

namespace S6A0702.Services.Implementation
{
    public class PersonService : IPersonService
    {
        private WebApi001Context _webApi001Context;

        public PersonService(WebApi001Context webApi001Context)
        {
            _webApi001Context = webApi001Context;
        }
        public IEnumerable<Person> GetAll() =>
            _webApi001Context.People
        ;

        public Person GetById(int id) =>
             _webApi001Context.People.FirstOrDefault(item => item.Id == id)
        ;

        public void Create(ref Person entity)
        {
            _webApi001Context.Add(entity);
            _webApi001Context.SaveChanges();
        }
        public void Update(ref Person entity)
        {
            var _databaseEntity =
                GetById(entity.Id) ??
                throw new KeyNotFoundException($"Person {entity.Id} not found")
            ;
            _webApi001Context.Entry(_databaseEntity).CurrentValues.SetValues(entity);
            _webApi001Context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var _databaseEntity = GetById(id);
            if (_databaseEntity != null)
            {
                _webApi001Context.People.Remove(_databaseEntity);
                _webApi001Context.SaveChanges();
            }
        }

    }
}
