using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MimeKit;
using PetFinder.Domain.Abstract;
using PetFinder.Domain.Entities;

namespace PetFinder.WebUI.Controllers
{
    public class NewAdvertisementController : Controller
    {
        private IAdvertisementRepository repository;

        public NewAdvertisementController(IAdvertisementRepository repo)
        {
            repository = repo;
        }

        public ViewResult InputForm()
        {
            return View(new NewAdvertisement());
        }

        [HttpPost]
        public ActionResult InputForm(Advertisement advertisement, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    advertisement.ImageMimeType = image.ContentType;
                    advertisement.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(advertisement.ImageData, 0, image.ContentLength);
                }
                repository.SaveAdvertisement(advertisement);

                SendEmail(advertisement);      

                return View("ComleteInput");
            }

            else
            {
                return View(new NewAdvertisement());
            }
        }

        public void SendEmail(Advertisement emailInfo)
        {
            List<Advertisement> advertisements = repository.Advertisements.ToList();

            StringBuilder body = new StringBuilder()
                .AppendLine("Здравствуйте!")
                .AppendLine("На сайте добавили новое объявление:")
                .AppendLine()
                .AppendLine()
                .Append(emailInfo.Category)
                .AppendLine(":")
                .Append(emailInfo.Pet)
                .Append(" - ")
                .AppendLine(emailInfo.Gender)
                .AppendLine()
                .AppendLine("Адрес:")
                .Append("Улица: ")
                .Append(emailInfo.AddressStreet)
                .Append(", дом: ")
                .AppendLine(emailInfo.AddressHouse)
                .AppendLine()
                .AppendLine("Краткое описание:")
                .AppendLine(emailInfo.Description)
                .AppendLine()
                .Append("Телефон для связи: ")
                .Append(emailInfo.Phone)
                .Append(", ")
                .Append(emailInfo.Name);
            

            var builder = new BodyBuilder();
            builder.TextBody = body.ToString();

            foreach (var email in advertisements)
            {
                if (email.SendMessage == true)
                {
                    MimeMessage message = new MimeMessage();
                    message.From.Add(new MailboxAddress("PetFinder", "PetFinderAssistant@gmail.com"));
                    message.To.Add(new MailboxAddress($"{email.Name}", email.Email));
                    message.Subject = "Новое объявление на сайте PetFinder";
                    message.Body = builder.ToMessageBody();

                    using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 465, true);
                        client.Authenticate("PetFinderAssistant@gmail.com", "PetFinder_2021");
                        client.Send(message);

                        client.Disconnect(true);
                    }
                }
            }
        }
    }
}