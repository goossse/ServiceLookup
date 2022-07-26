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

/*            ConditionDTO condition = new ConditionDTO() { Title = "Заявка подана", Info = "Заявку на бронювання було подано користувачем, очікується розгляд власником послуги." };
            ConditionDTO condition2 = new ConditionDTO() { Title = "Заявку відхилено", Info = "Власник послуги після розгляду заявки відмовив у бронюванні." };
            ConditionDTO condition3 = new ConditionDTO() { Title = "Заявку підтверджено", Info = "Власник послуги після розгляду заявки підтвердив бронювання." };
            ConditionDTO condition4 = new ConditionDTO() { Title = "Заявка виконана", Info = "Заявку було виконано у вказаний у бронюванні час." };
            moderService.AddCondition(condition);
            moderService.AddCondition(condition2);
            moderService.AddCondition(condition3);
            moderService.AddCondition(condition4);
*/

            /*            ServiceTypeDTO serviceType = new ServiceTypeDTO() { Name = "Здоров'я", Criterias = "Спілкування Безпека Професіоналізм Приміщення" };
                        ServiceTypeDTO serviceType2 = new ServiceTypeDTO() { Name = "Краса", Criterias = "Спілкування Результат Комфортабельність Приміщення" };
                        ServiceTypeDTO serviceType3 = new ServiceTypeDTO() { Name = "Сантехніка", Criterias = "Спілкування Результативність Акуратність Швидкість" };
                        moderService.AddServiceType(serviceType);
                        moderService.AddServiceType(serviceType2);
                        moderService.AddServiceType(serviceType3);*/
            return Redirect("");
        }
    }
}
