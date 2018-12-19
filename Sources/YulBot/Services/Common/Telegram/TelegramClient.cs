using System.Threading.Tasks;
using Telegram.Bot;

namespace YulBot.Services.Common.Telegram
{
    public class TelegramClient
    {
        private string ChatId { get; set; }

        private TelegramBotClient BotClient { get; set; }
        
        public TelegramClient(string apiToken, string chatId)
        {
            BotClient = new TelegramBotClient(apiToken);
            ChatId = chatId;
        }
        
        public async Task SendMessageAsync(string message)
        {
            await BotClient.SendTextMessageAsync(ChatId, message);
        }
    }
}