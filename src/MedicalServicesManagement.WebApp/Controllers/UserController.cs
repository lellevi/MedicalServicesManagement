using AutoMapper;
using MedicalServicesManagement.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedicalServicesManagement.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IEntityUserManager _userManager;
        private readonly IMapper _mapper;

        public UserController(IEntityUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }


    }
}
