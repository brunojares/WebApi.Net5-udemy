using S6A0702.Moldel.Entities;
using System.Collections.Generic;

namespace S6A0702.Business
{
    public interface IPersonBusiness
    {
        void Create(ref Person entity);
        void Update(ref Person entity);
        void DeleteById(long id);
        Person GetById(long id);
        IEnumerable<Person> GetAll();
    }
}
