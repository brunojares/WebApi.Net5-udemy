﻿using S6A0702.Moldel.Entities;

namespace S6A0702.VO.v2
{
    public class PersonInVO : IVO<Person>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public bool Enabled { get; set; }

        public virtual void CopyFrom(Person entity)
        {
            if (entity != null)
            {
                FirstName = entity.FirstName;
                LastName = entity.LastName;
                Address = entity.Address;
                Gender = entity.Gender;
                Enabled = entity.Enabled;
            }
        }

        public virtual void CopyTo(ref Person entity)
        {
            if (entity != null)
            {
                entity.FirstName = FirstName;
                entity.LastName = LastName;
                entity.Address = Address;
                entity.Gender = Gender;
                entity.Enabled = Enabled;
            }
        }
    }
}
