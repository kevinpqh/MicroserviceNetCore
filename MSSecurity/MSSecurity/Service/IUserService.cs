using MSSecurity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSSecurity.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
    }
}
