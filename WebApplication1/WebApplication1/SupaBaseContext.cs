using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using Supabase;
using Supabase.Interfaces;
using System.Reflection;
using WebApplication1.Controllers;

namespace WebApplication1
{
    public class SupaBaseContext
    {
        public SupaBaseContext()
        {
        }

        public async Task<List<User>> GetUsers(Supabase.Client _supabaseClient)
        {
            var result = await _supabaseClient.From<User>().Get();
            return result.Models;
        }

        public async Task<bool> InsertUsers(Supabase.Client _supabaseClient, User newUser)
        {

            try
            {
                await _supabaseClient.From<User>().Insert(newUser);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> UpdateUser(Supabase.Client _supabaseClient, UserName newUser)
        {
            try
            {
                await _supabaseClient.From<User>().Where(x => x.Id == newUser.Id).Set(x => x.Name, newUser.Name).Update();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateUserAll(Client _supabaseClient, UserDates userDates)
        {
            try
            {
                var user = await _supabaseClient.From<User>().Where(x => x.Id == userDates.Id).Single();
                user.Name = userDates.Name;
                user.Age = userDates.Age;
                user.Login = userDates.Login;
                user.Password = userDates.Password;
                user.CityId = userDates.CityId;

                await user.Update<User>();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteUsers(Client _supabaseClient, UserDelete userDelete)
        {
            try
            {
                await _supabaseClient.From<User>().Where(x => x.Id == userDelete.Id).Delete();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
