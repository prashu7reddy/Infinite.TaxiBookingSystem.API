using Infinite.TaxiBookingSystem.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.TaxiBookingSystem.API.Repositories
{
    public class UsersRepository:IUsersRepository
    {
        private readonly ApplicationDbContext _Context;


        public UsersRepository(ApplicationDbContext context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<UserRole>> GetUsers()
        {
            var users = await _Context.UserRoles.ToListAsync();
            return users;
        }
    }
}
