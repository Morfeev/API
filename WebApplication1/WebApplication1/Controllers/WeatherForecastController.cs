using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
                var result = await _supabaseContext.GetUsers(_supabaseClient);
                return JsonConvert.SerializeObject(result, Formatting.Indented);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        [HttpPost("InsertUser", Name = "InsertUser")]
        public async Task<ActionResult> InsertUser([FromBody] UserData userData)
        {
            try
            {
                if(string.IsNullOrEmpty(userData.Login) || string.IsNullOrEmpty(userData.Password))
                {
                    return BadRequest("Пустой логин или пароль");
                }
                else
                {
                    User newUser = new User
                    {
                        Login = userData.Login,
                        Password = userData.Password
                    };
                    bool result = await _supabaseContext.InsertUsers(_supabaseClient, newUser);
                    if (result == true)
                    {
                        return Ok("Регистрация прошла успешно");
                    }
                    else
                    {
                        return BadRequest("Не удалось добавить пользователя в БД");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Неизвестная ошибка");
            }
        }

        [HttpPut("UpdateUser", Name = "UpdateUser")]
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

        [HttpPut("UpdateUserAll", Name = "UpdateUserAll")]
        public async Task<ActionResult> UpdateUserAll([FromBody] UserDates userDates)
        {
            try
            {
                bool update = await _supabaseContext.UpdateUserAll(_supabaseClient, userDates);
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
        }

        public class UserData
        {
            [JsonProperty("login")]
            public string Login { get; set; }
            [JsonProperty("password")]
            public string Password { get; set; }
        }
    }

    public class UserDelete
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }

    public class UserDates
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("age")]
        public int Age { get; set; }
        [JsonProperty("login")]
        public string Login { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("city_id")]
        public int? CityId { get; set; }
    }

    public class UserName
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
