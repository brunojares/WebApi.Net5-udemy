using S6A0702.Moldels.Person;
using System.Collections.Generic;

namespace S6A0702.Services
{
    public interface IPersonService
    {
        void Create(ref PersonModel model);
        void Update(ref PersonModel model);
        void DeleteById(int id);
        PersonModel GetById(int id);
        IEnumerable<PersonModel> GetAll();
    }
}
