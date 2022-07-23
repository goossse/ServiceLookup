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
        private readonly ISearch searchService;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public ManageController(UserManager<User> _userManager, ApplicationDbContext db)
        {
            serviceService = new ServiceService(db);
            searchService = new SearchService(db);
            userManager = _userManager;
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperView());
            });
            mapper = mappingConfig.CreateMapper();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteService(int id)
        {
            await serviceService.DeleteService(id);
            return Redirect("~/Manage/MyServices");
        }

        [HttpGet]
        public async Task<IActionResult> EditService(int id)
        {
            ServiceViewModel service = mapper.Map<ServiceViewModel>(await searchService.GetService(id));
            return View(service);
        }

        [HttpPost]
        public async Task<IActionResult> EditService(ServiceViewModel serviceVM)
        {
            ServiceDTO service = await searchService.GetService(serviceVM.Id);
            service.Title = serviceVM.Title;
            service.Info = serviceVM.Info;
            if (serviceVM.ImageFile != null)
            {
                service.Image = SaveImage(serviceVM.ImageFile);
            }
            return Redirect("~/Manage/MyServices");
        }

        [HttpGet]
        public async Task<IActionResult> MyServices()
        {
            int userId = (await userManager.GetUserAsync(HttpContext.User)).Id;
            return View(await serviceService.MyServices(userId));
        }

        [HttpGet]
        public IActionResult CreateService()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateService(ServiceViewModel _service)
        {
            var userId = (await userManager.GetUserAsync(HttpContext.User)).Id;
            PriceDTO temp = new PriceDTO() { Currency = "Гривень", Value = 100 };
            ServiceDTO serv = new ServiceDTO() { Title = _service.Title, Info = _service.Info, UserId = userId, Price = temp };
            if (_service.Image != null)
                serv.Image = SaveImage(_service.ImageFile);
            bool check = serviceService.CreateService(serv);
            return Redirect("~/Manage/MyServices");
        }

        [HttpGet]
        public  async Task<IActionResult> MyRequests()
        {
            int userId = (await userManager.GetUserAsync(HttpContext.User)).Id;
            IEnumerable<RequestDTO> list = await serviceService.MyRequests(userId);
            return View(list);
        }
        
        [HttpPost]
        public async Task<IActionResult> AcceptRequest(int id)
        {
            await serviceService.AnswerRequest(id, 1);
            return Redirect("~/Manage/MyBookings");
        }

        [HttpPost]
        public async Task<IActionResult> RejectRequest(int id)
        {
            await serviceService.AnswerRequest(id, 2);
            return Redirect("~/Manage/MyServices");
        }

        [HttpGet]
        public async Task<IActionResult> MyReviews()
        {
            int userId = (await userManager.GetUserAsync(HttpContext.User)).Id;
            IEnumerable<ReviewDTO> list = await serviceService.MyReviews(userId);
            return View(list);

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
