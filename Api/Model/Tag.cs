using System.ComponentModel.DataAnnotations;
using CourseProject.Models;

namespace Api.Model;

public class Tag
{
    public int TagId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public ICollection<Template> Templates { get; set; } = new List<Template>();
}