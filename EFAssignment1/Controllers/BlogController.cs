using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFAssignment1.Core.Entities;
using EFAssignment1.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFAssignment1.Controllers
{
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BlogController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new Blog record.
        /// </summary>
        /// <param name="blog">The Blog object to create.</param>
        /// <returns>The created Blog object.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Blog
        ///
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<Blog>> PostBlog([FromBody] Blog blog)
        {
            try
            {
                _context.Blogs.Add(blog);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetBlog), new { id = blog.Id }, blog);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a specific Blog record by ID.
        /// </summary>
        /// <param name="id">The ID of the Blog record.</param>
        /// <returns>The requested Blog record.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Blog/{id}
        ///
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlog(long id)
        {
            try
            {
                var user = await _context
                    .Blogs
                    .Include(b => b.BlogType)
                    .FirstOrDefaultAsync(b => b.Id == id);

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
        /// Deletes a specific Blog record by ID.
        /// </summary>
        /// <param name="id">The ID of the Blog record to delete.</param>
        /// <returns>No content on success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/Blog/{id}
        ///
        /// </remarks>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(long id)
        {
            try
            {
                var blog = await _context
                    .Blogs
                    .FirstOrDefaultAsync(t => t.Id == id);
                if (blog == null)
                {
                    return NotFound();
                }

                _context.Blogs.Remove(blog);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

