using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.BLL.Jwt;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.WebApp.Models;
using MedicalServicesManagement.WebApp.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalServicesManagement.WebApp.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IEntityUserManager _userManager;
        private readonly UserManager<AuthUser> _identityUserManager;
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public UsersController(IEntityUserManager userManager, IMapper mapper, IServiceManager serviceManager, UserManager<AuthUser> identityUserManager)
        {
            _userManager = userManager;
            _serviceManager = serviceManager;
            _identityUserManager = identityUserManager;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var roles = new List<string> { Constants.GuestRole, Constants.PatientRole, Constants.MedicRole, Constants.ReceptionistRole };

            return View(roles);
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

        [HttpGet("allMedics")]
        public async Task<JsonResult> AllMedicsJson()
        {
            var medicsDto = await _userManager.GetMedicsAsync();
            var result = _mapper.Map<List<UserViewModel>>(medicsDto);
            return Json(result);
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

        [HttpGet("medic/{id}")]
        public async Task<IActionResult> Medic([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View("Error", new ErrorViewModel("Ошибка просмотра услуг, врач не найден."));
            }

            var medicDto = await _userManager.GetByIdIncludingRoles(id);
            if (string.IsNullOrEmpty(medicDto.MedSpecialityId))
            {
                var model = _mapper.Map<UserViewModel>(medicDto);
                model.Services = new List<ServiceViewModel>();
                return View(model);
            }

            var resultModel = _mapper.Map<UserViewModel>(medicDto);
            var medicServices = await _serviceManager.GetByMedSpecialityIdAsync(resultModel.MedSpecialityId);

            resultModel.Services = _mapper.Map<List<ServiceViewModel>>(medicServices);

            return View(resultModel);
        }

        [HttpGet("newPatient")]
        public IActionResult NewPatient()
        {
            return View();
        }

        [HttpPost("newPatient")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewPatient(RegisterModel model)
        {
            ArgumentNullException.ThrowIfNull(model);
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new AuthUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = model.Email,
                UserName = model.Email,
                NormalizedUserName = model.Email.ToUpperInvariant(),
                NormalizedEmail = model.Email.ToUpperInvariant(),
                EmailConfirmed = true,
                LockoutEnabled = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
            };

            var creationResult = await _identityUserManager.CreateAsync(user, model.Password);

            if (creationResult.Succeeded)
            {
                await _identityUserManager.AddToRoleAsync(user, Constants.PatientRole);

                var entityUser = new EntityUserDTO()
                {
                    AuthUserId = user.Id,
                    Surname = model.Surname,
                    Name = model.Name,
                    MiddleName = model.MiddleName,
                    BirthDate = model.BirthDate,
                    Telephone = model.Telephone,
                };

                await _userManager.CreateAsync(entityUser);

                return RedirectToAction("Patient", new { id = entityUser.Id });
            }

            foreach (var error in creationResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpGet("patient/{id}")]
        public async Task<IActionResult> Patient(string id)
        {
            var userDto = await _userManager.GetByIdAsync(id);
            var model = _mapper.Map<UserViewModel>(userDto);
            var authUser = await _identityUserManager.FindByIdAsync(model.AuthUserId);
            model.IsGuest = await _identityUserManager.IsInRoleAsync(authUser, Constants.GuestRole);

            return View(model);
        }

        [HttpGet("makePatient/{id}")]
        public async Task<IActionResult> MakePatient(string id, string entityUserId)
        {
            var authUser = await _identityUserManager.FindByIdAsync(id);
            if (await _identityUserManager.IsInRoleAsync(authUser, Constants.GuestRole))
            {
                await _identityUserManager.RemoveFromRoleAsync(authUser, Constants.GuestRole);
                await _identityUserManager.AddToRoleAsync(authUser, Constants.PatientRole);
            }

            return RedirectToAction("Patient", new { id = entityUserId });
        }

        [HttpGet("byRole")]
        public async Task<JsonResult> ByRole([FromQuery] string role)
        {
            var usersDtoList = await _userManager.GetAllByRoleAsync(role);
            var items = _mapper.Map<List<UserViewModel>>(usersDtoList);

            return Json(items);
        }
    }
}
