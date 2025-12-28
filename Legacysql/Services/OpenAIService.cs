using Legacysql.Services;
using OpenAI.Chat; // Make sure this is at the top

namespace Legacysql.Services
{
    public class OpenAIService : IAIService
    {
        private readonly string _apiKey;

        public OpenAIService(IConfiguration config)
        {
            //getting the API key from appsettings.json
            _apiKey = config["OpenAI:ApiKey"] ?? throw new Exception("OpenAI Key missing!");
        }

        public async Task<string> GetSqlFromText(string userQuestion)
        {
            // 1. Setup the Client
            ChatClient client = new(model: "gpt-4o-mini", apiKey: _apiKey);

            // 2. Define the System Prompt (The Rules)
            string systemPrompt = @" 
               Convert the user's natural language question into a T-SQL query.
                The Table Schema is:
                Table: Tickets
                Columns: Id (Int), IssueDescription (NVARCHAR), Status (NVARCHAR), CreatedDate (DATETIME)
                
                IMPORTANT RULES:
                1. Return ONLY the SQL string. No markdown, no explanations.
                2. Do not start with ```sql. Just the raw query.
                3. Use valid T-SQL syntax.";

            // 3. Send the request
            ChatCompletion completion = await client.CompleteChatAsync(
                new SystemChatMessage(systemPrompt),
                new UserChatMessage(userQuestion)
            );

            // 4. Return the clean SQL
            string sql = completion.Content[0].Text.Trim();

            // If  AI adds "sql" at the start. Remove it.
            sql = sql.Replace("```sql", "").Replace("```", "").Trim();

            return sql;
        }
    }
}