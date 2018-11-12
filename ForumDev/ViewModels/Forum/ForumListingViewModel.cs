using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumDev.ViewModels.Forum
{
    public class ForumListingViewModel
    {
        public int Id               { get; set; }
        public string Title         { get; set; }
        public string Description   { get; set; }
        public string ImageUrl      { get; set; }

        public int NumberOfPosts    { get; set; }
        public int NumberOfUsers    { get; set; }
        public bool HasRecentPost   { get; set; }

    }
}
