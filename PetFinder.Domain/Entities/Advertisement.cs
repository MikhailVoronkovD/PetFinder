using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PetFinder.Domain.Entities
{
    public class Advertisement
    {
        [HiddenInput(DisplayValue = false)]
        public int AdvertisementID { get; set; }

        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Пожалуйста, введите категорию")]
        public string Category { get; set; }

        [Display(Name = "Животное")]
        [Required(ErrorMessage = "Пожалуйста, введите животное")]
        public string Pet { get; set; }

        [Display(Name = "Пол")]
        [Required(ErrorMessage = "Пожалуйста, введите пол")]
        public string Gender { get; set; }

        [Display(Name = "Улица")]
        [Required(ErrorMessage = "Пожалуйста, введите улицу")]
        public string AddressStreet { get; set; }

        [Display(Name = "Дом")]
        [Required(ErrorMessage = "Пожалуйста, введите номер дома")]
        public string AddressHouse { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Пожалуйста, введите краткое описание")]
        [StringLength(maximumLength: 500, ErrorMessage = "Длина, не более 500 символов")]
        public string Description { get; set; }

        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "Пожалуйста, введите номер телефона")]
        public string Phone { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Пожалуйста, введите имя")]
        public string Name { get; set; }

        [Display(Name = "Емейл")]
        [Required(ErrorMessage = "Пожалуйста, введите адрес электронной почты")]
        [EmailAddress(ErrorMessage = "Введите корректный адрес электронной почты")]
        public string Email { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        [Display(Name = "Присылать сообщения о новых объявлениях")]
        public bool SendMessage { get; set; }
    }
}
