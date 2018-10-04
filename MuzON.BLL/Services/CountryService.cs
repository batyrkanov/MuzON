using AutoMapper;
using MuzON.BLL.DTO;
using MuzON.BLL.Interfaces;
using MuzON.Domain.Entities;
using MuzON.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.BLL.Services
{
    public class CountryService : ICountryService
    {
        private IUnitOfWork _unitOfWork;

        public CountryService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }
        
        public IEnumerable<CountryDTO> GetCountries()
        {
            var countries = _unitOfWork.Countries.GetAll().ToList();
            return Mapper.Map<IEnumerable<Country>, IEnumerable<CountryDTO>>(countries);
        }

        public CountryDTO GetCountryById(Guid Id)
        {
            Country country = _unitOfWork.Countries.Get(Id);
            return Mapper.Map<CountryDTO>(country);
        }
    }
}
