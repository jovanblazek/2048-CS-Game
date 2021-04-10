using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using game1024Core.Entities;
using game1024Core.Services;

namespace game1024Web.APIControllers
{
    [Route("api/Comment")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService = new CommentServiceEF();

        //GET: /api/Comment
        [HttpGet]
        public IEnumerable<Comment> GetComments()
        {
            return _commentService.GetLatestComments();
        }

        //POST: /api/Comment
        [HttpPost]
        public void PostScore(Comment comment)
        {
            comment.SubmittedAt = DateTime.Now;
            _commentService.AddComment(comment);
        }
    }
}
