using AutoMapper;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.BLL.Managers;
using Microsoft.AspNetCore.Mvc;

namespace MedicalServicesManagement.WebApp.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public ServiceController(ServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }


    }
}
