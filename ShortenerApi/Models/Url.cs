using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortenerApi.Models
{
    public class Url
    {
        public int Id { get; set; }
        public string FullUrl { get; set; }
        public string ShortUrl { get; set; }
        public string Extension { get; set; }
        public DateTime Created { get; set; }
    }
}
