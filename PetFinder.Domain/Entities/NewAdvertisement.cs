using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PetFinder.Domain.Entities
{
    public class NewAdvertisement
    {
        public int AdvertisementID { get; set; }
        public string Category { get; set; }
        public string Pet { get; set; }
        public string Gender { get; set; }
        public string AddressStreet { get; set; }
        public string AddressHouse { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        public bool SendMessage { get; set; }
    }
}
