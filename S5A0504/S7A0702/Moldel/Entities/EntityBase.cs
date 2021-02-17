using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace S6A0702.Moldel.Entities
{
    public class EntityBase
    {
        [Column("id")]
        public long Id { get; set; }
    }
}
