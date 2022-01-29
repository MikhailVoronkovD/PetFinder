using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PetFinder.Domain.Abstract;
using PetFinder.Domain.Entities;
using PetFinder.WebUI.Controllers;
using System.Linq;
using System.Web.Mvc;

namespace PetFinder.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Advertisements()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IAdvertisementRepository> mock = new Mock<IAdvertisementRepository>();
            mock.Setup(m => m.Advertisements).Returns(new List<Advertisement>
            {
                new Advertisement { AdvertisementID = 1, Pet = "Животное1"},
                new Advertisement { AdvertisementID = 2, Pet = "Животное2"},
                new Advertisement { AdvertisementID = 3, Pet = "Животное3"},
                new Advertisement { AdvertisementID = 4, Pet = "Животное4"},
                new Advertisement { AdvertisementID = 5, Pet = "Животное5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            List<Advertisement> result = ((IEnumerable<Advertisement>)controller.Index().
                ViewData.Model).ToList();

            // Утверждение
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual("Животное1", result[0].Pet);
            Assert.AreEqual("Животное2", result[1].Pet);
            Assert.AreEqual("Животное3", result[2].Pet);
        }

        [TestMethod]
        public void Can_Edit_Advertisement()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IAdvertisementRepository> mock = new Mock<IAdvertisementRepository>();
            mock.Setup(m => m.Advertisements).Returns(new List<Advertisement>
            {
                new Advertisement { AdvertisementID = 1, Pet = "Животное1"},
                new Advertisement { AdvertisementID = 2, Pet = "Животное2"},
                new Advertisement { AdvertisementID = 3, Pet = "Животное3"},
                new Advertisement { AdvertisementID = 4, Pet = "Животное4"},
                new Advertisement { AdvertisementID = 5, Pet = "Животное5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            Advertisement advertisement1 = controller.Edit(1).ViewData.Model as Advertisement;
            Advertisement advertisement2 = controller.Edit(2).ViewData.Model as Advertisement;
            Advertisement advertisement3 = controller.Edit(3).ViewData.Model as Advertisement;

            // Assert
            Assert.AreEqual(1, advertisement1.AdvertisementID);
            Assert.AreEqual(2, advertisement2.AdvertisementID);
            Assert.AreEqual(3, advertisement3.AdvertisementID);
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IAdvertisementRepository> mock = new Mock<IAdvertisementRepository>();

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Организация - создание объекта Advertisement
            Advertisement advertisement = new Advertisement { Description = "Test" };

            // Организация - добавление ошибки в состояние модели
            controller.ModelState.AddModelError("error", "error");

            // Действие - попытка сохранения оьбъявления
            ActionResult result = controller.Edit(advertisement);

            // Утверждение - проверка того, что обращение к хранилищу НЕ производится 
            mock.Verify(m => m.SaveAdvertisement(It.IsAny<Advertisement>()), Times.Never());

            // Утверждение - проверка типа результата метода
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Delete_Valid_Advertisement()
        {
            // Организация - создание объекта Advertisement
            Advertisement advertisement = new Advertisement { AdvertisementID = 2, Pet = "Животное1" };

            // Организация - создание имитированного хранилища данных
            Mock<IAdvertisementRepository> mock = new Mock<IAdvertisementRepository>();
            mock.Setup(m => m.Advertisements).Returns(new List<Advertisement>
            {
                new Advertisement { AdvertisementID = 1, Pet = "Животное1"},
                new Advertisement { AdvertisementID = 2, Pet = "Животное2"},
                new Advertisement { AdvertisementID = 3, Pet = "Животное3"},
                new Advertisement { AdvertisementID = 4, Pet = "Животное4"},
                new Advertisement { AdvertisementID = 5, Pet = "Животное5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие - удаление объявления
            controller.DeleteAdvertisement(advertisement.AdvertisementID);

            // Утверждение - проверка того, что метод удаления в хранилище
            // вызывается для корректного объекта Advertisement
            mock.Verify(m => m.DeleteAdvertisement(advertisement.AdvertisementID));
        }
    }
}