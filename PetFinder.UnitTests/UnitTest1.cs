using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PetFinder.Domain.Abstract;
using PetFinder.Domain.Entities;
using PetFinder.WebUI.Controllers;
using PetFinder.WebUI.Models;
using PetFinder.WebUI.HtmlHelpers;

namespace PetFinder.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // Организация (arrange)
            Mock<IAdvertisementRepository> mock = new Mock<IAdvertisementRepository>();
            mock.Setup(m => m.Advertisements).Returns(new List<Advertisement>
            {
                new Advertisement { AdvertisementID = 1, Pet = "Животное1"},
                new Advertisement { AdvertisementID = 2, Pet = "Животное2"},
                new Advertisement { AdvertisementID = 3, Pet = "Животное3"},
                new Advertisement { AdvertisementID = 4, Pet = "Животное4"},
                new Advertisement { AdvertisementID = 5, Pet = "Животное5"}

            });
            AdvertisementController controller = new AdvertisementController(mock.Object);
            controller.pageSize = 3;

            // Действие (act)
            AdvertisementsListViewModel result = (AdvertisementsListViewModel)controller.List(null, 2).Model;

            // Утверждение (assert)
            List<Advertisement> advertisements = result.Advertisements.ToList();
            Assert.IsTrue(advertisements.Count == 2);
            Assert.AreEqual(advertisements[0].Pet, "Животное2");
            Assert.AreEqual(advertisements[1].Pet, "Животное1");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            // Организация - определение вспомогательного метода HTML - это необходимо
            // для применения расширяющего метода
            HtmlHelper myHelper = null;

            // Организация - создание объекта PagingInfo
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Организация - настройка делегата с помощью лямбда-выражения
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Действие
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Утверждение
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // Организация (arrange)
            Mock<IAdvertisementRepository> mock = new Mock<IAdvertisementRepository>();
            mock.Setup(m => m.Advertisements).Returns(new List<Advertisement>
                {
                    new Advertisement { AdvertisementID = 1, Pet = "Животное1"},
                    new Advertisement { AdvertisementID = 2, Pet = "Животное2"},
                    new Advertisement { AdvertisementID = 3, Pet = "Животное3"},
                    new Advertisement { AdvertisementID = 4, Pet = "Животное4"},
                    new Advertisement { AdvertisementID = 5, Pet = "Животное5"}
                });
            AdvertisementController controller = new AdvertisementController(mock.Object);
            controller.pageSize = 3;

            // Act
            AdvertisementsListViewModel result
                = (AdvertisementsListViewModel)controller.List(null, 2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Filter_Advertisements()
        {
            // Организация (arrange)
            Mock<IAdvertisementRepository> mock = new Mock<IAdvertisementRepository>();
            mock.Setup(m => m.Advertisements).Returns(new List<Advertisement>
            {
                new Advertisement { AdvertisementID = 1, Pet = "Животное1", Category = "Потерялось"},
                new Advertisement { AdvertisementID = 2, Pet = "Животное2", Category = "Нашлось"},
                new Advertisement { AdvertisementID = 3, Pet = "Животное3", Category = "Потерялось"},
                new Advertisement { AdvertisementID = 4, Pet = "Животное4", Category = "Потерялось"},
                new Advertisement { AdvertisementID = 5, Pet = "Животное5", Category = "Нашлось"}
            });
            AdvertisementController controller = new AdvertisementController(mock.Object);
            controller.pageSize = 3;

            // Action
            List<Advertisement> result = ((AdvertisementsListViewModel)controller.List("Потерялось", 1).Model)
                .Advertisements.ToList();

            // Assert
            Assert.AreEqual(result.Count(), 3);
            Assert.IsTrue(result[0].Pet == "Животное4" && result[0].Category == "Потерялось");
            Assert.IsTrue(result[1].Pet == "Животное3" && result[1].Category == "Потерялось");
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            // Организация - создание имитированного хранилища
            Mock<IAdvertisementRepository> mock = new Mock<IAdvertisementRepository>();
            mock.Setup(m => m.Advertisements).Returns(new Advertisement[] 
            {
                new Advertisement { AdvertisementID = 1, Pet = "Животное1", Category="Потерялось"},
                new Advertisement { AdvertisementID = 2, Pet = "Животное2", Category="Нашлось"}
            });

            // Организация - создание контроллера
            NavController target = new NavController(mock.Object);

            // Организация - определение выбранной категории
            string categoryToSelect = "Нашлось";

            // Действие
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            // Утверждение
            Assert.AreEqual(categoryToSelect, result);
        }
    }
}