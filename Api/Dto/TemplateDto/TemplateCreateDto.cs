using System.ComponentModel.DataAnnotations;

namespace Api.Dto.TemplateDto;

public class TemplateCreateDto
{
    [Required]
    [MaxLength(255)]
    public string Title { get; set; }

    public string Description { get; set; }

    [MaxLength(100)]
    public string Topic { get; set; }
        
    public string ImageUrl { get; set; }

    public bool IsPublic { get; set; }  

}