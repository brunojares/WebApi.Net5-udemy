using Microsoft.AspNetCore.Http;
using S6A0702.Moldel.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace S6A0702.Business.Implementation
{
    public class FileBusiness : IFileBusiness
    {
        private readonly string _basePath;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FileBusiness(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _basePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadDir");
            Directory.CreateDirectory(_basePath);
        }

        public byte[] GetFile(string fileName)
        {
            var _path = Path.Combine(_basePath, Path.GetFileName(fileName));
            return File.ReadAllBytes(_path);
        }

        public async Task<FileDetail> SaveFile(IFormFile file, string apiVersion)
        {
            var _result = new FileDetail();
            //======
            _result.DocType = Path.GetExtension(file.FileName);
            var _baseUrl = _httpContextAccessor.HttpContext.Request.Host.Value;
            if (new string[] { ".pdf", ".jpg", ".png", ".jpeg" }.Contains(_result.DocType.Trim().ToLower()))
            {
                var _docName = Path.GetFileName(file.FileName);
                _result.FileName = _docName;
                if (file?.Length > 0)
                {
                    var _destination = Path.Combine(_basePath, _docName);
                    _result.DocUrl = Path.Combine(_baseUrl, "api", "file", apiVersion, _result.FileName);
                    using (var stream = new FileStream(_destination, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }
            //======
            return _result;
        }

        public async Task<IEnumerable<FileDetail>> SaveFiles(IEnumerable<IFormFile> files, string apiVersion)
        {
            var _taskList = files.Select(file => SaveFile(file, apiVersion));
            return await Task.WhenAll(_taskList);
        }
    }
}