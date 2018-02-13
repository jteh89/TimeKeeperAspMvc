using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeKeeperAspMvc.Data;
using TimeKeeperAspMvc.Models;

using TimeKeeperAspMvc.Services;
using Autofac;

namespace TimeKeeperAspMvc.Controllers
{
    public class AccessTimesController : Controller
    {
        private readonly TimeKeeperContext _context;
        private readonly ITimeService _timeService;

        public AccessTimesController(TimeKeeperContext context, ITimeService timeService)
        {
            _context = context;
            _timeService = timeService;
        }

        // GET: AccessTimes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AccessTimes.ToListAsync());
        }

        public async Task<DateTime> GetTime()
        {
            try
            {
                await _timeService.LogTime(_context, DateTime.Now);
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return _timeService.GetTime();
        }

        // GET: AccessTimes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessTime = await _context.AccessTimes
                .SingleOrDefaultAsync(m => m.ID == id);
            if (accessTime == null)
            {
                return NotFound();
            }

            return View(accessTime);
        }

        // GET: AccessTimes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AccessTimes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Time")] AccessTime accessTime)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(accessTime);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            
            return View(accessTime);
        }

        // GET: AccessTimes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessTime = await _context.AccessTimes.SingleOrDefaultAsync(m => m.ID == id);
            if (accessTime == null)
            {
                return NotFound();
            }
            return View(accessTime);
        }

        // POST: AccessTimes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Time")] AccessTime accessTime)
        {
            if (id != accessTime.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accessTime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccessTimeExists(accessTime.ID))
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
            return View(accessTime);
        }

        // GET: AccessTimes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessTime = await _context.AccessTimes
                .SingleOrDefaultAsync(m => m.ID == id);
            if (accessTime == null)
            {
                return NotFound();
            }

            return View(accessTime);
        }

        // POST: AccessTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accessTime = await _context.AccessTimes.SingleOrDefaultAsync(m => m.ID == id);
            _context.AccessTimes.Remove(accessTime);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccessTimeExists(int id)
        {
            return _context.AccessTimes.Any(e => e.ID == id);
        }
    }
}
