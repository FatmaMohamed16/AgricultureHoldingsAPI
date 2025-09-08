using AmlakState.Data;
using Arch.DTO;
using Arch.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Arch.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SourceOfOwnershipController : ControllerBase
    {
        private readonly ArchDbContext _context;

        public SourceOfOwnershipController(ArchDbContext context)
        {
            _context = context;
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<SourceOfOwnershipDto>>> GetAll()
        {
            try
            {
                if (_context.SourceOfOwnership == null)
                {
                    return NotFound("Entity set 'ApplicationDbContext.SourceOfOwnership' is null.");
                }

          
                var sources = await _context.SourceOfOwnership
                    .Select(s => new SourceOfOwnershipDto
                    {
                        ID = s.ID,
                        Name = s.Name
                    })
                    .ToListAsync();

            
                return Ok(sources);
            }
            catch (Exception ex)
            {
          
                Console.WriteLine($"Error retrieving data: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }
    }
}
