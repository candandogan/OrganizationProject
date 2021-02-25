using Microsoft.EntityFrameworkCore;
using OrganizationProject.Data;
using OrganizationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationProject.Services
{
    public class UserService : IUserService
    {
        private FinalEventDbContext dbContext;

        public UserService(FinalEventDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<User> GetUsers()
        {
            var users = dbContext.Users.ToList();
            return users;
        }

        public void AddUser(User user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

        public int EditUser(User user)
        {
            dbContext.Entry(user).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }

        public User GetUserById(int id)
        {
            return dbContext.Users.Find(id);
        }

        public List<User> GetUsersByConfirmThingId(int confirmThingId)
        {

            return dbContext.Users.Where(item => item.ConfirmThingUsers.Any(j => j.ConfirmThingId == confirmThingId && j.Vote==true)).ToList();

        }

        public User ValidUser(string email, string password)
        {
            return dbContext.Users.FirstOrDefault(e => e.Email == email && e.Password == password);
        }

    }
}
