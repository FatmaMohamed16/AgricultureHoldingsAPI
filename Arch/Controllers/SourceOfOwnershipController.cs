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

        // Constructor يقوم بحقن DbContext
        // هذا يسمح للمتحكم بالوصول إلى قاعدة البيانات
        public SourceOfOwnershipController(ArchDbContext context)
        {
            _context = context;
        }

        // نقطة نهاية API للحصول على جميع مصادر الملكية.
        // [HttpGet("GetAll")] ستجعل المسار هو api/SourceOfOwnership/GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<SourceOfOwnershipDto>>> GetAll()
        {
            try
            {
                // التحقق مما إذا كان هناك اتصال بقاعدة البيانات
                if (_context.SourceOfOwnership == null)
                {
                    return NotFound("Entity set 'ApplicationDbContext.SourceOfOwnership' is null.");
                }

                // استرجاع جميع البيانات من جدول SourceOfOwnership بشكل غير متزامن
                // ثم تحويلها إلى DTO لتجنب إرسال بيانات غير ضرورية مثل ICollection
                var sources = await _context.SourceOfOwnership
                    .Select(s => new SourceOfOwnershipDto
                    {
                        ID = s.ID,
                        Name = s.Name
                    })
                    .ToListAsync();

                // إرجاع قائمة البيانات بنجاح كاستجابة HTTP 200 OK
                return Ok(sources);
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ والعودة باستجابة خطأ 500
                // يمكنك استخدام logger هنا بدلاً من Console.WriteLine
                Console.WriteLine($"Error retrieving data: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }
    }
}
