using AutoMapper;
using MedicalServicesManagement.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedicalServicesManagement.WebApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IEntityUserManager _userManager;
        private readonly IMapper _mapper;

        public ProfileController(IEntityUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Auth");

            /* Load necessary data */

            return View();
        }

    }
}
