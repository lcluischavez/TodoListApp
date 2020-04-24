using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Controllers
{
    [Authorize]
    public class TodoItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TodoItemsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: TodoItems
        public async Task<ActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var todoItems = await _context.TodoItem
                .Where(ti => ti.ApplicationUserId == user.Id)
                .Include(tdi => tdi.ApplicationUser)
                .Include(tdi => tdi.TodoStatus)
                .ToListAsync();

            return View(todoItems);
        }

        // GET: TodoItems/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TodoItems/Create
        public ActionResult Create()
        {
            var allTodoStatuses = _context.TodoStatus
                .Select(s => new SelectListItem() { Text = s.Title, Value = s.Id.ToString() })
                .ToList();

            var viewModel = new TodoItemCreateViewModel();

            viewModel.TodoStatusOptions = allTodoStatuses;

            return View(viewModel);
        }

        // POST: TodoItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TodoItem todoItem)
        {
            try
            {
                var user = await GetCurrentUserAsync();
                todoItem.ApplicationUserId = user.Id;

                _context.TodoItem.Add(todoItem);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TodoItems/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var allTodoStatuses = _context.TodoStatus
                .Select(s => new SelectListItem() { Text = s.Title, Value = s.Id.ToString() })
                .ToList();

            var viewModel = new TodoItemCreateViewModel();

            viewModel.TodoStatusOptions = allTodoStatuses;

            var item = await _context.TodoItem.FirstOrDefaultAsync(ti => ti.Id == id);
            var loggedInUser = await GetCurrentUserAsync();

            if (item.ApplicationUserId != loggedInUser.Id)
            {
                return NotFound();
            }

            return View(viewModel);
        }



        // POST: TodoItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TodoItem todoItem)
        {
            try
            {
                var user = await GetCurrentUserAsync();
                todoItem.ApplicationUserId = user.Id;

                _context.TodoItem.Update(todoItem);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TodoItems/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TodoItems/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}