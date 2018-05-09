using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Blog.Models.DbModels;

namespace Blog.Db.Controllers
{
    public class LikeLayerContext : DbContext 
    {
        public DbSet<LikeLayer> LikesLayer { get; set; }
        public LikeLayerContext(DbContextOptions<LikeLayerContext> optionsBuilder) : base(optionsBuilder){

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
