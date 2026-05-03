using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkPulse.Services.Teams.Contracts;
using WorkPulse.Services.Teams.Contracts.DTOs.Requests;

namespace WorkPulse.RestApi.Controllers.Teams;
[ApiController]
[Route("api/[controller]")]

[Authorize]
public class TeamsController(ITeamService teamService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddTeamDto dto)
    {
        await teamService.Add(dto);
        return Ok();
    }
}