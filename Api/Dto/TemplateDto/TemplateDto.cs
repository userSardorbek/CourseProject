namespace Api.Dto.TemplateDto;

public class TemplateDto
{
    public long TemplateId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Topic { get; set; }

    public string ImageUrl { get; set; }

    public DateTime CreationDate { get; set; }

    public bool IsPublic { get; set; }
    
    public string CreatorId { get; set; }

}