﻿using Advanced_Web_Application.Data.Enum;
using Advanced_Web_Application.Models;

namespace Advanced_Web_Application.ViewModels
{
    public class EditRaceViewModel
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public IFormFile Image { get; set; }

        public string? URl { get; set; }

        public string Description { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public RaceCatagory RaceCatagory { get; set; }
    }
}
