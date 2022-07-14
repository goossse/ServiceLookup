using Microsoft.AspNetCore.Mvc;
using ServiceLookup.BL.DTO;
using ServiceLookup.BL.Services.Implementations;
using ServiceLookup.BL.Services.Interfaces;
using ServiceLookup.DAL;

namespace ServiceLookup.Controllers
{
    public class UserController : Controller
    {
        ILogger<UserController> logger;
        /*public ApplicationDbContext db;*/
        List<ServiceDTO> services;
        ISearch searchService;

        public UserController(ILogger<UserController> _logger, ApplicationDbContext db)
        {
            logger = _logger;
            searchService = new SearchService(db);//?? не должны ли мы получить в конструкторе реализацию??
        }

        /*        public async Task<IActionResult> GetService(int id)
                {
                    if (id != 0)
                    {

                    }
                }*/

        public IActionResult GetServices()
        {

            /*            Service ser = new Service() { Title = "And Another One Massage", Info = "Just another one ordinary massage" };
            */
            return View(searchService.GetServices());
        }
        /*        public string UpdateService(int id, string title)
                {
                    var temp = unitOfWork.Services.FindById(id);
                    temp.Title = title;
                    unitOfWork.Services.Update(temp);
                    return "OK";
                }*/


        //Впихнуть в клиент если поделю на 2 контроллера
/*        public IActionResult ChangePassword()
        {
            string temp = userManager.GetUserId(User);
            return View();
        }*/
    }
}
