using System.ComponentModel.DataAnnotations;

namespace CyberBlog.Models;
public class ArticleViewModel
{
    [Required(ErrorMessage = "Please enter Title")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Please enter Topic")]
    public string Topic { get; set; }
    [Required(ErrorMessage = "Please enter Description")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Please choose cover image")]
    public IFormFile CoverImage { get; set; }
    [Required(ErrorMessage = "Please enter Paragraph 1")]
    public string Paragraph1 { get; set; }
    [Required(ErrorMessage = "Please enter Paragraph 2")]
    public string Paragraph2 { get; set; }
    public IFormFile? Image1 { get; set; }
    public string? Paragraph3 { get; set; }
    public string? Paragraph4 { get; set; }
}

