using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumDev.Data;
using ForumDev.Data.Models;
using ForumDev.ViewModels.Forum;
using ForumDev.ViewModels.Post;
using ForumDev.ViewModels.Search;
using Microsoft.AspNetCore.Mvc;

namespace ForumDev.Controllers
{
    public class SearchController : Controller
    {
        #region Fields

        private readonly IPost postService;

        #endregion

        #region Constructor

        public SearchController(IPost postService)
        {
            this.postService = postService;
        }

        #endregion

        #region Actions

        public IActionResult Results(string searchQuery)
        {
            var posts = postService.GetFilteredPosts(searchQuery);
            var areNoResults = (!string.IsNullOrEmpty(searchQuery) && !posts.Any());

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

            var viewMod = new SearchResultViewModel
            {
                Posts = postListings,
                SearchQuery = searchQuery,
                EmptySearchResults = areNoResults
            };

            return View();
        }

        [HttpPost]
        public IActionResult Search(string searchQuery)
        {
            return RedirectToAction("Results", new { searchQuery });
        }

        #endregion

        #region Helpers

        private ForumListingViewModel BuildForumListing(Post post)
        {
            var forum = post.Forum;

            return new ForumListingViewModel
            {
                Id = forum.Id,
                ImageUrl = forum.ImageURL,
                Title = forum.Title,
                Description = forum.Description
            };
        }

        #endregion
    }
}