using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLookup.BL.DTO;
using ServiceLookup.BL.Services.Implementations;
using ServiceLookup.BL.Services.Interfaces;
using ServiceLookup.DAL;
using ServiceLookup.DAL.Entity;
using ServiceLookup.Mapper;
using ServiceLookup.Models.UserVM;

namespace ServiceLookup.Controllers
{
    public class UserController : Controller
    {
        ILogger<UserController> logger;
        ISearch searchService;
        IUser userService;
        IMapper mapper;
        public UserController(ILogger<UserController> _logger, ApplicationDbContext db, UserManager<User> userManager)
        {
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
            ServiceViewModel service = mapper.Map<ServiceViewModel>(await searchService.GetService(id));
            return View(service);
        }

        [HttpGet]
        public IActionResult GetUsersServices(int userId) //посмотреть ка происходит передача айди
        {

            return View(searchService.GetUsersServices(userId));
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            //сделать через автомаппер
            return View(await userService.GetUsers());
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(int id)
        {
            return View(await userService.GetUserAsync(id));
        }

        public async Task<IActionResult> SearchByTitle(string text)
        {
            return View(await searchService.GetServicesByTitle(text));
        }
        //Впихнуть в клиент если поделю на 2 контроллера
/*        public IActionResult ChangePassword()
        {
            string temp = userManager.GetUserId(User);
            return View();
        }*/
    }
}
// сделать онмодел креэйтин (не надо??)