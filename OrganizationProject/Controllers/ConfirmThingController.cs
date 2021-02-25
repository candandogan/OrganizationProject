using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrganizationProject.Models;
using OrganizationProject.Services;
using SautinSoft.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationProject.Controllers
{
    [Authorize]
    public class ConfirmThingController : Controller
    {
        private readonly ILogger<ConfirmThingController> _logger;
        private IAdminService adminService;
        private IUserService userService;
        private IConfirmThingService confirmThingService;
        private IConfirmThingUserService confirmThingUserService;

        public ConfirmThingController(ILogger<ConfirmThingController> logger, IAdminService adminService, IUserService userService, IConfirmThingService confirmThingService, IConfirmThingUserService confirmThingUserService)
        {
            _logger = logger;
            this.adminService = adminService;
            this.userService = userService;
            this.confirmThingService = confirmThingService;
            this.confirmThingUserService = confirmThingUserService;

        }
        public IActionResult Index()
        {
            var confirmThings = confirmThingService.GetConfirmThings();
            return View(confirmThings);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]

        public IActionResult Create(ConfirmThing confirmThing)
        {
            if (ModelState.IsValid)
            {
                confirmThingService.AddConfirmThing(confirmThing);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public IActionResult Edit(int id)
        {
            var existingConThing = confirmThingService.GetConfirmThingById(id);
            if (existingConThing == null)
            {
                return NotFound();
            }

            return View(existingConThing);
        }


        [HttpPost]
        public IActionResult Edit(ConfirmThing confirmThing)
        {
            if (ModelState.IsValid)
            {
                confirmThingService.EditConfirmThing(confirmThing);
                return RedirectToAction(nameof(Index));
            }
            return NotFound();

        }

        public IActionResult Delete(int id)
        {
            var deletingConfirmThing = confirmThingService.GetConfirmThingById(id);


            return View(deletingConfirmThing);


        }

        [HttpPost]
        public IActionResult Delete(ConfirmThing confirmThing)
        {
            confirmThingService.DeleteConfirmThing(confirmThing);
            return RedirectToAction(nameof(Index));
        }

        
        //Lists the personal information of the participants who will attend the event.
        public IActionResult ListUsers(int id=0)
        {
            var users = id == 0 ? userService.GetUsers() : userService.GetUsersByConfirmThingId(id);
            

                return View(users);
        }

       

    }
}
