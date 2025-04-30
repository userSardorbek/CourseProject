using System.ComponentModel.DataAnnotations;
using CourseProject.Models;

namespace Api.Model;

public class Question
{
    [Key]
    public long QuestionId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Title { get; set; }

    public string Description { get; set; }

    [Required]
    public QuestionType Type { get; set; }

    public int Order { get; set; }

    public bool ShowInTable { get; set; } = false;

    // For "one from the given list" type
    public string Options { get; set; } // masalan checkbox dagi ma'lumotlar qayerdadir turishi kk ku

    public long TemplateId { get; set; }
    public Template Template { get; set; }
    
    public ICollection<Answer> Answers { get; set; }
}