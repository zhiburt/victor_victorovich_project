using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models.DbModels
{
    public class Post 
    {
        [Key]
        public string Id { get; set; }
        public UserIndentity Creator { get; set; }
        public string Theme { get; set; }
        public string Content { get; set; }
        public TagsLayer Tags { get; set; }
        public RepostsLayer Reposts { get; set; }
        public CommentsLayer Comments { get; set; }
        public LikeLayer Likes { get; set; } 
    }
}
