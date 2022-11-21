using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceTexterBot.Configuration;
using VoiceTexterBot.Services;


namespace VoiceTexterBot.Controllers
{
    public  class VoiceMessageController
    {
        private readonly IStorage _memoryStorage;
        private readonly IFileHandler _audioFileHandler; 
        private readonly ITelegramBotClient _telegramClient;
        public VoiceMessageController(IStorage memoryStorage, IFileHandler fileHandler,ITelegramBotClient telegramClient)
        {
            _memoryStorage = memoryStorage;
            _audioFileHandler = fileHandler;
            _telegramClient = telegramClient;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {            
            var fileId = message.Voice?.FileId;
            if (fileId == null)
                return;
            await _audioFileHandler.Download(fileId, ct);
            string userLanguageCode = _memoryStorage.GetSession(message.Chat.Id).LanguageCode; // Здесь получим язык из сессии пользователя
            var result = _audioFileHandler.Process(userLanguageCode); // Запустим обработку
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, result, cancellationToken: ct);
        }

    }
}
