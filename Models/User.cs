using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortenerUrl.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Path { get; set; }
    }
}
