using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S6A0702.Moldels.Person
{
    public class PersonModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        public void CopyFrom(PersonModel model, bool withId = false)
        {
            if (model != null)
            {
                if (withId)
                    Id = model.Id;
                FirstName = model.FirstName;
                LastName = model.LastName;
                Address = model.Address;
                Gender = model.Gender;
            }
        }
    }
}
