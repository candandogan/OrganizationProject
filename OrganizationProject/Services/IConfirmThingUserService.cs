using OrganizationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationProject.Services
{
   public interface IConfirmThingUserService
    {
        public List<ConfirmThingUser> GetConfirmThingUsers();

        public void AddConfirmThingUser(ConfirmThingUser confirmThingUser);

        List<ConfirmThingUser> GetConfirmThingUserByConfirmThingId(int confirmThingId);
    }
}
