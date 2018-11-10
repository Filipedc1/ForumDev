using ForumDev.Data;
using ForumDev.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public Task Create(Forum forum)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
