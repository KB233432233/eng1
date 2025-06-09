using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace TaskManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private static List<TaskModel> _tasks = new List<TaskModel>();
        private static int _nextId = 1;

        public IActionResult Index()
        {
            return View(_tasks);
        }

        [HttpPost]
        public IActionResult Add(string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                _tasks.Add(new TaskModel
                {
                    Id = _nextId++,
                    Title = title,
                    IsCompleted = false
                });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Complete(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.IsCompleted = true;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                var taskToUpdate = _tasks.FirstOrDefault(t => t.Id == task.Id);
                if (taskToUpdate != null)
                {
                    // Update only the title if needed
                    taskToUpdate.Title = task.Title;
                    
                    // If you want to update completion status too:
                    // taskToUpdate.IsCompleted = task.IsCompleted;
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                _tasks.Remove(task);
            }
            return RedirectToAction("Index");
        }
    }

    public class TaskModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
