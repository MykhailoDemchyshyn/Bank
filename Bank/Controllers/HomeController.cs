using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bank.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult About()
        {
            string temp = User.Identity.Name;
            User user = null;
            using (UserContext db = new UserContext())
            {
                user = db.Users.FirstOrDefault(u => u.Email == temp);

            }
            return View(user);
        }

        [Authorize]
        public ActionResult EditPin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPin(EditPinModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (UserContext db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.CardNumber == model.CardNumber);
                    if (user == null)
                    {
                        ModelState.AddModelError("", "bad card number");
                        return View(model);
                    }
                    else
                    {
                        if (user.PIN == model.OldPIN)
                        {
                            user.PIN = model.NewPIN;
                            db.SaveChanges();
                        }
                        else
                        {
                            ModelState.AddModelError("", "bad old pin");
                            return View(model);
                        }
                    }
                }
            }
            else
            {
                return View(model);
            }
            return Redirect("/Home/Index");
        }

        public ActionResult ForwardMoneyWithIban()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForwardMoneyWithIban(ForwardMoneyWithIbanModel model)
        {
            if (ModelState.IsValid)
            {
                string temp = User.Identity.Name;
                int money = 0;
                User user1 = null;
                User user2 = null;
                using (UserContext db = new UserContext())
                {
                    user1 = db.Users.FirstOrDefault(u => u.Email == temp);
                    user2 = db.Users.FirstOrDefault(u => u.IBAN == model.IBAN);
                    if(user2==null)
                    {
                        ModelState.AddModelError("", "bad IBAN");
                        return View(model);
                    }
                    try
                    {
                        money = Convert.ToInt32(model.Money);
                        if (money > user1.Money)
                        {
                            ModelState.AddModelError("", "to large sum");
                            return View(model);
                        }
                        else
                        {
                            user1.Money -= money;
                            user2.Money += money;
                            db.SaveChanges();
                        }
                    }
                    catch(Exception)
                    {
                        ModelState.AddModelError("", "bad field money");
                        return View(model);
                    }
                    

                }

            }
            else
            {
                return View(model);
            }
            return Redirect("/Home/Index");
        }
        public ActionResult ForwardMoneyWithCard()
        {
            return View();
        }
        
        [Authorize(Roles = "admin,manager")]
        public ActionResult ShowAllPersons()
        {
            using (UserContext db = new UserContext())
            {
                List<User> us = db.Users.ToList();
                return View(us);
            }
            
        }

        [Authorize(Roles = "admin,manager")]
        public ActionResult Details(int id)
        {
            User us;
            Role rl;
            using (UserContext db = new UserContext())
            {
                us = db.Users.FirstOrDefault(u => u.Id == id);
                rl = db.Roles.FirstOrDefault(r => r.Id == us.RoleId);
                
            }
            ViewBag.Role = rl.Name;
            return View(us);
        }

        
        [Authorize(Roles = "admin")]
        public ActionResult EditRole(int id)
        {
            User us;
            Role rl;
            using (UserContext db = new UserContext())
            {
                us = db.Users.FirstOrDefault(u => u.Id == id);
                rl= db.Roles.FirstOrDefault(r => r.Id == us.RoleId);
            }
            ViewBag.FirstName = us.FirstName;
            ViewBag.LastName = us.LastName;
            ViewBag.Email = us.Email;
            ViewBag.Role = rl.Name;
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditRole(string role, int Id)
        {
            EditRoleModel erm = new EditRoleModel();
            erm.PersonId = Id;
            erm.NewRole = role;
            using (UserContext db = new UserContext())
            {
                User us = db.Users.FirstOrDefault(u => u.Id == erm.PersonId);
                int roleId = db.Roles.FirstOrDefault(r => r.Name == erm.NewRole).Id;
                us.RoleId = roleId;
                db.SaveChanges();
            }
            return Redirect("/Home/Index");
        }
    }
}