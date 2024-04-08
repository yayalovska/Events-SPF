using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventsDomain.Models;
using EventsInfrastructure;

namespace EventsInfrastructure.Controllers
{
    public class FacultiesController : Controller
    {
        private readonly BdeventsContext _context;

        public FacultiesController(BdeventsContext context)
        {
            _context = context;
        }

        // GET: Faculties
        public async Task<IActionResult> Index()
        {
            var faculties = await _context.Faculties.ToListAsync();
            return View(faculties);
        }



        // GET: Faculties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = await _context.Faculties
                .Include(f => f.Departments) // Для відображення департаментів в деталях факультету
                .FirstOrDefaultAsync(m => m.Id == id);

            if (faculty == null)
            {
                return NotFound();
            }

            return View(faculty); // Зверніть увагу: використовуємо "Details" як назву в'юшки
        }

        // GET: Faculties/Create
        public IActionResult Create()
        {
            // Метод Create вже не залежить від departmentId, тому що факультети створюються окремо
            return View(); // Використовується в'юшка Create без передачі даних
        }

        // POST: Faculties/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name, FacultyId")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(department);
        }


        // GET: Faculties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty == null)
            {
                return NotFound();
            }
            return View(faculty); // Використовується в'юшка Edit для редагування факультету
        }

        // POST: Faculties/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Faculty faculty)
        {
            if (id != faculty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faculty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultyExists(faculty.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(faculty);
        }

        // GET: Faculties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = await _context.Faculties
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faculty == null)
            {
                return NotFound();
            }

            return View(faculty); // Використовується в'юшка Delete для видалення факультету
        }

        // POST: Faculties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty != null)
            {
                _context.Faculties.Remove(faculty);
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacultyExists(int id)
        {
            return _context.Faculties.Any(e => e.Id == id);
        }
    }
}


