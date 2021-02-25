using Microsoft.EntityFrameworkCore;
using OrganizationProject.Data;
using OrganizationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationProject.Services
{
    public class ConfirmThingUserService : IConfirmThingUserService
    {

        private FinalEventDbContext dbContext;

        public ConfirmThingUserService(FinalEventDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddConfirmThingUser(ConfirmThingUser confirmThingUser)
        {
            dbContext.ConfirmThingUsers.Add(confirmThingUser);
            dbContext.SaveChanges();
        }

        public List<ConfirmThingUser> GetConfirmThingUserByConfirmThingId(int confirmThingId)
        {

            //return dbContext.Users.Where(item => item.ConfirmThingUsers.Any(j => j.ConfirmThingId == confirmThingId)).ToList();

            // var bookStoreDbContext = _context.AuthorBook.Include(a => a.Author).Include(a => a.Book);
            return dbContext.ConfirmThingUsers.Include(a => a.User).Include(a => a.ConfirmThing).Where(j => j.ConfirmThingId == confirmThingId).ToList();
        }

        public List<ConfirmThingUser> GetConfirmThingUsers()
        {
            var confirmThingUsers = dbContext.ConfirmThingUsers.ToList();
            return confirmThingUsers;
        }
    }
}
