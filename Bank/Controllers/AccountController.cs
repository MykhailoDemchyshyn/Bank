using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Bank.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                User user = null;
                using (UserContext db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "not found user with this logi nand password");
                }
            }

            return View(model);
        }
        int id = 0;
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (UserContext db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Email);
                }
                if (user == null)
                {
                    
                    using (UserContext db = new UserContext())
                    {
                        string cardnumber = "";
                        Random random = new Random();
                        for(int i=0;i<16;i++)
                        {
                            cardnumber += random.Next(10).ToString();
                        }
                        id++;
                        string Mfo = "000000";
                        Mfo = db.Banks.FirstOrDefault(b => b.Id == 1).MFO;
                        db.Users.Add(new User { Email = model.Email, Password = model.Password,
                            FirstName =model.FirstName,LastName=model.LastName,
                            DateBorn =model.DateBorn,PhoneNumber=model.PhoneNumber,
                            Money =5000,PIN="1111",CardNumber=cardnumber,BankId=1,RoleId=2,
                            IBAN="UA"+random.Next(10,100).ToString()+Mfo+"000"+cardnumber});
                        db.SaveChanges();
                        
                        user = db.Users.Where(u => u.Email == model.Email && u.Password == model.Password).FirstOrDefault();
                    }
                    
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "user with this data exist");
                }
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}