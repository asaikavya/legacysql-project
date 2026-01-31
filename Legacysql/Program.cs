using Legacysql.Data;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;
using Legacysql.Models;

var builder = WebApplication.CreateBuilder(args);

// --- 1. THE FIX: Register the Database Context Here ---
// This tells .NET: "Whenever someone asks for AppDbContext, give them this SQL connection."

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
/*
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
*/

builder.Services.AddDbContext<SupportTicketsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=(localdb)\\MSSQLLocalDB;Database=SupportTicketsDB;Trusted_Connection=True;TrustServerCertificate=True;"));
// -------------------------------------------------------


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// --- 2. THE API ENDPOINT ---
/*
// We use the 'db' parameter here. Because we registered it above, .NET now knows what it is.
app.MapGet("/api/projects", async (AppDbContext db) =>
{
    // This connects to Azure and grabs the data
    return await db.Projects.ToListAsync();
});
*/
app.MapControllers();

app.Run();