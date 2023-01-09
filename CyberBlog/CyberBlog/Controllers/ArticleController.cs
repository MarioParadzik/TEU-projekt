using CyberBlog.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyberBlog.Controllers;
public class ArticleController : Controller
{
    private readonly ILogger<ArticleController> _logger;
    private readonly CyberDbContext _context;
    public ArticleController(ILogger<ArticleController> logger, CyberDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Text(int id)
    {
        var dbProduct = await _context.Article
            .FirstOrDefaultAsync(m => m.Id == id);
        if (dbProduct == null)
        {
            return NotFound();
        }

        return View(dbProduct);
    }
}

