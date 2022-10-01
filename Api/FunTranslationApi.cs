using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuskyBot.DTOs;
using HuskyBot.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HuskyBot.Api
{
    public class FunTranslationApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public FunTranslationApi(HttpClient httpClient, IConfiguration config)
        {
            _config = config;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.funtranslations.com/translate/");
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
        public async Task<YodaTranslateDto> TranslateYoda(string toTranslate)
        {
            var res = await _httpClient.GetAsync($"yoda?text={toTranslate}");
            if(res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync();
                JObject contentObject = JObject.Parse(json);
                var resultString = JsonConvert.SerializeObject(contentObject["contents"]);
                var translate = JsonConvert.DeserializeObject<YodaTranslateDto>(resultString);
                return translate;
            } else {
                return null;
            }
        }

        private string ToJsonString(Object dto)
        {
            return JsonConvert.SerializeObject(dto);
        }
    }
}