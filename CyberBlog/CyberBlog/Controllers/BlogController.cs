using CyberBlog.DbContexts;
using CyberBlog.Entities;
using CyberBlog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CyberBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;
        private readonly CyberDbContext _context;

        public BlogController(ILogger<BlogController> logger, CyberDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var selected = _context.Article.Where(m => m.Selected == true).ToList();
            return View(selected);
        }

        [HttpPost, ActionName("SendMessage")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Message Message)
        {
            var message = new Message
            {
                Body = Message.Body,
                Title = Message.Title,
            };
            _context.Message.Add(message);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}