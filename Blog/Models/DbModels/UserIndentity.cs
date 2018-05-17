using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System;

namespace Blog.Models.DbModels
{
    public class UserIndentity : IdentityUser 
    {
        //Properties User
        public String Picture { get; set; } 
        public string MessagesBoxLayerId { get; set; }
        public string FolowersId { get; set; }
    }
}
