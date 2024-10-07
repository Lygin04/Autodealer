using Microsoft.AspNetCore.Mvc;
using UserCore.DTOs;
using UserCore.Services;

namespace UserCore.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IUserService service) : ControllerBase
{
    [HttpPost("/register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        try
        {
            var user = await service.Register(registerDto);
            return Ok(user);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("/Login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        try
        {
            await service.Login(loginDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            await service.Delete(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}