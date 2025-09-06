using AmlakState.Data;
using Arch.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;

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
        public async Task<ActionResult> GetAll()
        {
            try
            {
                // الحصول على معرف المستخدم من المطالبات (Claims)
                var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdString == null || !int.TryParse(userIdString, out int userId))
                {
                    // If userId is null or cannot be parsed as an integer
                    return Unauthorized("User ID not found or is invalid.");
                }

                // البحث عن المستخدم في قاعدة البيانات للتحقق من صلاحياته
                // هنا تم تعديل المقارنة لتحويل userId من string إلى int
                var user = await _context.User.FirstOrDefaultAsync(u => u.ID == userId);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                // التحقق من حالة المستخدم: هل هو مسؤول (MarkazId == null) أم مستخدم عادي؟
                if (user.MarkazId == null)
                {
                    // حالة المسؤول (Admin):
                    // إرجاع جميع المراكز مع تحديد الحقول (ID والاسم فقط)
                    var allMarkazes = await _context.Markazes
                        .Select(m => new
                        {
                            ID = m.ID,
                            Name = m.Name
                        })
                        .ToListAsync();

                    // إرجاع الاستجابة بتنسيق يطابق الكود في Flutter
                    return Ok(new
                    {
                        isAdmin = true,
                        markazes = allMarkazes
                    });
                }
                else
                {
                    // حالة المستخدم العادي:
                    // إرجاع المركز المرتبط بمعرف المستخدم
                    var userMarkaz = await _context.Markazes.FirstOrDefaultAsync(m => m.ID == user.MarkazId);

                    if (userMarkaz == null)
                    {
                        return NotFound("Markaz associated with the user not found.");
                    }

                    // إرجاع الاستجابة بتنسيق يطابق الكود في Flutter
                    return Ok(new
                    {
                        isAdmin = false,
                        markazes = new List<object> {
                            new {
                                id = userMarkaz.ID,
                                name = userMarkaz.Name,
                                markazId = userMarkaz.ID
                            }
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving data: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
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




        [HttpGet("GetAllMadinaMaglas")]
        public async Task<ActionResult<IEnumerable<MadinaMaglasDto>>> GetAllMadinaMaglas()
        {
            
            var allVillageCouncils = await _context.Madina_Maglas
                                              
                                                .Select(mm => new MadinaMaglasDto
                                                {
                                                    ID = mm.ID,
                                                    Name = mm.Name,
                                                    MarkazId = mm.MarkazId
                                                })
                                                .ToListAsync();

            if (!allVillageCouncils.Any())
            {
                return NotFound("Not  found.");
            }

            return Ok(allVillageCouncils);
        }
    }
}
