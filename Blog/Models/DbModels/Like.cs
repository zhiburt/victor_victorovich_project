using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models.DbModels
{
    public enum LikeLevel
    {
        Dislike = -1, Like = 1
    }
    public class Like
    {
        [Key]
        public string Id { get; set; }
        public UserIndentity User { get; set; }
        public LikeLevel LikeLevel { get; set; }
    }
}
