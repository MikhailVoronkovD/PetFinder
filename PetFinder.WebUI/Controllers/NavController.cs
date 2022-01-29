using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetFinder.Domain.Abstract;

namespace PetFinder.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IAdvertisementRepository repository;

        public NavController(IAdvertisementRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = repository.Advertisements
                .Select(Advertisement => Advertisement.Category)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}