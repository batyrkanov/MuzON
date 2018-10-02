using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.Common.Common
{
    public class DateTimeConverter : IsoDateTimeConverter
    {
        public DateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
