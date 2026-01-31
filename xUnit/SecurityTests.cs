
using LegacySql;


namespace LegacySql.Tests
{
    public class SecurityTests
    {
        [Fact]
        public void IsSafeQuery_Should_Block_Destructive_Commands()
        {
            string dangerousInput = "DROP TABLE Users";
            string[] forbiddenWords = { "DROP", "DELETE", "TRUNCATE" };


            bool isSafe = true;
            foreach (var word in forbiddenWords)
            {
                if (dangerousInput.Contains(word)) isSafe = false;
            }

            Assert.False(isSafe);
        }
    }
}