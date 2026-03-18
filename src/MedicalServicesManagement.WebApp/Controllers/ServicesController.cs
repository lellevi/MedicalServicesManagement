using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Managers;
using MedicalServicesManagement.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedicalServicesManagement.WebApp.Controllers
{
    [Route("[controller]")]
    public class ServicesController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public ServicesController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        [HttpGet("getAll")]
        public async Task<JsonResult> GetAll()
        {
            var items = await _serviceManager.GetAllAsync();
            return Json(items);
        }

        [HttpGet("services")]
        public async Task<IActionResult> Index()
        {
            var servicesDtos = await _serviceManager.GetAllAsync();

            var items = _mapper.Map<List<ServiceViewModel>>(servicesDtos);

            return View(items);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View("Error", new ErrorViewModel("Ошибка редактирования записи."));
            }

            var sevice = await _serviceManager.GetByIdAsync(id);
            var resultSevice = _mapper.Map<ServiceViewModel>(sevice);
            return View(resultSevice);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit(ServiceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _serviceManager.UpdateAsync(_mapper.Map<ServiceDTO>(model));

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
        public async Task<IActionResult> Add(ServiceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _serviceManager.CreateAsync(_mapper.Map<ServiceDTO>(model));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }

            return RedirectToAction("Index");
        }
    }
}
