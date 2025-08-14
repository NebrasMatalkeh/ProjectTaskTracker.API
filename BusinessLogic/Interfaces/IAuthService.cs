using ProjectTaskTracker.API.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IAuthService
    {
       
        Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO);
        Task<UserDTO> RegisterDeveloper(RegisterDTO registerDTO, int managerId);
    }
}
