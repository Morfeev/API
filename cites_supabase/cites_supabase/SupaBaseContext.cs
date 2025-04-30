using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using Supabase;
using Supabase.Gotrue;
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

        public async Task<List<City>> GetCities(Supabase.Client _supabaseClient)
        {
            var result = await _supabaseClient.From<City>().Get();
            return result.Models;
        }

        public async Task<bool> InserCities(Supabase.Client _supabaseClient, City newCity)
        {

            try
            {
                await _supabaseClient.From<City>().Insert(newCity);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> UpdateCities(Supabase.Client _supabaseClient, CitiesUpdate newTitle)
        {
            try
            {
                await _supabaseClient.From<City>().Where(x => x.Id == newTitle.Id).Set(x => x.Title, newTitle.Title).Update();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateCitiesAll(Supabase.Client _supabaseClient, CitiesUpdateAll UpdateAll)
        {
            try
            {
                var city = await _supabaseClient.From<City>().Where(x => x.Id == UpdateAll.Id).Single();
                city.Title = UpdateAll.Title;
                city.Population = UpdateAll.Population;

                await city.Update<City>();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteCities(Supabase.Client _supabaseClient, CitiesDelete DeleteCity)
        {
            try
            {
                await _supabaseClient.From<City>().Where(x => x.Id == DeleteCity.Id).Delete();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
