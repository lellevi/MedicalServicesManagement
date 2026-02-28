using AutoMapper;
using MedicalServicesManagement.BLL.Managers;
using Microsoft.AspNetCore.Mvc;

namespace MedicalServicesManagement.WebApp.Controllers
{
    public class AppointmentServicesController : Controller
    {
        private readonly AppointmentServiceManager _appointmentServiceManager;
        private readonly IMapper _mapper;

        public AppointmentServicesController(AppointmentServiceManager appointmentServiceManager, IMapper mapper)
        {
            _appointmentServiceManager = appointmentServiceManager;
            _mapper = mapper;
        }


    }
}
