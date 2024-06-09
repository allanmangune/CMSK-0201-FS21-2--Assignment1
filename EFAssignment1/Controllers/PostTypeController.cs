using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using EFAssignment1.Core.Entities;
using EFAssignment1.Infrastructure.Data;

namespace EFAssignment1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostTypeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes the DbContext.
        /// </summary>
        /// <param name="context">The ApplicationDbContext instance.</param>
        public PostTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all PostType records from the database.
        /// </summary>
        /// <returns>A list of PostType records.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/PostType
        ///
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostType>>> GetPostTypes()
        {
            try
            {
                return await _context.PostTypes.ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a specific PostType record by ID.
        /// </summary>
        /// <param name="id">The ID of the PostType record.</param>
        /// <returns>The requested PostType record.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/PostType/{id}
        ///
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<ActionResult<PostType>> GetPostType(long id)
        {
            try
            {
                var postType = await _context
                    .PostTypes
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (postType == null)
                {
                    return NotFound();
                }

                return postType;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates a specific PostType record by ID.
        /// </summary>
        /// <param name="id">The ID of the PostType record to update.</param>
        /// <param name="postType">The updated PostType object.</param>
        /// <returns>No content on success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/PostType/{id}
        ///
        /// </remarks>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPostType(long id, PostType postType)
        {
            if (id != postType.Id)
            {
                return BadRequest();
            }

            _context.Entry(postType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!PostTypeExists(id))
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
        /// Creates a new PostType record.
        /// </summary>
        /// <param name="postType">The PostType object to create.</param>
        /// <returns>The created PostType object.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/PostType
        ///
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<PostType>> PostPostType(PostType postType)
        {
            try
            {
                _context.PostTypes.Add(postType);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPostType), new { id = postType.Id }, postType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a specific PostType record by ID.
        /// </summary>
        /// <param name="id">The ID of the PostType record to delete.</param>
        /// <returns>No content on success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/PostType/{id}
        ///
        /// </remarks>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostType(long id)
        {
            try
            {
                var postType = await _context.PostTypes.FindAsync(id);
                if (postType == null)
                {
                    return NotFound();
                }

                _context.PostTypes.Remove(postType);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Checks if a PostType record exists by ID.
        /// </summary>
        /// <param name="id">The ID of the PostType record.</param>
        /// <returns>True if the PostType record exists, otherwise false.</returns>
        private bool PostTypeExists(long id)
        {
            try
            {
                return _context.PostTypes.Any(e => e.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal server error: {ex.Message}");
            }
        }
    }
}
