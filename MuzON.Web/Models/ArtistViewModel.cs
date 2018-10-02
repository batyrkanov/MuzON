using MuzON.Common.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MuzON.Web.Models
{
    public class ArtistViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }
        public Guid CountryId { get; set; }
        //public CountryViewModel Country { get; set; }
    }
}