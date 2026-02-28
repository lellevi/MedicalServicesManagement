using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Managers;
using Microsoft.AspNetCore.Mvc;

namespace MedicalServicesManagement.WebApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IEntityUserManager _userManager;
        private readonly IMapper _mapper;

        public UsersController(IEntityUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("searchMedics")]
        public async Task<IActionResult> SearchMedics()
        {
            return View();
        }

        [HttpGet("searchDoc")]
        public async Task<JsonResult> SearchMedicsJson([FromQuery] string surname = null, [FromQuery] string specialityId = null)
        {
            if (surname != null)
            {
                var items = await _userManager.GetMedicsBySurnameAsync(surname);
                return Json(items);
            }

            if (specialityId != null)
            {
                var items = await _userManager.GetMedicsBySpecialityAsync(specialityId);
                return Json(items);
            }

            return Json(new List<EntityUserDTO>());
        }


    }
}
