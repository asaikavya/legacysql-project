using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Legacysql.Models; 

namespace Legacysql.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SqlController : ControllerBase
    {
        // We need the Database Connection to fetch real tickets
        private readonly SupportTicketsDbContext _context;

        // Constructor Injection: The app gives us the database connection here
        public SqlController(SupportTicketsDbContext context)
        {
            _context = context;
        }

        [HttpPost("analyze")]
        public async Task<IActionResult> AnalyzeQuery([FromBody] QueryRequest request)
        {
           //check for words delete or drop
            string[] forbiddenWords = { "DROP", "DELETE", "TRUNCATE", "ALTER" };
            foreach (var word in forbiddenWords)
            {
                if (request.Text.ToUpper().Contains(word))
                {
                    return BadRequest(new
                    {
                        status = "Blocked",
                        reason = $"Destructive command '{word}' detected.",
                        safety_check = "FAILED"
                    });
                }
            }

            // --- STEP 2: BUSINESS LOGIC 

            // "Plain English" translation (Simulating your AI for now)
            if (request.Text.ToLower().Contains("tickets"))
            {
                try
                {
                    // REAL DATABASE CALL: Fetch all tickets from the table
                    var tickets = await _context.Tickets.ToListAsync();

                    return Ok(new
                    {
                        status = "Success",
                        safety_check = "PASSED",
                        message = "Ticket details from database:",
                        data = tickets
                    });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Database Error: {ex.Message}");
                }
            }

            // --- STEP 3: FALLBACK ---
          
            return Ok(new
            {
                status = "Safe",
                message = "Query passed security."
            });
        }

        public class QueryRequest
        {
            public string Text { get; set; }
        }
        // GET: api/sql/projects
        [HttpGet("projects")]
        public async Task<IActionResult> GetAllProjects()
        {
            try
            {
                // This replaces the app.MapGet logic you had in Program.cs
                var projects = await _context.Tickets.ToListAsync();

                // Note: If your Projects were in a different table/DbContext, 
                // make sure you are referencing the correct DbSet.
                return Ok(projects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}