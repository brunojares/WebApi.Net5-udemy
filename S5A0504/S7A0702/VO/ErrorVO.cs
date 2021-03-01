using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S6A0702.VO
{
    public class ErrorVO
    {
        public string Title { get; }
        public string[] Errors { get; }
        public ErrorVO(string title, params string[] errors)
        {
            Title = title;
            Errors = errors ?? Array.Empty<string>();
        }
    }
}
