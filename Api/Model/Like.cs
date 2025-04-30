using System.ComponentModel.DataAnnotations;
using CourseProject.Models;

namespace Api.Model;

public class Like
{
    public long LikeId { get; set; }

    [Required]
    public string UserId { get; set; }
    public User User { get; set; }

    [Required]
    public long TemplateId { get; set; }
    public Template Template { get; set; }
}