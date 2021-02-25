using OrganizationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationProject.Services
{
   public interface IUserService
    {
        public List<User> GetUsers();

        List<User> GetUsersByConfirmThingId(int confirmThingId);

        public void AddUser(User user);
        public User GetUserById(int id);
        public int EditUser(User user);

        User ValidUser(string email, string password);
    }
}
