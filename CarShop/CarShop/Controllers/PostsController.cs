namespace CarShop.Controllers
{
    using CarShop.Models.Posts;
    using CarShop.Services.Posts;
    using Microsoft.AspNetCore.Mvc;

    public class PostsController : Controller
    {
        private readonly IPostsService postsService;

        public PostsController(IPostsService postsService)
        {
            this.postsService = postsService;
        }

        public IActionResult Create()
        {
            var model = new PostInputModel()
            {
                Data = postsService.GetCarEntities(),
            };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Create(PostInputModel input)
        {
            // Check for validation
            // Create car
            // Create post

            return this.View(input);
        }
    }
}
