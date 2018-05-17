using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System;
using Blog.Models.DbModels;

namespace Blog.Models.PostController
{
    public class PostViewModel
    {
        public UserIndentity Creator { get; set; }
        public string Theme { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Repost> Reposts { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; } 
    }
}
