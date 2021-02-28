using S6A0702.Moldel.Entities;
using S6A0702.Repository;
using System.Collections.Generic;

namespace S6A0702.Business.Implementation
{
    public class PersonBusiness : IPersonBusiness
    {
        private readonly IPersonRepository _personRepository;

        public PersonBusiness(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public IEnumerable<Person> GetAll() =>
            _personRepository.GetAll()
        ;

        public Person GetById(long id) =>
             _personRepository.GetById(id)
        ;

        public void Create(ref Person entity)
        {
            _personRepository.Create(ref entity);
        }

        public void Update(ref Person entity)
        {
            if (!_personRepository.Exists(entity.Id))
                throw new KeyNotFoundException($"Person {entity.Id} not found");
            _personRepository.Update(ref entity);
        }

        public void DeleteById(long id)
        {
            if (_personRepository.Exists(id))
                _personRepository.DeleteById(id);
        }

        public Person SetEnabled(long id, bool enabled)
        {
            if (!_personRepository.Exists(id))
                throw new KeyNotFoundException($"Person {id} not found");
            return _personRepository.SetEnabled(id, enabled);
        }
    }
}
