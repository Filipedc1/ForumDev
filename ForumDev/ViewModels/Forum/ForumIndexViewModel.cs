using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumDev.ViewModels.Forum
{
    public class ForumIndexViewModel
    {
        public IEnumerable<ForumListingViewModel> ForumList { get; set; }
    }
}
