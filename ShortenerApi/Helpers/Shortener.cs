using ShortenerApi.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortenerApi.Helpers
{
    public class Shortener
    {
        private readonly ShortenerContext _context;
        public string _ShortUrl { get; set; }
        public Shortener(ShortenerContext context)
        {
            _context = context;
        }
        public string GetUrl()
        {
            var urls = _context.Urls.ToList();
            if (urls.Count != 0)
            {
                while (urls.Exists(url => url.ShortUrl == GenereteShortUrl())) ;
            }
            else GenereteShortUrl();

            return _ShortUrl;
        }
        private string GenereteShortUrl()
        {
            string url = string.Empty;
            Random rnd = new Random();
            Enumerable.Range(48, 90)
                .Where(w => w > 48 && w < 57 || w > 64 && w < 90)
                .OrderBy(o => rnd.Next())
                .ToList()
                .ForEach(i => url += (char)i);
            _ShortUrl = url.Substring(rnd.Next(0, url.Length - 7), rnd.Next(4, 6));
            return _ShortUrl;
        }
    }
}
