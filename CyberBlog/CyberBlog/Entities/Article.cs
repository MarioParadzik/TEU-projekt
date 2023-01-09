using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace CyberBlog.Entities;
public class Article
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Please enter Title")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Please enter Topic")]
    public string Topic { get; set; }
    [Required(ErrorMessage = "Please enter Description")]
    public string Description { get; set; }

    public string CoverImage { get; set; }
    [Required(ErrorMessage = "Please enter Paragraph 1")]
    public string Paragraph1 { get; set; }
    [Required(ErrorMessage = "Please enter Paragraph 2")]
    public string Paragraph2 { get; set; }
    public string? Image1 { get; set; }
    public string? Paragraph3 { get; set; }
    public string? Paragraph4 { get; set; }
    public bool Selected { get; set; }
    public DateTime DatePublished { get; set; }

    private class ArticleBuilder : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable(nameof(Article));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);
            builder.Property(x => x.Title)
                .IsRequired();
            builder.Property(x => x.Topic)
                .IsRequired();
            builder.Property(x => x.Description)
                .IsRequired();
            builder.Property(x => x.CoverImage)
                .IsRequired();
            builder.Property(x => x.Paragraph1)
                .IsRequired();
            builder.Property(x => x.Paragraph2)
                .IsRequired();
            builder.Property(x => x.Image1);
            builder.Property(x => x.Paragraph3);
            builder.Property(x => x.Paragraph4);
            builder.Property(x => x.Selected);
            builder.Property(x => x.DatePublished);
        }
    }
}

