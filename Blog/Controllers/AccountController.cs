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
        public const string SEPARATOR = "|";
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

            var splitFollowers = SplitField(user.FolowersId, SEPARATOR);
            var folowersEntity = await GetElements(splitFollowers, _userManager.Users.ToArray());
            var followersPosts = FollowerPosts(folowersEntity);
            var newsViewModel = new NewsViewModel
            {
                UsersActivity = folowersEntity,
                PostsActivity = followersPosts
            };

            ViewData["newsViewModel"] = newsViewModel;

            return View();
        }

        [Route("/followers")]
        public async Task<IActionResult> Followers()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var splitFollowers = SplitField(user.FolowersId, SEPARATOR);
            var folowersEntity = await GetElements(splitFollowers, _userManager.Users.ToArray());
            var followersPosts = FollowerPosts(folowersEntity);
            var followersViewModel = new FollowersViewModel
            {
                UsersActivity = folowersEntity,
                PostsActivity = followersPosts
            };

            ViewData["followersViewModel"] = followersViewModel;

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

        private IEnumerable<Post> FollowerPosts(IEnumerable<UserIndentity> folowers)
        {
            if(folowers == null){
                return null;
            }

            var responceList = new List<Post>();

            IEnumerable<Post> enumerable = null;
            foreach(var follower in folowers){
                enumerable = UserPosts(follower, _postContext);
                
                if(enumerable != null)
                    responceList.AddRange(enumerable);
            }

            return responceList.Count > 0 ? responceList : null;
        }
        

        //Func --> algoritm sort posts
        private IEnumerable<Post> FunctionBeutifulPost(IEnumerable<Post> enumarable)
        {
            return enumarable;
        }

        private async Task<IEnumerable<T>> GetElements<T>(IEnumerable<String> elementsId, IEnumerable<T> context) where T : class
        {
            return await Task.Run(() =>
            {
                if(elementsId == null)
                    return null;
            

                var responceElements = new List<T>();

                T element = null;
                foreach (var strId in elementsId)
                {
                    element = context.FirstOrDefault(t => (string)(t.GetType().GetProperty("Id").GetValue(t)) == strId);
                    if (element != null)
                    {
                        responceElements.Add(element);
                    }

                    element = null;
                }

                return responceElements.Count == 0 ? null : responceElements;

            });
        }

        public IEnumerable<string> SplitField(string field, string separator)
        {
            return field?.Split(separator) ?? null;
        }
        #endregion
    }
}
