using S6A0702.Moldel.Entities;
using System.Collections.Generic;
using System.Linq;

namespace S6A0702.Services.Implementation
{
    public class PersonService : IPersonService
    {
        private static List<Person> _models;
        private static List<Person> Models
        {
            get
            {
                if (_models == null)
                {
                    _models = new List<Person>()
                    {
                        New(1, "Fulano"),
                        New(2, "Ciclano"),
                        New(3, "Beltrano")
                    };
                }
                return _models;
            }
        }

        public void Create(ref Person model)
        {
            try
            {
                model.Id = Models.Select(item => item.Id).Max() + 1;
            }
            catch
            {
                model.Id = 1;
            }
            Models.Add(model);
        }
        public void Update(ref Person model)
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

        public IEnumerable<Person> GetAll() =>
            Models
        ;

        public Person GetById(int id) =>
             Models.FirstOrDefault(item => item.Id == id)
        ;

        private static Person New(int id, string firstName) =>
            new Person()
            {
                Id = id,
                FirstName = firstName,
                LastName = "de Tal",
                Address = "Rua dos Bobos, 0",
                Gender = "Male"
            }
        ;
    }
}
