using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumDev.ViewModels.Post
{
    public class NewPostViewModel
    {
        public int ForumId           { get; set; }
        public string ForumName      { get; set; }
        public string AuhtorName     { get; set; }
        public string ForumImageUrl  { get; set; }
        public string Title          { get; set; }
        public string Content        { get; set; }
    }
}
