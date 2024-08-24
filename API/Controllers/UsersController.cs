﻿using API.Data;
using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

//[Authorize]
public class UsersController(IUserRepository userRepository, IMapper mapper) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        try
        {
            var users = await userRepository.GetMembersAsync();

            return Ok(users);
        }
        catch
        {
            return BadRequest("Error");
        }
    }

    [HttpGet("{username}")] // api/users/{username}
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        try
        {
            var user = await userRepository.GetMemberAsync(username);

            if(user == null) return NotFound();
            
            return user;
        }
        catch
        {
            return BadRequest("Error");
        }
    }
}