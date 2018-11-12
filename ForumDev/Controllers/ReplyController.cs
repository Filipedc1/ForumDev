using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumDev.Data;
using ForumDev.Data.Models;
using ForumDev.ViewModels.Reply;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForumDev.Controllers
{
    public class ReplyController : Controller
    {
        #region Fields

        private readonly IPost postService;
        private readonly IApplicationUser userService;
        private readonly UserManager<ApplicationUser> userManager;

        #endregion

        #region Constructor

        public ReplyController(IPost postService, IApplicationUser userService, UserManager<ApplicationUser> manager)
        {
            this.postService = postService;
            this.userManager = manager;
            this.userService = userService;
        }

        #endregion

        #region Actions

        public async Task<IActionResult> Create(int id)
        {
            var post = postService.GetById(id);
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            var viewMod = new PostReplyViewModel()
            {
                PostContent = post.Content,
                PostTitle = post.Title,
                PostId = post.Id,
                AuthorId = user.Id,
                AuthorName = User.Identity.Name,
                AuthorImageUrl = user.ProfileImageUrl,
                AuthorRating = user.Rating,
                IsAuthorAdmin = User.IsInRole("Admin"),
                ForumName = post.Forum.Title,
                ForumId = post.Forum.Id,
                ForumImageUrl = post.Forum.ImageURL,
                Created = DateTime.Now
            };

            return View(viewMod);
        }

        [HttpPost]
        public async Task<IActionResult> AddReply(PostReplyViewModel model)
        {
            var userId = userManager.GetUserId(User);
            var user = await userManager.FindByIdAsync(userId);

            var reply = BuildReply(model, user);

            await postService.AddReply(reply);
            await userService.UpdateUserRating(userId, typeof(PostReply));

            return RedirectToAction("Index", "Post", new { id = model.PostId });
        }



        #endregion

        #region Helpers

        private PostReply BuildReply(PostReplyViewModel model, ApplicationUser user)
        {
            var post = postService.GetById(model.PostId);

            return new PostReply
            {
                Post = post,
                Content = model.ReplyContent,
                Created = DateTime.Now,
                User = user
            };
        }

        #endregion
    }
}