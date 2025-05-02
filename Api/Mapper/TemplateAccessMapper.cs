using Api.Dto.TemplateAccessDto;
using Api.Model;

namespace Api.Mapper;

public static class TemplateAccessMapper
{
    public static TemplateAccessDto TemplateAccessToDto(this TemplateAccess templateAccess)
    {
        return new TemplateAccessDto()
        {
            UserId = templateAccess.UserId,
            TemplateId = templateAccess.TemplateId
        };
    } 
}