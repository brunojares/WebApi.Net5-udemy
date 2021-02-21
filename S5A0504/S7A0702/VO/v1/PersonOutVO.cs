using S6A0702.Moldel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S6A0702.VO.v1
{
    public class PersonOutVO : PersonInVO
    {
        public long Id { get; set; }
        public override void CopyFrom(Person entity)
        {
            Id = entity?.Id ?? 0;
            base.CopyFrom(entity);
        }

        public override void CopyTo(ref Person entity)
        {
            if (entity != null)
                entity.Id = Id;
            base.CopyTo(ref entity);
        }
    }
}
