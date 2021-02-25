using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrganizationProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IronPdf;

namespace OrganizationProject.Controllers
{
    public class ConfirmThingUserController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private IAdminService adminService;
        private IUserService userService;
        private IConfirmThingService confirmThingService;
        private IConfirmThingUserService confirmThingUserService;

        public ConfirmThingUserController(ILogger<AdminController> logger, IAdminService adminService, IUserService userService, IConfirmThingService confirmThingService, IConfirmThingUserService confirmThingUserService)
        {
            _logger = logger;
            this.adminService = adminService;
            this.userService = userService;
            this.confirmThingService = confirmThingService;
            this.confirmThingUserService = confirmThingUserService;

        }




        //Lists the participants' information, vote and notes.
        public IActionResult Index(int id = 0)
        {
            var confirmThingUsers = id == 0 ? confirmThingUserService.GetConfirmThingUsers() : confirmThingUserService.GetConfirmThingUserByConfirmThingId(id);
            return View(confirmThingUsers);
        }

        public IActionResult DonePdf(int id)
        {
            string title = confirmThingService.Title(id);
            var Renderer = new IronPdf.HtmlToPdf();
            var PDF = Renderer.RenderUrlAsPdf("https://localhost:44368/ConfirmThingUser/Index/" + id.ToString());
            var OutputPath = title + ".pdf";
            PDF.SaveAs(OutputPath);

            return View();
        }


    }
}
