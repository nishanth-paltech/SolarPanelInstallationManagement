using Microsoft.AspNetCore.Mvc;
using SolarPanelInstallationManagement.Services.Contracts;

namespace SolarPanelInstallationManagement.Controllers
{
    [Route("ConsumerSurveyAttachment")]
    public class ConsumerSurveyAttachmentController : Controller
    {
        private readonly IConsumerSurveyAttachmentService _attachmentService;

        public ConsumerSurveyAttachmentController(
            IConsumerSurveyAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        // ------------------------------------
        // LIST ATTACHMENTS BY SURVEY
        // ------------------------------------
        [HttpGet("List/{surveySno}")]
        public async Task<IActionResult> List(int surveySno)
        {
            var attachments = await _attachmentService
                .GetBySurveySnoAsync(surveySno);

            return PartialView("_AttachmentsList", attachments);
        }

        // ------------------------------------
        // UPLOAD ATTACHMENTS
        // ------------------------------------
        [HttpPost("Upload")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(
            int surveySno,
            List<IFormFile> files)
        {
            if (surveySno <= 0)
                return BadRequest("Invalid survey reference.");

            if (files == null || files.Count == 0)
                return BadRequest("No files uploaded.");

            await _attachmentService.UploadAsync(surveySno, files);

            return Ok();
        }

        // ------------------------------------
        // DOWNLOAD ATTACHMENT
        // ------------------------------------
        [HttpGet("Download/{id}")]
        public async Task<IActionResult> Download(int id)
        {
            var attachment = await _attachmentService.GetByIdAsync(id);

            if (attachment == null)
                return NotFound();

            var fileBytes = await System.IO.File.ReadAllBytesAsync(
                attachment.FilePath);

            return File(
                fileBytes,
                attachment.ContentType,
                attachment.OriginalFileName);
        }

        // ------------------------------------
        // DELETE ATTACHMENT (SOFT DELETE)
        // ------------------------------------
        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _attachmentService.DeleteAsync(id);
            return Ok();
        }
    }
}
