using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopForge.Shared.DTOs.Common;
using ShopForge.Shared.DTOs.Products;

namespace ShopForge.Api.Controllers;

[Authorize(Policy = "AdminOrManager")]
[ApiController]
[Route("api/admin/uploads")]
public class AdminUploadsController : ControllerBase
{
    private static readonly HashSet<string> AllowedContentTypes = new(StringComparer.OrdinalIgnoreCase)
    {
        "image/jpeg",
        "image/png",
        "image/webp",
        "image/gif"
    };

    private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg",
        ".jpeg",
        ".png",
        ".webp",
        ".gif"
    };

    private const long MaxFileSizeBytes = 5 * 1024 * 1024;
    private readonly IWebHostEnvironment _environment;

    public AdminUploadsController(IWebHostEnvironment environment) => _environment = environment;

    [HttpPost("product-images")]
    [RequestSizeLimit(MaxFileSizeBytes)]
    public async Task<ActionResult<ApiResponse<ProductImageUploadResult>>> UploadProductImage(IFormFile? file)
    {
        if (file == null || file.Length == 0)
            return BadRequest(ApiResponse<ProductImageUploadResult>.Fail("Please choose an image file."));

        if (file.Length > MaxFileSizeBytes)
            return BadRequest(ApiResponse<ProductImageUploadResult>.Fail("Image must be 5 MB or smaller."));

        var extension = Path.GetExtension(file.FileName);
        if (!AllowedExtensions.Contains(extension) || !AllowedContentTypes.Contains(file.ContentType))
            return BadRequest(ApiResponse<ProductImageUploadResult>.Fail("Only JPG, PNG, WEBP, and GIF images are allowed."));

        var webRoot = _environment.WebRootPath ?? Path.Combine(_environment.ContentRootPath, "wwwroot");
        var uploadDirectory = Path.Combine(webRoot, "uploads", "products");
        Directory.CreateDirectory(uploadDirectory);

        var storedFileName = $"{DateTime.UtcNow:yyyyMMddHHmmssfff}-{Guid.NewGuid():N}{extension.ToLowerInvariant()}";
        var filePath = Path.Combine(uploadDirectory, storedFileName);

        await using (var stream = System.IO.File.Create(filePath))
        {
            await file.CopyToAsync(stream);
        }

        var imageUrl = $"{Request.Scheme}://{Request.Host}/uploads/products/{storedFileName}";
        return Ok(ApiResponse<ProductImageUploadResult>.Ok(new ProductImageUploadResult
        {
            ImageUrl = imageUrl,
            FileName = storedFileName,
            SizeBytes = file.Length
        }));
    }
}
