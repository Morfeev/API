using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;
using System.Reflection;

namespace WebApplication1
{
    [Table("cities")]
    public class Cites : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }
        [Column("title")]
        public string Title { get; set; }
    }
}
