using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrganizationProject.Models;
using OrganizationProject.Services;
using SautinSoft.Document;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using PdfDocument = Syncfusion.Pdf.PdfDocument;

namespace OrganizationProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IAdminService adminService;
        private IUserService userService;
        private IConfirmThingService confirmThingService;
        private IConfirmThingUserService confirmThingUserService;
        private IHostingEnvironment _hostingEnvironment;

        public HomeController(ILogger<HomeController> logger, IAdminService adminService, IUserService userService, IConfirmThingService confirmThingService, IConfirmThingUserService confirmThingUserService, IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            this.adminService = adminService;
            this.userService = userService;
            this.confirmThingService = confirmThingService;
            this.confirmThingUserService = confirmThingUserService;
            this._hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            var confirmThings = confirmThingService.GetConfirmThings();
            return View(confirmThings);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        

        
        public IActionResult Thanx()
        {

            return View();
        }
       
    }
}
