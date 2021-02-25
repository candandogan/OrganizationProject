using Microsoft.EntityFrameworkCore;
using OrganizationProject.Data;
using OrganizationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationProject.Services
{
    public class AdminService :IAdminService
    {
        private FinalEventDbContext dbContext;

        public AdminService(FinalEventDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddAdmin(Admin admin)
        {
            dbContext.Admins.Add(admin);
            dbContext.SaveChanges();
        }

        public void DeleteAdmin(Admin admin)
        {
            dbContext.Admins.Remove(admin);
            dbContext.SaveChanges();
        }

        public int EditAdmin(Admin admin)
        {
            dbContext.Entry(admin).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }

        public Admin GetAdminById(int id)
        {
            return dbContext.Admins.Find(id);
        }

        public List<Admin> GetAdmins()
        {
            var admins = dbContext.Admins.ToList();
            return admins;
        }

        public Admin ValidAdmin(string email, string password)
        {
            
            return dbContext.Admins.FirstOrDefault(e => e.Email == email && e.Password == password);
        }
    }
}
