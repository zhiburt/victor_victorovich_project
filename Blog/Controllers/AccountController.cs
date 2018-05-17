using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Blog.Models.DbModels;
using Blog.Db.Controllers;
using Blog.ViewModel.Authefication;
using Blog.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Controllers
{
    [Authorize]
    [Route("/Acc")]
    public class AccountController : Controller
    {
        private readonly UserManager<UserIndentity> _userManager;
        private readonly SignInManager<UserIndentity> _signInManager;
        private readonly ILogger<AutheficationController> _logger;
        private readonly PostContext _postContext;

        public AccountController(
            UserManager<UserIndentity> userManager,
            SignInManager<UserIndentity> signInManager,
            ILogger<AutheficationController> logger,
            PostContext postContext)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
            this._postContext = postContext;
        }

        [Route("/news")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var followersPosts = FollowerPosts(user, _postContext);
            var newsViewModel = new NewsViewModel
            {
                // UsersActivity = user.FolowersId,
                PostsActivity = followersPosts
            };

            ViewData["newsViewModel"] = newsViewModel;

            return View();
        }

        [AllowAnonymous]
        [Route("/profile")]
        public async Task<IActionResult> Profile(string userId, string returnUrl = null)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound("message");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userPosts = _postContext.Posts.Where(post => post.CreatorId == user.Id);

                var profileViewModel = new ProfileViewModel
                {
                    User = user,
                    Likes = 12,
                    Rating = 30,
                    Followers = 112,
                    Posts = userPosts
                };

                return View(profileViewModel);
            }

            return NotFound(userId);
        }

        #region private

        private IEnumerable<Post> UserPosts(UserIndentity user, PostContext postContext)
        {
            return postContext.Posts.Where(post => post.CreatorId == user.Id);
        }

        private IEnumerable<Post> FollowerPosts(UserIndentity user, PostContext postContext)
        {
            // var followers = user.Folowers?.ToList();
            // if (followers != null)
            // {
            //     var list = new List<Post>();

            //     IEnumerable<Post> posts;
            //     foreach (var item in followers)
            //     {
            //         posts = UserPosts(item, postContext);
            //         list.AddRange(posts);
            //     }

            //     IEnumerable<Post> supperPosts;
            //     supperPosts = FunctionBeutifulPost(list);

            //     return supperPosts;
            // }

            return null;
        }

        //Func --> algoritm sort posts
        private IEnumerable<Post> FunctionBeutifulPost(IEnumerable<Post> enumarable)
        {
            return enumarable;
        }

        #endregion
    }
}
