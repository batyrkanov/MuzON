using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using MuzON.BLL.DTO;
using MuzON.BLL.Infrastructure;
using MuzON.BLL.Interfaces;
using MuzON.DAL.EF;
using MuzON.DAL.Identity;
using MuzON.Domain.Identity;
using MuzON.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public async Task Save()
        {
            await _unitOfWork.SaveAsync();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
        
        public UserDTO GetUserById(Guid id)
        {
            User user = _unitOfWork.Users.Get(id);
            var userDTO = Mapper.Map<UserDTO>(user);
            userDTO.Role = _unitOfWork.Roles.Get(user.Roles.Select(x=>x.RoleId).Single()).Name;
            return userDTO;
        }
        
        public IEnumerable<UserDTO> GetUsers()
        {
            return Mapper.Map<IEnumerable<UserDTO>>(_unitOfWork.Users.GetAll().ToList());
        }

        public void DeleteUser(Guid id)
        {
            var user = _unitOfWork.Users.Get(id);
            user.Roles.Clear();
            _unitOfWork.Users.Delete(user.Id);
            _unitOfWork.Save();
        }

        public IEnumerable<RoleDTO> GetRoles()
        {
            var roles = _unitOfWork.Roles.GetAll().ToList();
            return Mapper.Map<IEnumerable<RoleDTO>>(roles);
        }
    }
}
