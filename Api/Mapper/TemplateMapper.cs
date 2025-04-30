using Api.Dto.TemplateDto;
using Api.Model;

namespace Api.Mapper;

public static class TemplateMapper
{
    public static TemplateDto? TemplateToTemplateDto(this Template template)
    {
        return new TemplateDto()
        {
            TemplateId = template.TemplateId,
            CreationDate = template.CreationDate,
            Description = template.Description,
            ImageUrl = template.ImageUrl,
            IsPublic = template.IsPublic,
            Topic = template.Topic,
            Title = template.Title,
            CreatorId = template.CreatorId
        };
    }
}