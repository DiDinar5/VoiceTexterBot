using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoiceTexterBot.Configuration;
using Telegram.Bot;
using VoiceTexterBot.Configuration;
using Telegram.Bot;

namespace VoiceTexterBot.Services
{
    public interface IFileHandler
    {
        Task Download(string fileId, CancellationToken ct);
        string Process(string param);
    }
}
