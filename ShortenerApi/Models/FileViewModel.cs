using Microsoft.AspNetCore.Http;
using ShortenerApi.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShortenerApi.Models
{
    public class FileViewModel
    {
        [Required(ErrorMessage = "Please write a name of the file")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [MaxFileSize(20 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile File { get; set; }
    }
}
