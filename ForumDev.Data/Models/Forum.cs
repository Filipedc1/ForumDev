using System;
using System.Collections.Generic;
using System.Text;

namespace ForumDev.Data.Models
{
    public class Forum
    {
        public int Id               { get; set; }
        public string Title         { get; set; }
        public string Description   { get; set; }
        public DateTime Created     { get; set; }
        public string ImageURL      { get; set; }

        // virtual allows us to lazy load the property in entity framework
        public virtual IEnumerable<Post> Posts { get; set; }
    }
}
