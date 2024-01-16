using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UTB.Eshop.Application.Abstractions;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("nickname")]
    public async Task<IActionResult> GetNickname()
    {
        // Assuming you want to get the nickname for the currently authenticated user
        var userName = User.Identity.Name;

        if (string.IsNullOrEmpty(userName))
        {
            return Unauthorized(); // Or handle as needed
        }

        var nickName = await _userService.GetNickNameAsync(userName);

        if (nickName == null)
        {
            return NotFound();
        }

        return Ok(new { NickName = nickName });
    }
}