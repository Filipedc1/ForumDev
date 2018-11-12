using ForumDev.ViewModels.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumDev.ViewModels.Search
{
    public class SearchResultViewModel
    {
        public IEnumerable<PostListingViewModel> Posts { get; set; }
        public string SearchQuery { get; set; }
        public bool EmptySearchResults { get; set; }
    }
}
