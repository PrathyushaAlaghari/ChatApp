﻿using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class UserController : BaseAPIController
{
    
    private readonly DataContext context;

    public UserController(DataContext context)
    {
        this.context = context;
    }
    
    [AllowAnonymous]
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
