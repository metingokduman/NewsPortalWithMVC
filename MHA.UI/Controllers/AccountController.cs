using MHA.Core.Repository.Contract;
using MHA.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MHA.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAppUserRepository _userRepository;
        public AccountController(IAppUserRepository appUserRepository)
        {
            _userRepository = appUserRepository;
        }

        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AppUser appUser)
        {
            var user = _userRepository.GetMany(x => x.EmailAddress == appUser.EmailAddress && x.Password == appUser.Password && x.IsActived == true).SingleOrDefault();
            if (user != null)
            {
                
                    Session["AppUserEmail"] = user.EmailAddress;
                    return RedirectToAction("Index", "Home");
             
            }
            ViewBag.Mesaj = "User not found!";

            return View();
        }
    }
}