using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    public class TodosController : Controller
    {
        private readonly TodoContext tContext;

        public TodosController(TodoContext context)
        {
            tContext = context;
        }

        public IActionResult Index(string filter)
        {
            var todos = tContext.Todos.AsQueryable();

            if (filter == "done")
            {
                todos = todos.Where(t => t.IsDone);
            }
            else if (filter == "todo")
            {
                todos = todos.Where(t => !t.IsDone);
            }

            return View(todos.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Todo todo)
        {
            if (ModelState.IsValid)
            {
                tContext.Todos.Add(todo);
                tContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(todo);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var todo = tContext.Todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var todo = tContext.Todos.Find(id);
            if (todo != null)
            {
                tContext.Todos.Remove(todo);
                tContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkDone(int id)
        {
            var todo = tContext.Todos.Find(id);
            if (todo != null)
            {
                todo.IsDone = !todo.IsDone;
                tContext.SaveChanges();
            }

            return RedirectToAction("Index");

        }

    }
}
