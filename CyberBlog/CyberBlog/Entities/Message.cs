using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CyberBlog.Entities;
public class Message
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }

    private class MessageBuilder : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable(nameof(Message));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);
            builder.Property(x => x.Title)
                .IsRequired().HasMaxLength(60);
            builder.Property(x => x.Body)
                .IsRequired().HasMaxLength(250);
        }
    }
}

