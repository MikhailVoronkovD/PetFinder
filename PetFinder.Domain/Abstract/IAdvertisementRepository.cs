using System.Collections.Generic;
using PetFinder.Domain.Entities;


namespace PetFinder.Domain.Abstract
{
    public interface IAdvertisementRepository
    {
        IEnumerable<Advertisement> Advertisements { get; }
        void SaveAdvertisement(Advertisement advertisement);
        void EditAdvertisement(Advertisement advertisement);
        Advertisement DeleteAdvertisement(int advertisementId);
    }
}
