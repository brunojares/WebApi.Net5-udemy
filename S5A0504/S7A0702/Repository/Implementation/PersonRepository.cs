using S6A0702.Moldel.Entities;
using S6A0702.Moldels.Context;
using System.Collections.Generic;
using System.Linq;

namespace S6A0702.Repository.Implementation
{
    public class PersonRepository : IPersonRepository
    {
        private WebApi001Context _webApi001Context;

        public PersonRepository(WebApi001Context webApi001Context)
        {
            _webApi001Context = webApi001Context;
        }
        public IEnumerable<Person> GetAll() =>
            _webApi001Context.People
        ;

        public Person GetById(long id) =>
             _webApi001Context.People.FirstOrDefault(item => item.Id == id)
        ;

        public bool Exists(long id) =>
            _webApi001Context.People.Count(item => item.Id == id) > 0
        ;
        public void Create(ref Person entity)
        {
            _webApi001Context.Add(entity);
            _webApi001Context.SaveChanges();
        }
        public void Update(ref Person entity)
        {
            var _databaseEntity = GetById(entity.Id);
            _webApi001Context.Entry(_databaseEntity).CurrentValues.SetValues(entity);
            _webApi001Context.SaveChanges();
        }

        public void DeleteById(long id)
        {
            var _databaseEntity = GetById(id);
            _webApi001Context.People.Remove(_databaseEntity);
            _webApi001Context.SaveChanges();
        }

    }
}
