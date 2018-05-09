using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System;

namespace Blog.Models.DbModels.Layer
{    
    public class MessagesLayer
    {
        [Key]
        public int Id { get; set; }
        public List<Message> Messages { get; set; }
    }
}
