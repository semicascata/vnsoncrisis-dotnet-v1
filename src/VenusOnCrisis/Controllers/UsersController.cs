using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VenusOnCrisis.Data;
using VenusOnCrisis.Entities;

namespace VenusOnCrisis.Controllers
{
  public class UsersController : BaseApiController
  {
    private readonly DataContext _context;
    
    public UsersController(DataContext context)
    {
        _context = context;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> FetchUsers() 
    {
        // const users = await this.userModel.find();
        var users = await _context.Users.ToListAsync();
        return users;
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        return user;
    }
  }
}