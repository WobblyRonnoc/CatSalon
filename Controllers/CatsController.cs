using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CatSalon.Models.DataAccess;
using CatSalon.Models.ViewModels;

namespace CatSalon.Controllers
{
    public class CatsController : Controller
    {
        private readonly CatSalonContext _context;

        public CatsController(CatSalonContext context)
        {
            _context = context;
        }

        #region Create Actions

        // GET: Cats/Create
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                if (HttpContext.Session.GetString("userId") != null)
                {
                    ViewData["OwnerId"] = Convert.ToInt32(HttpContext.Session.GetString("userId"));
                    return View();
                }
            }
            return NotFound();
        }

        // POST: Cats/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cat cat)
        {
            if (cat.BirthDate > DateTime.UtcNow)
            {
                ModelState.AddModelError("Date", "Invalid Birthdate.");
            }

            if (ModelState.IsValid)
            {
                _context.Cats.Add(cat);
                await _context.SaveChangesAsync();
                return RedirectToAction("Dashboard", "Owners", new { area = "" });
            }
            return View(cat);
        }

        #endregion

        #region Edit Actions
        // GET: Cats/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cats == null)
            {
                return NotFound();
            }

            var cat = await _context.Cats.FindAsync(id);
            if (cat == null)
            {
                return NotFound();
            }
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "Id", cat.OwnerId);
            return View(cat);
        }

        // POST: Cats/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cat cat)
        {
            if (id != cat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatExists(cat.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Dashboard", "Owners", new { area = "" });
            }
            ViewData["OwnerId"] = id;
            return View();
        }
        #endregion

        #region Delete Actions
        // GET: Cats/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cats == null)
            {
                return NotFound();
            }

            var cat = await _context.Cats.FindAsync(id);
            //var cat = await _context.Cats
            //    .Include(c => c.OwnerId)
            //    .FirstOrDefaultAsync(m => m.Id == id);

            if (cat == null)
            {
                return NotFound();
            }

            return View(cat);
        }

        // POST: Cats/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cats == null)
            {
                return Problem("Entity set 'CatSalonContext.Cats' is null.");
            }
            var cat = await _context.Cats.FindAsync(id);
            if (cat != null)
            {
                //Delete any existing appointment for the cat
                if (_context.Appointments.Any(x => x.CatId.Equals(cat.Id)))
                {
                    Appointment app = _context.Appointments.Where(a => a.CatId.Equals(cat.Id)).Select(x => x).FirstOrDefault();
                    _context.Appointments.Remove(app);
                }

                _context.Cats.Remove(cat);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Dashboard", "Owners", new { area = "" });
        }
        #endregion

        private bool CatExists(int id)
        {
          return (_context.Cats?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
