using S6A0702.Moldel.Entities;
using System.Collections.Generic;

namespace S6A0702.Business
{
    public interface IPersonBusiness
    {
        Person GetById(long id);
        IEnumerable<Person> GetAll();
        public IEnumerable<Person> GetByFilter(string filter);
        void Create(ref Person entity);
        void Update(ref Person entity);
        void DeleteById(long id);
        public Person SetEnabled(long id, bool enabled);
    }
}
