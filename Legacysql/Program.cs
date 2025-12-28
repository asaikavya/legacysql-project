using Legacysql.Models; 
using Microsoft.EntityFrameworkCore;
using Legacysql.Services;

var builder = WebApplication.CreateBuilder(args);

// --- 1. SERVICES (The Setup) ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//  Register the Database ---
// "Use SQL Server with this connection string"
builder.Services.AddDbContext<SupportTicketsDbContext>(options =>
    options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SupportTicketsDB;Trusted_Connection=True;TrustServerCertificate=True;"));

//Register the services

builder.Services.AddScoped<IAIService, MockAIService>();
//builder.Services.AddScoped<IAIService, OpenAIService>();

var app = builder.Build();

// --- 2. MIDDLEWARE (The Pipeline) ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

// --- 3. ENDPOINTS (The Menu) ---

// Endpoint 1: Get All Tickets
app.MapGet("/tickets", (SupportTicketsDbContext db) =>
{
    // This is EF Core translation: "SELECT * FROM Tickets"
    return db.Tickets.ToList();
})
.WithName("GetTickets");

//AI end point

app.MapPost("/ask", async (String question, IAIService ai, SupportTicketsDbContext db) =>
{
    String sqlQuery = "";
    //exception handling
    try
    {
        // sending the user question 
        sqlQuery = await ai.GetSqlFromText(question);

        var data = await db.Tickets.FromSqlRaw(sqlQuery).ToListAsync();

        return Results.Ok(new
        {
            UserQuestion = question,
            AI_Generated_SQL = sqlQuery,
            Results = data
        });
    }
    catch(Exception ex)
    {
        return Results.Problem(
            detail: ex.Message,
            title: "AI Generation Failed to convert plaintext  to sql query",
            statusCode: 500,
            extensions: new Dictionary<string, object?>
            {
                {"failed_query",sqlQuery }
            });
    }

}).WithName("AskAI");

app.Run();