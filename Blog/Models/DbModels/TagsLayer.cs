using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System;

namespace Blog.Models.DbModels
{    public class TagsLayer
    {
        [Key]
        public int Id { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
