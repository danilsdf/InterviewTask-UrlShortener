using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShortenerUrl.EntityFramework;
using Microsoft.AspNetCore.Hosting;
using ShortenerUrl.Models;
using ShortenerUrl.Helper;

namespace ShortenerUrl.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShortenerContext _context;
        private readonly Shortener _shortener;
        private readonly IWebHostEnvironment _environment;
        private readonly List<string> AllExtensions = new List<string>() { ".png",".jpg",
            ".svg",".pdf",".gif", ".heic", ".heif","webp"/*,".ai",".fig",".sketch"*/ };

        public HomeController(ShortenerContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            _shortener = new Shortener(context);
        }

        public IActionResult Index(string Error = null)
        {
            ViewBag.Error = Error;
            return View(_context.Urls.ToList());
        }

        [HttpGet("{strurl}")]
        public ActionResult GetPage(string strurl)
        {
            Url url = _context.Urls.Where(w=>w.ShortUrl == strurl).FirstOrDefault();
            if(url is null)
            {
                return RedirectToAction(nameof(Index), new { Error = "Cannot find a file" });
            }
            return Redirect(url.FullUrl);
        }

        [HttpPost]
        public IActionResult Create(FileViewModel fileviewmodel)
        {
            if (fileviewmodel != null)
            {
                string wwrootPath = _environment.WebRootPath;
                var urls = _context.Urls.ToList();
                var Extension = Path.GetExtension(fileviewmodel.File.FileName);

                if (AllExtensions.Contains(Extension.ToLower()))
                {
                    var RelativeImagePath = "UploadedFiles/" + fileviewmodel.Name + Extension;

                    var AbsImagePath = Path.Combine(wwrootPath, RelativeImagePath);

                    using (var filestream = new FileStream(AbsImagePath, FileMode.Create))
                    {
                        fileviewmodel.File.CopyTo(filestream);
                    }

                    if (!urls.Exists(url => url.FullUrl == RelativeImagePath))
                    {
                        Url mainurl = new Url
                        {
                            FullUrl = RelativeImagePath,
                            Extension = Extension,
                            ShortUrl = _shortener.GetUrl()
                        };
                        _context.Urls.Add(mainurl);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    return RedirectToAction(nameof(Index), new { Error = "Incorrect extension"});
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
