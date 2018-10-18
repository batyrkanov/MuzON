using Microsoft.AspNet.Identity;
using MuzON.BLL.DTO;
using MuzON.BLL.Infrastructure;
using MuzON.BLL.Interfaces;
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

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDTO)
        {
            ClaimsIdentity claim = null;
            // find user 
            User user = await _unitOfWork.ApplicationUserManager
                                            .FindAsync(userDTO.Email, userDTO.Password);
            // authorize him and return claims identity obj
            if (user != null)
                claim = await _unitOfWork.ApplicationUserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public async Task<OperationDetails> Create(UserDTO userDTO)
        {
            User user = await _unitOfWork.ApplicationUserManager.FindByEmailAsync(userDTO.Email);
            if (user == null)
            {
                user = new User { Email = userDTO.Email, UserName = userDTO.Email };
                var result =
                    await _unitOfWork.ApplicationUserManager.CreateAsync(user, userDTO.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                //fill userRoles table
                await _unitOfWork.ApplicationUserManager.AddToRoleAsync(user.Id, userDTO.Role);
                await _unitOfWork.SaveAsync();
                return new OperationDetails(true, "Registration succesfull", "");
            }
            else
            {
                return new OperationDetails(false, "User with this Email already exists, please enter other Email", "Email");
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public User GetUserById(Guid id)
        {
            User user = _unitOfWork.ApplicationUserManager.FindById(id);
            return user;
        }
    }
}
