using AutoMapper;
using MedicalServicesManagement.BLL.Managers;
using Microsoft.AspNetCore.Mvc;

namespace MedicalServicesManagement.WebApp.Controllers
{
    [Route("[controller]")]
    public class AppointmentServicesController : Controller
    {
        private readonly IAppointmentServiceManager _appointmentServiceManager;
        private readonly IMapper _mapper;

        public AppointmentServicesController(IAppointmentServiceManager appointmentServiceManager, IMapper mapper)
        {
            _appointmentServiceManager = appointmentServiceManager;
            _mapper = mapper;
        }
    }
}
