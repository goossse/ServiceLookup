using Microsoft.AspNetCore.Mvc;
using ServiceLookup.Models;
using System.Diagnostics;
using ServiceLookup.DAL.Interfaces;
using ServiceLookup.DAL.Entity;
using ServiceLookup.DAL;
using ServiceLookup.DAL.Repositories;


namespace ServiceLookup.Controllers
{
    public class SearchController : Controller
    {
        ILogger<SearchController> logger;
        private readonly IUnitOfWork unitOfWork;

        public SearchController(ILogger<SearchController> _logger, ApplicationDbContext db)
        {
            logger = _logger;
            unitOfWork = new UnitOfWork(db);
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
*/            var response = unitOfWork.Services.Get();
            return View(response);
        }
        public string UpdateService(int id, string title)
        {
            var temp = unitOfWork.Services.FindById(id);
            temp.Title = title;
            unitOfWork.Services.Update(temp);
            return "OK";
        }
    }
}
