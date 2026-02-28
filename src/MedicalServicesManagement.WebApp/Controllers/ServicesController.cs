using AutoMapper;
using MedicalServicesManagement.BLL.Managers;
using Microsoft.AspNetCore.Mvc;

namespace MedicalServicesManagement.WebApp.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public ServicesController(ServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }


    }
}
