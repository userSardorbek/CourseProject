using Api.Data;
using Api.Dto.TemplateAccessDto;
using Api.Interfaces;
using Api.Mapper;
using Api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository;

public class TemplateAccessRepository : ITemplateAccess
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<User> _userManager;

    public TemplateAccessRepository(ApplicationDbContext dbContext, UserManager<User> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task<TemplateAccessDto> GiveAccess(string userId, long templateId)
    {
        var templateAccess = new TemplateAccess() { UserId = userId, TemplateId = templateId };
        await _dbContext.TemplateAccesses.AddAsync(templateAccess);
        await _dbContext.SaveChangesAsync();
        return templateAccess.TemplateAccessToDto();
    }

    public async Task<TemplateAccessDto?> DeleteAccess(string userId, long templateId)
    {
        var templateAccess = await _dbContext.TemplateAccesses.Where(a => a.TemplateId == templateId && a.UserId == userId).FirstOrDefaultAsync();
        if (templateAccess == null)
            return null;
        _dbContext.TemplateAccesses.Remove(templateAccess);
        await _dbContext.SaveChangesAsync();
        return templateAccess.TemplateAccessToDto();
    }
}