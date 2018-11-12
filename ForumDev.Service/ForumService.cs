using ForumDev.Data;
using ForumDev.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDev.Service
{
    // Uses Entity Framework to interact with our actual data
    public class ForumService : IForum
    {
        private readonly ApplicationDbContext dbContext;

        #region Constructor

        public ForumService(ApplicationDbContext context)
        {
            dbContext = context;
        }

        #endregion

        #region Methods

        public async Task Create(Forum forum)
        {
            dbContext.Forums.Add(forum);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var forum = GetById(id);
            dbContext.Forums.Remove(forum);
            await dbContext.SaveChangesAsync();
        }

        public IEnumerable<Forum> GetAll()
        {
            return dbContext.Forums.Include(forum => forum.Posts);
        }

        public IEnumerable<ApplicationUser> GetAllActiveUsers()
        {
            throw new NotImplementedException();
        }

        public Forum GetById(int id)
        {
            var forum = dbContext.Forums.Where(f => f.Id == id)
                .Include(f => f.Posts).ThenInclude(p => p.User)
                .Include(f => f.Posts).ThenInclude(p => p.Replies).ThenInclude(r => r.User)
                .FirstOrDefault();

            return forum;
        }

        public Task UpdateForumDescription(int forumId, string newDescription)
        {
            throw new NotImplementedException();
        }

        public Task UpdateForumTitle(int forumId, string newTitle)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
