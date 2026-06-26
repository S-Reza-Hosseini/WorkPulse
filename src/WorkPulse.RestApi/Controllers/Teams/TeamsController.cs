using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkPulse.RestApi.Auth;
using WorkPulse.Services.TeamMemberships.Contracts;
using WorkPulse.Services.TeamMemberships.Contracts.DTOs.Requests;
using WorkPulse.Services.Teams.Contracts;
using WorkPulse.Services.Teams.Contracts.DTOs.Requests;
using WorkPulse.Services.Teams.Contracts.DTOs.Responses;
using WorkPulse.Services.Teams.Exceptions;

namespace WorkPulse.RestApi.Controllers.Teams;
[ApiController]
[Route("api/[controller]")]

[Authorize]
public class TeamsController(
    ITeamService teamService,
    ITeamQuery teamQuery,
    ITeamMembershipService teamMembershipService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddTeamDto dto)
    {
        await teamService.Add(dto);
        return Ok();
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<List<GetAllTeamsDto>> GetAll()
    {
        return await teamQuery.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] long id)
    {
        var userId = HttpContext.GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest();
        }

        try
        {
            var team = await teamQuery.GetById(userId, User.IsInRole("admin"), id);
            if (team is null)
            {
                return NotFound($"Team with ID '{id}' not found.");
            }

            return Ok(team);
        }
        catch (InsufficientTeamPermissionException)
        {
            return Forbid();
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateTeamDto dto)
    {
        var userId = HttpContext.GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest();
        }

        try
        {
            await teamService.Update(userId, User.IsInRole("admin"), id, dto);
            return Ok();
        }
        catch (TeamNotFoundException)
        {
            return NotFound($"Team with ID '{id}' not found.");
        }
        catch (InsufficientTeamPermissionException)
        {
            return Forbid();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] long id)
    {
        var userId = HttpContext.GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest();
        }

        try
        {
            await teamService.Delete(userId, User.IsInRole("admin"), id);
            return Ok();
        }
        catch (TeamNotFoundException)
        {
            return NotFound($"Team with ID '{id}' not found.");
        }
        catch (InsufficientTeamPermissionException)
        {
            return Forbid();
        }
    }

    [HttpPost("{id}/members")]
    public async Task<IActionResult> AddMember([FromRoute] long id, [FromBody] AddTeamMemberDto dto)
    {
        var userId = HttpContext.GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest();
        }

        try
        {
            await teamMembershipService.AddMember(userId, User.IsInRole("admin"), id, dto);
            return Ok();
        }
        catch (TeamNotFoundException)
        {
            return NotFound($"Team with ID '{id}' not found.");
        }
        catch (InsufficientTeamPermissionException)
        {
            return Forbid();
        }
        catch (DuplicateTeamMembershipException)
        {
            return Conflict("User is already a member of this team.");
        }
    }

    [HttpDelete("{id}/members/{membershipId}")]
    public async Task<IActionResult> RemoveMember([FromRoute] long id, [FromRoute] int membershipId)
    {
        var userId = HttpContext.GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest();
        }

        try
        {
            await teamMembershipService.RemoveMember(userId, User.IsInRole("admin"), id, membershipId);
            return Ok();
        }
        catch (TeamMembershipNotFoundException)
        {
            return NotFound($"Membership with ID '{membershipId}' not found in team '{id}'.");
        }
        catch (InsufficientTeamPermissionException)
        {
            return Forbid();
        }
    }

    [HttpPut("{id}/members/{membershipId}/role")]
    public async Task<IActionResult> ChangeMemberRole(
        [FromRoute] long id,
        [FromRoute] int membershipId,
        [FromBody] ChangeTeamMemberRoleDto dto)
    {
        var userId = HttpContext.GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest();
        }

        try
        {
            await teamMembershipService.ChangeRole(userId, User.IsInRole("admin"), id, membershipId, dto.Role);
            return Ok();
        }
        catch (TeamMembershipNotFoundException)
        {
            return NotFound($"Membership with ID '{membershipId}' not found in team '{id}'.");
        }
        catch (InsufficientTeamPermissionException)
        {
            return Forbid();
        }
    }
}
