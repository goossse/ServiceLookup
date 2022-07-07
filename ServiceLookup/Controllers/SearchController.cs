using Microsoft.AspNetCore.Mvc;
using ServiceLookup.Models;
using System.Diagnostics;
using ServiceLookup.DAL;
using ServiceLookup.BL.Services.Implementations;
using ServiceLookup.BL.DTO;
using ServiceLookup.BL.Services.Interfaces;

namespace ServiceLookup.Controllers
{
    public class SearchController : Controller
    {
        ILogger<SearchController> logger;
        /*public ApplicationDbContext db;*/
        List<ServiceDTO> services;
        ISearch searchService;

        public SearchController(ILogger<SearchController> _logger, ApplicationDbContext db)
        {
            logger = _logger;
            searchService = new SearchService(db);
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
    }
}
