using AutoMapper;
using MedicalServicesManagement.BLL.Managers;
using Microsoft.AspNetCore.Mvc;

namespace MedicalServicesManagement.WebApp.Controllers
{
    public class AppointmentServiceController : Controller
    {
        private readonly AppointmentServiceManager _appointmentServiceManager;
        private readonly IMapper _mapper;

        public AppointmentServiceController(AppointmentServiceManager appointmentServiceManager, IMapper mapper)
        {
            _appointmentServiceManager = appointmentServiceManager;
            _mapper = mapper;
        }


    }
}
