using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetFinder.Domain.Abstract;
using PetFinder.Domain.Entities;

namespace PetFinder.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        IAdvertisementRepository repository;

        public AdminController(IAdvertisementRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Advertisements);
        }

        public ViewResult Edit(int advertisementId)
        {
            Advertisement advertisement = repository.Advertisements.FirstOrDefault(x => x.AdvertisementID == advertisementId);
            return View(advertisement);
        }

        [HttpPost]
        public ActionResult Edit(Advertisement advertisement, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    advertisement.ImageMimeType = image.ContentType;
                    advertisement.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(advertisement.ImageData, 0, image.ContentLength);
                }
                repository.EditAdvertisement(advertisement);
                TempData["message"] = string.Format("Изменения в объявлении были сохранены");
                return RedirectToAction("Index");
            }
            else
            {
                return View(advertisement);
            }
        }

        [HttpPost]
        public ActionResult DeleteAdvertisement(int advertisementId)
        {
            Advertisement deleted = repository.DeleteAdvertisement(advertisementId);
            if (deleted != null)
            {
                TempData["message"] = string.Format("Объявление было удалено");
            }
            return RedirectToAction("Index");
        }
    }
}