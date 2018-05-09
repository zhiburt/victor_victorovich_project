using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models.DbModels
{
    public class Comment
    {
        [Key]
        public string Id { get; set; }
        public UserIndentity User { get; set; }
        public UserIndentity AbsentUser { get; set; }
        public string Content { get; set; }
        public List<Like> Likes { get; set; }
        public List<Repost> Reposts { get; set; }
    }
}
