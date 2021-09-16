using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TestAppWithSignalR;
using TestAppWithSignalR.Models;

namespace TestAppWithSignalR.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RosterApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<SignalRService> _hubContext;

        public RosterApiController(ApplicationDbContext context, IHubContext<SignalRService> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

      

        // POST: api/RosterApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Roster>> PostRoster(Roster roster)
        {
            _context.Rosters.Add(roster);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("Rosters");
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", roster.FullName);
          

            return CreatedAtAction("GetRoster", new { id = roster.Id }, roster);
        }

       
    }
}
