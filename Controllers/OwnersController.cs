using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CatSalon.Models.DataAccess;

namespace CatSalon.Controllers
{
    public class OwnersController : Controller
    {
        //displays the modelstate error messages to the console
        public void LogModelStateErrors()
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var i in errors)
            {
                Console.WriteLine(i.ErrorMessage);
            }
        }

        private readonly CatSalonContext _context;

        public OwnersController(CatSalonContext context)
        {
            _context = context;
        }

        #region Create Actions

        // GET: Owners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Owners/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Owner owner)
        {
            // Check for duplicate email in database 
            if(_context.Owners.Any(o => o.Email.Equals(owner.Email)))
            {
                ModelState.AddModelError("EmailDataBase", "This Email is already registered.");
            }

            // Check for duplicate phone number in database
            if (_context.Owners.Any(o => o.Phone.Equals(owner.Phone)))
            {
                ModelState.AddModelError("PhoneDataBase", "This Phone number is already registered.");
            }

            LogModelStateErrors();
            if (ModelState.IsValid)
            {
                _context.Add(owner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Dashboard));
            }
            return View(owner);
        }
        #endregion

        #region Login Actions

        // GET: Owners/Login
        public IActionResult Login()
        {
            if(HttpContext.Session.GetString("userId") != null)
            {
                return RedirectToAction(nameof(Dashboard));
            }

            return View();
        }

        // POST: Owners/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            //Handle Null Arguments
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return NotFound();
            }

            #region Password Validation
            //Check DB to validate Email And Passwords existance
            var passwords = (from o in _context.Owners select o.Password).ToList();

            var passwordExists = false;
            //compare password strings for each password (so the case is acutally considered)
            foreach (var p in passwords)
            {
                //Compare strings
                passwordExists = (p == password);
                
                // break to preserve exists value (incase password is found before end of passwords list)
                if (passwordExists)
                {
                    break;
                }
            }
            #endregion

            #region Email Validation
            //Check DB to validate Email And Passwords existance
            var emails = (from o in _context.Owners select o.Email).ToList();

            var emailExists = false;
            
            //compare password strings for each password (so the case is acutally considered)
            foreach (var e in emails)
            {
                //Compare strings
                emailExists = (e == email);

                // break to preserve exists value (incase password is found before end of passwords list)
                if (emailExists)
                {
                    break;
                }
            }
            #endregion

            //Set ModelState error 
            if (!emailExists || !passwordExists)
            {
                ModelState.AddModelError("Login", "Incorrect Email or Password");
                return View();
            }

            if (ModelState.IsValid)
            {
                Owner owner = await _context.Owners.Where(m => m.Email == email && m.Password == password).FirstAsync();

                //Add user Id to Session
                HttpContext.Session.SetString("userId", owner.Id.ToString());

                //Redirect to the user's Dashboard page
                int id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
                return RedirectToAction(nameof(Dashboard), id);
            }
            return View();
        }
        #endregion

        #region Edit Actions
        // GET: Owners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Owners == null)
            {
                return NotFound();
            }

            var owner = await _context.Owners.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }
            return View(owner);
        }

        // POST: Owners/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Owner owner)
        {
            if (id != owner.Id)
            {
                return NotFound();
            }

            //Check if email is edited -> if so: validate for duplicates 
            if (!owner.Email.Equals(_context.Owners.Find(id).Email))
            {            
                // Check for duplicate email in database 
                if (_context.Owners.Any(o => o.Email.Equals(owner.Email)))
                {
                    ModelState.AddModelError("EmailDataBase", "This Email is already registered.");
                }
            }

            //Check if phone number is edited -> if so: validate for duplicates 
            if (!owner.Phone.Equals(_context.Owners.Find(id).Phone))
            {
                // Check for duplicate phone number in database
                if (_context.Owners.Any(o => o.Phone.Equals(owner.Phone)))
                {
                    ModelState.AddModelError("PhoneDataBase", "This Phone number is already registered.");
                }
            }

            LogModelStateErrors();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.ChangeTracker.Clear();
                    _context.Update(owner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OwnerExists(owner.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Dashboard));
            }
            return View(owner);
        }
        #endregion

        #region Delete Actions
        // GET: Owners/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Owners == null)
            {
                return NotFound();
            }

            var owner = await _context.Owners
                .FirstOrDefaultAsync(m => m.Id == id);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // POST: Owners/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Owners == null)
            {
                return Problem("Entity set 'CatSalonContext.Owners' is null.");
            }
            var owner = await _context.Owners.FindAsync(id);
            if (owner != null)
            {
                _context.Owners.Remove(owner);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home",new { logout = true, area = "Home" });
        }
        #endregion

        // GET: Owner/Dashboard (index renamed)
        public async Task<IActionResult> Dashboard()
        {
            if (HttpContext.Session.GetString("userId") != null)
            {
                var owner = await _context.Owners
                    .FirstOrDefaultAsync(m => m.Id == Convert.ToInt32(HttpContext.Session.GetString("userId")));

                if (owner.Cat.Count > 0)
                {
                    List<Appointment> apps = (from Appointment a in _context.Appointments
                               join c in _context.Cats on a.CatId equals c.Id
                               where c.OwnerId == owner.Id && a.CatId == c.Id
                               select a).ToList<Appointment>();

                    ViewData["Appointments"] = apps;
                               
                }


                return View(owner);
            }
            return RedirectToAction(nameof(Login));
        }

      
        private bool OwnerExists(int id)
        {
          return (_context.Owners?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
