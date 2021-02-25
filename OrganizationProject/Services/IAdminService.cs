using OrganizationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationProject.Services
{
    public interface IAdminService
    {
        public List<Admin> GetAdmins();
        public void AddAdmin(Admin admin);
        public Admin GetAdminById(int id);
        public int EditAdmin(Admin admin);
        public void DeleteAdmin(Admin admin);
        Admin ValidAdmin(string email, string password);
    }
}
