using Microsoft.AspNet.Identity;
using MuzON.BLL.DTO;
using MuzON.BLL.Infrastructure;
using MuzON.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.BLL.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetUsers();
        Task<OperationDetails> Create(UserDTO userDTO);
        UserDTO GetUserById(Guid id);
        Task<User> GetUserByNameAsync(string userName);
        Task<ClaimsIdentity> Authenticate(UserDTO userDTO);
        Task<string> GeneratePasswordResetTokenAsync(Guid id);
        Task<IdentityResult> ResetPasswordAsync(Guid id, string code, string password);
        void DeleteUser(Guid id);
    }
}
