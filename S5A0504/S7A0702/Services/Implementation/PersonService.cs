using S6A0702.Moldels.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S6A0702.Services.Implementation
{
    public class PersonService : IPersonService
    {
        private static List<PersonModel> _models;
        private static List<PersonModel> Models
        {
            get
            {
                if (_models == null)
                    _models = new List<PersonModel>();
                return _models;
            }
        }

        public void Create(ref PersonModel model)
        {
            model.Id = Models.Select(item => item.Id).Max() + 1;
            Models.Add(model);
        }
        public void Update(ref PersonModel model)
        {
            var _item = GetById(model.Id) ;
            if (_item != null)
                _item.CopyFrom(model);
        }

        public void DeleteById(int id)
        {
            var _item = GetById(id);
            if (_item != null)
                Models.Remove(_item);
        }

        public IEnumerable<PersonModel> GetAll() =>
            Models
        ;

        public PersonModel GetById(int id) =>
             Models.FirstOrDefault(item => item.Id == id)
        ;
    }
}
