using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System;

namespace Blog.Models.DbModels
{    public class LikeLayer
    {
        [Key]
        public int Id { get; set; }
        public List<Like> Like { get; set; }
    }
}
