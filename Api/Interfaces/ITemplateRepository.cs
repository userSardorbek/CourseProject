using Api.Dto.TemplateDto;
using Api.Model;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Interfaces;

public interface ITemplateRepository
{
    Task<IEnumerable<TemplateDto>> GetAllTemplatesAsync();
    Task<TemplateDto?> AddTemplateAsync(TemplateCreateDto createDto, string userId);
    Task<TemplateDto?> GetTemplateByIdAsync(long id);
    Task<TemplateDto?> EditTemplateAsync(long id, TemplateCreateDto dto);
    // Task<string> GetCreatorByTemplateId(long id);
    // Task<IEnumerable<Template>> GetPublicTemplatesAsync();
    // Task<IEnumerable<Template>> GetTemplatesByTagAsync(string tagName);
    // Task UpdateTemplateAsync(Template template);
    // Task DeleteTemplateAsync(int id);
}