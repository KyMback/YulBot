using System.IO;
using Newtonsoft.Json.Linq;

namespace YulBot.Services.Common.Utils
{
    public class SettingsBuilder
    {
        private JObject RawFile { get; set; }
        
        private SettingsBuilder()
        {
        }
        
        public static SettingsBuilder Instance()
        {
            return new SettingsBuilder();
        }

        public SettingsBuilder AddSettingsFile(string fileName, bool isOverride = false)
        {
            JObject newFile = GetJObject(fileName);
            if (isOverride)
            {
                RawFile.Merge(newFile, new JsonMergeSettings
                {
                    MergeArrayHandling = MergeArrayHandling.Concat,
                    MergeNullValueHandling = MergeNullValueHandling.Ignore
                });
            }
            else
            {
                RawFile = newFile;
            }

            return this;
        }

        public TSettings Build<TSettings>()
        {
            return JsonSettingsParserHelper
                .GetSettingsWithEnvVariables<TSettings>(RawFile.ToString());
        }

        private JObject GetJObject(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                return JObject.Parse(reader.ReadToEnd());
            }
        }
    }
}