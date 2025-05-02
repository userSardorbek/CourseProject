using CourseProject.Models;

namespace Api.Model;

public class TemplateAccess
{
    public int TemplateAccessId { get; set; }

    public long TemplateId { get; set; }
    public Template Template { get; set; }

    public string UserId { get; set; }
    public User User { get; set; }
}