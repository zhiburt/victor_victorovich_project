using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models.DbModels
{
    public class Repost
    {
        [Key]
        public string Id { get; set; }
        public Post NaturePost { get; set; }
        public string Content { get; set; }
        public LikeLayer Likes { get; set; }
    }
}
