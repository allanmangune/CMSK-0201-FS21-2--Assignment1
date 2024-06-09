using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFAssignment1.Core.Entities;
using EFAssignment1.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFAssignment1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes the DbContext.
        /// </summary>
        /// <param name="context">The ApplicationDbContext instance.</param>
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new User record.
        /// </summary>
        /// <param name="user">The User object to create.</param>
        /// <returns>The created User object.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/User
        ///
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] User user)
        {
            try
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.Name == user.Name
                || u.EmailAddress == user.EmailAddress || u.PhoneNumber == user.PhoneNumber);
                if (existingUser == null)
                {
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
                }
                else
                {
                    return BadRequest("User exists already!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a specific User record by ID.
        /// </summary>
        /// <param name="id">The ID of the User record.</param>
        /// <returns>The requested User record.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/User/{id}
        ///
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                return user;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates a specific User record by ID.
        /// </summary>
        /// <param name="id">The ID of the User record to update.</param>
        /// <param name="user">The updated User object.</param>
        /// <returns>No content on success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/User/{id}
        ///
        /// </remarks>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500, $"Concurrency error: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return NoContent();
        }

        /// <summary>
        /// Checks if a User record exists by ID.
        /// </summary>
        /// <param name="id">The ID of the User record.</param>
        /// <returns>True if the User record exists, otherwise false.</returns>
        private bool UserExists(long id)
        {
            try
            {
                return _context.Users.Any(e => e.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal server error: {ex.Message}");
            }
        }
    }
}

