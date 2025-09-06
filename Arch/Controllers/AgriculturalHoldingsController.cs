using AmlakState.Data;
using AmlakState.Models;
using Arch.DTO;
using Arch.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AgriculturalHoldingsController : ControllerBase
{
    private readonly ArchDbContext _context;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public AgriculturalHoldingsController(ArchDbContext context, IWebHostEnvironment hostingEnvironment)
    {
        _context = context;
        _hostingEnvironment = hostingEnvironment;
    }



 
[Authorize] // 1. Add this attribute to secure the endpoint.
[HttpGet("GetAll")]
public async Task<IActionResult> GetAll(
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 100,
    [FromQuery] int? markazId = null,
    [FromQuery] string? search = null)
{
    // Input validation
    if (pageNumber < 1)
    {
        return BadRequest("رقم الصفحة يجب أن يكون أكبر من أو يساوي 1.");
    }
    if (pageSize < 1)
    {
        return BadRequest("حجم الصفحة يجب أن يكون أكبر من أو يساوي 1.");
    }

    
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    var userRole = User.FindFirstValue(ClaimTypes.Role);
    var userMarkazIdClaim = User.FindFirstValue("MarkazId"); 

    if (string.IsNullOrEmpty(userId))
    {
        return Unauthorized("المستخدم غير موجود");
    }

    
    var query = _context.AgriculturalHoldings
        .Include(ah => ah.Markaz)
        .Include(ah => ah.PropertyCoordinates)
        .Include(ah => ah.Madina_Maglas)
        .Include(ah => ah.SourceOfOwnership)
        .AsQueryable();

    if (userRole != "Admin")
    {
        if (string.IsNullOrEmpty(userMarkazIdClaim))
        {
            return BadRequest("لا يوجد مركز مرتبط بالمستخدم");
        }
        var userMarkazId = int.Parse(userMarkazIdClaim);
        query = query.Where(ah => ah.MarkazId == userMarkazId);
    }
    else
    {
        if (markazId.HasValue)
        {
            query = query.Where(ah => ah.MarkazId == markazId.Value);
        }
    }

    if (!string.IsNullOrWhiteSpace(search))
    {
        query = query.Where(ah =>
            (ah.Name != null && ah.Name.Contains(search)) ||
            (ah.NationalId != null && ah.NationalId.Contains(search)) ||
            (ah.PhoneNumber != null && ah.PhoneNumber.Contains(search)) ||
            (ah.Address != null && ah.Address.Contains(search)) ||
            (ah.Houd != null && ah.Houd.Contains(search)) ||
            (ah.HyazaNumber != null && ah.HyazaNumber.Contains(search))
        );
    }


    var totalItems = await query.CountAsync();

    var paginatedQuery = query
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize);

    var items = await paginatedQuery
        .Select(ah => new AgriculturalHoldingDTO
        {
            Id = ah.Id,
            Name = ah.Name,
            NationalId = ah.NationalId,
            PhoneNumber = ah.PhoneNumber,
            BuildingsCount = ah.BuildingsCount ?? 0,
            HyazaNumber = ah.HyazaNumber,
            NorthernBorder = ah.NorthernBorder,
            SouthernBorder = ah.SouthernBorder,
            EasternBorder = ah.EasternBorder,
            WesternBorder = ah.WesternBorder,
            Description = ah.Description,
            MarkazId = ah.MarkazId,
            MarkazName = ah.Markaz.Name,
            Address = ah.Address,
            Notes = ah.Notes,
            DataResourses = ah.DataResourses,
            Faddan = ah.Faddan ?? 0,
            Qirat = ah.Qirat ?? 0,
            Sahm = ah.Sahm ?? 0,
            ActualAreaInSquareMeters = ah.ActualAreaInSquareMeters ?? 0,
            RegistedArea = ah.RegistedArea ?? 0,
            MadinaMaglasName = ah.Madina_Maglas.Name,
            Houd = ah.Houd,
            Association = ah.Association,
            SourceOfOwnershipName = ah.SourceOfOwnership.Name,
            Coordinates = ah.PropertyCoordinates
                .Select(c => new PropertyCoordinateDto
                {
                    X = c.X,
                    Y = c.Y
                }).ToList()
        })
        .ToListAsync();

    var result = new
    {
        data = items,
        totalItems = totalItems
    };

    return Ok(result);
}



[HttpPost("create")]
    public async Task<IActionResult> Create([FromForm] AgriculturalHoldingCreateDto agriculturalHoldingDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }



        List<Attachments> attachmentList = new List<Attachments>();
        if (agriculturalHoldingDto.Attachments != null && agriculturalHoldingDto.Attachments.Any())
        {
            var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            try
            {
                foreach (var file in agriculturalHoldingDto.Attachments)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    attachmentList.Add(new Attachments { Attachment = uniqueFileName });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"حدث خطأ أثناء رفع المرفقات: {ex.Message}");
            }
        }

        var newHolding = new AgriculturalHolding
        {
            Name = agriculturalHoldingDto.Name,
            NationalId = agriculturalHoldingDto.NationalId,
            PhoneNumber = agriculturalHoldingDto.PhoneNumber,
            BuildingsCount = agriculturalHoldingDto.BuildingsCount,
            HyazaNumber = agriculturalHoldingDto.HyazaNumber,
            NorthernBorder = agriculturalHoldingDto.NorthernBorder,
            SouthernBorder = agriculturalHoldingDto.SouthernBorder,
            EasternBorder = agriculturalHoldingDto.EasternBorder,
            WesternBorder = agriculturalHoldingDto.WesternBorder,
            Description = agriculturalHoldingDto.Description,
            Address = agriculturalHoldingDto.Address,
            Notes = agriculturalHoldingDto.Notes,
            DataResourses = agriculturalHoldingDto.DataResourses,
            Faddan = agriculturalHoldingDto.Faddan,
            Qirat = agriculturalHoldingDto.Qirat,
            Sahm = agriculturalHoldingDto.Sahm,
            ActualAreaInSquareMeters = agriculturalHoldingDto.ActualAreaInSquareMeters,
            RegistedArea = agriculturalHoldingDto.RegistedArea,
            Houd = agriculturalHoldingDto.Houd,
            Association = agriculturalHoldingDto.Association,
            MarkazId = agriculturalHoldingDto.MarkazId,

            MadinaMaglasId= agriculturalHoldingDto.MadinaMaglasId,
            SourceOfOwnershipId = agriculturalHoldingDto.SourceOfOwnershipId,
            PropertyCoordinates = agriculturalHoldingDto.PropertyCoordinates?
                      .Select(pc => new PropertyCoordinate { X = pc.X, Y = pc.Y })
                      .ToList(),
            Attachments = attachmentList
        };

        _context.AgriculturalHoldings.Add(newHolding);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException?.Message.Contains("FOREIGN KEY constraint") == true)
            {
                return BadRequest("خطأ في البيانات المدخلة: معرف المركز (MarkazId) أو معرف مصدر الملكية (SourceOfOwnershipId) غير موجود.");
            }
            return StatusCode(500, "حدث خطأ في قاعدة البيانات. يرجى مراجعة البيانات.");
        }
        catch (Exception)
        {
            return StatusCode(500, "حدث خطأ داخلي في الخادم. يرجى المحاولة لاحقاً.");
        }

        return Ok(new { message = "تمت الإضافة بنجاح", newHolding.Id });
    }
}