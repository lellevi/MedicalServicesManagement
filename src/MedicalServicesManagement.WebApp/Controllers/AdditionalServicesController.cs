using AutoMapper;
using MedicalServicesManagement.BLL.Managers;
using Microsoft.AspNetCore.Mvc;

namespace MedicalServicesManagement.WebApp.Controllers
{
    [Route("[controller]")]
    public class AdditionalServicesController : Controller
    {
        private readonly IAdditionalServiceManager _additionalServiceManager;
        private readonly IMapper _mapper;

        public AdditionalServicesController(IAdditionalServiceManager additionalServiceManager, IMapper mapper)
        {
            _additionalServiceManager = additionalServiceManager;
            _mapper = mapper;
        }
    }
}
