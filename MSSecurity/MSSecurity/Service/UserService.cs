using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MSSecurity.Models;

namespace MSSecurity.Service
{
    public class UserService : IUserService
    {
        private readonly Data.SecurityContext _context;
        public UserService(IConfiguration configuration)
        {
            _context = new Data.SecurityContext(configuration);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                return await _context.User.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
