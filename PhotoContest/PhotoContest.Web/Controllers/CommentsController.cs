namespace PhotoContest.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Data.Contracts;
    using InputModels;
    using PhotoContest.Models;
    using ViewModels;

    public class CommentsController : BaseController
    {
        public CommentsController(IPhotoContestData data)
            : base(data)
        {
        }

        // GET: Comments
        public ActionResult Index(int id)
        {
            var commentViewModel = new PictureCommentsViewModel()
            {
                Comments = this.Data.Comments
                    .All()
                    .Where(p => p.PictureId == id)
                    .OrderByDescending(c => c.CreatedOn)
                    .ProjectTo<CommentViewModel>(),
                PictureId = id
            };

            return this.PartialView("Partial/_Index", commentViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CommentInputModel();
            return this.PartialView("Partial/_Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentInputModel model, int id)
        {
            var comment = new Comment()
            {
                Text = model.Text,
                Author = this.UserProfile,
                CreatedOn = DateTime.Now,
                PictureId = id,
                IsDeleted = false
            };

            this.Data.Comments.Add(comment);

            this.Data.SaveChanges();

            return this.PartialView("DisplayTemplates/CommentViewModel", new CommentViewModel
            {
                Author = comment.Author,
                Id = comment.Id,
                PictureId = comment.PictureId,
                Text = comment.Text
            });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var comment = this.Data.Comments.GetById(id);

            if (comment == null)
            {
                return HttpNotFound();
            }

            if (comment.Author != this.UserProfile)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You cannot delete a comment which is not yours.");
            }

            this.Data.Comments.Delete(comment);

            this.Data.SaveChanges();

            return new EmptyResult();
        }

        public ActionResult Edit(int id)
        {
            var oldComment = this.Data.Comments.GetById(id);

            if (oldComment == null)
            {
                return this.HttpNotFound();
            }

            if (oldComment.Author != this.UserProfile)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You cannot edit a comment which is not yours.");
            }

            return this.PartialView("Partial/_Edit", new CommentViewModel()
            {
                Id = oldComment.Id,
                PictureId = oldComment.PictureId,
                Text = oldComment.Text,
                Author = oldComment.Author
            });
        }

        public ActionResult Update(CommentViewModel model)
        {
            var comment = this.Data.Comments.GetById(model.Id);
            if (comment == null)
            {
                return this.HttpNotFound();
            }
            if (comment.Author != this.UserProfile)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest,
                    "You cannot edit a picture which is not yours.");
            }

            comment.Text = model.Text;

            this.Data.SaveChanges();

            return this.PartialView("DisplayTemplates/CommentViewModel", new CommentViewModel
            {
                Author = comment.Author,
                Id = comment.Id,
                PictureId = comment.PictureId,
                Text = comment.Text
            });
        }
    }
}