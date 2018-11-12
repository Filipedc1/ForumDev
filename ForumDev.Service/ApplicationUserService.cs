using ForumDev.Data;
using ForumDev.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDev.Service
{
    public class ApplicationUserService : IApplicationUser
    {
        private readonly ApplicationDbContext dbContext;

        public ApplicationUserService(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return dbContext.ApplicationUsers;
        }

        public ApplicationUser GetById(string id)
        {
            return GetAll().FirstOrDefault(user => user.Id == id);
        }

        public Task IncrementRating(string id, Type type)
        {
            throw new NotImplementedException();
        }

        public async Task SetProfileImage(string id, Uri uri)
        {
            var user = GetById(id);
            user.ProfileImageUrl = uri.AbsoluteUri;
            dbContext.Update(user);
            await dbContext.SaveChangesAsync();
        }
    }
}
