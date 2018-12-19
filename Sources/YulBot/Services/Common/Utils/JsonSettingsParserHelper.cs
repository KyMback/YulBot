using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace YulBot.Services.Common.Utils
{
    public static class JsonSettingsParserHelper
    {
        private static string RegexForMatchingEnvVariables => @"\${(?<variable>.*?)}";
        private static string NameOfGroup => "variable";
        
        public static TSettings GetSettingsWithEnvVariables<TSettings>(string rawJsonString)
        {
            var regex = new Regex(RegexForMatchingEnvVariables);
            string result = regex.Replace(rawJsonString, GetReplacedValue);
            
            return JsonConvert.DeserializeObject<TSettings>(result);
        }

        private static string GetReplacedValue(Match match)
        {
            string variable = Environment.GetEnvironmentVariable(match.Groups[NameOfGroup].Value);

            return string.IsNullOrWhiteSpace(variable) ? match.Value : variable;
        }
    }
}