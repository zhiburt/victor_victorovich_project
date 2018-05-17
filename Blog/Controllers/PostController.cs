using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Models;
using Blog.ViewModels.PostController;
using Microsoft.AspNetCore.Identity;
using Blog.Models.DbModels;
using Microsoft.Extensions.Logging;
using Blog.Db.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Blog.Models.PostController;
using System.Text;
using System.Reflection;

//TODO: BUGS IF TAGS AND ... == null
//TODO: CHEKS NULL PARAMETRS!

namespace Blog.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        public const string SEPARATOR = "|";
        private readonly UserManager<UserIndentity> _userManager;
        private readonly SignInManager<UserIndentity> _signInManager;
        private readonly ILogger<AutheficationController> _logger;
        private readonly PostContext _postContext;
        private readonly TagContext _tagContext;

        public PostController(
            UserManager<UserIndentity> userManager,
            SignInManager<UserIndentity> signInManager,
            ILogger<AutheficationController> logger,
            PostContext postContext,
            TagContext tagContext)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
            this._postContext = postContext;
            this._tagContext = tagContext;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string postId, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var post = await _postContext.Posts.FirstOrDefaultAsync(p => p.Id == postId);
                var tags = await GetTags(SplitField(post.TagsId, SEPARATOR));
                var tests = await GetElements(SplitField(post.TagsId, SEPARATOR), _tagContext.Tags.ToArray());

                var user = await _userManager.FindByIdAsync(post.CreatorId);
                if (post != null)
                {
                    var postViewModel = new PostViewModel
                    {
                        Theme = post.Theme,
                        // Likes = SplitField(post.LikesId,SEPARATOR),
                        Tags = tags.ToList(),
                        // Reposts = post.Reposts.ToList(),
                        Content = post.Content,
                        Creator = user,
                        Date = post.Date,
                        // Comments = post.Comments.ToList(),
                        Title = post.Title
                    };

                    return View(postViewModel);
                }
            }

            return NotFound("not found post");
        }

        [AllowAnonymous]
        public IActionResult Post(string postId, string returnUrl = null)
        {
            return View();
        }


        public IActionResult Create(string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreatePostViewModel createPostViewModel, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                var stateTitle = await _postContext.Posts.FirstOrDefaultAsync(post =>
                       post.Title == createPostViewModel.Title &&
                       post.CreatorId == user.Id
                );

                if (stateTitle == null)
                {
                    //TODO SNIPPING TAGS!!!! --> 1string
                    var validTags = ParsTags(createPostViewModel.Tags);
                    var tags = await GetTags(validTags, user);
                    var post = new Post
                    {
                        CreatorId = user.Id,
                        Theme = createPostViewModel.Theme,
                        Content = createPostViewModel.Body,
                        Title = createPostViewModel.Title,
                        Date = DateTime.UtcNow,
                        TagsId = TagsToIdString(tags, SEPARATOR)

                    };

                    await _postContext.Posts.AddAsync(post);

                    await _postContext.SaveChangesAsync();
                    return RedirectToLocal(returnUrl);
                }
                else
                    ModelState.AddModelError(String.Empty, "Invalid title !!");
            }

            return View(createPostViewModel);
        }

        #region Private

        private async Task<IEnumerable<Tag>> GetTags(IEnumerable<String> tagsId)
        {
            var tagsContext = _tagContext.Tags.ToArray();
            var responceTags = new List<Tag>();

            Tag tag = null;
            foreach (var strTagId in tagsId)
            {
                tag = await _tagContext.Tags.FirstOrDefaultAsync(t => t.Id == strTagId);
                if (tag != null)
                {
                    responceTags.Add(tag);
                }

                tag = null;
            }

            return responceTags.Count == 0 ? null : responceTags;
        }


        private async Task<IEnumerable<T>> GetElements<T>(IEnumerable<String> elementsId, IEnumerable<T> context) where T : class
        {
            return await Task.Run(() =>
            {
                var responceElements = new List<T>();

                Type type = typeof(T);
                Type modelType = CheckType(type);

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

        private Type CheckType(Type type)
        {


            return null;
        }

        private async Task<IEnumerable<Tag>> GetTags(IEnumerable<String> tags, UserIndentity user)
        {
            if(tags == null){
                return null;
            }

            var tagsContext = _tagContext.Tags.ToArray();
            var responceTags = new List<Tag>();

            Tag tag = null;
            foreach (var strTag in tags)
            {
                tag = await _tagContext.Tags.FirstOrDefaultAsync(t => t.Name == strTag);
                if (tag == null)
                {

                    tag = new Tag
                    {
                        UserCreatedId = user.Id,
                        Name = strTag
                    };

                    await _tagContext.Tags.AddAsync(tag);
                }

                responceTags.Add(tag);
                tag = null;
            }

            await _tagContext.SaveChangesAsync();

            return responceTags.Count == 0 ? null : responceTags;
        }


        private IEnumerable<string> ParsTags(IEnumerable<string> tags)
        {
            if (tags == null || tags.Count() == 0)
                return null;

            return tags.First().Split(',');
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

        public string TagsToIdString(IEnumerable<Tag> enumerable, string separator)
        {
            if(enumerable == null)
                return null;

            var idEnumerable = enumerable.Select(s => s.Id);

            var builder = new StringBuilder();
            foreach (var item in idEnumerable)
            {
                builder.Append($"{item}{separator}");
            }

            return builder.ToString();
        }
        #endregion
    }
}
