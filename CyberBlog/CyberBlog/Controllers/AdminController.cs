using CyberBlog.DbContexts;
using CyberBlog.Entities;
using CyberBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyberBlog.Controllers;

[Authorize]
public class AdminController : Controller
{
    private readonly ILogger<ArticleController> _logger;
    private readonly CyberDbContext _context;
    private IWebHostEnvironment _environment;
    public AdminController(ILogger<ArticleController> logger, CyberDbContext context, IWebHostEnvironment environment)
    {
        _logger = logger;
        _context = context;
        _environment = environment;
    }

    public async Task<IActionResult> Index()
    {
        var myViewModel = new MyViewModel
        {
            Messages = await _context.Message.ToListAsync(),
            Articles = await _context.Article.ToListAsync()
        };
        return View(myViewModel);
    }
        


    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ArticleViewModel dbArticle)
    {
        if (ModelState.IsValid)
        {
            string uniqueCoverFileName = UploadedFile(dbArticle, "Cover");
            string uniqueImageFileName = UploadedFile(dbArticle, "Image");

            Article article = new Article
            {
                Title = dbArticle.Title,
                Topic = dbArticle.Topic,
                Description = dbArticle.Description,
                CoverImage = uniqueCoverFileName,
                Paragraph1 = dbArticle.Paragraph1,
                Paragraph2 = dbArticle.Paragraph2,
                Image1 = uniqueImageFileName,
                Paragraph3 = dbArticle.Paragraph3,
                Paragraph4 = dbArticle.Paragraph4,
                DatePublished = DateTime.Now
            };
            _context.Add(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(dbArticle);
    }

    private string UploadedFile(ArticleViewModel model, string image)
    {
        string uniqueFileName = null;

        if (model.CoverImage != null && image == "Cover")
        {
            string uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + model.CoverImage.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                model.CoverImage.CopyTo(fileStream);
            }
        }

        if(model.Image1 != null && image == "Image")
        {
            string uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image1.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                model.Image1.CopyTo(fileStream);
            }
        }
        return uniqueFileName;
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var dbProduct = await _context.Article
            .FirstOrDefaultAsync(m => m.Id == id);
        if (dbProduct == null)
        {
            return NotFound();
        }

        return View(dbProduct);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var dbProduct = await _context.Article.FindAsync(id);
        _context.Article.Remove(dbProduct);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ActionName("UpdateActiveArticle")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateActiveArticle()
    {
        var form = Request.Form["item.Id"].ToString();
        int id = 0;
        try
        {
             id= Int32.Parse(form);
        }
        catch (FormatException)
        {
            return NotFound();
        }

        var dbProduct = await _context.Article
            .FirstOrDefaultAsync(m => m.Id == id);

        if (dbProduct == null)
        {
            return NotFound();
        }

        int selected = _context.Article.Where(m => m.Selected == true).ToList().Count();

        if( selected == 3 && !dbProduct.Selected)
        {
            var myViewModel = new MyViewModel
            {
                Messages = await _context.Message.ToListAsync(),
                Articles = await _context.Article.ToListAsync(),
                ErrorMessage = "Ne možete odabrati više od 3 članka!"
            };
            return View("Index", myViewModel);
        }

        dbProduct.Selected = !dbProduct.Selected;

        _context.Article.Update(dbProduct);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}

