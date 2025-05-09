using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Abstractions.DTOs.Storage
{
    public class Upload
    {
        public string FileBytes { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}
