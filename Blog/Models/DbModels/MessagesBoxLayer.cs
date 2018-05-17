using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System;

namespace Blog.Models.DbModels
{    public class MessagesBoxLayer
    {
        [Key]
        public int Id { get; set; }
        public string MessageBoxesId { get; set; }
    }
}
