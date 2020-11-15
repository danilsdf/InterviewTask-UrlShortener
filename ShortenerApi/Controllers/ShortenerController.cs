using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShortenerApi.EntityFramework;
using ShortenerApi.Helpers;
using ShortenerApi.Models;

namespace ShortenerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortenerController : ControllerBase
    {
        private readonly ShortenerContext _context;
        private readonly Shortener _shortener;
        private readonly List<string> AllExtensions = new List<string>() { ".png",".jpg",
            ".svg",".pdf",".gif", ".heic", ".heif","webp"/*,".ai",".fig",".sketch"*/ };

        public ShortenerController(ShortenerContext context )
        {
            _context = context;
            _shortener = new Shortener(context);
        }
        
        [HttpGet]
        public IEnumerable<Url> GetUrls()
        {
            return _context.Urls.ToList();
        }

        [HttpGet("last")]
        public Url GetLastUrl()
        {
            return _context.Urls.ToList().LastOrDefault();
        }

        [HttpGet("/{strurl}")]
        public ActionResult GetFile(string strurl)
        {
            
            Url url = _context.Urls.Where(w => w.ShortUrl == strurl).FirstOrDefault();
            if (url is null)
            {
                return BadRequest();
            }
            Byte[] b = System.IO.File.ReadAllBytes(@$"{url.FullUrl}");          
            if(url.Extension == ".pdf")
            {
            return File(b, "application/pdf");
            }
            return File(b, "image/jpeg");
            //return Redirect(url.FullUrl);
        }

        [HttpPost]
        public ActionResult<Url> Create(IFormFile file)
        {
            Url mainurl = null;
            if (file != null && ModelState.IsValid)
            {
                var urls = _context.Urls.ToList();
                var Extension = Path.GetExtension(file.FileName);

                if (AllExtensions.Contains(Extension.ToLower()) && file.Length<20*1024*1024)
                {
                    var RelativeImagePath = "./UploadedFiles/" + file.FileName;


                    using (var filestream = new FileStream(RelativeImagePath, FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }

                    if (!urls.Exists(url => url.FullUrl == RelativeImagePath))
                    {
                        mainurl = new Url
                        {
                            FullUrl = RelativeImagePath,
                            Extension = Extension,
                            ShortUrl = _shortener.GetUrl(),
                            Created = DateTime.Now
                        };
                        _context.Urls.Add(mainurl);
                        _context.SaveChanges();
                    }
                    else
                    {
                        mainurl = _context.Urls.Where(w => w.FullUrl == RelativeImagePath).FirstOrDefault();
                    }
                }
                else
                {
                    return BadRequest();
                }
                return mainurl;
            }
            return BadRequest();
        }
    }
}
