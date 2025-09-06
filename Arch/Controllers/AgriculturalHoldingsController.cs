using AmlakState.Data;
using AmlakState.Models;
using Arch.DTO;
using Arch.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            ActualAreaInSquareMeters = agriculturalHoldingDto.ActualAreaInSquareMeters,
            RegistedArea = agriculturalHoldingDto.RegistedArea,
            Houd = agriculturalHoldingDto.Houd,
            Association = agriculturalHoldingDto.Association,
            MarkazId = agriculturalHoldingDto.MarkazId,
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