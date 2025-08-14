using BusinessLogic.DataTransferObjects;
using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProjectTaskTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AttachmentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _config;

        public AttachmentsController(
            AppDbContext context,
            IWebHostEnvironment environment,
            IConfiguration config)
        {
            _context = context;
            _environment = environment;
            _config = config;
        }

        [HttpPost]
        public async Task<ActionResult<AttachmentDto>> UploadAttachment(int taskId,
       IFormFile file)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null)
            {
                return NotFound("Task not found.");
            }
           
            var allowedExtensions = _config["FileStorage:AllowedExtensions"].Split(',');
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            
            if (string.IsNullOrEmpty(fileExtension))
                return BadRequest("File has no extension");

            if (!allowedExtensions.Contains(fileExtension))
                return BadRequest("File type not allowed");

          
            var uploadFolder = _config["FileStorage:UploadFolder"] ?? "Uploads";
            if (string.IsNullOrEmpty(uploadFolder))
            {
                return BadRequest("Upload folder path is invalid.");
            }

            var uploadFolderPath = Path.Combine(uploadFolder);
            if (string.IsNullOrEmpty(uploadFolderPath))
            {
                return BadRequest("Generated upload folder path is invalid.");
            }

            Console.WriteLine($"Upload Folder Path: {uploadFolderPath}");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            var fileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(uploadFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var attachment = new Attachment
            {
                FileName = file.FileName,
                FileSize = file.Length,
                FilePath = filePath,
                UploadDate = DateTime.UtcNow,
                TaskItemId = taskId
            };
            _context.Attachments.Add(attachment);
            await _context.SaveChangesAsync();
            return Ok(new AttachmentDto
            {
                Id = attachment.Id,
                FileName = attachment.FileName,
                FileSize = attachment.FileSize,
                FilePath = attachment.FilePath,
                UploadDate = attachment.UploadDate,
                TaskItemId = attachment.TaskItemId
            });


        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AttachmentDto>> DownloadAttachment(int id)
        {
            var attachment = await _context.Attachments.FindAsync(id);
            if (attachment == null)
            {
                return NotFound("Attachment not found.");
            }
            var filePath = attachment.FilePath;
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found on server.");
            }
            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, "application/octet-stream", attachment.FileName);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttachment(int id)
        {
            var attachment = await _context.Attachments.FindAsync(id);
            if (attachment == null)
            {
                return NotFound("Attachment not found.");
            }
            var filePath = attachment.FilePath;
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            _context.Attachments.Remove(attachment);
            await _context.SaveChangesAsync();
            return NoContent();


        }
        [HttpGet("task/{taskId}")]
        public async Task<ActionResult<IEnumerable<AttachmentDto>>> GetAttachmentsByTaskId(int taskId)
        {
            var attachments = await _context.Attachments
                .Where(a => a.TaskItemId == taskId)
                .Select(a => new AttachmentDto
                {
                    Id = a.Id,
                    FileName = a.FileName,
                    FileSize = a.FileSize,
                    FilePath = a.FilePath,
                    UploadDate = a.UploadDate,
                    TaskItemId = a.TaskItemId
                })
                .ToListAsync();
            if (attachments == null || !attachments.Any())
            {
                return NotFound("No attachments found for this task.");
            }
            return Ok(attachments);
        }

    }
}