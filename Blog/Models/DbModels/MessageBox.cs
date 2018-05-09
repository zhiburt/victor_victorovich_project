using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System;
using Blog.Models.DbModels.Layer;

namespace Blog.Models.DbModels
{
    public enum MessageBoxLevel
    {
        UnImportant = -1, Simple = 0, Important = 1
    }
    public class MessageBox
    {
        [Key]
        public int Id { get; set; }
        public UserIndentity Creator { get; set; }
        public List<UserIndentity> OtherUsers { get; set; }
        public MessagesLayer Messages { get; set; }
        public DateTime TimeCreated { get; set; }
        public string Content { get; set; }
        public MessageBoxLevel MessageBoxLevel { get; set; }
    }
}
