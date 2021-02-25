using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MimeKit;
using OrganizationProject.Models;
using OrganizationProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OrganizationProject.Controllers
{
   [Authorize]
    public class AdminController : Controller
    {
        
            private readonly ILogger<AdminController> _logger;
            private IAdminService adminService;
            private IUserService userService;
            private IConfirmThingService confirmThingService;
            private IConfirmThingUserService confirmThingUserService;

            public AdminController(ILogger<AdminController> logger, IAdminService adminService, IUserService userService, IConfirmThingService confirmThingService, IConfirmThingUserService confirmThingUserService)
            {
                _logger = logger;
                this.adminService = adminService;
                this.userService = userService;
                this.confirmThingService = confirmThingService;
                this.confirmThingUserService = confirmThingUserService;

            }
        public IActionResult Index()
        {
            var admins = adminService.GetAdmins();
            return View(admins);
        }

       [AllowAnonymous] //It can be used to allow unauthenticated users to access individual actions.
        public IActionResult Login(string returnUrl)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }


        [AllowAnonymous]
        [HttpPost]
            public async Task<IActionResult> Login(Admin admin, string returnUrl)
            {
                var admins = adminService.ValidAdmin(admin.Email, admin.Password);
                if (admins != null)
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Email, admin.Email));
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Admin");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync("Admin", claimsPrincipal);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return Redirect("/");
                }

            string failPosta = admin.Email;
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Bilgilendirme", "example@gmail.com"));
            message.To.Add(new MailboxAddress("naren", "example@gmail.com"));
            message.Subject = "Başarısız Admin Girişi";
            message.Body = new TextPart("plain")
            {
                Text = failPosta + " tarafından başarısız admin girişi yapıldı."
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("example@gmail.com", "example");
                client.Send(message);
                client.Disconnect(true);
            }
            ModelState.AddModelError("Hata", "Kullanıcı adı ya da şifre yanlış.");
                return View();
            }

        
        public async Task<IActionResult> LogOut()
            {
                await HttpContext.SignOutAsync();
                return Redirect("/");
            }

        public IActionResult Edit(int id)
        {
            var existingAdmin = adminService.GetAdminById(id);
            if (existingAdmin == null)
            {
                return NotFound();
            }

            return View(existingAdmin);
        }


        [HttpPost]
        public IActionResult Edit(Admin admin)
        {
            if (ModelState.IsValid)
            {
                adminService.EditAdmin(admin);
                return RedirectToAction(nameof(Index));
            }
            return NotFound();

        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]

        public IActionResult Create(Admin admin)
        {
            if (ModelState.IsValid)
            {
                var admins = adminService.GetAdmins();
                foreach (var item in admins)
                {
                    if (item.Email==admin.Email) //If there is an admin registered with the same e-mail, it will fail.
                    {
                        return RedirectToAction(nameof(Sorry));
                    }
                }
                adminService.AddAdmin(admin);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public IActionResult Sorry()
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            var deletingAdmin = adminService.GetAdminById(id);


            return View(deletingAdmin);


        }

        [HttpPost]
        public IActionResult Delete(Admin admin)
        {
            adminService.DeleteAdmin(admin);
            return RedirectToAction(nameof(Index));
        }
    }
}

