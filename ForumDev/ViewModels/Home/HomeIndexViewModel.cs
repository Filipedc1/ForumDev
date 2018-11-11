using ForumDev.ViewModels.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumDev.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        public IEnumerable<PostListingViewModel> LatestPosts { get; set; }
        public string SearchQuery { get; set; }
    }
}
