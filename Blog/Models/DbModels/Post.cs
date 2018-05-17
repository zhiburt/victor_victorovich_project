using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System;

namespace Blog.Models.DbModels
{
    public class Post 
    {
        [Key]
        public string Id { get; set; }
        public string CreatorId { get; set; }
        public string Theme { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string TagsId { get; set; }
        public string RepostsId { get; set; }
        public string CommentsId { get; set; }
        public string LikesId { get; set; } 
   }
}
