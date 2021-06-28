using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sportclub.Database;
using Sportclub.Domain;
using Sportclub.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Sportclub.Database.SporterDatabase;

namespace Sportclub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISporterDatabase sporters;
        private readonly IWebHostEnvironment hostEnvironment;
        public HomeController(ISporterDatabase sporters, IWebHostEnvironment hosting)
        {
            this.sporters = sporters;
            this.hostEnvironment = hosting;
        }
        public IActionResult Index()
        {
            var vm = sporters.GetSporters().Select(x => new ListViewModel
            {
                Id = x.Id,
                Voornaam = x.Voornaam,
                Achternaam = x.Achternaam,
                LengteCm = x.LengteCm,
                GewichtKg = x.GewichtKg,
                Geboortedatum = x.Geboortedatum,
                ImgPath = x.ImgPath,              
            });

            return View(vm);
        }
        public IActionResult Create()
        {
            return View(new CreateViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] CreateViewModel x)
        {
            if (TryValidateModel(x))
            {
                
                var newsporter = new Sporter
                {

                    Voornaam = x.Voornaam,
                    Achternaam = x.Achternaam,
                    LengteCm = x.LengteCm,
                    GewichtKg = x.GewichtKg,
                    Geboortedatum = x.Geboortedatum,
                    Geslacht = x.Geslacht
                };
                if (x.Photo != null)
                {
                    string uniqueFileName = UploadPhoto(x.Photo);
                    newsporter.ImgPath = Path.Combine("/Images", uniqueFileName);

                }
                sporters.Insert(newsporter);

                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Detail([FromRoute] int id)
        {
            var x = sporters.GetSporter(id);
            var vm = new DetailViewModel
            {
                Voornaam = x.Voornaam,
                Achternaam = x.Achternaam,
                LengteCm = x.LengteCm,
                GewichtKg = x.GewichtKg,
                Geboortedatum = x.Geboortedatum,
                ImgPath = x.ImgPath,
                Geslacht = x.Geslacht
            };

            return View(vm);

        }
        public IActionResult Edit([FromRoute] int id)
        {
            var x = sporters.GetSporter(id);
            var vm = new EditViewModel
            {
                Voornaam = x.Voornaam,
                Achternaam = x.Achternaam,
                LengteCm = x.LengteCm,
                GewichtKg = x.GewichtKg,
                Geboortedatum = x.Geboortedatum,
                ImgPath = x.ImgPath,
                Geslacht = x.Geslacht
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, [FromForm] EditViewModel x)
        {
            if (TryValidateModel(x))
            {
                var newSporter = new Sporter
                {
                    Voornaam = x.Voornaam,
                    Achternaam = x.Achternaam,
                    LengteCm = x.LengteCm,
                    GewichtKg = x.GewichtKg,
                    Geboortedatum = x.Geboortedatum,
                    Geslacht = x.Geslacht
                };

                var dbImage = sporters.GetSporter(id);
                if (x.Photo == null)
                {
                    newSporter.ImgPath = dbImage.ImgPath;
                }
                else
                {
                    if (!string.IsNullOrEmpty(dbImage.ImgPath))
                    {
                        DeletePhoto(dbImage.ImgPath);

                    }

                    string uniqueFileName = UploadPhoto(x.Photo);
                    newSporter.ImgPath = Path.Combine("/Images", uniqueFileName);

                }
                sporters.Update(id, newSporter);

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ComfirmDelete([FromRoute] int id, bool useless)
        {
            sporters.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private string UploadPhoto(IFormFile photo)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
            string pathname = Path.Combine(hostEnvironment.WebRootPath, "Images");
            string fileNameWithPath = Path.Combine(pathname, uniqueFileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                photo.CopyTo(stream);

            }
            return uniqueFileName;
        }
        private void DeletePhoto(string imgPath)
        {
            string path = Path.Combine(hostEnvironment.WebRootPath, imgPath.Substring(1));
            System.IO.File.Delete(path);
        }
    }
}
