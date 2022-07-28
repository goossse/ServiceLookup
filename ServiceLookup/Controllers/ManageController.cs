using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLookup.BL.DTO;
using ServiceLookup.BL.DTO.PagedList;
using ServiceLookup.BL.Services.Implementations;
using ServiceLookup.BL.Services.Interfaces;
using ServiceLookup.DAL;
using ServiceLookup.DAL.Entity;
using ServiceLookup.Mapper;
using ServiceLookup.Models;
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

        public ManageController(UserManager<User> _userManager, IService _serviceService, ISearch _searchService)//User manager!
        {
            serviceService = _serviceService;
            searchService = _searchService;
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
            ServiceDTO service = await searchService.GetService(id);
            ServiceViewModel serviceVM = new ServiceViewModel()
            {
                Title = service.Title, Info = service.Info, Price = service.Price
            };
            return View(serviceVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditService(ServiceViewModel serviceVM)
        {
            ServiceDTO service = await searchService.GetService(serviceVM.Id);
            service.Title = serviceVM.Title;
            service.Info = serviceVM.Info;
            service.Price = serviceVM.Price;
            if (serviceVM.ImageFile != null)
            {
                service.Image = SaveImage(serviceVM.ImageFile);
            }
            await serviceService.EditService(service);
            return Redirect("~/Manage/MyServices");
        }

        [HttpGet]
        public async Task<IActionResult> Myservice(int id)
        {
            int userId = (await userManager.GetUserAsync(HttpContext.User)).Id;
            ServiceDTO service = await searchService.GetService(id);
            if (service.UserId != userId)
            {
                return Redirect($"~/User/GetService/{id}");
            }
            return View(service);
        }

        [HttpGet]
        public async Task<IActionResult> MyServices()
        {
            int userId = (await userManager.GetUserAsync(HttpContext.User)).Id;
            return View( await serviceService.MyServices(userId));
        }

        [HttpGet]
        public async Task<IActionResult> CreateService()
        {
            var types = await searchService.GetTypes();
            CreateServiceViewModel serviceVM = new CreateServiceViewModel() { Types = types };
            return View(serviceVM);
        }

        [HttpPost]
        public async Task<IActionResult> CreateService(CreateServiceViewModel _service)
        {
            var userId = (await userManager.GetUserAsync(HttpContext.User)).Id;
            ServiceDTO serv = new ServiceDTO() { Title = _service.Title, Info = _service.Info, UserId = userId, Price = _service.Price,
                DateOfCreating = DateTime.Now, ServiceTypeId = _service.TypeId 
            };
            if (_service.ImageFile != null)
                serv.Image = SaveImage(_service.ImageFile);
            bool check = serviceService.CreateService(serv);
            return Redirect("~/Manage/MyServices");
        }

        [HttpGet]
        public async Task<IActionResult> MyRequests(int Page = 1)
        {
            int pageSize = 5;
            int userId = (await userManager.GetUserAsync(HttpContext.User)).Id;
            PagedListDTO<RequestDTO> list = await serviceService.GetMyRequests(userId, Page, pageSize);
            RequestsViewModel requests = new RequestsViewModel() { Requests = list.Items, pageViewModel = new PageViewModel(list.Count, Page, pageSize)  };
            return View(requests);
        }

        [HttpGet]
        public async Task<IActionResult> MyBookings(int Page = 1)
        {
            int pageSize = 5;
            int userId = (await userManager.GetUserAsync(HttpContext.User)).Id;
            PagedListDTO<RequestDTO> list = await serviceService.GetMyBookings(userId, Page , pageSize);
            RequestsViewModel requests = new RequestsViewModel() { Requests = list.Items, pageViewModel = new PageViewModel(list.Count, Page, pageSize) };
            return View(requests);
        }

        [HttpGet]
        public async Task<IActionResult> AcceptRequest(int id)
        {
            await serviceService.AnswerRequest(id, 3);
            return Redirect("~/Manage/MyBookings");
        }

        [HttpGet]
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
