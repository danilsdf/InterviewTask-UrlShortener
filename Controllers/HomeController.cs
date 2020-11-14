using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShortenerUrl.EntityFramework;
using Microsoft.AspNetCore.Hosting;
using ShortenerUrl.Models;

namespace ShortenerUrl.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShortenerContext _context;
        private readonly IWebHostEnvironment _environment;

        public HomeController(ShortenerContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View(_context.Files.ToList());
        }

        [HttpPost]
        public IActionResult Create(FileViewModel fileviewmodel)
        {
            if (fileviewmodel != null)
            {
                string wwrootPath = _environment.WebRootPath;

                var Extension = Path.GetExtension(fileviewmodel.File.FileName);

                var RelativeImagePath = "UploadedFiles/" + fileviewmodel.Name + Extension;

                var AbsImagePath = Path.Combine(wwrootPath, RelativeImagePath);

                using (var filestream = new FileStream(AbsImagePath, FileMode.Create))
                {
                    fileviewmodel.File.CopyTo(filestream);
                }
                FileModel filemodel = new FileModel { Name = fileviewmodel.Name, Extension = Extension, Path = RelativeImagePath };
                _context.Files.Add(filemodel);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        //[HttpPost]
        //public async Task<IActionResult> AddFile(IFormFile file)
        //{
        //    if (file != null)
        //    {
        //        string Path = "/UploadedFiles/" + file.FileName;

        //        using var filestream = new FileStream(_environment.WebRootPath + Path, FileMode.Create);
        //        await file.CopyToAsync(filestream);

        //        FileModel filemodel = new FileModel { Name = file.FileName, Path = Path };
        //        _context.Files.Add(filemodel);
        //        _context.SaveChanges();

        //    }
        //    return RedirectToAction("Index");

        //}

        //[HttpPost]
        //public async Task<IActionResult> AddFile(IFormFileCollection files)
        //{
        //    foreach (var file in files)
        //    {
        //        if (file != null)
        //        {
        //            string Path = "/UploadedFiles" + file.FileName;

        //            using var filestream = new FileStream(_environment.WebRootPath + Path, FileMode.Create);
        //            await file.CopyToAsync(filestream);

        //            FileModel filemodel = new FileModel { Name = file.FileName, Path = Path };
        //            _context.Files.Add(filemodel);
        //            _context.SaveChanges();

        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
    }
}
