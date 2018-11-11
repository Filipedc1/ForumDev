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
    public class PostService : IPost
    {
        private readonly ApplicationDbContext dbContext;

        #region Constructor

        public PostService(ApplicationDbContext context)
        {
            dbContext = context;
        }

        #endregion

        #region Methods

        public async Task Add(Post post)
        {
            await dbContext.AddAsync(post);
            await dbContext.SaveChangesAsync();
        }

        public Task AddReply(PostReply reply)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditPostContent(int id, string newContent)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAll()
        {
            return dbContext.Posts;
        }

        public Post GetById(int id)
        {
            return dbContext.Posts.Where(post => post.Id == id)
                    .Include(post => post.User)
                    .Include(post => post.Replies).ThenInclude(reply => reply.User)
                    .Include(post => post.Forum)
                    .First();
        }

        public IEnumerable<Post> GetFilteredPosts(string searchQuery)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetPostsByForum(int id)
        {
            return dbContext.Forums
                   .Where(forum => forum.Id == id).First()
                   .Posts;
        }

        #endregion
    }
}
