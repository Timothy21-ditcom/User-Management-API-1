using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }


        // GET: api/users
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
    public IActionResult GetById(int id)
          {
    try
    {
        var user = _userService.GetById(id);

        if (user == null)
        {
            return NotFound($"User with ID {id} was not found.");
        }

        return Ok(user);
    }
    catch (Exception ex)
    {
        return StatusCode(500,
            $"An error occurred: {ex.Message}");
    }
         }

        // POST: api/users
        [HttpPost]
    public IActionResult Create(User user)
           {
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    var createdUser = _userService.Create(user);

    return CreatedAtAction(
        nameof(GetById),
        new { id = createdUser.Id },
        createdUser);
          }

        // PUT: api/users/{id}
       [HttpPut("{id}")]
    public IActionResult Update(int id, User user)
        {
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    var updated = _userService.Update(id, user);

    if (!updated)
    {
        return NotFound($"User with ID {id} was not found.");
    }

    return Ok("User updated successfully.");
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
    public IActionResult Delete(int id)
        {
    try
    {
        var deleted = _userService.Delete(id);

        if (!deleted)
        {
            return NotFound($"User with ID {id} was not found.");
        }

        return Ok("User deleted successfully.");
    }
    catch (Exception ex)
    {
        return StatusCode(500,
            $"An error occurred: {ex.Message}");
    }
       }
    }
}