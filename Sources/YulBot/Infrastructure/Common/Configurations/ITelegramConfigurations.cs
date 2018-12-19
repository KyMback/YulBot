using System.Collections.Generic;

namespace YulBot.Infrastructure.Common.Configurations
{
    public interface ITelegramConfigurations
    {
        string TelegramApiToken { get; set; }

        IEnumerable<Context> Contexts { get; set; }
    }
}