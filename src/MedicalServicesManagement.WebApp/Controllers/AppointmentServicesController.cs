using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.BLL.Managers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MedicalServicesManagement.WebApp.Controllers
{
    [Route("[controller]")]
    public class AppointmentServicesController : Controller
    {
        private readonly IManager<AppointmentServiceDTO> _appointmentServiceManager;
        private readonly IMapper _mapper;

        public AppointmentServicesController(IManager<AppointmentServiceDTO> appointmentServiceManager, IMapper mapper)
        {
            _appointmentServiceManager = appointmentServiceManager;
            _mapper = mapper;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(
            [FromForm] string appointmentId,
            [FromForm] string additionalServiceId,
            [FromForm] int amount)
        {
            if (string.IsNullOrEmpty(appointmentId)
                || string.IsNullOrEmpty(additionalServiceId)
                || amount < 1)
            {
                return BadRequest();
            }

            var dto = new AppointmentServiceDTO
            {
                AppointmentId = appointmentId,
                AdditionalServiceId = additionalServiceId,
                Amount = amount
            };

            await _appointmentServiceManager.CreateAsync(dto);

            return RedirectToAction("Details", "MedicalResults", new { appointmentId });
        }
    }
}
