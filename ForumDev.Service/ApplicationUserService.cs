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

        public async Task UpdateUserRating(string userId, Type type)
        {
            var user = GetById(userId);
            user.Rating = CalculateUserRating(type, user.Rating);
            await dbContext.SaveChangesAsync();
        }

        private int CalculateUserRating(Type type, int userRating)
        {
            int inc = 0;

            if (type == typeof(Post))
            {
                inc = 1;
            }
            else if (type == typeof(PostReply))
            {
                inc = 3;
            }

            return userRating + inc;
        }

        public async Task SetProfileImage(string id, string url)
        {
            var user = GetById(id);
            user.ProfileImageUrl = url;
            dbContext.Update(user);
            await dbContext.SaveChangesAsync();
        }
    }
}
