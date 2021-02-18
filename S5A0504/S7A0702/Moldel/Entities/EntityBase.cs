using System.ComponentModel.DataAnnotations.Schema;

namespace S6A0702.Moldel.Entities
{
    public class EntityBase
    {
        [Column("id")]
        public long Id { get; set; }
    }
}
