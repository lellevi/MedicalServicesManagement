using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.BLL.Managers;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalServicesManagement.WebApp.Controllers
{
    [Route("[controller]")]
    public class MedicalResultsController : Controller
    {
        private readonly IMongoBaseManager<MedicalResult, MedicalResultDto> _manager;
        private readonly IMapper _mapper;

        public MedicalResultsController(
            IMongoBaseManager<MedicalResult, MedicalResultDto> manager,
            IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var dtos = await _manager.GetAllDtosAsync();
            var model = _mapper.Map<List<MedicalResultViewModel>>(dtos);
            return View(model);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(MedicalResultViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = _mapper.Map<MedicalResultDto>(model);
            await _manager.CreateAsync(dto);

            return RedirectToAction("Index");
        }
    }
}
