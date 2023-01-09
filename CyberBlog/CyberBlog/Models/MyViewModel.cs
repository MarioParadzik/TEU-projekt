using CyberBlog.Entities;

namespace CyberBlog.Models;
public class MyViewModel
{
    public IEnumerable<Message> Messages { get; set; }
    public IEnumerable<Article> Articles { get; set; }
    public string ErrorMessage { get; set; }
}

