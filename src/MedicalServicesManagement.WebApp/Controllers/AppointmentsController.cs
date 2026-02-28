using AutoMapper;
using MedicalServicesManagement.BLL.Managers;
using Microsoft.AspNetCore.Mvc;

namespace MedicalServicesManagement.WebApp.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly AppointmentManager _appointmentManager;
        private readonly IMapper _mapper;

        public AppointmentsController(AppointmentManager appointmentManager, IMapper mapper)
        {
            _appointmentManager = appointmentManager;
            _mapper = mapper;
        }


    }
}
