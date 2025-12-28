namespace Legacysql.Services
{
    public class MockAIService : IAIService
    {
        public async Task<String> GetSqlFromText(String userQuestion)
        {
            await Task.Delay(5000);
            return "SELECT * FROM Tickets WHERE status='Open'";
        }
    }
}
