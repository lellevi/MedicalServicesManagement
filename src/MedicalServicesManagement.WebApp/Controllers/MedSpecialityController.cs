using AutoMapper;
using MedicalServicesManagement.BLL.Managers;
using Microsoft.AspNetCore.Mvc;

namespace MedicalServicesManagement.WebApp.Controllers
{
    public class MedSpecialityController : Controller
    {
        private readonly MedSpecialityManager _medSpecialityManager;
        private readonly IMapper _mapper;

        public MedSpecialityController(MedSpecialityManager medSpecialityManager, IMapper mapper)
        {
            _medSpecialityManager = medSpecialityManager;
            _mapper = mapper;
        }


    }
}
