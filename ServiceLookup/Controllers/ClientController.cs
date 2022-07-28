using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLookup.BL.DTO;
using ServiceLookup.BL.DTO.PagedList;
using ServiceLookup.BL.Services.Implementations;
using ServiceLookup.BL.Services.Interfaces;
using ServiceLookup.DAL;
using ServiceLookup.DAL.Entity;
using ServiceLookup.DAL.Entity.PagedList;
using ServiceLookup.Mapper;
using ServiceLookup.Models;
using ServiceLookup.Models.ClientVM;
using ServiceLookup.Models.ManageVM;
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
        public ClientController(ILogger<UserController> _logger, UserManager<User> _userManager, IBooking _bookingService, IProfile _profileService, ISearch _searchService)
        {
            logger = _logger;
            profileService = _profileService;
            bookingService = _bookingService;
            searchService = _searchService;
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
            PriceDTO price = await searchService.GetPrice(service.PriceId);// need include!
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
                ConditionId = 1,
                Description = reqVM.Description
            };
            bookingService.ApplyRequest(request);
            return Redirect("/Client/MyRequests");
        }
        [HttpGet]
        public async Task<IActionResult> MyRequests(int Page = 1)
        {
            int pageSize = 5;
            int userId = (await userManager.GetUserAsync(HttpContext.User)).Id;
            PagedListDTO<RequestDTO> list = await bookingService.GetRequests(userId, Page, pageSize);
            RequestsViewModel requests = new RequestsViewModel() { Requests = list.Items, pageViewModel = new PageViewModel(list.Count, Page, pageSize) };
            return View(requests);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            await bookingService.DeleteRequest(id);
            return Redirect("/Client/MyRequests");
        }

        [HttpGet]
        public async Task<IActionResult> CreateReview(int id)
        {
            RequestDTO request = await bookingService.GetRequest(id);
            List<string> criterias = await bookingService.GetCriteriesList(request.ServiceId);
            ReviewViewModel reviewVM = new ReviewViewModel(){ Criterias = criterias,
                ServiceId = request.ServiceId, Service = request.Service, RequestId = id};
            return View(reviewVM);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(ReviewViewModel reviewVM)
        {
            int userId = (await userManager.GetUserAsync(HttpContext.User)).Id;
            List<ReviewCriteriaDTO> criterias = new List<ReviewCriteriaDTO>();
            for (int i = 0; i < reviewVM.Criterias.Count; i++)
            {
                criterias.Add(new ReviewCriteriaDTO() { Title = reviewVM.Criterias[i], Rate = reviewVM.Rates![i] });
            }
            ReviewDTO review = new ReviewDTO()
            {
                Criterias = criterias,
                Info = reviewVM.Text,
                ServiceId = reviewVM.ServiceId,
                UserId = userId
            };
            await bookingService.CreateReview(review);
            await bookingService.MarkRequestCompleted(reviewVM.RequestId);
            return Redirect("/Client/MyReviews");
        }

        /*[HttpGet]*/
    }
}
