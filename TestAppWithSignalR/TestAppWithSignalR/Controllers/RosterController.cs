using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TestAppWithSignalR.Models;

namespace TestAppWithSignalR.Controllers
{

    public class RosterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<SignalRService> _hubContext;

        public RosterController(ApplicationDbContext context, IHubContext<SignalRService> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET: Roster
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rosters.ToListAsync());
        }
        [HttpGet]
        public IActionResult GetRosters()
        {
            var res = _context.Rosters.ToList();
            return Ok(res);
        }
        // GET: Roster/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roster = await _context.Rosters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roster == null)
            {
                return NotFound();
            }

            return View(roster);
        }
       

       

        // GET: Roster/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roster = await _context.Rosters.FindAsync(id);
            if (roster == null)
            {
                return NotFound();
            }
            return View(roster);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,GroupName,Age")] Roster roster)
        {
            if (id != roster.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roster);
                    await _context.SaveChangesAsync();
                    await _hubContext.Clients.All.SendAsync("Rosters");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RosterExists(roster.Id))
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
            return View(roster);
        }

        // GET: Roster/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roster = await _context.Rosters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roster == null)
            {
                return NotFound();
            }

            return View(roster);
        }

        // POST: Roster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roster = await _context.Rosters.FindAsync(id);
            _context.Rosters.Remove(roster);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("Rosters");
            return RedirectToAction(nameof(Index));
        }

        private bool RosterExists(int id)
        {
            return _context.Rosters.Any(e => e.Id == id);
        }
    }
}
