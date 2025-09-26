using Microsoft.AspNetCore.Mvc;
using TicketSys.Application.DTOs;
using TicketSys.Application.Interfaces;

namespace TicketSys.WebUI.Controllers
{
    public class UnitController(IUnitService unitService) : Controller
    {
        private readonly IUnitService _unitService = unitService;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var units = await _unitService.GetAllUnits();
            return View(units);
        }


        [HttpGet()]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UnitDTO unit)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View(unit);
            }

            await _unitService.Add(unit);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetUsuarios()
        {
            var usuarios = new List<object>

            {
                new { id = 1, nome = "Matheus", email = "matheus@email.com", dataCriacao = DateTime.Now },
                new { id = 2, nome = "João", email = "joao@email.com", dataCriacao = DateTime.Now }
            };

            return Json(usuarios);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
                return NotFound();

            var unitDTO = await _unitService.GetById(id);

            if (unitDTO == null)
                return NotFound();

            return View(unitDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UnitDTO unit)
        {
            if (!ModelState.IsValid)
            {
                return View(unit);
            }

            await _unitService.Update(unit);
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest();

            await _unitService.Remove(id);

            return NoContent();
        }
    }
}
