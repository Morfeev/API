using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;
using System.Reflection;

namespace WebApplication1
{
    [Table("users")]
    public class User : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("age")]
        public int Age { get; set; }
        [Column("login")]
        public string Login { get; set; }
        [Column("password")]
        public string Password { get; set; }
    }
}
