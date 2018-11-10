﻿using ForumDev.ViewModels.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumDev.ViewModels.Post
{
    public class PostListingViewModel
    {
        public int Id               { get; set; }
        public string Title         { get; set; }
        public string AuthorName    { get; set; }
        public string AuthorRating  { get; set; }
        public int AuthorId         { get; set; }
        public string DatePosted    { get; set; }

        public ForumListingViewModel Forum { get; set; }

        public int RepliesCount { get; set; }
    }
}
