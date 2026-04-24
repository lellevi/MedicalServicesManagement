using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MedicalServicesManagement.WebApp.Controllers
{
    [Route("[controller]")]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentManager _appointmentManager;
        private readonly IServiceManager _serviceManager;
        private readonly IEntityUserManager _entityUserManager;
        private readonly IMapper _mapper;

        public AppointmentsController(IAppointmentManager appointmentManager, IServiceManager serviceManager, IEntityUserManager entityUserManager, IMapper mapper)
        {
            _appointmentManager = appointmentManager;
            _serviceManager = serviceManager;
            _entityUserManager = entityUserManager;
            _mapper = mapper;
        }

        private string GetUserIdFromClaims()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return null;
            }

            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                   ?? User.FindFirst("sub")?.Value
                   ?? User.FindFirst("userId")?.Value;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index(
            [FromQuery] string specialityId = null,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] string medicId = null,
            [FromQuery] string status = null)
        {
            try
            {
                BLL.Enums.AppointmentStatus? statusEnum = null;

                if (!string.IsNullOrEmpty(status) && Enum.TryParse<BLL.Enums.AppointmentStatus>(status, out var parsed))
                {
                    statusEnum = parsed;
                }

                var dtos = await _appointmentManager.GetFilteredAppointmentsAsync(
                    specialityId,
                    medicId,
                    statusEnum,
                    startDate,
                    endDate);

                var model = _mapper.Map<List<AppointmentViewModel>>(dtos);
                return View(model);
            }
            catch
            {
                return View(new List<AppointmentViewModel>());
            }
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll([FromQuery] string specialityId = null)
        {
            var items = await _appointmentManager.GetAllAsync(specialityId);

            return Json(items);
        }

        [HttpGet("add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(AppointmentViewModel model)
        {
            if (model is null || !ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                model.Status = Enums.AppointmentStatus.Free;
                await _appointmentManager.CreateAsync(_mapper.Map<AppointmentDTO>(model));
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
                await _appointmentManager.DeleteByIdAsync(id);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpGet("searchForPatient/{id}")]
        public async Task<IActionResult> SearchForPatient([FromRoute] string id)
        {
            var patient = await _entityUserManager.GetByIdAsync(id);
            var services = await _serviceManager.GetAllIncludingSpecialitiesAsync();

            var model = new TakeAppointmentForPatientViewModel
            {
                PatientId = id,
                Patient = _mapper.Map<UserViewModel>(patient),
                Services = _mapper.Map<List<ServiceViewModel>>(services),
            };

            return View(model);
        }

        [HttpPost("searchForPatient")]
        public async Task<IActionResult> SearchForPatient([FromForm] TakeAppointmentForPatientViewModel model)
        {
            var serviceDto = await _serviceManager.GetByIdAsync(model.ServiceId);

            var avaibleAppointments = await _appointmentManager.GetAllFreeByMedicAndServiceOrderedAsync(model.ServiceId, model.MedicId);

            model.Service = _mapper.Map<ServiceViewModel>(serviceDto);
            model.AvailableAppointments = _mapper.Map<List<AppointmentViewModel>>(avaibleAppointments);

            if (!string.IsNullOrEmpty(model.MedicId))
            {
                var medic = await _entityUserManager.GetByIdAsync(model.MedicId);
                model.Medic = _mapper.Map<UserViewModel>(medic);
            }

            return RedirectToAction("ChooseForPatient", model);
        }

        [HttpGet("chooseForPatient")]
        public async Task<IActionResult> ChooseForPatient(string patientId, string serviceId, string medicId)
        {
            var serviceDto = await _serviceManager.GetByIdAsync(serviceId);
            var service = _mapper.Map<ServiceViewModel>(serviceDto);
            var availableAppointmentsDtos = await _appointmentManager.GetAllFreeByMedicAndServiceOrderedAsync(serviceId, medicId);
            var availableAppointments = _mapper.Map<List<AppointmentViewModel>>(availableAppointmentsDtos);

            var model = new TakeAppointmentForPatientViewModel
            {
                PatientId = patientId,
                ServiceId = serviceId,
                MedicId = medicId,
                Service = service,
                AvailableAppointments = availableAppointments,
                AppointmentsByDate = availableAppointments.GroupBy(a => a.StartDate.Date).ToDictionary(g => g.Key, g => g.ToList()),
            };

            if (!string.IsNullOrEmpty(medicId))
            {
                var medic = await _entityUserManager.GetByIdAsync(medicId);
                model.Medic = _mapper.Map<UserViewModel>(medic);
            }

            return View(model);
        }

        [HttpGet("choose")]
        public async Task<IActionResult> Choose(string serviceId, string medicId)
        {
            var serviceDto = await _serviceManager.GetByIdAsync(serviceId);
            var service = _mapper.Map<ServiceViewModel>(serviceDto);
            var availableAppointmentsDtos = await _appointmentManager.GetAllFreeByMedicAndServiceOrderedAsync(serviceId, medicId);
            var availableAppointments = _mapper.Map<List<AppointmentViewModel>>(availableAppointmentsDtos);

            var model = new TakeAppointmentViewModel
            {
                Service = service,
                AvailableAppointments = availableAppointments,
                AppointmentsByDate = availableAppointments.GroupBy(a => a.StartDate.Date).ToDictionary(g => g.Key, g => g.ToList()),
            };

            if (!string.IsNullOrEmpty(medicId))
            {
                var medicDto = await _entityUserManager.GetByIdAsync(medicId);
                model.Medic = _mapper.Map<UserViewModel>(medicDto);
            }

            return View(model);
        }

        [HttpGet("take")]
        public async Task<IActionResult> Take(string appointmentId, string patientId)
        {
            if (string.IsNullOrEmpty(patientId))
            {
                patientId = GetUserIdFromClaims();
            }

            var patientDto = await _entityUserManager.GetByAuthIdAsync(patientId);
            var patient = _mapper.Map<UserViewModel>(patientDto);

            var appointmentDto = await _appointmentManager.GetByIdIncludingServiceAndMedicAsync(appointmentId);
            var appointment = _mapper.Map<AppointmentViewModel>(appointmentDto);

            var model = new TakeConfirmViewModel
            {
                Appointment = appointment,
                Patient = patient,
                AppointmentId = appointment.Id,
                PatientId = patient.Id,
            };

            return View(model);
        }

        [HttpPost("take")]
        public async Task<IActionResult> Take(TakeConfirmViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _appointmentManager.MarkAsTakenAsync(model.AppointmentId, model.PatientId);
                return RedirectToAction("Index", "Profile");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                var appointmentDto = await _appointmentManager.GetByIdIncludingServiceAndMedicAsync(model.AppointmentId);
                model.Appointment = _mapper.Map<AppointmentViewModel>(appointmentDto);
                var patientDto = await _entityUserManager.GetByIdAsync(model.PatientId);
                model.Patient = _mapper.Map<UserViewModel>(patientDto);

                return View(model);
            }
        }

        [HttpGet("my")]
        public async Task<IActionResult> PatientAppointments()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Account");
                }

                var currentUserAuthId = GetUserIdFromClaims();

                var currentUser = await _entityUserManager.GetByAuthIdAsync(currentUserAuthId);

                if (string.IsNullOrEmpty(currentUser.Id))
                {
                    return View(new List<AppointmentViewModel>());
                }

                var allAppointments = await _appointmentManager.GetAllIncludingServiceAndMedicAsync();
                var userAppointments = allAppointments.Where(
                    a => a.PatientId == currentUser.Id &&
                    (a.Status == BLL.Enums.AppointmentStatus.Taken ||
                    a.Status == BLL.Enums.AppointmentStatus.DoneNoPay))
                    .ToList();

                var model = _mapper.Map<List<AppointmentViewModel>>(userAppointments);
                return View(model);
            }
            catch
            {
                return View(new List<AppointmentViewModel>());
            }
        }

        [HttpPost("free")]
        public async Task<IActionResult> Free(string appointmentId)
        {
            if (!string.IsNullOrEmpty(appointmentId))
            {
                await _appointmentManager.MarkAsFreeAsync(appointmentId);
            }

            if (User.IsInRole(Constants.PatientRole))
            {
                return RedirectToAction("PatientAppointments", "Appointments");
            }

            return RedirectToAction("Index", "Appointments");
        }

        [HttpGet("history")]
        public async Task<IActionResult> HistoryPatientAppointments()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Account");
                }

                var currentUserAuthId = GetUserIdFromClaims();
                var currentUser = await _entityUserManager.GetByAuthIdAsync(currentUserAuthId);

                if (string.IsNullOrEmpty(currentUser.Id))
                {
                    return View(new List<AppointmentViewModel>());
                }

                var allAppointments = await _appointmentManager.GetAllIncludingServiceAndMedicAsync();
                var userAppointments = allAppointments.Where(
                    a => a.PatientId == currentUser.Id &&
                    a.Status == BLL.Enums.AppointmentStatus.DonePaid).ToList();

                var model = _mapper.Map<List<AppointmentViewModel>>(userAppointments);
                return View(model);
            }
            catch
            {
                return View(new List<AppointmentViewModel>());
            }
        }
    }
}
