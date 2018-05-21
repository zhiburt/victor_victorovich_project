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
    public class AjaxApiPostController : Controller
    {
        public const string SEPARATOR = "|";
        private readonly UserManager<UserIndentity> _userManager;
        private readonly SignInManager<UserIndentity> _signInManager;
        private readonly ILogger<AjaxApiPostController> _logger;
        private readonly PostContext _postContext;
        private readonly TagContext _tagContext;
        private readonly LikeContext _likeContext;

        public AjaxApiPostController(
            UserManager<UserIndentity> userManager,
            SignInManager<UserIndentity> signInManager,
            ILogger<AjaxApiPostController> logger,
            PostContext postContext,
            TagContext tagContext,
            LikeContext LikeContext)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
            this._postContext = postContext;
            this._tagContext = tagContext;
            this._likeContext = LikeContext;
        }

        [Route("ajax/post/addLike")]
        public async Task<bool> AddLike(string postCreatorId, string postTitle, bool likeState)
        {
            if (ModelState.IsValid)
            {
                var post = await _postContext.Posts.FirstOrDefaultAsync(p => 
                    p.Title == postTitle &&
                    p.CreatorId == postCreatorId
                );

                var stringPostLikesId = SplitField(post.LikesId, SEPARATOR);
                var entityLikesPost = await GetElements(stringPostLikesId, _likeContext.Likes);
                var user = await _userManager.GetUserAsync(HttpContext.User);

                if (post != null && user != null)
                {
                    if(entityLikesPost == null || 
                        entityLikesPost.Where(like => like.UserId == user.Id).Count() == 0)
                    {
                        var like = new Like{
                            UserId = user.Id,
                            LikeLevel = likeState ? LikeLevel.Like : LikeLevel.Dislike
                        };

                        await _likeContext.Likes.AddAsync(like);
                        await _likeContext.SaveChangesAsync();

                        post.LikesId = AddNewInfo(post.LikesId, like.Id, SEPARATOR);
                        await _postContext.SaveChangesAsync();

                        return true;
                    }
                }
            }

            return false;
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
                if(elementsId == null)
                    return null;

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
