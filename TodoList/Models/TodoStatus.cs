﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models
{
    public class TodoStatus
    {
        [Required]
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
