using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ForumDev.Models;
using ForumDev.ViewModels.Home;
using ForumDev.Data;
using ForumDev.ViewModels.Post;
using ForumDev.Data.Models;
using ForumDev.ViewModels.Forum;

namespace ForumDev.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        private readonly IPost postService;

        #endregion

        #region Constructor

        public HomeController(IPost postService)
        {
            this.postService = postService;
        }

        #endregion

        #region Actions

        public IActionResult Index()
        {
            var viewModel = BuildHomeIndexViewModel();

            return View(viewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion

        #region Helpers

        private HomeIndexViewModel BuildHomeIndexViewModel()
        {
            var latestPosts = postService.GetLatestPosts(10);

            var posts = latestPosts.Select(post => new PostListingViewModel
            {
                Id = post.Id,
                Title = post.Title,
                AuthorName = post.User.UserName,
                AuthorId = post.User.Id,
                AuthorRating = post.User.Rating,
                DatePosted = post.Created.ToString(),
                RepliesCount = post.Replies.Count(),
                Forum = GetForumListingForPost(post)
            });

            return new HomeIndexViewModel
            {
                LatestPosts = posts,
                SearchQuery = string.Empty
            };
        }

        private ForumListingViewModel GetForumListingForPost(Post post)
        {
            var forum = post.Forum;

            return new ForumListingViewModel
            {
                Id = forum.Id,
                Title = forum.Title,
                ImageUrl = forum.ImageURL
            };
        }

        #endregion
    }
}
