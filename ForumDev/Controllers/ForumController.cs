using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumDev.Data;
using ForumDev.ViewModels.Forum;
using Microsoft.AspNetCore.Mvc;

namespace ForumDev.Controllers
{
    public class ForumController : Controller
    {
        #region Fields

        private readonly IForum forumService;
        private readonly IPost postService;

        #endregion

        #region Constructor

        public ForumController(IForum forumService)
        {
            this.forumService = forumService;
        }

        #endregion

        #region Actions

        public IActionResult Index()
        {
            var forumsList = forumService.GetAll()
                    .Select(forum => new ForumListingViewModel
                    {
                        Id = forum.Id,
                        Title = forum.Title,
                        Description = forum.Description
                    });

            var viewModel = new ForumIndexViewModel()
            {
                ForumList = forumsList
            };

            return View(viewModel);
        }

        public IActionResult Topic(int id)
        {
            var forum = forumService.GetById(id);
            var posts = postService.GetFilteredPosts(id);

            var postListings = 

            return View();
        }

        #endregion
    }
}