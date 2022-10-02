using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Students_CRUD.Models;

namespace Students_CRUD.Controllers
{
    [Authorize]
    public class StudentResultsController : Controller
    {
       
        private readonly StudentsCRUD_DBContext _context;

        public StudentResultsController(StudentsCRUD_DBContext context)
        {
            _context = context;
        }

        // GET: StudentResults
        public async Task<IActionResult> Index()
        {
              return View(await _context.StudentResults.ToListAsync());
        }

        // GET: StudentResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StudentResults == null)
            {
                return NotFound();
            }

            var studentResults = await _context.StudentResults
                .FirstOrDefaultAsync(m => m.ID == id);
            if (studentResults == null)
            {
                return NotFound();
            }

            return View(studentResults);
        }

        // GET: StudentResults/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Class_N_Sec,English,Maths,Language,Science,Social,Total,Average,Result,Class")] StudentResults studentResults)
        {
            int tempTotal = studentResults.English+ studentResults.Language + studentResults.Maths + studentResults.Science + studentResults.Social;
              int tempAvg =  (studentResults.English + studentResults.Language + studentResults.Maths + studentResults.Science + studentResults.Social) / 5;
            int result = 0;
              studentResults.Total = tempTotal;

              studentResults.Average = tempAvg;

              if(studentResults.English>=50 && studentResults.Language >= 50 && studentResults.Maths >=50 && 
                studentResults.Science>=50 && studentResults.Social>=50)
              {
                  studentResults.Result = "Pass";
                result = 1;

              }
              else
              {
                  studentResults.Result = "Fail";
              }
            //------------------------------------------------------------------------------
            if (result>=1) 
            {
                if (((studentResults.English + studentResults.Language + studentResults.Maths + studentResults.Science + studentResults.Social) / 5) >= 90)
                {
                    studentResults.Class = "SuperClass";
                }
                else if (((studentResults.English + studentResults.Language + studentResults.Maths + studentResults.Science + studentResults.Social) / 5) >= 80)
                {
                    studentResults.Class = "FirstClass";
                }
                else             {
                    studentResults.Class = "SecondClass";
                } }

            else
            {
                studentResults.Class = "NA";
            }

                _context.Add(studentResults);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            return View(studentResults);
        }

        // GET: StudentResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StudentResults == null)
            {
                return NotFound();
            }

            var studentResults = await _context.StudentResults.FindAsync(id);
            if (studentResults == null)
            {
                return NotFound();
            }
            return View(studentResults);
        }

        // POST: StudentResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Class_N_Sec,English,Maths,Language,Science,Social,Total,Average,Result,Class")] StudentResults studentResults)
        {
            if (id != studentResults.ID)
            {
                return NotFound();
            }
            int tempTotal = studentResults.English + studentResults.Language + studentResults.Maths + studentResults.Science + studentResults.Social;
            int tempAvg = (studentResults.English + studentResults.Language + studentResults.Maths + studentResults.Science + studentResults.Social) / 5;
            int result = 0;
            studentResults.Total = tempTotal;

            studentResults.Average = tempAvg;

            if (studentResults.English >= 50 && studentResults.Language >= 50 && studentResults.Maths >= 50 &&
              studentResults.Science >= 50 && studentResults.Social >= 50)
            {
                studentResults.Result = "Pass";
                result = 1;

            }
            else
            {
                studentResults.Result = "Fail";
            }
            //------------------------------------------------------------------------------
            if (result >= 1)
            {
                if (((studentResults.English + studentResults.Language + studentResults.Maths + studentResults.Science + studentResults.Social) / 5) >= 90)
                {
                    studentResults.Class = "SuperClass";
                }
                else if (((studentResults.English + studentResults.Language + studentResults.Maths + studentResults.Science + studentResults.Social) / 5) >= 80)
                {
                    studentResults.Class = "FirstClass";
                }
                else
                {
                    studentResults.Class = "SecondClass";
                }
            }

            else
            {
                studentResults.Class = "NA";
            }



            try
                {
                    _context.Update(studentResults);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentResultsExists(studentResults.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            return View(studentResults);
        }

        // GET: StudentResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StudentResults == null)
            {
                return NotFound();
            }

            var studentResults = await _context.StudentResults
                .FirstOrDefaultAsync(m => m.ID == id);
            if (studentResults == null)
            {
                return NotFound();
            }

            return View(studentResults);
        }

        // POST: StudentResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StudentResults == null)
            {
                return Problem("Entity set 'StudentsCRUD_DBContext.StudentResults'  is null.");
            }
            var studentResults = await _context.StudentResults.FindAsync(id);
            if (studentResults != null)
            {
                _context.StudentResults.Remove(studentResults);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentResultsExists(int id)
        {
          return _context.StudentResults.Any(e => e.ID == id);
        }
    }
}
