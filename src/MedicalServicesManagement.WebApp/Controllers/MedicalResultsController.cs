using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalServicesManagement.WebApp.Controllers
{
    [Route("[controller]")]
    public class MedicalResultsController : Controller
    {
        private readonly IMedicalResultMongoManager _mongoManager;
        private readonly IAppointmentManager _appointmentManager;
        private readonly IServiceManager _serviceManager;
        private readonly IEntityUserManager _entityUserManager;
        private readonly IAppointmentServiceManager _appointmentServiceManager;
        private readonly IAdditionalServiceManager _additionalServiceManager;
        private readonly IMapper _mapper;

        public MedicalResultsController(
            IMedicalResultMongoManager mongoManager,
            IAppointmentManager appointmentManager,
            IServiceManager serviceManager,
            IEntityUserManager entityUserManager,
            IAdditionalServiceManager additionalServiceManager,
            IAppointmentServiceManager appointmentServiceManager,
            IMapper mapper)
        {
            _mongoManager = mongoManager;
            _appointmentManager = appointmentManager;
            _serviceManager = serviceManager;
            _entityUserManager = entityUserManager;
            _additionalServiceManager = additionalServiceManager;
            _appointmentServiceManager = appointmentServiceManager;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var dtos = await _mongoManager.GetAllDtosAsync();
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
            await _mongoManager.CreateAsync(dto);

            return RedirectToAction("Index");
        }

        [HttpGet("create/{appointmentId}")]
        public async Task<IActionResult> Create(string appointmentId)
        {
            var model = new MedicalResultViewModel
            {
                AppointmentId = appointmentId,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
            };

            return View(model);
        }

        [HttpGet("details/{appointmentId}")]
        public async Task<IActionResult> Details(string appointmentId)
        {
            var appointmentDto = await _appointmentManager.GetByIdIncludingServiceAndMedicAsync(appointmentId);
            var appointment = _mapper.Map<AppointmentViewModel>(appointmentDto);

            var patientDto = await _entityUserManager.GetByIdAsync(appointment.PatientId);
            var patient = _mapper.Map<UserViewModel>(patientDto);

            var medicDto = await _entityUserManager.GetByIdAsync(appointment.MedicId);
            var medic = _mapper.Map<UserViewModel>(medicDto);

            var serviceDto = await _serviceManager.GetByIdAsync(appointment.ServiceId);
            var service = _mapper.Map<ServiceViewModel>(serviceDto);

            var additionalServicesDtos = await _additionalServiceManager.GetAllIncludingSpecialitiesAsync();
            var additionalServices = _mapper.Map<List<AdditionalServiceViewModel>>(additionalServicesDtos);

            var appointmentServicesDtos = await _appointmentServiceManager.GetByAppointmentIdAsync(appointmentId);
            var appointmentServices = _mapper.Map<List<AppointmentServiceViewModel>>(appointmentServicesDtos);

            var medicalResultDto = await _mongoManager.GetByAppointmentIdAsync(appointmentId);
            var medicalResult = medicalResultDto == null
                ? new MedicalResultViewModel
                {
                    Patient = patient,
                    Medic = medic,
                    Appointment = new AppointmentViewModel(),
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = DateTime.UtcNow,
                    ExaminationData = string.Empty,
                    Diagnosis = string.Empty,
                    Recommendations = string.Empty,
                }
                : _mapper.Map<MedicalResultViewModel>(medicalResultDto);

            var isReadOnly = DateTime.UtcNow > appointment.EndDate.AddHours(1);

            var model = new MedicalResultDetailsViewModel
            {
                Appointment = appointment,
                Patient = patient,
                Medic = medic,
                Service = service,
                AdditionalServices = additionalServices,
                AppointmentServices = appointmentServices,
                MedicalResult = medicalResult,
                IsReadOnly = isReadOnly,
            };

            if (appointmentServices.Count > 0)
            {
                var totalCost = appointment.TotalCost + appointment.Service.Cost + appointmentServices
                    .Sum(s => s.AdditionalService.Price * s.Amount);
                model.TotalCost = totalCost;
            }

            return View(model);
        }

        [HttpPost("details/{appointmentId}")]
        public async Task<IActionResult> Update(string appointmentId, MedicalResultDetailsViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Details", new { appointmentId });

            var dto = _mapper.Map<MedicalResultDto>(model.MedicalResult);
            await _mongoManager.CreateOrUpdateByAppointmentIdAsync(appointmentId, dto);

            return RedirectToAction("Index", "MedicalResults");
        }
    }
}