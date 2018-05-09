using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models.DbModels
{
    public class Tag
    {
        [Key]
        public string Id { get; set; }
        public UserIndentity UserCreated { get; set; }
        public string Name { get; set; }
        public double Fame { get; set; }
    }
}