using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Blog.Models.DbModels;

namespace Blog.ViewModels.Account{
    public class FollowersViewModel
    {
        public IEnumerable<UserIndentity> UsersActivity { get; set; }
        public IEnumerable<Post> PostsActivity { get; set; }
    }
}