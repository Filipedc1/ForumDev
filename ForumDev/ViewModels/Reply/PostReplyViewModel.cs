using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumDev.ViewModels.Reply
{
    public class PostReplyViewModel
    {
        public int Id                   { get; set; }
        public string AuthorId          { get; set; }
        public string AuthorName        { get; set; }
        public int AuthorRating         { get; set; }
        public string AuthorImageUrl    { get; set; }
        public DateTime Created         { get; set; }
        public string ReplyContent      { get; set; }
        public bool IsAuthorAdmin       { get; set; }

        public int PostId               { get; set; }
    }
}
