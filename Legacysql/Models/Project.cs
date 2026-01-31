using System.ComponentModel.DataAnnotations;

namespace Legacysql.Models
{
    public class Project
    {
        public int Id { get; set; } // Primary Key

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}