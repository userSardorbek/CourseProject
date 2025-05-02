using Api.Dto.TemplateAccessDto;

namespace Api.Interfaces;

public interface ITemplateAccess
{
    Task<TemplateAccessDto> GiveAccess(string userId, long templateId);
    Task<TemplateAccessDto?> DeleteAccess(string userId, long templateId);
}