using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogHost.Data.Interfaces;
using BlogHost.Data.Models;
using BlogHost.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BlogHost.Initializer;

namespace BlogHost.Controllers
{
    public class PublicationController : Controller
    {
        UserManager<User> _userManager;
        private readonly IUser _user;
        private readonly IPublication _publication;
        private readonly ITopic _topic;

        public PublicationController(UserManager<User> userManager, IUser iUser, IPublication iPublication, ITopic iTopic)
        {
            _user = iUser;
            _publication = iPublication;
            _topic = iTopic;
            _userManager = userManager;
        }

        public IActionResult CreatePost()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AllPosts()
        {
            return View(_publication.AllPost());
        }

        [HttpGet]
        public IActionResult ThemsPost(string nameTopic)
        {
            return View(_publication.AllThemsPost(_topic.GetIDTopicDB(nameTopic)));
        }

        [HttpPost]
        public IActionResult CreatePost(CreatePublicationViewModel model)
        {
            string a = _userManager.GetUserId(User);
            Publication publ = new Publication {
                PublicationName = model.PublicationName,
                Discription = model.Discription,
                PublicationText = model.PublicationText,
                ImgUrlPublication = model.ImgUrlPublication,
                isFavorite = model.isFavorite,
                TopicId = _topic.GetTopicDB(model.TopicName).Id,
                Topic = _topic.GetTopicDB(model.TopicName),
                User = _user.GetUserDB(_userManager.GetUserId(User)),
            };

            _publication.AddPublicationDB(publ);
            return RedirectToAction("CreatePost");
        }
    }
}