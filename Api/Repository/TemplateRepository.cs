using Api.Data;
using Api.Dto.TemplateDto;
using Api.Interfaces;
using Api.Mapper;
using Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository;

public class TemplateRepository : ITemplateRepository
{
    private readonly ApplicationDbContext _context;

    public TemplateRepository(ApplicationDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<IEnumerable<TemplateDto>> GetAllTemplatesAsync()
    {
        return await _context.Templates.Select(t => new TemplateDto()
            {
                TemplateId = t.TemplateId, Description = t.Description, Title = t.Title, Topic = t.Topic,
                CreationDate = t.CreationDate, ImageUrl = t.ImageUrl, IsPublic = t.IsPublic
            }
        ).ToListAsync();
    }

    public async Task<TemplateDto?> AddTemplateAsync(TemplateCreateDto createDto, string userId)
    {
        var template = new Template()
        {
            Title = createDto.Title, Topic = createDto.Topic, Description = createDto.Description,
            ImageUrl = createDto.ImageUrl, IsPublic = createDto.IsPublic, CreatorId = userId
        };

        await _context.Templates.AddAsync(template);
        await _context.SaveChangesAsync();
        return template.TemplateToTemplateDto();
    }

    public async Task<TemplateDto?> GetTemplateByIdAsync(long id)
    {
        var templateDto = await _context.Templates.Where(t => t.TemplateId == id).Select(t => new TemplateDto()
        {
            TemplateId = t.TemplateId,
            CreationDate = t.CreationDate,
            Description = t.Description,
            ImageUrl = t.ImageUrl,
            IsPublic = t.IsPublic,
            Title = t.Title,
            Topic = t.Topic,
            CreatorId = t.CreatorId
        }).FirstOrDefaultAsync();
        return templateDto ?? null;
    }

    public async Task<TemplateDto?> EditTemplateAsync(long id, TemplateCreateDto dto)
    {
        var template = await _context.Templates.Where(t => t.TemplateId == id).FirstOrDefaultAsync();
        if (template == null)
            return null;
        template.Description = dto.Description;
        template.Title = dto.Title;
        template.IsPublic = dto.IsPublic;
        template.ImageUrl = dto.ImageUrl;
        template.Topic = dto.Topic;
        await _context.SaveChangesAsync();
        return template.TemplateToTemplateDto();
    }
    
}