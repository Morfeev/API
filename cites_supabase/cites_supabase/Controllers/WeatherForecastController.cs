using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Supabase.Gotrue;
using Supabase.Postgrest.Attributes;
using System.Reflection;
using System.Xml.Linq;
using static WebApplication1.Controllers.WeatherForecastController;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly Supabase.Client _supabaseClient;
        private readonly SupaBaseContext _supabaseContext;

        public WeatherForecastController(Supabase.Client supabaseClient, SupaBaseContext supabaseContext)
        {
            _supabaseClient = supabaseClient;
            _supabaseContext = supabaseContext;
        }

        [HttpGet("GetAllUsers", Name = "GetAllUsers")]
        public async Task<string> GetAllUsers()
        {
            try
            {
                var result = await _supabaseContext.GetCites(_supabaseClient);
                return JsonConvert.SerializeObject(result, Formatting.Indented);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        [HttpPost("InsertUser", Name = "InsertUser")]
        public async Task<ActionResult> InsertUser([FromBody] CitesData CiteData)
        {
            try
            {
                if (string.IsNullOrEmpty(CiteData.Title))
                {
                    return BadRequest("Пустое название");
                }
                else
                {
                    Cites newCite = new Cites
                    {
                        Title = CiteData.Title
                    };
                    bool result = await _supabaseContext.InserCites(_supabaseClient, newCite);
                    if (result == true)
                    {
                        return Ok("Добавление прошло успешно");
                    }
                    else
                    {
                        return BadRequest("Не удалось добавить город в БД");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Неизвестная ошибка");
            }
        }

/*        [HttpPut("UpdateUser", Name = "UpdateUser")]
        public async Task<ActionResult> UpdateUser([FromBody] UserName userName)
        {
            try
            {
                bool update = await _supabaseContext.UpdateUser(_supabaseClient, userName);
                if (update == true)
                {
                    return Ok("Обновление прошло успешно");
                }
                else
                {
                    return BadRequest("Не удалось добавить пользователя в БД");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Неизвестная ошибка");
            }
        }

        [HttpDelete("DeleteUser", Name = "DeleteUser")]
        public async Task<ActionResult> DeleteUser([FromBody] UserDelete userDelete)
        {
            try
            {
                bool update = await _supabaseContext.DeleteUsers(_supabaseClient, userDelete);
                if (update == true)
                {
                    return Ok("Удаление прошло успешно");
                }
                else
                {
                    return BadRequest("Не удалось добавить пользователя в БД");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Неизвестная ошибка");
            }
        }*/

        public class CitesData
        {
            public string Title { get; set; }
        }
    }
}
