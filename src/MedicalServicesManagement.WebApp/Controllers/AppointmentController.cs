using AutoMapper;
using MedicalServicesManagement.BLL.Managers;
using Microsoft.AspNetCore.Mvc;

namespace MedicalServicesManagement.WebApp.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly AppointmentManager _appointmentManager;
        private readonly IMapper _mapper;

        public AppointmentController(AppointmentManager appointmentManager, IMapper mapper)
        {
            _appointmentManager = appointmentManager;
            _mapper = mapper;
        }


    }
}
