using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Blog.Models.DbModels;

namespace Blog.Db.Controllers
{
    public class MessageContext : DbContext 
    {
        public MessageContext(DbContextOptions<MessageContext> optionsBuilder) : base(optionsBuilder){

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Message> Messages { get; set; }
    }
}
