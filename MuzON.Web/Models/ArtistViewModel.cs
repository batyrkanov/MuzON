using MuzON.Common.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MuzON.Web.Models
{
    public class ArtistViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }
        public Guid CountryId { get; set; }
        public CountryViewModel Country { get; set; }
    }
}