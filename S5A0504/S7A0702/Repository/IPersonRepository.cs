using S6A0702.Moldel.Entities;
using System.Collections.Generic;

namespace S6A0702.Repository
{
    public interface IPersonRepository
    {
        Person GetById(long id);
        IEnumerable<Person> GetAll();
        bool Exists(long id);
        void Create(ref Person entity);
        void Update(ref Person entity);
        void DeleteById(long id);
    }
}
