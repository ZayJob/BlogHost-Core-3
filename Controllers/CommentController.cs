using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogHost.Data.Interfaces;
using BlogHost.Data.Models;
using BlogHost.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogHost.Controllers
{
    public class CommentController : Controller
    {
        UserManager<User> _userManager;
        private readonly IUser _user;
        private readonly IPublication _publication;
        private readonly ITopic _topic;
        private readonly IComment _comment;

        public CommentController(UserManager<User> userManager, IUser iUser, IPublication iPublication, ITopic iTopic, IComment iComment)
        {
            _user = iUser;
            _publication = iPublication;
            _topic = iTopic;
            _userManager = userManager;
            _comment = iComment;
        }

        public static int ID;

        public IActionResult CreateComment(int id)
        {
            ID = id;
            return View();
        }

        [HttpPost]
        public IActionResult CreateComment(CreateCommentViewModel model)
        {

            Comment comment = new Comment
            {
                CommentText = model.CommentText,
                LoginUser = _user.GetUserDB(_userManager.GetUserId(User)).UserName,
                PublicationID = ID,
                Publication = _publication.GetPostDB(ID)
            };

            _comment.AddCommentDB(comment);
            return RedirectToAction("AllPosts","Publication");
        }

        [HttpGet]
        public IActionResult AllComments()
        {
            return View(_comment.AllComments());
        }
    }
}