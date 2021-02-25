using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MimeKit;
using OrganizationProject.Models;
using OrganizationProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using MailKit.Net.Smtp;

namespace OrganizationProject.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private IAdminService adminService;
        private IUserService userService;
        private IConfirmThingService confirmThingService;
        private IConfirmThingUserService confirmThingUserService;

        public UserController(ILogger<UserController> logger, IAdminService adminService, IUserService userService, IConfirmThingService confirmThingService, IConfirmThingUserService confirmThingUserService)
        {
            _logger = logger;
            this.adminService = adminService;
            this.userService = userService;
            this.confirmThingService = confirmThingService;
            this.confirmThingUserService = confirmThingUserService;

        }
        public IActionResult Index()
        {
            return View();
        }


        
        //to create a new user
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(User user)
        {
            var users = userService.GetUsers();
            string u1 = user.Email;
            if (ModelState.IsValid)
            {
                foreach (var i1 in users)
                {
                    if (i1.Email == u1)
                    {
                        //If you have registered with the same e-mail before, it gives an error.
                            return RedirectToAction(nameof(SorryEmail));
                        
                    }
                }

                userService.AddUser(user);


                //Redirects to organization registration page.

                return RedirectToAction(nameof(Join));
            }

            return View();
        }

        public IActionResult SorryEmail()
        {
            return View();
        }
        public IActionResult Join(int id)
        {
            List<SelectListItem> selectListUsers = getUsersForSelect();
            ViewBag.Users = selectListUsers;
            List<SelectListItem> selectListConfirmThings = getConfirmThingsForSelect();
            ViewBag.ConfirmThings = selectListConfirmThings;
            return View();
        }

        [HttpPost]
        public IActionResult Join(ConfirmThingUser confirmThingUser)
        {
            var confirmThingUsers = confirmThingUserService.GetConfirmThingUsers();
            if (ModelState.IsValid)
            {
                int count1 = 0;
                int id1 = confirmThingUser.ConfirmThingId;
                int id2 = confirmThingUser.UserId;
                foreach ( var i1 in confirmThingUsers)
                {
                    if (i1.ConfirmThingId==id1)
                    {
                        
                        if (i1.UserId == id2)
                        {
                            //If the user has voted for this event before, it gives an error.
                            return RedirectToAction(nameof(Sorry));
                        }

                        if (i1.Vote == true)
                        {
                           // The number of people who approve 'yes' to the event is kept.
                            count1 += 1;
                        }
                    }
                }
                
                if (confirmThingUser.Vote == true)
                {
                    count1 += 1;

                }

                //  int value = DateTime.Compare(DateTime date1, DateTime date2)
                //if return value is Zero then both date are same i.e. (date1 == date2)
                //if return value is Less than zero then date1 is is earlier than date2 i.e. (date1 < date2) 
                //if return value is Greater than zero then date1 is later than date2 i.e.(date1 > date2)
                

                confirmThingUserService.AddConfirmThingUser(confirmThingUser);
                

                int count3 = confirmThingService.NumOfReq(confirmThingUser.ConfirmThingId);
                string title= confirmThingService.Title(confirmThingUser.ConfirmThingId);
                //If the number of people who approve 'yes' to the event is equal to the minimum number of participants, an email is sent to admine.
                if (count1 == count3)
                    {
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Bilgilendirme", "example@gmail.com"));
                    message.To.Add(new MailboxAddress("naren", "example@gmail.com"));
                    message.Subject = title + " Organizasyonu";
                    message.Body = new TextPart("plain")
                    {
                        Text = title + " organizasyonu için minimum katılımcı sayısını sağlamıştır."
                    };

                    using (var client = new MailKit.Net.Smtp.SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, false);
                        client.Authenticate("example@gmail.com", "example");
                        client.Send(message);
                        client.Disconnect(true);
                    }
                    }
                
                return RedirectToAction(nameof(Thanx));


                
               
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Sorry()
        {
            return View();
        }

        public IActionResult SorryDate()
        {
            return View();
        }
        public IActionResult Thanx()
        {

            return View();
        }
        private List<SelectListItem> getUsersForSelect()
        {
            var users = userService.GetUsers();
            List<SelectListItem> selectListUsers = new List<SelectListItem>();
            users.ToList().ForEach(user => selectListUsers.Add(new SelectListItem { Text = user.Email, Value = user.UserId.ToString() }));
            return selectListUsers;
        }

        private List<SelectListItem> getConfirmThingsForSelect()
        {
           
            var confirmThings1 = confirmThingService.GetConfirmThings();
            
            foreach (var item in confirmThings1.ToList())
            {
                if (DateTime.Compare(item.Deadline, DateTime.Now) < 0)
                {
                   // Events that have passed the deadline are hidden. Thus, the user cannot participate in these events.
                    confirmThings1.Remove(item);


                }
            }

          
            List<SelectListItem> selectListConfirmThings = new List<SelectListItem>();
            confirmThings1.ToList().ForEach(confirmThing => selectListConfirmThings.Add(new SelectListItem { Text = confirmThing.Title, Value = confirmThing.ConfirmThingId.ToString() }));
            return selectListConfirmThings;
        }

        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user, string returnUrl)
        {
            var users = userService.ValidUser(user.Email, user.Password);
            if (users != null)
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Email, user.Email));
                claims.Add(new Claim(ClaimTypes.Role, "User"));

                //ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "User");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync("User", claimsPrincipal);
                //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                //var status = HttpContext.SignInAsync("UsersAuth", new ClaimsPrincipal(principal), properties)IsCompleted;
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return Redirect("/");
            }

            ModelState.AddModelError("Hata", "Kullanıcı adı ya da şifre yanlış.");
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
