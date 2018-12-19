using System.Collections.Generic;

namespace YulBot.Infrastructure.Common.Configurations
{
    public class TelegramConfigurations: ITelegramConfigurations
    {
        public string TelegramApiToken { get; set; }
        
        public IEnumerable<Context> Contexts { get; set; }
    }
}