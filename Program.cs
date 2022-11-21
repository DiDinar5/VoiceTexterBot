using System;
using System.Text;
using System.Threading.Tasks;
using FFMpegCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceTexterBot.Configuration;
using VoiceTexterBot.Controllers;
using VoiceTexterBot.Services;

namespace VoiceTexterBot
{
    public class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            // Объект, отвечающий за постоянный жизненный цикл приложения
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
                .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
                .Build(); // Собираем

            Console.WriteLine("Сервис запущен");
            // Запускаем сервис
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }

        static void ConfigureServices(IServiceCollection services)
        {
            AppSettings appSettings = BuildAppSettings();
            services.AddSingleton(BuildAppSettings());

            services.AddSingleton<IStorage, MemoryStorage>();
           
            // Подключаем контроллеры сообщений и кнопок
            services.AddTransient<DefaultMessageController>();
            services.AddTransient<VoiceMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();
            // Регистрируем объект TelegramBotClient c токеном подключения
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("5490797925:AAGhxh9ySnCbb91yLSjE0gMkO7295j55d6E"));
            services.AddSingleton<IFileHandler, AudioFileHandler>();
            // Регистрируем постоянно активный сервис бота
            services.AddHostedService<Bot>();
        }
        static AppSettings BuildAppSettings()
        {
            return new AppSettings()
            {
                DownloadsFolder = "C:\\Users\\dinar\\Downloads",
                BotToken = "5490797925:AAGhxh9ySnCbb91yLSjE0gMkO7295j55d6E",
                AudioFileName = "audio",
                InputAudioFormat = "ogg",
                OutputAudioFormat = "wav",
                InputAudioBitrate = 76800
            };
        }
    }
}
//C: \Users\dinar\source\repos\VoiceTexterBot\FFmpeg - win64\bin\ffmpeg.exe
