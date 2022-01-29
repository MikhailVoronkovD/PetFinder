using System.Linq;
using System.Web.Mvc;
using PetFinder.Domain.Abstract;
using PetFinder.Domain.Entities;
using PetFinder.WebUI.Models;

namespace PetFinder.WebUI.Controllers
{
    public class AdvertisementController : Controller
    {
        private IAdvertisementRepository repository;
        public int pageSize = 5;

        public AdvertisementController(IAdvertisementRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int page = 1)
        {
            AdvertisementsListViewModel model = new AdvertisementsListViewModel
            {
                Advertisements = repository.Advertisements
                .Where(p => category == null || p.Category == category)
                .OrderByDescending(Advertisement => Advertisement.AdvertisementID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                    repository.Advertisements.Count() :
                    repository.Advertisements.Where(Advertisement => Advertisement.Category == category).Count()
                },
                CurrentCategory = category
            };

            return View(model);
        }

        public FileContentResult GetImage(int advertisementId)
        {
            Advertisement advertisement = repository.Advertisements
                .FirstOrDefault(g => g.AdvertisementID == advertisementId);

            if (advertisement != null)
            {
                return File(advertisement.ImageData, advertisement.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}