using ForumDev.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ForumDev.Data
{
    public interface IApplicationUser
    {
        ApplicationUser GetById(string id);
        IEnumerable<ApplicationUser> GetAll();

        Task SetProfileImage(string id, Uri uri);
        Task IncrementRating(string id, Type type);
    }
}
