using S6A0702.Moldel.Entities;
using System.Collections.Generic;

namespace S6A0702.Business
{
    public interface IPersonService
    {
        void Create(ref Person entity);
        void Update(ref Person entity);
        void DeleteById(int id);
        Person GetById(int id);
        IEnumerable<Person> GetAll();
    }
}
