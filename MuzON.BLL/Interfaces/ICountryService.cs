using MuzON.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.BLL.Interfaces
{
    public interface ICountryService
    {
        IEnumerable<CountryDTO> GetCountries();
    }
}
