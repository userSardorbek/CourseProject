using System.ComponentModel.DataAnnotations;
using CourseProject.Models;

namespace Api.Model;

public class Comment
{
    public Guid CommentId { get; set; }  = Guid.NewGuid();

    [Required]
    public string Text { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    public string UserId { get; set; }
    public User Author { get; set; }

    public long TemplateId { get; set; }
    public Template Template { get; set; }
}