using System.Security.Claims;
using Api.Dto;
using Api.Dto.TemplateAccessDto;
using Api.Dto.TemplateDto;
using Api.Interfaces;
using Api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controller;

[Route("api/template")]
[ApiController]
public class TemplateController : ControllerBase
{
    private readonly ITemplateRepository _templateRepository;
    private readonly ITemplateAccess _templateAccess;
    private readonly UserManager<User> _userManager;

    public TemplateController(ITemplateRepository templateRepository, ITemplateAccess templateAccess,
        UserManager<User> userManager)
    {
        _templateRepository = templateRepository;
        _templateAccess = templateAccess;
        _userManager = userManager;
    }

    [HttpGet]
    [ProducesDefaultResponseType(typeof(ApiResponse<TemplateDto>))]
    public async Task<IActionResult> GetAllTemplates()
    {
        var allTemplatesAsync = await _templateRepository.GetAllTemplatesAsync();
        return Ok(new ApiResponse<IEnumerable<TemplateDto>>(allTemplatesAsync));
    }

    [HttpPost]
    [Authorize]
    [ProducesDefaultResponseType(typeof(ApiResponse<TemplateDto>))]
    public async Task<IActionResult> CreateTemplate([FromBody] TemplateCreateDto createDto)
    {
        var username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
        var user = await _userManager.Users.Where(x => x.UserName == username).FirstOrDefaultAsync();
        if (user == null)
            return BadRequest(new ApiResponse<TemplateDto>("User not found"));

        var addTemplateDto = await _templateRepository.AddTemplateAsync(createDto, user.Id);
        if (addTemplateDto == null)
            return BadRequest(new ApiResponse<TemplateDto>("Template not added"));

        return Ok(new ApiResponse<TemplateDto>(addTemplateDto));
    }

    /**
     * Only template creator can give access to change to other users
     */
    [HttpPost("giveAccess")]
    [Authorize]
    [ProducesDefaultResponseType(typeof(ApiResponse<TemplateAccessDto>))]
    public async Task<IActionResult> GiveAccessForTemplate([FromBody] TemplateAccessRequestDto templateAccessRequestDto)
    {
        var userId = await _userManager.Users.Where(u => u.UserName == templateAccessRequestDto.username).Select(user => user.Id).FirstOrDefaultAsync();
        if (userId == null)
            return BadRequest(new ApiResponse<TemplateAccessDto>($"{templateAccessRequestDto.username} Such user not found"));
        var templateByIdAsync = await _templateRepository.GetTemplateByIdAsync(templateAccessRequestDto.TemplateId);
        if (templateByIdAsync == null)
            return BadRequest(new ApiResponse<TemplateAccessDto>("Something went wrong, check all data"));
        var templateAccessDto = await _templateAccess.GiveAccess(userId, templateByIdAsync.TemplateId);
        return Ok(new ApiResponse<TemplateAccessDto>(templateAccessDto));
    }

    

    [HttpPut]
    [Authorize]
    [Route("{id:long}")]
    [ProducesDefaultResponseType(typeof(TemplateDto))]
    public async Task<IActionResult> EditTemplate(long id, [FromBody] TemplateCreateDto dto)
    {
        bool isAdmin = HttpContext.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
        var username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
        var user = await _userManager.Users.Where(x => x.UserName == username).FirstOrDefaultAsync();
        if (user == null)
            return BadRequest(new ApiResponse<TemplateDto>("User not found"));

        var templateByIdAsync = await _templateRepository.GetTemplateByIdAsync(id);
        if (templateByIdAsync == null)
            return NotFound(new ApiResponse<TemplateDto>("Not found"));

        if (isAdmin == false && templateByIdAsync.CreatorId != user.Id)
            return Unauthorized(new ApiResponse<TemplateDto>("Only template creator or admin allow to change"));

        var templateDto = await _templateRepository.EditTemplateAsync(id, dto);
        if (templateDto == null)
            return BadRequest(new ApiResponse<TemplateDto>("Something went wrong, template doesn't change"));

        return Ok(new ApiResponse<TemplateDto>(templateDto));
    }
}