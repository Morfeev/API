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

        [HttpGet("GetAllCity", Name = "GetAllCity")]
        public async Task<string> GetAllCity()
        {
            try
            {
                var result = await _supabaseContext.GetCities(_supabaseClient);
                return JsonConvert.SerializeObject(result, Formatting.Indented);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        [HttpPost("InsertCity", Name = "InsertCity")]
        public async Task<ActionResult> InsertCity([FromBody] CitiesData CityData)
        {
            try
            {
                if (string.IsNullOrEmpty(CityData.Title))
                {
                    return BadRequest("Пустое название");
                }
                else
                {
                    City newCity = new City
                    {
                        Title = CityData.Title
                    };
                    bool result = await _supabaseContext.InserCities(_supabaseClient, newCity);
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

        [HttpPut("UpdateCity", Name = "UpdateCity")]
        public async Task<ActionResult> UpdateCity([FromBody] CitiesUpdate cityTitle)
        {
            try
            {
                bool update = await _supabaseContext.UpdateCities(_supabaseClient, cityTitle);
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

        [HttpPut("UpdateCityAll", Name = "UpdateCityAll")]
        public async Task<ActionResult> UpdateCityAll([FromBody] CitiesUpdateAll UpdateAll)
        {
            try
            {
                bool update = await _supabaseContext.UpdateCitiesAll(_supabaseClient, UpdateAll);
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

        [HttpDelete("DeleteCity", Name = "DeleteCity")]
        public async Task<ActionResult> DeleteCity([FromBody] CitiesDelete cityDelete)
        {
            try
            {
                bool update = await _supabaseContext.DeleteCities(_supabaseClient, cityDelete);
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
        }

        public class CitiesData
        {
            public string Title { get; set; }
        }
    }

    public class CitiesDelete
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }

    public class CitiesUpdate
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
    }

    public class CitiesUpdateAll
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("population")]
        public int Population { get; set; }
    }
}
