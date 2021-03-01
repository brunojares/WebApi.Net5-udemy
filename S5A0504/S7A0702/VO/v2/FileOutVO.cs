using S6A0702.Moldel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S6A0702.VO.v2
{
    public class FileOutVO:IVO<FileDetail>
    {
        public string FileName { get; set; }
        public string DocType { get; set; }
        public string DocUrl { get; set; }

        public void CopyFrom(FileDetail entity)
        {
            if (entity != null)
            {
                FileName = entity.FileName;
                DocType = entity.DocType;
                DocUrl = entity.DocUrl;
            }
        }

        public void CopyTo(ref FileDetail entity)
        {
            if (entity != null)
            {
                entity.FileName = FileName;
                entity.DocType = DocType;
                entity.DocUrl = DocUrl;
            }
        }
    }
}
