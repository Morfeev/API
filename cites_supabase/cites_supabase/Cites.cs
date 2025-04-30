using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;
using System.Reflection;

namespace WebApplication1
{
    [Table("city")]
    public class City : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("population")]
        public int Population { get; set; }
    }
}
