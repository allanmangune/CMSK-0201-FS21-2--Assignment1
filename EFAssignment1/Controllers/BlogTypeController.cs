using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFAssignment1.Core.Entities;
using EFAssignment1.Infrastructure.Data;

namespace EFAssignment1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogTypeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes the DbContext.
        /// </summary>
        /// <param name="context">The ApplicationDbContext instance.</param>
        public BlogTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all BlogType records from the database.
        /// </summary>
        /// <returns>A list of BlogType records.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/BlogType
        ///
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogType>>> GetBlogTypes()
        {
            try
            {
                return await _context.BlogTypes.ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a specific BlogType record by ID.
        /// </summary>
        /// <param name="id">The ID of the BlogType record.</param>
        /// <returns>The requested BlogType record.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/BlogType/{id}
        ///
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogType>> GetBlogType(long id)
        {
            try
            {
                var blogType = await _context
                    .BlogTypes
                    .FirstOrDefaultAsync(b => b.Id == id);

                if (blogType == null)
                {
                    return NotFound();
                }

                return blogType;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates a specific BlogType record by ID.
        /// </summary>
        /// <param name="id">The ID of the BlogType record to update.</param>
        /// <param name="blogType">The updated BlogType object.</param>
        /// <returns>No content on success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/BlogType/{id}
        ///
        /// </remarks>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogType(long id, BlogType blogType)
        {
            if (id != blogType.Id)
            {
                return BadRequest();
            }

            _context.Entry(blogType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!BlogTypeExists(id))
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
        /// Creates a new BlogType record.
        /// </summary>
        /// <param name="blogType">The BlogType object to create.</param>
        /// <returns>The created BlogType object.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/BlogType
        ///
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<BlogType>> PostBlogType(BlogType blogType)
        {
            try
            {
                _context.BlogTypes.Add(blogType);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetBlogType), new { id = blogType.Id }, blogType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a specific BlogType record by ID.
        /// </summary>
        /// <param name="id">The ID of the BlogType record to delete.</param>
        /// <returns>No content on success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/BlogType/{id}
        ///
        /// </remarks>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogType(long id)
        {
            try
            {
                var blogType = await _context
                    .BlogTypes
                    .FirstOrDefaultAsync(t => t.Id == id);
                if (blogType == null)
                {
                    return NotFound();
                }

                _context.BlogTypes.Remove(blogType);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Checks if a BlogType record exists by ID.
        /// </summary>
        /// <param name="id">The ID of the BlogType record.</param>
        /// <returns>True if the BlogType record exists, otherwise false.</returns>
        private bool BlogTypeExists(long id)
        {
            try
            {
                return _context.BlogTypes.Any(e => e.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal server error: {ex.Message}");
            }
        }
    }
}
