using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {   
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<TodoItem> TodoItem { get; set; }
        public DbSet<TodoStatus> TodoStatus { get; set; }


    }
}
