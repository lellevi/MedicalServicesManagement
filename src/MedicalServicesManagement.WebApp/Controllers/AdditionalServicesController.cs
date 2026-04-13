using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedicalServicesManagement.WebApp.Controllers
{
    [Route("[controller]")]
    public class AdditionalServicesController : Controller
    {
        private readonly IManager<AdditionalServiceDTO> _additionalServiceManager;
        private readonly IMapper _mapper;

        public AdditionalServicesController(IManager<AdditionalServiceDTO> additionalServiceManager, IMapper mapper)
        {
            _additionalServiceManager = additionalServiceManager;
            _mapper = mapper;
        }
    }
}
