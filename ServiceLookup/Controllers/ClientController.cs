using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLookup.BL.DTO;
using ServiceLookup.BL.Services.Implementations;
using ServiceLookup.BL.Services.Interfaces;
using ServiceLookup.DAL;
using ServiceLookup.DAL.Entity;
using ServiceLookup.Mapper;
using ServiceLookup.Models.ClientVM;
using ServiceLookup.Models.UserVM;

namespace ServiceLookup.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        IBooking bookingService;
        IProfile profileService;
        ISearch searchService;
        ILogger logger;
        UserManager<User> userManager;
        IMapper mapper;
        public ClientController(ILogger<UserController> _logger, ApplicationDbContext db, UserManager<User> _userManager)
        {
            logger = _logger;
            profileService = new ProfileService(db);//?? не должны ли мы получить в конструкторе реализацию??
            bookingService = new BookingService(db);
            searchService = new SearchService(db);
            userManager = _userManager;
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperView());
            });
            mapper = mappingConfig.CreateMapper();
        }

        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            int id = (await userManager.GetUserAsync(HttpContext.User)).Id;
            var profile = mapper.Map<ProfileViewModel>(await profileService.GetProfile(id));
            return View(profile);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            int id = (await userManager.GetUserAsync(HttpContext.User)).Id;
            var profile = mapper.Map<ProfileViewModel>(await profileService.GetProfile(id));
            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(ProfileViewModel _profile)
        {
            int id = (await userManager.GetUserAsync(HttpContext.User)).Id;
            var profile = userManager.Users.FirstOrDefault(u => u.Id == id);
            profile.Name = _profile.Name;
            profile.Surname = _profile.Surname;
            profile.DateOfBirth = _profile.DateOfBirth;
            profile.ContactDetails = _profile.ContactDetails;
            profile.ShortDescription = _profile.ShortDescription;
            await profileService.EditProfile(profile, userManager);
            return Redirect("~/Client/MyProfile");
        }

        [HttpGet]
        public IActionResult DeleteProfile()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ApplyRequest(int id)
        {
            // Получаем Услугу и Цену
            ServiceDTO service = await searchService.GetService(id);
            PriceDTO price = await searchService.GetPrice(service.PriceId);
            // Добавляем данные в ViewModel
            BookingViewModel bookingVM = new BookingViewModel()
            {
                Service = service,
                Price = price,
                Request = new RequestViewModel
                {
                    PriceId = price.Id,
                    ServiceId = service.Id,
                    DateOfBooking = DateTime.Parse(DateTime.Now.ToShortDateString())
                }
            };

            return View(bookingVM);
        }

        [HttpPost]
        public async Task<IActionResult> ApplyRequest(RequestViewModel reqVM)
        {
            Console.WriteLine();
            int userId = (await userManager.GetUserAsync(HttpContext.User)).Id;
            RequestDTO request = new RequestDTO()
            {
                UserId = userId,
                ServiceId = reqVM.ServiceId,
                PriceId = reqVM.PriceId,
                StartOfBooking = DateTime.Parse(reqVM.DateOfBooking.ToShortDateString() + " " + reqVM.StartOfBooking.ToShortTimeString()),
                EndOfBooking = DateTime.Parse(reqVM.DateOfBooking.ToShortDateString() + " " + reqVM.EndOfBooking.ToShortTimeString()),
                DateTimeOfCreating = DateTime.Now,
                /*ConditionId = 0,*/
                Description = reqVM.Description
            };
            bookingService.ApplyRequest(request);
            return Redirect("/Client/MyRequests");
        }
        [HttpGet]
        public async Task<IActionResult> MyRequests()
        {
            int userId = (await userManager.GetUserAsync(HttpContext.User)).Id;
            IEnumerable<RequestDTO> requests = await bookingService.GetRequests(userId);
            List<MyRequestsViewModel> list = new List<MyRequestsViewModel>();
            foreach (RequestDTO request in requests)
            {
                ServiceDTO service = await searchService.GetService(request.ServiceId);
                MyRequestsViewModel temp = new MyRequestsViewModel()
                {
                    Id = request.Id,
                    DateOfBooking = request.StartOfBooking.ToShortDateString(),
                    StartOfBooking = request.StartOfBooking.ToShortTimeString(),
                    EndOfBooking = request.EndOfBooking.ToShortTimeString(),
                    Description = request.Description,
                    Price = request.Price,
                    ServiceId = service.Id,
                    ServiceImage = service.Image,
                    ServiceTitle = service.Title,
                };
                list.Add(temp);
            }
            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            await bookingService.DeleteRequest(id);
            return Redirect("/Client/MyRequests");
        }
    }
}
