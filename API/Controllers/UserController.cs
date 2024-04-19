using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[Controller]")] // /api/user [controller] means the name of the controller
public class UserController : ControllerBase
{
    private readonly DataContext context;

    public UserController(DataContext context)
    {
        this.context = context;
    }

    [HttpGet] // one end point /api/user
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await this.context.Users.ToListAsync();
        return users; 
    }

    [HttpGet("{id}")] // /api/user/2
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        var user = await this.context.Users.FindAsync(id);
        return user;
    }
}
