using AutoMapper;
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
    public class ManageController : Controller
    {
        private readonly IService serviceService;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHost;

        public ManageController(UserManager<User> _userManager, ApplicationDbContext db, IWebHostEnvironment _webHost)
        {
            serviceService = new ServiceService(db);
            userManager = _userManager;
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperView());
            });
            mapper = mappingConfig.CreateMapper();
            webHost = _webHost;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateServiceViewModel _service)
        {
            /*var userId = (await userManager.GetUserId);*/
            var serv = new ServiceDTO() { Title = _service.Title, Info = _service.Info };
            if (_service.Image != null)
            {
                string path = "/images/" + _service.Image?.FileName;
                using (var fileStream = new FileStream(webHost.WebRootPath + path, FileMode.Create))
                {
                    await _service.Image.CopyToAsync(fileStream);
                }
                serv.Image = path;
            }

            /*serv.UserId = Guid(userId);*/
            bool check = serviceService.CreateService(serv);
            return Redirect("~/User/GetServices");
        }
    }
}
