using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkPulse.Services.Users.Contracts;

namespace WorkPulse.RestApi.Controllers.Users;
[ApiController]
[Route("users")]
[Authorize]
public class UserController(IUserService service): ControllerBase
{
    
}