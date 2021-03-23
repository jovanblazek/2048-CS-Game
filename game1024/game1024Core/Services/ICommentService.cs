using System;
using System.Collections.Generic;
using System.Text;
using game1024Core.Entities;

namespace game1024Core.Services
{
    public interface ICommentService
    {
        void AddComment(Comment comment);

        IList<Comment> GetLatestComments();

        void ResetComments();
    }
}
