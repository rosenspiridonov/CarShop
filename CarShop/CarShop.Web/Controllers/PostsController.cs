namespace CarShop.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CarShop.Services.Cars;
    using CarShop.Services.Posts;
    using CarShop.Web.Models.Posts;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class PostsController : Controller
    {
        private readonly IPostsService postsService;
        private readonly ICarsService carsService;
        private readonly UserManager<IdentityUser> userManager;

        public PostsController(
            IPostsService postsService,
            ICarsService carsService,
            UserManager<IdentityUser> userManager)
        {
            this.postsService = postsService;
            this.carsService = carsService;
            this.userManager = userManager;
        }

        public IActionResult Create()
        {
            var model = new PostInputModel()
            {
                Data = carsService.GetCarEntities(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostInputModel input)
        {
            input.Data = carsService.GetCarEntities();

            // Check for validation
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }

            // Create car
            var car = await carsService.CreateCarAsync(input.Car);

            // Create post
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await postsService.CreatePostAsync(userId, car);

            return RedirectToAction("All", "Cars");
        }
    }
}
