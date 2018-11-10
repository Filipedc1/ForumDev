using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumDev.Data;
using ForumDev.Data.Models;
using ForumDev.ViewModels.Post;
using ForumDev.ViewModels.Reply;
using Microsoft.AspNetCore.Mvc;

namespace ForumDev.Controllers
{
    public class PostController : Controller
    {
        #region Fields

        private readonly IPost postService;

        #endregion

        #region Constructor

        public PostController(IPost postService)
        {
            this.postService = postService;
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
                Replies = replies
            };

            return View(viewModel);
        }

        #endregion

        #region Helpers

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
                ReplyContent = reply.Content
            });
        }

        #endregion
    }
}