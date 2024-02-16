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
    public class AppointmentsController : Controller
    {
        private readonly CatSalonContext _context;

        public AppointmentsController(CatSalonContext context)
        {
            _context = context;
        }

        #region Index Action
        //GET: Appointments
        public async Task<IActionResult> Index()
        {
            // Redirect non logged in users
            if (HttpContext.Session.GetString("userId") == null)
            {
                return RedirectToAction("Login", "Owners", new { area = "Owners" });
            }
            
            int? id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            if (id != null)
            {
                var owner = await _context.Owners.FindAsync(id);
                if (owner == null) { return NotFound(); }
                List<Appointment> allAppointments = await _context.Appointments.ToListAsync();
                List<Appointment> appointments = new List<Appointment>();
                List<Cat> cats = new List<Cat>();
                
                // Build a list of appointments and cats from this Owner's Cats
                foreach (var appointment in allAppointments)
                {
                    foreach (var cat in owner.Cat)
                    {
                        if (appointment.CatId.Equals(cat.Id))
                        {
                            cats.Add(cat);
                            appointments.Add(appointment);
                        }
                    }
                }
                
                ViewData["Appointments"] = appointments;
                ViewData["Cats"] = cats;
                return View(appointments);
            }
            return NotFound();
        }
        #endregion

        #region Details Action
        //GET: Appointments/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }


            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(m => m.Id == id);

            AppointmentDetails appointmentDetails = new AppointmentDetails(id); 

            Employee employee = await _context.Employees.FindAsync(appointment.EmployeeId);

            ViewData["employee"] = employee.Name;

            if (appointment == null)
            {
                return NotFound();
            };

            return View(appointmentDetails);
        }
        #endregion

        #region Create Actions
        // GET: Appointments/Create
        public IActionResult Create(int? id)
        {
            //if the id is not passed, get the id from the session
            if (id == null)
            {
               id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            }
                var cats = (from owner in _context.Owners
                        join cat in _context.Cats
                             on owner.Id equals cat.OwnerId
                        where owner.Id == id
                        select cat);

            AppointmentServiceSelections appointmentServiceSelections = new AppointmentServiceSelections();
            ViewData["CatList"] = new SelectList(cats, "Id","Name");
            ViewData["ServiceSelections"] = new List<ServiceSelection>(appointmentServiceSelections.ServiceSelections);
            return View(appointmentServiceSelections);
        }

        // POST: Appointments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentServiceSelections appointmentServiceSelections)
        {
            //Validate Checkboxes
            if (!appointmentServiceSelections.ServiceSelections.Any(s => (bool)s.Selected))
            {
                ModelState.AddModelError("ServiceSelections", "You must select at least one service.");
            }

            if (_context.Appointments.Any(a => a.CatId == appointmentServiceSelections.Appointment.CatId))
            {
                ModelState.AddModelError("Cat", "This cat already has an appointment.");
            }

            // Validate datetime range
            if (appointmentServiceSelections.Appointment.ScheduledDate < DateTime.Today) 
            { 
                ModelState.AddModelError("Appointment", "Cannot book an appointment in the past");
            }
            if (appointmentServiceSelections.Appointment.ScheduledDate.Year > DateTime.Today.AddYears(1).Year)
            {
                ModelState.AddModelError("Appointment", "Cannot book this far in advance.");
            }

            if (ModelState.IsValid)
            {
                // make appointment
                _context.Appointments.Add(appointmentServiceSelections.Appointment);
                

                await _context.SaveChangesAsync();

                foreach (ServiceSelection serviceSelection in appointmentServiceSelections.ServiceSelections)
                {
                    if (serviceSelection.Selected)
                    {
                        _context.ChangeTracker.Clear();
                        AppointmentService appointmentService = new AppointmentService
                        {
                            AppointmentId = appointmentServiceSelections.Appointment.Id,
                            ServiceId = serviceSelection.Service.Id
                        };

                        _context.AppointmentServices.Add(appointmentService);
                        await _context.SaveChangesAsync();
                    }
                }

                await _context.SaveChangesAsync();
                //return RedirectToAction("Dashboard","Owners"); !REVERT TO THIS IF IT DOESNT WORK!
                return RedirectToAction("Index");
            }

            var cats = (from owner in _context.Owners
                        join cat in _context.Cats
                             on owner.Id equals cat.OwnerId
                        where owner.Id == appointmentServiceSelections.Appointment.Cat.OwnerId
                        select cat);
            ViewData["CatList"] = new SelectList(cats, "Id", "Name");
            ViewData["Services"] = await _context.Services.ToListAsync();

            return View(appointmentServiceSelections);
            
        }
        #endregion

        #region Delete Actions
        // GET: Appointments/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            Employee employee = await _context.Employees.FindAsync(appointment.EmployeeId);

            ViewData["employee"] = employee.Name;

            return View(appointment);
        }

        // POST: Appointments/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointments == null)
            {
                return Problem("Entity set 'CatSalonContext.Appointments'  is null.");
            }
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
            

        }
        #endregion

        private bool AppointmentExists(int id)
        {
          return (_context.Appointments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
