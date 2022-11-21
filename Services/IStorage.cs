using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoiceTexterBot.Models;
using System.Collections.Concurrent;

namespace VoiceTexterBot.Services
{
    public interface IStorage
    {
        Session GetSession(long chatId);
    }
}