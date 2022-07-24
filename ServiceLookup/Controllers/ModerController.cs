using Microsoft.AspNetCore.Mvc;
using ServiceLookup.BL.Services.Implementations;
using ServiceLookup.BL.Services.Interfaces;
using ServiceLookup.DAL;
using ServiceLookup.BL.DTO;

namespace ServiceLookup.Controllers
{
    public class ModerController : Controller
    {

        ILogger logger;
        IModer moderService;

        public ModerController(ILogger<UserController> _logger, ApplicationDbContext db)
        {
            logger = _logger;
            moderService = new ModerService(db);
        }

        public IActionResult Index()
        {
            ServiceTypeDTO serviceType = new ServiceTypeDTO() { Name = "Здоров'я", Criterias = "Спілкування Безпека Професіоналізм Приміщення" };
            ServiceTypeDTO serviceType2 = new ServiceTypeDTO() { Name = "Краса", Criterias = "Спілкування Результат Комфортабельність Приміщення" };
            ServiceTypeDTO serviceType3 = new ServiceTypeDTO() { Name = "Сантехніка", Criterias = "Спілкування Результативність Акуратність Швидкість" };
            moderService.AddServiceType(serviceType);
            moderService.AddServiceType(serviceType2);
            moderService.AddServiceType(serviceType3);
            return Redirect("");
        }
    }
}
