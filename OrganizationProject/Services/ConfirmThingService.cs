using Microsoft.EntityFrameworkCore;
using OrganizationProject.Data;
using OrganizationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationProject.Services
{
    public class ConfirmThingService : IConfirmThingService
    {
        private FinalEventDbContext dbContext;

        public ConfirmThingService(FinalEventDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddConfirmThing(ConfirmThing confirmThing)
        {
            dbContext.ConfirmThings.Add(confirmThing);
            dbContext.SaveChanges();
        }

        public void DeleteConfirmThing(ConfirmThing confirmThing)
        {

            dbContext.ConfirmThings.Remove(confirmThing);
            dbContext.SaveChanges();


        }

        public int EditConfirmThing(ConfirmThing confirmThing)
        {
            dbContext.Entry(confirmThing).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }

        public ConfirmThing GetConfirmThingByConfirmThingUserId(int id)
        {
            //return dbContext.ConfirmThings.Where(item => item.ConfirmThingUsers.Any(j => j.ConfirmThingId == id)).FirstOrDefault();
            return dbContext.ConfirmThings.FirstOrDefault(item => item.ConfirmThingUsers.Any(j => j.ConfirmThingId == id));
        }

        public ConfirmThing GetConfirmThingById(int id)
        {
            return dbContext.ConfirmThings.Find(id);
        }

        public List<ConfirmThing> GetConfirmThingByUserId(int userId)
        {
            return dbContext.ConfirmThings.Where(item => item.ConfirmThingUsers.Any(j => j.UserId == userId)).ToList();


        }

        public List<ConfirmThing> GetConfirmThings()
        {
            var confirmThings = dbContext.ConfirmThings.ToList();
            return confirmThings;
        }

        public int NumOfReq(int id)
        {
            //dbContext.Entry(confirmThing).State = EntityState.Modified;
            //return dbContext.SaveChanges();

            var confirmThings = dbContext.ConfirmThings.FirstOrDefault(item => item.ConfirmThingUsers.Any(j => j.ConfirmThingId == id));

            return confirmThings.NumOfConfReq;
        }

        public string Title(int id)
        {
            var confirmThings = dbContext.ConfirmThings.FirstOrDefault(item => item.ConfirmThingUsers.Any(j => j.ConfirmThingId == id));

            return confirmThings.Title;
        }
    }
}
