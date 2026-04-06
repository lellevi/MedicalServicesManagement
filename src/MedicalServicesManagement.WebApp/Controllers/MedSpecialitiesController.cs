using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Managers;
using MedicalServicesManagement.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedicalServicesManagement.WebApp.Controllers
{
    [Route("[controller]")]
    public class MedSpecialitiesController : Controller
    {
        private readonly IMedSpecialityManager _medSpecialityManager;
        private readonly IMapper _mapper;

        public MedSpecialitiesController(IMedSpecialityManager medSpecialityManager, IMapper mapper)
        {
            _medSpecialityManager = medSpecialityManager;
            _mapper = mapper;
        }

        [HttpGet("getAll")]
        public async Task<JsonResult> GetAll()
        {
            var items = await _medSpecialityManager.GetAllAsync();
            return Json(items);
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var medSpecialitiesDtos = await _medSpecialityManager.GetAllAsync();

            var items = _mapper.Map<List<MedSpecialityViewModel>>(medSpecialitiesDtos);

            return View(items);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View("Error", new ErrorViewModel("Ошибка редактирования записи."));
            }

            var medSpeciality = await _medSpecialityManager.GetByIdAsync(id);
            var resultMedSpeciality = _mapper.Map<MedSpecialityViewModel>(medSpeciality);
            return View(resultMedSpeciality);
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] string id, MedSpecialityViewModel model)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(id))
            {
                return View(model);
            }

            try
            {
                await _medSpecialityManager.UpdateAsync(_mapper.Map<MedSpecialityDTO>(model));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet("add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(MedSpecialityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _medSpecialityManager.CreateAsync(_mapper.Map<MedSpecialityDTO>(model));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            try
            {
                await _medSpecialityManager.DeleteByIdAsync(id);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }

            return RedirectToAction("Index");
        }
    }
}
