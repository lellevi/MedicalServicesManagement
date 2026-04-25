using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.BLL.Managers;
using Microsoft.AspNetCore.Mvc;

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
    }
}
