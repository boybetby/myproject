using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using myproject.Models;

namespace myproject.Controllers
{
    public class CommunityController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Community
        public ActionResult Index()
        {
            return View(db.Post.ToList());
        }
        [HttpPost]
        public ActionResult EventSubs(string subsemail)
        {
            Event newEvent = db.Event
                        .OrderByDescending(p => p.EventDate)
                       .FirstOrDefault();

            string MailSend = "yengreenliving@gmail.com";
            string Password = "yenmail@123";
            using (MailMessage m = new MailMessage(MailSend, subsemail))
            {
                m.Subject = "Thank You For Registered";
                m.Body = ("You have registered to attend the " + newEvent.EventName + "event, the event will be held on: <br /> "+ newEvent.EventDate.ToString("dd/MM/yyyy hh:mm tt") +" at " + newEvent.EventAddress);
                m.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential networkCred = new NetworkCredential(MailSend, Password);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCred;
                    smtp.Port = 587;
                    smtp.Send(m);
                    ViewBag.Message = "";
                }
            }
            EventSubscriber eventSubscriber = new EventSubscriber();
            eventSubscriber.SubscriberEmail = subsemail;

            newEvent.EventSubscribers.Add(eventSubscriber);
            Session["Subscribed"] = "ok";
            db.SaveChanges();
            return RedirectToAction("Event");
        }

        public ActionResult SendEventMail(String youtubelink)
        {
            string MailSend = "yengreenliving@gmail.com";
            string Password = "yenmail@123";
            Event newEvent = db.Event
                        .OrderByDescending(p => p.EventDate)
                       .FirstOrDefault();
            List<EventSubscriber> listSubscribers = new List<EventSubscriber>();
            listSubscribers = newEvent.EventSubscribers.ToList();
            new Thread(() => {

                foreach(EventSubscriber email in listSubscribers)
                {
                    using (MailMessage m = new MailMessage(MailSend, email.SubscriberEmail))
                    {
                        m.Subject = "Yen Live is live: " + newEvent.EventName;
                        m.Body = ("Watch live at: " + youtubelink);
                        m.IsBodyHtml = true;
                        using (SmtpClient smtp = new SmtpClient())
                        {
                            smtp.Host = "smtp.gmail.com";
                            smtp.EnableSsl = true;
                            NetworkCredential networkCred = new NetworkCredential(MailSend, Password);
                            smtp.UseDefaultCredentials = true;
                            smtp.Credentials = networkCred;
                            smtp.Port = 587;
                            smtp.Send(m);
                            ViewBag.Message = "";
                        }
                    }
                }

            }).Start();
            return RedirectToAction("EventSubscriberList", "Admin");
        }
        public ActionResult Event()
        {
            Event newEvent = db.Event
                        .OrderByDescending(p => p.EventDate)
                       .FirstOrDefault();
            return View(newEvent);
        }
        [HttpPost]
        public ActionResult LikePost(int id)
        {
            int userid = (int)Session["Id"];
            var user = db.userModels.Find(userid);
            var post = db.Post.Find(id);
            int getLike = post.PostLike + 1;
            post.PostLike = getLike;
            user.PostsLike.Add(post);
            db.SaveChanges();
            return RedirectToAction("OurGreen");
        }

        [HttpPost]
        public ActionResult DislikePost(int id)
        {
            int userid = (int)Session["Id"];
            var user = db.userModels.Find(userid);
            var post = db.Post.Find(id);
            int getLike = post.PostLike - 1;
            post.PostLike = getLike;
            user.PostsLike.Remove(post);
            db.SaveChanges();
            return RedirectToAction("OurGreen");
        }

        [HttpPost]
        public ActionResult Comment(int id, string comment)
        {
            PostComment newComment = new PostComment();
            newComment.PostCommentAuthor = (string)Session["FullName"];
            newComment.PostCommentContent = comment;
            DateTime now = DateTime.Now;
            newComment.PostCommentDate = now.ToString("dd/MM/yyyy HH:mm tt");
            var Post = db.Post.Find(id);
            Post.PostComment.Add(newComment);
            db.SaveChanges();
            return RedirectToAction("OurGreen");
        }
        public ActionResult OurGreen()
        {

            return View(db.Post.OrderByDescending(m => m.PostDate).ToList());
        }

        [HttpPost]
        public ActionResult Post(String inputcontent, String imagebase64)
        {
            DateTime currentTime = DateTime.Now;

            Post newPost = new Post();

            newPost.PostContent = inputcontent;
            newPost.PostImage = imagebase64;
            newPost.PostDate = currentTime;
            newPost.PostAuthor = Session["FullName"].ToString();

            db.Post.Add(newPost);
            db.SaveChanges();

            return RedirectToAction("OurGreen");
        }

        [HttpPost]
        public ActionResult Signup(String username, String fullname, String password, String confirmpassword)
        {
            User newUser = new User();
            newUser.Username = username;
            newUser.FullName = fullname;
            newUser.Password = password;
            newUser.Confirm = confirmpassword;

            db.userModels.Add(newUser);

            db.SaveChanges();

            var checkUser = db.userModels.SingleOrDefault(u => u.Username == username && u.Password == password);
            if (checkUser != null)
            {
                Session["Id"] = checkUser.Id;
                Session["FullName"] = checkUser.FullName;
                int id = (int)Session["Id"];
                Session["InvalidUser"] = null;
                return RedirectToAction("OurGreen");
            }

            return RedirectToAction("OurGreen");
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (username == null || password == null)
            {
                Session["InvalidUser"] = "Username or Password is empty";
                return RedirectToAction("OurGreen");
            }
            else
            {
                var checkUser = db.userModels.SingleOrDefault(u => u.Username == username && u.Password == password);
                if (checkUser != null)
                {
                    Session["Id"] = checkUser.Id;
                    Session["FullName"] = checkUser.FullName;
                    int id = (int)Session["Id"];
                    Session["InvalidUser"] = null;
                    return RedirectToAction("OurGreen");
                }
                else
                {
                    Session["InvalidUser"] = "Invalid Username or Password";
                    return RedirectToAction("OurGreen");
                }
                    
            }
        }
        public ActionResult Logout()
        {
            int id = (int)Session["Id"];
            var user = db.userModels.Find(id);
            if (user != null)
            {
                Session["Id"] = null;
                return RedirectToAction("OurGreen");
            }
            else
            {
                Session["Id"] = null;
                Session["Username"] = null;
                return RedirectToAction("OurGreen");
            }
        }

        static string GetSHA256(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // GET: Community/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Community/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Community/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostID,PostAuthor,PostDate,PostContent,PostLike")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Post.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Community/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Community/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostID,PostAuthor,PostDate,PostContent,PostLike")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Community/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Community/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Post.Find(id);
            db.Post.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
