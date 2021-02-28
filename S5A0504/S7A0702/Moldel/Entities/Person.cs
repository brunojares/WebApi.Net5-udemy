using System.ComponentModel.DataAnnotations.Schema;

namespace S6A0702.Moldel.Entities
{
    [Table("person")]
    public class Person : EntityBase
    {
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        [Column("address")]
        public string Address { get; set; }
        [Column("gender")]
        public string Gender { get; set; }
        [Column("enabled")]
        public bool Enabled { get; set; }
    }
}