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
    public class PublicationController : Controller
    {
        UserManager<User> _userManager;
        private readonly IUser _user;
        private readonly IPublication _publication;

        public PublicationController(UserManager<User> userManager, IUser iUser, IPublication iPublication)
        {
            _user = iUser;
            _publication = iPublication;
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

        [HttpPost]
        public IActionResult CreatePost(CreatePublicationViewModel model)
        {
            Publication publ = new Publication {
                PublicationName = model.PublicationName,
                Discription = model.Discription,
                PublicationText = model.PublicationText,
                ImgUrlPublication = model.ImgUrlPublication,
                isFavorite = model.isFavorite,
            };

            _publication.AddPublicationDB(publ);
            return RedirectToAction("CreatePost");
        }
    }
}