using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System;

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
        public string Name { get; set; }
        public string CreatorId { get; set; }
        public string OtherUsersId { get; set; }
        public string MessagesId { get; set; }
        public DateTime TimeCreated { get; set; }
        public string Content { get; set; }
        public MessageBoxLevel MessageBoxLevel { get; set; }
    }
}
