using CourseProject.Models;

namespace Api.Model;

public class Form
{
    public long FormId { get; set; }

    public DateTime SubmissionDate { get; set; } = DateTime.UtcNow;

    public string UserId { get; set; }
    public User User { get; set; } // User who filled the form
    
    public long TemplateId { get; set; }
    public Template Template { get; set; }

    public bool EmailCopyToSubmitter { get; set; } = false; 
    
    public ICollection<Answer> Answers { get; set; }
}