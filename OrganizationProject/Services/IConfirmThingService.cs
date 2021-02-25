using OrganizationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationProject.Services
{
   public interface IConfirmThingService
    {
        public List<ConfirmThing> GetConfirmThings();
        List<ConfirmThing> GetConfirmThingByUserId(int userId);
        public void AddConfirmThing(ConfirmThing confirmThing);
        public ConfirmThing GetConfirmThingById(int id);
        public int EditConfirmThing(ConfirmThing confirmThing);

        public int NumOfReq(int id);
        public string Title(int id);
        public void DeleteConfirmThing(ConfirmThing confirmThing);

        public ConfirmThing GetConfirmThingByConfirmThingUserId(int id);
    }
}
