using AutoMapper;
using MedicalServicesManagement.BLL.Managers;
using Microsoft.AspNetCore.Mvc;

namespace MedicalServicesManagement.WebApp.Controllers
{
    public class AdditionalServiceController : Controller
    {
        private readonly AdditionalServiceManager _additionalServiceManager;
        private readonly IMapper _mapper;

        public AdditionalServiceController(AdditionalServiceManager additionalServiceManager, IMapper mapper)
        {
            _additionalServiceManager = additionalServiceManager;
            _mapper = mapper;
        }


    }
}
