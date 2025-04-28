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

        public async Task<List<Cites>> GetCites(Supabase.Client _supabaseClient)
        {
            var result = await _supabaseClient.From<Cites>().Get();
            return result.Models;
        }

        public async Task<bool> InserCites(Supabase.Client _supabaseClient, Cites newCite)
        {

            try
            {
                await _supabaseClient.From<Cites>().Insert(newCite);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> UpdateCites(Supabase.Client _supabaseClient, CitesUpdate newTitle)
        {
            try
            {
                await _supabaseClient.From<Cites>().Where(x => x.Id == newTitle.Id).Set(x => x.Title, newTitle.Title).Update();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteCites(Supabase.Client _supabaseClient, CitesDelete DeleteCite)
        {
            try
            {
                await _supabaseClient.From<Cites>().Where(x => x.Id == DeleteCite.Id).Delete();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
