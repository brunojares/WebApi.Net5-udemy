using S6A0702.Moldel.Entities;

namespace S6A0702.VO.v2
{
    public class PersonVO : IVO<Person>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        public void CopyFrom(Person entity)
        {
            if (entity != null)
            {
                FirstName = entity.FirstName;
                LastName = entity.LastName;
                Address = entity.Address;
                Gender = entity.Gender;
            }
        }

        public void CopyTo(ref Person entity)
        {
            if (entity != null)
            {
                entity.FirstName = FirstName;
                LastName = entity.LastName;
                Address = entity.Address;
                Gender = entity.Gender;
            }
        }
    }
}
