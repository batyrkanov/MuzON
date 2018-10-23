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
                // if role "user" wasn't find in the table Roles, then create this
                Role role = await _unitOfWork.ApplicationRoleManager.FindByNameAsync(userDTO.Role);
                if(role == null)
                {
                    role = new Role { Id = Guid.NewGuid(), Name = userDTO.Role };
                    var roleAddResult =
                        await _unitOfWork.ApplicationRoleManager.CreateAsync(role);
                    if (roleAddResult.Errors.Count() > 0)
                        return new OperationDetails(false, roleAddResult.Errors.FirstOrDefault(), "");
                }
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

        public async Task<string> GeneratePasswordResetTokenAsync(Guid id)
        {
            return await _unitOfWork.ApplicationUserManager.GeneratePasswordResetTokenAsync(id);
        }

        public UserDTO GetUserById(Guid id)
        {
            User user = _unitOfWork.ApplicationUserManager.FindById(id);
            return Mapper.Map<UserDTO>(user);
        }

        public async Task<User> GetUserByNameAsync(string userName)
        {
            User user = await _unitOfWork.ApplicationUserManager.FindByNameAsync(userName);
            return user;
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            return Mapper.Map<IEnumerable<UserDTO>>(_unitOfWork.ApplicationUserManager.Users.ToList());
        }

        public async Task<IdentityResult> ResetPasswordAsync(Guid id, string code, string password)
        {
            var result = await _unitOfWork.ApplicationUserManager.ResetPasswordAsync(id, code, password);
            return result;
        }

        public void DeleteUser(Guid id)
        {
            var user = _unitOfWork.ApplicationUserManager.FindById(id);
            _unitOfWork.ApplicationUserManager.Delete(user);
            _unitOfWork.Save();
        }
    }
}
