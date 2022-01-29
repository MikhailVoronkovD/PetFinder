using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PetFinder.Domain.Abstract;
using PetFinder.Domain.Entities;
using PetFinder.WebUI.Controllers;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

namespace PetFinder.UnitTests
{
    [TestClass]
    public class ImageTest
    {
        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {
            // Организация - создание объекта Advertisement с данными изображения
            Advertisement advertisement = new Advertisement
            {
                AdvertisementID = 2,
                Pet = "Животное2",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };

            // Организация - создание имитированного хранилища
            Mock<IAdvertisementRepository> mock = new Mock<IAdvertisementRepository>();
            mock.Setup(m => m.Advertisements).Returns(new List<Advertisement> {
                new Advertisement {AdvertisementID = 1, Pet = "Животное1"},
                advertisement,
                new Advertisement {AdvertisementID = 3, Pet = "Животное3"}
            }.AsQueryable());

            // Организация - создание контроллера
            AdvertisementController controller = new AdvertisementController(mock.Object);

            // Действие - вызов метода действия GetImage()
            ActionResult result = controller.GetImage(2);

            // Утверждение
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(advertisement.ImageMimeType, ((FileResult)result).ContentType);
        }

        [TestMethod]
        public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
        {
            // Организация - создание имитированного хранилища
            Mock<IAdvertisementRepository> mock = new Mock<IAdvertisementRepository>();
            mock.Setup(m => m.Advertisements).Returns(new List<Advertisement> {
                new Advertisement {AdvertisementID = 1, Pet = "Животное1"},
                new Advertisement {AdvertisementID = 2, Pet = "Животное2"}
            }.AsQueryable());

            // Организация - создание контроллера
            AdvertisementController controller = new AdvertisementController(mock.Object);

            // Действие - вызов метода действия GetImage()
            ActionResult result = controller.GetImage(10);

            // Утверждение
            Assert.IsNull(result);
        }
    }
}