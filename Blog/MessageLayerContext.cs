using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Blog.Models.DbModels;
using Blog.Models.DbModels.Layer;

namespace Blog.Db.Controllers
{
    public class MessageLayerContext : DbContext 
    {
        //TODO!!!!!!! VJOPU!
        public DbSet<MessagesLayer> CommentsLayer { get; set; }
        public MessageLayerContext(DbContextOptions<MessageLayerContext> optionsBuilder) : base(optionsBuilder){

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
