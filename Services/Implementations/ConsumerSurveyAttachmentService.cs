using Microsoft.EntityFrameworkCore;
using SolarPanelInstallationManagement.Data;
using SolarPanelInstallationManagement.Models.Entities;
using SolarPanelInstallationManagement.Services.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace SolarPanelInstallationManagement.Services.Implementations
{
    public class ConsumerSurveyAttachmentService
    : IConsumerSurveyAttachmentService
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        // configuration (can move to appsettings later)
        private const long MaxFileSize = 20 * 1024 * 1024; // 5 MB

        private static readonly HashSet<string> AllowedExtensions =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ".pdf", ".jpg", ".jpeg", ".png", ".doc", ".docx"
            };

        public ConsumerSurveyAttachmentService(
            AppDbContext db,
            IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        // -----------------------------
        // READ
        // -----------------------------
        public async Task<IEnumerable<ConsumerSurveyAttachment>> GetBySurveySnoAsync(
            int surveySno)
        {
            return await _db.ConsumerSurveyAttachments
                .Where(a => a.ConsumerSurveySno == surveySno)
                .OrderByDescending(a => a.UploadedOn)
                .ToListAsync();
        }

        public async Task<ConsumerSurveyAttachment?> GetByIdAsync(int id)
        {
            return await _db.ConsumerSurveyAttachments
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        // -----------------------------
        // UPLOAD
        // -----------------------------
        public async Task UploadAsync(
            int surveySno,
            IEnumerable<IFormFile> files,
            CancellationToken cancellationToken = default)
        {
            if (files == null)
                return;

            var basePath = Path.Combine(
                _env.WebRootPath,
                "uploads",
                "consumer-survey",
                surveySno.ToString());

            Directory.CreateDirectory(basePath);

            foreach (var file in files)
            {
                if (file.Length == 0)
                    continue;

                ValidateFile(file);

                var extension = Path.GetExtension(file.FileName);
                var storedFileName = $"{Guid.NewGuid()}{extension}";
                var fullPath = Path.Combine(basePath, storedFileName);

                await using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream, cancellationToken);
                }

                var attachment = new ConsumerSurveyAttachment
                {
                    ConsumerSurveySno = surveySno,
                    OriginalFileName = file.FileName,
                    StoredFileName = storedFileName,
                    FilePath = fullPath,
                    ContentType = file.ContentType,
                    FileSize = file.Length,
                    UploadedOn = DateTime.UtcNow
                };

                _db.ConsumerSurveyAttachments.Add(attachment);
            }

            await _db.SaveChangesAsync(cancellationToken);
        }

        // -----------------------------
        // DELETE (Soft)
        // -----------------------------
        public async Task DeleteAsync(int id)
        {
            var attachment = await _db.ConsumerSurveyAttachments
                .FirstOrDefaultAsync(a => a.Id == id);

            if (attachment == null)
                return;

            attachment.IsDeleted = true;

            await _db.SaveChangesAsync();
        }

        // -----------------------------
        // VALIDATION
        // -----------------------------
        private static void ValidateFile(IFormFile file)
        {
            if (file.Length > MaxFileSize)
                throw new InvalidOperationException(
                    $"File '{file.FileName}' exceeds maximum size limit.");

            var extension = Path.GetExtension(file.FileName);
            if (!AllowedExtensions.Contains(extension))
                throw new InvalidOperationException(
                    $"File type '{extension}' is not allowed.");
        }
    }
}

