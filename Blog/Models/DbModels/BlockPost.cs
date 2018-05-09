using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models.DbModels
{
    public class BlockPost 
    {
        [Key]
        public string Id { get; set; }
        public UserIndentity Creator { get; set; }
        public string Theme { get; set; }
        public string Content { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Repost> Reposts { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; } 
        public List<Post> Posts { get; set; }
    }
}
