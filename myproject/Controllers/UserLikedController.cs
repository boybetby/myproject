using myproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace myproject.Controllers
{
    public class UserLikedController : ApiController
    {
        //private readonly ApplicationDbContext db;

        //public UserLikedController()
        //{
        //    db = new ApplicationDbContext();
        //}

        //[HttpPost]
        //public IHttpActionResult Like(int userid, Post post)
        //{            
        //    var getuser = db.userModels.Find(userid);
        //    if (getuser.Posts != null && post.Users != null)
        //        return BadRequest("Liked");
        //    else
        //    {
                
        //        _dbContext.SaveChanges();
        //    }           
        //    return Ok();
        //}

        //[HttpDelete]
        //public IHttpActionResult DeleteFollow(string Id)
        //{
        //    var userId = User.Identity.GetUserId();
        //    var following = _dbContext.Followings.SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == Id);
        //    if (following == null)
        //    {
        //        return NotFound();
        //    }
        //    _dbContext.Followings.Remove(following);
        //    _dbContext.SaveChanges();
        //    return Ok(Id);
        //}
    }
}
