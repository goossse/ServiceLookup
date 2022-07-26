using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLookup.BL.DTO;
using ServiceLookup.BL.Services.Implementations;
using ServiceLookup.BL.Services.Interfaces;
using ServiceLookup.DAL;
using ServiceLookup.DAL.Entity;
using ServiceLookup.Mapper;
using ServiceLookup.Models;
using ServiceLookup.Models.UserVM;

namespace ServiceLookup.Controllers
{
    public class UserController : Controller
    {
        ILogger<UserController> logger;
        ISearch searchService;
        IUser userService;
        IMapper mapper;
        private readonly UserManager<User> userManager;
        public UserController(ILogger<UserController> _logger, ApplicationDbContext db, UserManager<User> _userManager)
        {
            userManager = _userManager;
            logger = _logger;
            searchService = new SearchService(db);//?? не должны ли мы получить в конструкторе реализацию??
            userService = new UserService(db);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperView());
            });
            mapper = mappingConfig.CreateMapper();
        }

        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            
            return View(await searchService.GetServices());
        }

        [HttpGet]
        public async Task<IActionResult> GetService(int id)
        {
            int userId = (await userManager.GetUserAsync(HttpContext.User)).Id;//exception if unregist user
            ServiceViewModel service = mapper.Map<ServiceViewModel>(await searchService.GetService(id));
            if (service.UserId == userId)
            {
                return Redirect($"~/Manage/MyService/{id}");
            }
            return View(service);
        }

        [HttpGet]
        public IActionResult GetUsersServices(int userId)
        {

            return View(searchService.GetUsersServices(userId));
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return View(await userService.GetUsers());
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(int id)
        {
            return View(await userService.GetUserAsync(id));
        }


        [HttpGet]
        public async Task<IActionResult> FindServices(string Order, int? TypeId = null, string TextSearch = "",
            bool IsRatedOnly = false, int? RateStart = null, int? RateEnd = null, int Page = 1)
        {
            int PageSize = 12;
            var pagedList = await searchService.FindServices(TextSearch, TypeId, Order, IsRatedOnly, RateStart, RateEnd, Page, PageSize);
            SearchViewModel searchVM = new SearchViewModel()
            {
                Types = await searchService.GetTypes(),
                TypeId = TypeId,
                TextSearch = TextSearch,
                IsRatedOnly = IsRatedOnly,
                RateStart = RateStart,
                RateEnd = RateEnd,
                Order = Order,
                services = pagedList.Items,
                pageViewModel = new PageViewModel(pagedList.Count, Page, PageSize )
            };
            return View(searchVM);
        }


        
/*        public IActionResult ChangePassword()
        {
            string temp = userManager.GetUserId(User);
            return View();
        }*/
    }
}
