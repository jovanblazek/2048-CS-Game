using game1024Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace game1024Core.Services
{
    public class CommentServiceEF : ICommentService
    {
        public void AddComment(Comment comment)
        {
            using (var context = new Game1024DbContext())
            {
                context.Comments.Add(comment);
                context.SaveChanges();
            }
        }

        public IList<Comment> GetLatestComments()
        {
            using (var context = new Game1024DbContext())
            {
                return (from c in context.Comments orderby c.SubmittedAt descending select c).Take(3).ToList();
            }
        }

        public void ResetComments()
        {
            using (var context = new Game1024DbContext())
            {
                context.Database.ExecuteSqlRaw("DELETE FROM Comments");
            }
        }
    }
}
