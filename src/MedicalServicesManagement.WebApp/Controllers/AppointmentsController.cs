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
        private readonly IManager<ServiceDTO> _serviceManager;
        private readonly IEntityUserManager _entityUserManager;
        private readonly IMapper _mapper;

        public AppointmentsController(IAppointmentManager appointmentManager, IManager<ServiceDTO> serviceManager, IEntityUserManager entityUserManager, IMapper mapper)
        {
            _appointmentManager = appointmentManager;
            _serviceManager = serviceManager;
            _entityUserManager = entityUserManager;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var items = await _appointmentManager.GetAllIncludingServiceAndMedicAsync();
                var model = _mapper.Map<List<AppointmentViewModel>>(items);
                return View(model);
            }
            catch
            {
                return View(new List<AppointmentViewModel>());
            }
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var items = await _appointmentManager.GetAllAsync();
            return Json(items);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View("Error", new ErrorViewModel("Ошибка редактирования записи."));
            }

            var appointmentDto = await _appointmentManager.GetByIdAsync(id);
            var resultAppointment = _mapper.Map<AppointmentViewModel>(appointmentDto);
            return View(resultAppointment);
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] string id, AppointmentViewModel model)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(id))
            {
                return View(model);
            }

            try
            {
                await _appointmentManager.UpdateAsync(_mapper.Map<AppointmentDTO>(model));
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

        [HttpGet("choose")]
        public async Task<IActionResult> Choose([FromQuery] string medicId, [FromQuery] string serviceId, [FromQuery] string patientId)
        {
            var serviceDto = await _serviceManager.GetByIdAsync(serviceId);

            var avaibleAppointments = await _appointmentManager.GetAllFreeByMedicAndServiceOrderedAsync(serviceId, medicId);

            var model = new TakeAppointmentViewModel
            {
                Service = _mapper.Map<ServiceViewModel>(serviceDto),
                AvailableAppointments = _mapper.Map<List<AppointmentViewModel>>(avaibleAppointments),
            };

            if (!string.IsNullOrEmpty(medicId))
            {
                var medic = await _entityUserManager.GetByIdAsync(medicId);
                model.Medic = _mapper.Map<UserViewModel>(medic);
            }

            if (!string.IsNullOrEmpty(patientId))
            {
                model.Patient = _mapper.Map<UserViewModel>(await _entityUserManager.GetByIdAsync(patientId));
            }
            else if (User.Identity.IsAuthenticated)
            {
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                           User.FindFirst("sub")?.Value ??
                           User.FindFirst("id")?.Value;

                if (!string.IsNullOrEmpty(currentUserId))
                {
                    model.Patient = _mapper.Map<UserViewModel>(await _entityUserManager.GetByIdAsync(currentUserId));
                }
            }

            return View(model);
        }

        [HttpGet("take")]
        public async Task<IActionResult> Take(string appointmentId, string patientId)
        {
            var appointmentDto = await _appointmentManager.GetByIdAsync(appointmentId);
            appointmentDto.PatientId = patientId;
            await _appointmentManager.UpdateAsync(appointmentDto);

            var appointment = _mapper.Map<AppointmentViewModel>(appointmentDto);

            appointment.Medic = _mapper.Map<UserViewModel>(await _entityUserManager.GetByIdAsync(appointment.MedicId));
            appointment.Service = _mapper.Map<ServiceViewModel>(await _serviceManager.GetByIdAsync(appointment.ServiceId));

            var patientDto = await _entityUserManager.GetByIdAsync(patientId);
            var patient = _mapper.Map<UserViewModel>(patientDto);

            var model = new TakeConfirmViewModel
            {
                Appointment = appointment,
                Patient = patient,
            };

            model.Appointment.PatientId = patientId;
            return View(model);
        }

        [HttpPost("take")]
        public async Task<IActionResult> Take(TakeConfirmViewModel model)
        {
            if (!ModelState.IsValid || model.Appointment == null)
            {
                return View(model);
            }

            try
            {
                model.Appointment.Status = Enums.AppointmentStatus.Taken;
                await _appointmentManager.UpdateAsync(_mapper.Map<AppointmentDTO>(model.Appointment));

                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        [HttpGet("patientAppointments")]
        public async Task<IActionResult> PatientAppointments()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Account");
                }

                var currentUserAuthId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                                   User.FindFirst("userId")?.Value;

                var currentUser = await _entityUserManager.GetByAuthIdAsync(currentUserAuthId);

                if (string.IsNullOrEmpty(currentUser.Id))
                {
                    return View(new List<AppointmentViewModel>());
                }

                var allAppointments = await _appointmentManager.GetAllIncludingServiceAndMedicAsync();
                var userAppointments = allAppointments.Where(a => a.PatientId == currentUser.Id && a.Status != BLL.Enums.AppointmentStatus.DonePaid).ToList();

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
