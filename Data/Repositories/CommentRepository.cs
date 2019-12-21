using BlogHost.Data.Interfaces;
using BlogHost.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogHost.Data.Repositories
{
    public class CommentRepository : IComment
    {
        private readonly AppDBContext _appDbContext;

        public CommentRepository(AppDBContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public void AddCommentDB(Comment comment)
        {
            _appDbContext.Comments.Add(comment);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Comment> AllComments()
        {
            return _appDbContext.Comments.ToList();
        }

    }
}
