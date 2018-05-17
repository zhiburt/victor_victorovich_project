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
        public string UserId { get; set; }
        public string AbsentUser { get; set; }
        public string Content { get; set; }
        public string LikesId { get; set; }
        public string RepostsId { get; set; }
    }
}
