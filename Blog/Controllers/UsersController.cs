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
    public class UsersController : Controller
    {
        public const string SEPARATOR = "|";
        private readonly UserManager<UserIndentity> _userManager;
        private readonly SignInManager<UserIndentity> _signInManager;
        private readonly ILogger<UsersController> _logger;
        private readonly PostContext _postContext;

        public UsersController(
            UserManager<UserIndentity> userManager,
            SignInManager<UserIndentity> signInManager,
            ILogger<UsersController> logger,
            PostContext postContext)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
            this._postContext = postContext;
        }

        [Route("/find")]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() =>
            {

                var users = _userManager.Users;
                ViewData["Users"] = users;

                return View();
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddFollow(string userId, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                var follower = await _userManager.FindByIdAsync(userId);
                
                if (follower != null &&
                    follower != currentUser)
                {
                    if(!currentUser.FolowersId?.Contains(userId) ?? true){

                        currentUser.FolowersId = AddNewInfo(currentUser.FolowersId, follower.Id, SEPARATOR);

                        await _userManager.UpdateAsync(currentUser);

                        return RedirectToLocal(returnUrl);
                    }
                }
                else
                    NotFound("NOT FIND USER");
            }

            return new StatusCodeResult(404);
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

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(AccountController.Index), "Index");
            }
        }

        public IEnumerable<string> SplitField(string field, string separator)
        {
            return field?.Split(separator) ?? null;
        }

        public string AddNewInfo(string field, string newField, string separator)
        {
            if (field == null)
            {
                return newField;
            }
            return (field + separator + newField);
        }

        #endregion
    }
}
