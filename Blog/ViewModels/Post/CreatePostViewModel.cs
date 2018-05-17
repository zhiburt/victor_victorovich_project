using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Blog.Models.DbModels;

namespace Blog.ViewModels.PostController{
    public class CreatePostViewModel
    {
        public String Theme { get; set; }
        public IEnumerable<String> Tags { get; set; }
        public String Title { get; set; }
        public String Body { get; set; }
        public String MainImage { get; set; }
    }
}