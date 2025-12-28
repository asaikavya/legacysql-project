namespace Legacysql.Services
{
    public interface IAIService
    {
        Task<String> GetSqlFromText(String userQuestion);
    }
}
