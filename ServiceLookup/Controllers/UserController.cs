using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLookup.BL.DTO;
using ServiceLookup.BL.Services.Implementations;
using ServiceLookup.BL.Services.Interfaces;
using ServiceLookup.DAL;
using ServiceLookup.DAL.Entity;

namespace ServiceLookup.Controllers
{
    public class UserController : Controller
    {
        ILogger<UserController> logger;
        ISearch searchService;
        IUser userService;
        public UserController(ILogger<UserController> _logger, ApplicationDbContext db, UserManager<User> userManager)
        {
            logger = _logger;
            searchService = new SearchService(db);//?? не должны ли мы получить в конструкторе реализацию??
            userService = new UserService(userManager);
        }

        [HttpGet]
        public IActionResult GetServices()
        {
            return View(searchService.GetServices());
        }

        [HttpGet]
        public IActionResult GetService(int id)
        {
            return View(searchService.GetService(id));
        }

        [HttpGet]
        public IActionResult GetUsersServices(int userId) //посмотреть ка происходит передача айди
        {

            return View(searchService.GetUsersServices(userId));
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return View(userService.GetUsers());
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(int id)
        {
            return View(await userService.GetUserAsync(id));
        }
        //Впихнуть в клиент если поделю на 2 контроллера
/*        public IActionResult ChangePassword()
        {
            string temp = userManager.GetUserId(User);
            return View();
        }*/
    }
}
// сделать онмодел креэйтинг
// добавить средний рейтинг
// изменить ключ у юзера
// роли