using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Blog.Models.DbModels;

namespace Blog.Db.Controllers
{
    public class TagsLayerContext : DbContext 
    {
        public DbSet<TagsLayer> Comments { get; set; }
        public TagsLayerContext(DbContextOptions<TagsLayerContext> optionsBuilder) : base(optionsBuilder){

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
