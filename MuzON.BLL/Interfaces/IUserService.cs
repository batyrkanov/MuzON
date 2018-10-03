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
        Task<OperationDetails> Create(UserDTO userDTO);
        User GetUserById(Guid id);
        Task<ClaimsIdentity> Authenticate(UserDTO userDTO);
    }
}
