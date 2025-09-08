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





    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll(
      [FromQuery] int pageNumber = 1,
      [FromQuery] int pageSize = 100,
      [FromQuery] int? markazId = null,
      [FromQuery] string? search = null)
    {

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
            .Include(ah => ah.Attachments)
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
            .Select(ah => new Arch.DTO.AgriculturalHoldingDTO
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
                    }).ToList(),
                Attachments = ah.Attachments
                    .Select(a => a.Attachment)
                    .ToList()
            })
            .ToListAsync();

        var result = new
        {
            data = items,
            totalItems = totalItems
        };

        return Ok(result);
    }




    //[HttpPost("create")]
    //public async Task<IActionResult> Create([FromForm] AgriculturalHoldingCreateDto agriculturalHoldingDto)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest(ModelState);
    //    }



    //    List<Attachments> attachmentList = new List<Attachments>();
    //    if (agriculturalHoldingDto.Attachments != null && agriculturalHoldingDto.Attachments.Any())
    //    {
    //        var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
    //        if (!Directory.Exists(uploadsFolder))
    //        {
    //            Directory.CreateDirectory(uploadsFolder);
    //        }

    //        try
    //        {
    //            foreach (var file in agriculturalHoldingDto.Attachments)
    //            {
    //                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
    //                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

    //                using (var fileStream = new FileStream(filePath, FileMode.Create))
    //                {
    //                    await file.CopyToAsync(fileStream);
    //                }
    //                attachmentList.Add(new Attachments { Attachment = uniqueFileName });
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            return StatusCode(500, $"حدث خطأ أثناء رفع المرفقات: {ex.Message}");
    //        }
    //    }

    //    var newHolding = new AgriculturalHolding
    //    {
    //        Name = agriculturalHoldingDto.Name,
    //        NationalId = agriculturalHoldingDto.NationalId,
    //        PhoneNumber = agriculturalHoldingDto.PhoneNumber,
    //        BuildingsCount = agriculturalHoldingDto.BuildingsCount,
    //        HyazaNumber = agriculturalHoldingDto.HyazaNumber,
    //        NorthernBorder = agriculturalHoldingDto.NorthernBorder,
    //        SouthernBorder = agriculturalHoldingDto.SouthernBorder,
    //        EasternBorder = agriculturalHoldingDto.EasternBorder,
    //        WesternBorder = agriculturalHoldingDto.WesternBorder,
    //        Description = agriculturalHoldingDto.Description,
    //        Address = agriculturalHoldingDto.Address,
    //        Notes = agriculturalHoldingDto.Notes,
    //        DataResourses = agriculturalHoldingDto.DataResourses,
    //        Faddan = agriculturalHoldingDto.Faddan,
    //        Qirat = agriculturalHoldingDto.Qirat,
    //        Sahm = agriculturalHoldingDto.Sahm,
    //        ActualAreaInSquareMeters = agriculturalHoldingDto.ActualAreaInSquareMeters,
    //        RegistedArea = agriculturalHoldingDto.RegistedArea,
    //        Houd = agriculturalHoldingDto.Houd,
    //        Association = agriculturalHoldingDto.Association,
    //        MarkazId = agriculturalHoldingDto.MarkazId,

    //        MadinaMaglasId= agriculturalHoldingDto.MadinaMaglasId,
    //        SourceOfOwnershipId = agriculturalHoldingDto.SourceOfOwnershipId,
    //        PropertyCoordinates = agriculturalHoldingDto.PropertyCoordinates?
    //                  .Select(pc => new PropertyCoordinate { X = pc.X, Y = pc.Y })
    //                  .ToList(),
    //        Attachments = attachmentList
    //    };

    //    _context.AgriculturalHoldings.Add(newHolding);

    //    try
    //    {
    //        await _context.SaveChangesAsync();
    //    }
    //    catch (DbUpdateException ex)
    //    {
    //        if (ex.InnerException?.Message.Contains("FOREIGN KEY constraint") == true)
    //        {
    //            return BadRequest("خطأ في البيانات المدخلة: معرف المركز (MarkazId) أو معرف مصدر الملكية (SourceOfOwnershipId) غير موجود.");
    //        }
    //        return StatusCode(500, "حدث خطأ في قاعدة البيانات. يرجى مراجعة البيانات.");
    //    }
    //    catch (Exception)
    //    {
    //        return StatusCode(500, "حدث خطأ داخلي في الخادم. يرجى المحاولة لاحقاً.");
    //    }

    //    return Ok(new { message = "تمت الإضافة بنجاح", newHolding.Id });
    //}









    //[HttpPut("update/{id}")]
    //public async Task<IActionResult> Update(int id, [FromForm] AgriculturalHoldingUpdateDto agriculturalHoldingDto)
    //{

    //    var existingHolding = await _context.AgriculturalHoldings
    //        .Include(ah => ah.Attachments)
    //        .Include(ah => ah.PropertyCoordinates)
    //        .FirstOrDefaultAsync(ah => ah.Id == id);

    //    if (existingHolding == null)
    //    {
    //        return NotFound("لم يتم العثور على الممتلكات الزراعية.");
    //    }


    //    existingHolding.Name = agriculturalHoldingDto.Name ?? existingHolding.Name;
    //    existingHolding.PhoneNumber = agriculturalHoldingDto.PhoneNumber ?? existingHolding.PhoneNumber;
    //    existingHolding.BuildingsCount = agriculturalHoldingDto.BuildingsCount ?? existingHolding.BuildingsCount;
    //    existingHolding.HyazaNumber = agriculturalHoldingDto.HyazaNumber ?? existingHolding.HyazaNumber;
    //    existingHolding.NorthernBorder = agriculturalHoldingDto.NorthernBorder ?? existingHolding.NorthernBorder;
    //    existingHolding.SouthernBorder = agriculturalHoldingDto.SouthernBorder ?? existingHolding.SouthernBorder;
    //    existingHolding.EasternBorder = agriculturalHoldingDto.EasternBorder ?? existingHolding.EasternBorder;
    //    existingHolding.WesternBorder = agriculturalHoldingDto.WesternBorder ?? existingHolding.WesternBorder;
    //    existingHolding.Description = agriculturalHoldingDto.Description ?? existingHolding.Description;
    //    existingHolding.MarkazId = agriculturalHoldingDto.MarkazId ?? existingHolding.MarkazId;
    //    existingHolding.Address = agriculturalHoldingDto.Address ?? existingHolding.Address;
    //    existingHolding.Notes = agriculturalHoldingDto.Notes ?? existingHolding.Notes;
    //    existingHolding.DataResourses = agriculturalHoldingDto.DataResourses ?? existingHolding.DataResourses;
    //    existingHolding.Faddan = agriculturalHoldingDto.Faddan ?? existingHolding.Faddan;
    //    existingHolding.Qirat = agriculturalHoldingDto.Qirat ?? existingHolding.Qirat;
    //    existingHolding.Sahm = agriculturalHoldingDto.Sahm ?? existingHolding.Sahm;
    //    existingHolding.ActualAreaInSquareMeters = agriculturalHoldingDto.ActualAreaInSquareMeters ?? existingHolding.ActualAreaInSquareMeters;
    //    existingHolding.RegistedArea = agriculturalHoldingDto.RegistedArea ?? existingHolding.RegistedArea;
    //    existingHolding.MadinaMaglasId = agriculturalHoldingDto.MadinaMaglasId ?? existingHolding.MadinaMaglasId;
    //    existingHolding.Houd = agriculturalHoldingDto.Houd ?? existingHolding.Houd;
    //    existingHolding.Association = agriculturalHoldingDto.Association ?? existingHolding.Association;
    //    existingHolding.SourceOfOwnershipId = agriculturalHoldingDto.SourceOfOwnershipId ?? existingHolding.SourceOfOwnershipId;


    //    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

    //    if (agriculturalHoldingDto.AttachmentsToDelete != null && agriculturalHoldingDto.AttachmentsToDelete.Any())
    //    {
    //        foreach (var attachmentName in agriculturalHoldingDto.AttachmentsToDelete)
    //        {
    //            var attachmentToRemove = existingHolding.Attachments.FirstOrDefault(a => a.Attachment == attachmentName);
    //            if (attachmentToRemove != null)
    //            {
    //                var filePath = Path.Combine(uploadsFolder, attachmentName);
    //                if (System.IO.File.Exists(filePath))
    //                {
    //                    System.IO.File.Delete(filePath);
    //                }
    //                _context.Attachments.Remove(attachmentToRemove);
    //            }
    //        }
    //    }

    //    if (agriculturalHoldingDto.AttachmentsToAdd != null && agriculturalHoldingDto.AttachmentsToAdd.Any())
    //    {
    //        if (!Directory.Exists(uploadsFolder))
    //        {
    //            Directory.CreateDirectory(uploadsFolder);
    //        }
    //        foreach (var file in agriculturalHoldingDto.AttachmentsToAdd)
    //        {
    //            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
    //            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

    //            using (var fileStream = new FileStream(filePath, FileMode.Create))
    //            {
    //                await file.CopyToAsync(fileStream);
    //            }
    //            existingHolding.Attachments.Add(new Attachments { Attachment = uniqueFileName });
    //        }
    //    }

    //    if (agriculturalHoldingDto.PropertyCoordinates != null)
    //    {

    //        _context.PropertyCoordinates.RemoveRange(existingHolding.PropertyCoordinates);


    //        existingHolding.PropertyCoordinates = agriculturalHoldingDto.PropertyCoordinates
    //            .Select(pc => new PropertyCoordinate { X = pc.X, Y = pc.Y })
    //            .ToList();
    //    }

    //    try
    //    {
    //        await _context.SaveChangesAsync();
    //        return Ok(new { message = "تم التعديل بنجاح" });
    //    }
    //    catch (DbUpdateConcurrencyException)
    //    {
    //        return Conflict("حدث خطأ في التزامن، قد يكون تم تعديل العنصر من قبل مستخدم آخر.");
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(500, $"حدث خطأ داخلي في الخادم: {ex.Message}");
    //    }



    //}






    // في ملف Controller.cs
    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] AgriculturalHoldingUpdateDto agriculturalHoldingDto)
    {
        if (id != agriculturalHoldingDto.Id)
        {
            return BadRequest("Holding ID mismatch.");
        }

        // 1. Find the existing holding in the database
        var existingHolding = await _context.AgriculturalHoldings
            .Include(h => h.PropertyCoordinates)
            .Include(h => h.Attachments)
            .FirstOrDefaultAsync(h => h.Id == id);

        if (existingHolding == null)
        {
            return NotFound("Agricultural holding not found.");
        }

        // 2. Update the holding's properties
        existingHolding.Name = agriculturalHoldingDto.Name;
        existingHolding.NationalId = agriculturalHoldingDto.NationalId;
        existingHolding.PhoneNumber = agriculturalHoldingDto.PhoneNumber;
        existingHolding.BuildingsCount = agriculturalHoldingDto.BuildingsCount;
        existingHolding.HyazaNumber = agriculturalHoldingDto.HyazaNumber;
        existingHolding.NorthernBorder = agriculturalHoldingDto.NorthernBorder;
        existingHolding.SouthernBorder = agriculturalHoldingDto.SouthernBorder;
        existingHolding.EasternBorder = agriculturalHoldingDto.EasternBorder;
        existingHolding.WesternBorder = agriculturalHoldingDto.WesternBorder;
        existingHolding.Description = agriculturalHoldingDto.Description;
        existingHolding.Address = agriculturalHoldingDto.Address;
        existingHolding.Notes = agriculturalHoldingDto.Notes;
        existingHolding.DataResourses = agriculturalHoldingDto.DataResourses;
        existingHolding.Faddan = agriculturalHoldingDto.Faddan;
        existingHolding.Qirat = agriculturalHoldingDto.Qirat;
        existingHolding.Sahm = agriculturalHoldingDto.Sahm;
        existingHolding.ActualAreaInSquareMeters = agriculturalHoldingDto.ActualAreaInSquareMeters;
        existingHolding.RegistedArea = agriculturalHoldingDto.RegistedArea;
        existingHolding.Houd = agriculturalHoldingDto.Houd;
        existingHolding.Association = agriculturalHoldingDto.Association;
        existingHolding.MarkazId = (int)agriculturalHoldingDto.MarkazId;
        existingHolding.MadinaMaglasId = agriculturalHoldingDto.MadinaMaglasId;
        existingHolding.SourceOfOwnershipId = agriculturalHoldingDto.SourceOfOwnershipId;

        // 3. Handle coordinates
        // Remove old coordinates and add new ones from the DTO
        existingHolding.PropertyCoordinates.Clear();
        if (agriculturalHoldingDto.PropertyCoordinates != null)
        {
            foreach (var coordDto in agriculturalHoldingDto.PropertyCoordinates)
            {
                existingHolding.PropertyCoordinates.Add(new PropertyCoordinate
                {
                    X = coordDto.X,
                    Y = coordDto.Y
                });
            }
        }

        
        var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

        var attachmentsToRemove = existingHolding.Attachments
            .Where(a => !agriculturalHoldingDto.ExistingAttachments.Contains(a.Attachment))
            .ToList();

        foreach (var attachment in attachmentsToRemove)
        {
            var filePath = Path.Combine(uploadsFolder, attachment.Attachment);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            existingHolding.Attachments.Remove(attachment);
        }

   
        if (agriculturalHoldingDto.NewAttachments != null && agriculturalHoldingDto.NewAttachments.Any())
        {
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            foreach (var file in agriculturalHoldingDto.NewAttachments)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                existingHolding.Attachments.Add(new Attachments { Attachment = uniqueFileName });
            }
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException?.Message.Contains("FOREIGN KEY constraint") == true)
            {
                return BadRequest("خطأ في البيانات المدخلة: معرف المركز (MarkazId) أو معرف مصدر الملكية (SourceOfOwnershipId) أو معرف المجلس القروي (MadinaMaglasId) غير موجود.");
            }
            return StatusCode(500, "حدث خطأ في قاعدة البيانات. يرجى مراجعة البيانات.");
        }
        catch (Exception)
        {
            return StatusCode(500, "حدث خطأ داخلي في الخادم. يرجى المحاولة لاحقاً.");
        }

        return Ok(new { message = "تم التعديل بنجاح" });
    }













    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var agriculturalHolding = await _context.AgriculturalHoldings
            .Include(ah => ah.Markaz)
            .Include(ah => ah.PropertyCoordinates)
            .Include(ah => ah.Madina_Maglas)
            .Include(ah => ah.SourceOfOwnership)
            .Include(ah => ah.Attachments)
            .FirstOrDefaultAsync(ah => ah.Id == id);

        if (agriculturalHolding == null)
        {
            return NotFound("لم يتم العثور على الحيازة المطلوبة.");
        }

        var holdingDto = new Arch.DTO.AgriculturalHoldingDTO
        {
            Id = agriculturalHolding.Id,
            Name = agriculturalHolding.Name,
            NationalId = agriculturalHolding.NationalId,
            PhoneNumber = agriculturalHolding.PhoneNumber,
            BuildingsCount = agriculturalHolding.BuildingsCount ?? 0,
            HyazaNumber = agriculturalHolding.HyazaNumber,
            NorthernBorder = agriculturalHolding.NorthernBorder,
            SouthernBorder = agriculturalHolding.SouthernBorder,
            EasternBorder = agriculturalHolding.EasternBorder,
            WesternBorder = agriculturalHolding.WesternBorder,
            Description = agriculturalHolding.Description,
            MarkazId = agriculturalHolding.MarkazId, 
            MarkazName = agriculturalHolding.Markaz?.Name,
            Address = agriculturalHolding.Address,
            Notes = agriculturalHolding.Notes,
            DataResourses = agriculturalHolding.DataResourses,
            Faddan = agriculturalHolding.Faddan ?? 0,
            Qirat = agriculturalHolding.Qirat ?? 0,
            Sahm = agriculturalHolding.Sahm ?? 0,
            ActualAreaInSquareMeters = agriculturalHolding.ActualAreaInSquareMeters ?? 0,
            RegistedArea = agriculturalHolding.RegistedArea ?? 0,
            MadinaMaglasName = agriculturalHolding.Madina_Maglas?.Name,
            Houd = agriculturalHolding.Houd,
            Association = agriculturalHolding.Association,
            SourceOfOwnershipName = agriculturalHolding.SourceOfOwnership?.Name,
            Coordinates = agriculturalHolding.PropertyCoordinates?
                .Select(c => new PropertyCoordinateDto { X = c.X, Y = c.Y })
                .ToList(),
            Attachments = agriculturalHolding.Attachments?
                .Select(a => a.Attachment)
                .ToList()
        };

        return Ok(holdingDto);
    }
}