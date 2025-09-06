using AmlakState.Data;
using Arch.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Arch.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MarkazController : ControllerBase
    {
        private readonly ArchDbContext _context;

        public MarkazController(ArchDbContext context)
        {
            _context = context;
        }

      
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<MarkazDTO>>> GetAllMarkaz()
        {
            var markazList = await _context.Markazes
                                           .Select(m => new MarkazDTO
                                           {
                                               Id = m.ID,
                                               Name = m.Name
                                           })
                                           .ToListAsync();

            return Ok(markazList);
        }

   
    [HttpGet("GetMadinaMaglasByMarkazId/{markazId}")]
    public async Task<ActionResult<IEnumerable<MadinaMaglasDto>>> GetMadinaMaglasByMarkazId(int markazId)
        {
            var madinaMaglasList = await _context.Madina_Maglas
                                                 .Where(mm => mm.MarkazId == markazId)
                                                 .Select(mm => new MadinaMaglasDto
                                                 {
                                                     ID = mm.ID,
                                                     Name = mm.Name,
                                                     MarkazId = mm.MarkazId
                                                 })
                                                 .ToListAsync();

            if (!madinaMaglasList.Any())
            {
                return NotFound("No village councils found for the specified Markaz.");
            }

            return Ok(madinaMaglasList);
        }
    }
}
