using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Blog.Models.DbModels;

namespace Blog.Db.Controllers
{
    public class IndentityDbContext : IdentityDbContext<UserIndentity, IdentityRole, string> 
    {

        public IndentityDbContext(DbContextOptions<IndentityDbContext> optionsBuilder) : base(optionsBuilder){

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
