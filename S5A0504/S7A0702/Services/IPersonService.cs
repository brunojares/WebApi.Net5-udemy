using S6A0702.Moldel.Entities;
using System.Collections.Generic;

namespace S6A0702.Services
{
    public interface IPersonService
    {
        void Create(ref Person model);
        void Update(ref Person model);
        void DeleteById(int id);
        Person GetById(int id);
        IEnumerable<Person> GetAll();
    }
}
