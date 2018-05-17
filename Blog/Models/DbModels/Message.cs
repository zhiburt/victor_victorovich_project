using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System;

namespace Blog.Models.DbModels
{
    public enum MessageLevel
    {
        UnImportant = -1, Simple = 0, Important = 1
    }
    public class Message
    {
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime Time { get; set; }
        public string Content { get; set; }
        public MessageLevel MessageLevel { get; set; }
    }
}
