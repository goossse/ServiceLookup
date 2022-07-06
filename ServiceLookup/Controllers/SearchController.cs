using Microsoft.AspNetCore.Mvc;
using ServiceLookup.Models;
using System.Diagnostics;
using ServiceLookup.DAL.Interfaces;
using ServiceLookup.DAL.Entity;

namespace ServiceLookup.Controllers
{
    public class SearchController : Controller
    {
        ILogger<SearchController> logger;
        private readonly IServiceRepository serviceRepository;

        public SearchController(ILogger<SearchController> _logger, IServiceRepository _serviceRepository)
        {
            logger = _logger;
            serviceRepository = _serviceRepository;
        }

/*        public async Task<IActionResult> GetService(int id)
        {
            if (id != 0)
            {

            }
        }*/

        public async Task<IActionResult> GetServices()
        {
            
            Service ser = new Service() { Title = "And Another One Massage", Info = "Just another one ordinary massage" };
            await serviceRepository.Create(ser);
            var response = await serviceRepository.GetAll();
            return View(response);
        }
    }
}
