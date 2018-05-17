using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Blog.Models.DbModels;

namespace Blog.ViewModels.Account
{
    public class ProfileViewModel
    {
        public UserIndentity User { get; set; }
        public int Rating { get; set; }
        public int Likes { get; set; }
        public int Followers { get; set; }
        public IEnumerable<Post> Posts { get; set; }


        public static string ResponceHtml(string html, int lenght)
        {
            string template = $@"";
            var math = Regex.Match(html, template);
            
            return null;
        }

        public static string FirstImgSrcHtml(string html)
        {
            string template = "<img.+?src=[\"'](.+?)[\"'].*?>";
            var math = Regex.Match(html, template, RegexOptions.IgnoreCase);
            
            return math.Groups[1].Value;
        }
    }

}