using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ForumDev.Data;
using ForumDev.Data.Models;
using ForumDev.ViewModels.Forum;
using ForumDev.ViewModels.Post;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ForumDev.Controllers
{
    public class ForumController : Controller
    {
        #region Fields

        private readonly IForum forumService;
        private readonly IPost postService;
        private readonly IUpload uploadService;
        private readonly IHostingEnvironment _hosting;

        #endregion

        #region Constructor

        public ForumController(IForum forumService, IPost postService, IUpload uploadService, IHostingEnvironment he)
        {
            this.forumService = forumService;
            this.postService = postService;
            this.uploadService = uploadService;
            this._hosting = he;
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

        public IActionResult Topic(int id, string searchQuery)
        {
            var forum = forumService.GetById(id);
            var posts = new List<Post>();

            posts = postService.GetFilteredPosts(forum, searchQuery).ToList();

            var postListings = posts.Select(post => new PostListingViewModel
            {
                Id = post.Id,
                AuthorId = post.User.Id,
                AuthorName = post.User.UserName,
                AuthorRating = post.User.Rating,
                Title = post.Title,
                DatePosted = post.Created.ToString(),
                RepliesCount = post.Replies.Count(),
                Forum = BuildForumListing(post)
            });

            var viewModel = new ForumTopicViewModel()
            {
                Posts = postListings,
                Forum = BuildForumListing(forum)
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Search(int id, string searchQuery)
        {
            return RedirectToAction("Topic", new { id, searchQuery });
        }

        public IActionResult Create()
        {
            var viewMod = new AddForumViewModel();
            return View(viewMod);
        }

        [HttpPost]
        public async Task<IActionResult> AddForum(AddForumViewModel model)
        {
            string imageUrl = "/images/users/default.png";

            if (model.ImageUpload != null)
            {
                imageUrl = UploadForumImage(model.ImageUpload);
                imageUrl = "/images/" + Path.GetFileName(model.ImageUpload.FileName);
            }

            var forum = new Forum()
            {
                Title = model.Title,
                Description = model.Description,
                Created = DateTime.Now,
                ImageURL = imageUrl
            };

            await forumService.Create(forum);
            return RedirectToAction("Index", "Forum");
        }

        #endregion

        #region Helpers

        private ForumListingViewModel BuildForumListing(Post post)
        {
            var forum = post.Forum;
            return BuildForumListing(forum);
        }

        private ForumListingViewModel BuildForumListing(Forum forum)
        {
            return new ForumListingViewModel()
            {
                Id = forum.Id,
                Title = forum.Title,
                Description = forum.Description,
                ImageUrl = forum.ImageURL
            };
        }

        public string UploadForumImage(IFormFile img)
        {
            var filePath = Path.Combine(_hosting.WebRootPath + "\\images", Path.GetFileName(img.FileName));
            img.CopyTo(new FileStream(filePath, FileMode.Create));
            return filePath;
        }

        #endregion
    }
}