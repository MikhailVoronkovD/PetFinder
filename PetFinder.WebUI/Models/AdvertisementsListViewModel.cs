using System.Collections.Generic;
using PetFinder.Domain.Entities;

namespace PetFinder.WebUI.Models
{
    public class AdvertisementsListViewModel
    {
        public IEnumerable<Advertisement> Advertisements { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}