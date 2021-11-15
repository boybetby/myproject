using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using myproject.Models;

namespace myproject.Controllers
{
    public class PostCommentsAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PostCommentsAPI
        public IQueryable<PostComment> GetpostComment()
        {
            return db.postComment;
        }

        // GET: api/PostCommentsAPI/5
        [ResponseType(typeof(PostComment))]
        public IHttpActionResult GetPostComment(int id)
        {
            PostComment postComment = db.postComment.Find(id);
            if (postComment == null)
            {
                return NotFound();
            }

            return Ok(postComment);
        }

        // PUT: api/PostCommentsAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPostComment(int id, PostComment postComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != postComment.PostCommentID)
            {
                return BadRequest();
            }

            db.Entry(postComment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostCommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PostCommentsAPI
        [ResponseType(typeof(PostComment))]
        public IHttpActionResult PostPostComment(PostComment postComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.postComment.Add(postComment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = postComment.PostCommentID }, postComment);
        }

        // DELETE: api/PostCommentsAPI/5
        [ResponseType(typeof(PostComment))]
        public IHttpActionResult DeletePostComment(int id)
        {
            PostComment postComment = db.postComment.Find(id);
            if (postComment == null)
            {
                return NotFound();
            }

            db.postComment.Remove(postComment);
            db.SaveChanges();

            return Ok(postComment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostCommentExists(int id)
        {
            return db.postComment.Count(e => e.PostCommentID == id) > 0;
        }
    }
}