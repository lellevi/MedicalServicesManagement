using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Managers;
using MedicalServicesManagement.WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedicalServicesManagement.WebApp.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IEntityUserManager _userManager;
        private readonly IMapper _mapper;

        public UsersController(IEntityUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var usersDtoDictionary = await _userManager.GetAllByRolesAsync(new List<string> { Constants.GuestRole, Constants.PatientRole, Constants.MedicRole, Constants.ReceptionistRole });
            var items = _mapper.Map<Dictionary<string, List<UserViewModel>>>(usersDtoDictionary);

            return View(items);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View("Error", new ErrorViewModel("Ошибка редактирования пользователя."));
            }

            var user = await _userManager.GetByIdIncludingRoles(id);
            var resultUser = _mapper.Map<UserViewModel>(user);

            string[] roles =
            {
                Constants.PatientRole,
                Constants.MedicRole,
                Constants.ReceptionistRole,
            };

            ViewData["Roles"] = roles;
            return View(resultUser);
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel model)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(id))
            {
                return View(model);
            }

            try
            {
                await _userManager.UpdateAsync(_mapper.Map<EntityUserDTO>(model));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }

            return RedirectToAction("Index");
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
