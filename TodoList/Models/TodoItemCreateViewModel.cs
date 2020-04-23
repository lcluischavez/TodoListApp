using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models
{
    public class TodoItemCreateViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int TodoStatusId { get; set; }
        public List<SelectListItem> TodoStatusOptions { get; set; }
    }
}
