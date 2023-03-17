using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Banoth;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Banoth
{
    internal class Program
    {
        public static Task Main(string[] args) => new Program().MainAsync();

        private DiscordSocketClient _client;


        private static CommandService _commands;
        CommandServiceConfig cmdConfig;
        private CommandHandler commandHandler;

        public async Task MainAsync()
        {
            var config = new DiscordSocketConfig()
            {
                AlwaysDownloadUsers = true,
                MessageCacheSize = 100,
                GatewayIntents =
                GatewayIntents.Guilds |
              GatewayIntents.GuildMembers |
            GatewayIntents.GuildMessageReactions |
            GatewayIntents.GuildMessages |
            GatewayIntents.GuildVoiceStates
            };
            var cmdConfigVAr = new CommandServiceConfig()
            {
                DefaultRunMode = RunMode.Async
            };
            _client = new DiscordSocketClient(config);

            _client.Log += Log;
            _client.MessageReceived += _client_MessageReceived;
            _commands = new CommandService();

            commandHandler = new CommandHandler(_client, _commands);
            await commandHandler.InstallCommandsAsync();

            var token = "OTU3MzY1Njc1OTY4NTg5ODk1.Yj9uRA.cH2hlFUEmb2jfV9-9ZOba-wmkGk";
            // don't worry this token isn't the real actual token. it's expired

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();


            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private Task _client_MessageReceived(SocketMessage arg)
        {
            var chnl = arg.Channel as SocketGuildChannel;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var Guild = chnl.Guild.Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            Console.WriteLine($"{Guild} : {arg.Author} : {arg.Content}");

            return Task.CompletedTask;
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
