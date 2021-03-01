using Microsoft.AspNetCore.Http;
using S6A0702.Moldel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S6A0702.Business
{
    public interface IFileBusiness
    {
        byte[] GetFile(string fileName);
        public Task<FileDetail> SaveFile(IFormFile file, string apiVersion);
        public Task<IEnumerable<FileDetail>> SaveFiles(IEnumerable<IFormFile> files, string apiVersion);
    }
}
