using ProjectTaskTracker.API.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
   public interface IUserService
    {
        Task<string> Authenticate(string email, string password);
        Task<IEnumerable<UserDTO>> GetAllDevelopers();

    }

}
