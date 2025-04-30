using CourseProject.Models;

namespace Api.Model;

public class Answer
{
    public Guid AnswerId { get; set; } = Guid.NewGuid();

    public long FormId { get; set; }
    public Form Form { get; set; }

    public long QuestionId { get; set; }
    public Question Question { get; set; }

    public string Value { get; set; }
}