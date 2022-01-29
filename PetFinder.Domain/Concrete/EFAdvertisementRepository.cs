using System.Collections.Generic;
using PetFinder.Domain.Abstract;
using PetFinder.Domain.Entities;

namespace PetFinder.Domain.Concrete
{
    public class EFAdvertisementRepository : IAdvertisementRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Advertisement> Advertisements
        {
            get { return context.Advertisements; }
        }

        public void SaveAdvertisement(Advertisement advertisement)
        {
            context.Advertisements.Add(advertisement);
            context.SaveChanges();
        }

        public void EditAdvertisement(Advertisement advertisement)
        {
            if (advertisement.AdvertisementID == 0)
                context.Advertisements.Add(advertisement);
            else
            {
                Advertisement dbEntry = context.Advertisements.Find(advertisement.AdvertisementID);
                if (dbEntry != null)
                {
                    dbEntry.Category = advertisement.Category;
                    dbEntry.Pet = advertisement.Pet;
                    dbEntry.Gender = advertisement.Gender;
                    dbEntry.AddressStreet = advertisement.AddressStreet;
                    dbEntry.AddressHouse = advertisement.AddressHouse;
                    dbEntry.Description = advertisement.Description;
                    dbEntry.Phone = advertisement.Phone;
                    dbEntry.ImageData = advertisement.ImageData;
                    dbEntry.ImageMimeType = advertisement.ImageMimeType;
                    dbEntry.Name = advertisement.Name;
                    dbEntry.Email = advertisement.Email;
                    dbEntry.SendMessage = advertisement.SendMessage;
                }
            }
            context.SaveChanges();
        }

        public Advertisement DeleteAdvertisement(int advertisementId)
        {
            Advertisement dbEntry = context.Advertisements.Find(advertisementId);
            if (dbEntry != null)
            {
                context.Advertisements.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
