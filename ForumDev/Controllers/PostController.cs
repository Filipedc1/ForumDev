using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumDev.Data;
using ForumDev.Data.Models;
using ForumDev.ViewModels.Post;
using ForumDev.ViewModels.Reply;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForumDev.Controllers
{
    public class PostController : Controller
    {
        #region Fields

        private readonly IPost postService;
        private readonly IForum forumService;
        private readonly IApplicationUser userService;

        private static UserManager<ApplicationUser> userManager;

        #endregion

        #region Constructor

        public PostController(IPost postService, IForum forumService, IApplicationUser userService, UserManager<ApplicationUser> manager)
        {
            this.postService = postService;
            this.forumService = forumService;
            this.userService = userService;
            userManager = manager;
        }

        #endregion

        #region Actions

        public IActionResult Index(int id)
        {
            var post = postService.GetById(id);
            var replies = BuildPostReplies(post.Replies);

            var viewModel = new PostIndexViewModel()
            {
                Id = post.Id,
                Title = post.Title,
                AuthorId = post.User.Id,
                AuthorName = post.User.UserName,
                AuthorImageUrl = post.User.ProfileImageUrl,
                AuthorRating = post.User.Rating,
                Created = post.Created,
                PostContent = post.Content,
                Replies = replies,
                ForumId = post.Forum.Id,
                ForumName = post.Forum.Title,
                IsAuthorAdmin = IsAuthorAdmin(post.User)
            };

            return View(viewModel);
        }

        public IActionResult Create(int id)
        {
            var forum = forumService.GetById(id);

            var viemModel = new NewPostViewModel()
            {
                ForumId = forum.Id,
                ForumName = forum.Title,
                ForumImageUrl = forum.ImageURL,
                AuhtorName = User.Identity.Name
            };

            return View(viemModel);
        }


        [HttpPost]
        public async Task<IActionResult> AddPost(NewPostViewModel model)
        {
            var userId = userManager.GetUserId(User);
            var user = await userManager.FindByIdAsync(userId);
            var post = BuildPost(model, user);

            await postService.Add(post);
            await userService.UpdateUserRating(userId, typeof(Post));

            return RedirectToAction("Index", "Post", new { id = post.Id });
        }

        #endregion

        #region Helpers

        private Post BuildPost(NewPostViewModel model, ApplicationUser user)
        {
            var forum = forumService.GetById(model.ForumId);

            return new Post
            {
                Title = model.Title,
                Content = model.Content,
                Created = DateTime.Now,
                User = user,
                Forum = forum
            };
        }

        private IEnumerable<PostReplyViewModel> BuildPostReplies(IEnumerable<PostReply> replies)
        {
            return replies.Select(reply => new PostReplyViewModel
            {
                Id = reply.Id,
                AuthorName = reply.User.UserName,
                AuthorId = reply.User.Id,
                AuthorImageUrl = reply.User.ProfileImageUrl,
                AuthorRating = reply.User.Rating,
                Created = reply.Created,
                ReplyContent = reply.Content,
                IsAuthorAdmin = IsAuthorAdmin(reply.User)
            });
        }

        private bool IsAuthorAdmin(ApplicationUser user)
        {
            return userManager.GetRolesAsync(user)
                .Result.Contains("Admin");
        }

        #endregion
    }
}