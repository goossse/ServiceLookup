using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLookup.BL.DTO;
using ServiceLookup.BL.Services.Implementations;
using ServiceLookup.BL.Services.Interfaces;
using ServiceLookup.DAL;
using ServiceLookup.DAL.Entity;
using ServiceLookup.Mapper;
using ServiceLookup.Models.ManageVM;

namespace ServiceLookup.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private readonly IService serviceService;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public ManageController(UserManager<User> _userManager, ApplicationDbContext db)
        {
            serviceService = new ServiceService(db);
            userManager = _userManager;
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperView());
            });
            mapper = mappingConfig.CreateMapper();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateServiceViewModel _service)
        {
            var userId = (await userManager.GetUserAsync(HttpContext.User)).Id;
            var serv = new ServiceDTO() { Title = _service.Title, Info = _service.Info, UserId = userId };
            if (_service.Image != null)
                serv.Image = SaveImage(_service.Image);
            bool check = serviceService.CreateService(serv);
            return Redirect("~/User/GetServices");
        }

        [NonAction]
        public string SaveImage(IFormFile image)
        {
            string webRootPath = @"C:\Ucheba\nix\ServiceLookup\ServiceLookup\wwwroot\";
            string path = "/images/" + image.FileName;
            using (var fileStream = new FileStream(webRootPath + path, FileMode.Create))
            {
                image.CopyToAsync(fileStream);
            }
            return path;
        }
    }
}
