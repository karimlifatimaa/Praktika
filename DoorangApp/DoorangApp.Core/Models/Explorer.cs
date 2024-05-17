﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorangApp.Core.Models
{
    public class Explorer:BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Subtitle { get; set; }
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile? PhotoFile { get; set; }
    }
}
