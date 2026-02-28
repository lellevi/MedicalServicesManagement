using AutoMapper;
using MedicalServicesManagement.BLL.Managers;
using Microsoft.AspNetCore.Mvc;

namespace MedicalServicesManagement.WebApp.Controllers
{
    public class MedSpecialitiesController : Controller
    {
        private readonly MedSpecialityManager _medSpecialityManager;
        private readonly IMapper _mapper;

        public MedSpecialitiesController(MedSpecialityManager medSpecialityManager, IMapper mapper)
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
    }
}
