using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountsController : BaseAPIController
{
    private readonly DataContext context;
    private readonly ITokenService tokenService;

    public AccountsController(DataContext context, ITokenService tokenService)
    {
        this.context = context;
        this.tokenService = tokenService;
    }

    [HttpPost("register")] //api/Accounts/register
    public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
    {
        if(await UserExists(registerDTO.UserName)){
            return BadRequest("Username already exists. Please use other name");
        }
        using var hmac = new HMACSHA512();
        var user = new AppUser
            {
                UserName = registerDTO.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key,
            };
        this.context.Users.Add(user);
        await this.context.SaveChangesAsync();

        return new UserDTO
        {
            Username = user.UserName,
            Token = this.tokenService.CreateToken(user)
        };
    }

    [HttpPost("login")] //api/Accounts/login
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
    {
        var user = await this.context.Users.SingleOrDefaultAsync(x => x.UserName == loginDTO.username);

        if( user == null) return Unauthorized("invalid username");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedhash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.password));

        for(int i=0; i< computedhash.Length; i++){
            if(computedhash[i] != user.PasswordHash[i]) return Unauthorized("invalid password");
        }

        return new UserDTO
        {
            Username = user.UserName,
            Token = this.tokenService.CreateToken(user)
        };
    }

    private async Task<bool> UserExists(string username)
    {
        return await this.context.Users.AnyAsync(x => x.UserName == username.ToLower());
    }
}
