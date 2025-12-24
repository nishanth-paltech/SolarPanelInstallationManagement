using Microsoft.AspNetCore.Mvc;
using SolarPanelInstallationManagement.Models.DTOs;
using SolarPanelInstallationManagement.Models.Entities;
using SolarPanelInstallationManagement.Services.Contracts;

namespace SolarPanelInstallationManagement.Controllers
{
    public class ConsumerSurveyController : Controller
    {
        private readonly IConsumerSurveyService _service;

        public ConsumerSurveyController(IConsumerSurveyService service)
        {
            _service = service;
        }

        // GET: /ConsumerSurvey
        public IActionResult Index()
        {
            return View();
        }

        // GET: /ConsumerSurvey/GetData
        [HttpPost]
        public async Task<IActionResult> GetData()
        {
            var request = new DataTableRequestDto
            {
                Draw = Convert.ToInt32(Request.Form["draw"]),
                Start = Convert.ToInt32(Request.Form["start"]),
                Length = Convert.ToInt32(Request.Form["length"]),
                SearchValue = Request.Form["search[value]"],
                SortColumn = Request.Form[$"columns[{Request.Form["order[0][column]"]}][data]"],
                SortDirection = Request.Form["order[0][dir]"]
            };

            // Per-column search
            for (int i = 0; i < Request.Form["columns"].Count; i++)
            {
                var columnName = Request.Form[$"columns[{i}][data]"];
                var columnSearch = Request.Form[$"columns[{i}][search][value]"];

                if (!string.IsNullOrWhiteSpace(columnName) &&
                    !string.IsNullOrWhiteSpace(columnSearch))
                {
                    request.ColumnSearch[columnName!] = columnSearch;
                }
            }

            var result = await _service.GetDataTableAsync(request);
            return Json(result);
        }

        // GET: /ConsumerSurvey/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /ConsumerSurvey/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConsumerSurvey model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _service.AddAsync(model);
            return RedirectToAction(nameof(Index));
        }

        // GET: /ConsumerSurvey/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            return View(entity);
        }

        // POST: /ConsumerSurvey/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ConsumerSurvey model)
        {
            if (!ModelState.IsValid)
                return View(model);

            //await _service.UpdateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        // POST: /ConsumerSurvey/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int? id)
        {
            if (id == null)
                return PartialView("_AddEditModal", new ConsumerSurvey());

            var entity = await _service.GetByIdAsync(id.Value);
            if (entity == null)
                return NotFound();

            return PartialView("_AddEditModal", entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEdit(ConsumerSurvey model)
        {
            if (!ModelState.IsValid)
                return PartialView("_AddEditModal", model);

            if (model.Sno == 0)
                await _service.AddAsync(model);
            else
                await _service.UpdateAsync(model);

            return Json(new { success = true });
        }

    }
}
