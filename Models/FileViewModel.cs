using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortenerUrl.Models
{
    public class FileViewModel
    {
        public string Name { get; set; }
        public IFormFile File { get; set; }
    }
}
