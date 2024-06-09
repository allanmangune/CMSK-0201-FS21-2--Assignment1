using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFAssignment1.Core.Entities;
using EFAssignment1.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFAssignment1.Controllers
{
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Retrieves a specific Post record by ID.
        /// </summary>
        /// <param name="id">The ID of the Post record.</param>
        /// <returns>The requested Post record.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Post/{id}
        ///
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(long id)
        {
            try
            {
                var post = await _context
                    .Posts
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (post == null)
                {
                    return NotFound();
                }

                return post;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    
        /// <summary>
        /// Creates a new Post record.
        /// </summary>
        /// <param name="post">The Post object to create.</param>
        /// <returns>The created Post object.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Post
        ///
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<Post>> AddPost([FromBody] Post post)
        {
            try
            {
                _context.Posts.Add(post);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
        }
    }
}

