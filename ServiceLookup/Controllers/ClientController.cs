using AutoMapper;
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
            var profile = await profileService.GetProfile(id);
            profile.Name = _profile.Name;
            profile.Surname = _profile.Surname;
            profile.DateOfBirth = _profile.DateOfBirth;
            profile.ContactDetails = _profile.ContactDetails;
            profile.ShortDescription = _profile.ShortDescription;
            profileService.EditProfile(profile, userManager);
            return Redirect("~/Client/MyProfile");
        }

        [HttpGet]
        public IActionResult DeleteProfile()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ApplyRequest(int serviceId)
        {
            RequestViewModel requestVM = new RequestViewModel() { Service = await searchService.GetService(serviceId) };
            return View(requestVM);
        }

        [HttpPost]
        public IActionResult ApplyRequest(RequestViewModel requestVM)
        {

            bookingService.ApplyRequest(requestVM.Request);
            return Redirect("/Client/MyRequests");
        }

        /*[HttpGet]
        public Task<IActionResult> MyRequests()*/
    }
}
