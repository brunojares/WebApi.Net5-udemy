using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace S6A0702.Moldel.Entities
{
    [Table("person")]
    public class Person
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        [Column("address")]
        public string Address { get; set; }
        [Column("gender")]
        public string Gender { get; set; }

        public void CopyFrom(Person model, bool withId = false)
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
