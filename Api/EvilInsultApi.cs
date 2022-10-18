using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchDiscordBot.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TwitchDiscordBot.Api
{
    public class EvilInsultApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public EvilInsultApi(HttpClient httpClient, IConfiguration config)
        {
            _config = config;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://evilinsult.com/");
            var apiKey = "";
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (env == "Development")
            {
                apiKey = _config.GetSection("API_KEYS")["FUN_TRANSLATION_API_KEY"];
            }
            else
            {
                apiKey = Environment.GetEnvironmentVariable("FUN_TRANSLATION_API_KEY");
            }

            _httpClient.DefaultRequestHeaders.Add("X-FunTranslations-Api-Secret", apiKey);
        }

        public async Task<EvilInsultDto> GetInsult()
        {
           var res = await _httpClient.GetAsync("generate_insult.php?lang=en&type=json"); 
           if(res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync();
                Console.WriteLine(json);

                JObject contentObject = JObject.Parse(json);
                var resultString = JsonConvert.SerializeObject(contentObject);
                var translate = JsonConvert.DeserializeObject<EvilInsultDto>(resultString);
                return translate;
            } else {
                return null;
            }
        }
    }
}