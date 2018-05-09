using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Blog.Models.DbModels;

namespace Blog.Db.Controllers
{
    public class TagContext : DbContext 
    {
        public DbSet<Tag> Tags { get; set; }
        public TagContext(DbContextOptions<TagContext> optionsBuilder) : base(optionsBuilder){

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
