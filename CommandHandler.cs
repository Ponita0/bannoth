using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Banoth
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient client;
        private readonly CommandService commands;


        public CommandHandler(DiscordSocketClient _client, CommandService _commands  )
        {
            client = _client;
            commands = _commands;
        }

        public async Task InstallCommandsAsync()
        {
            client.MessageReceived += HandleCommandAsync;
            

            await commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
        }

        private async Task HandleCommandAsync(SocketMessage MessageParam)
        {
            var message = MessageParam as SocketUserMessage;
            if (message == null) return;

            int ArgPos = 0;

            // If there's no prefix or the message is from a bot then nothing happens
            if (!(message.HasCharPrefix('.', ref ArgPos) || message.HasMentionPrefix(client.CurrentUser, ref ArgPos)) || message.Author.IsBot) return;

            var context = new SocketCommandContext(client, message);

            await commands.ExecuteAsync(
                context: context,
                argPos: ArgPos,
                services: null
                );
        }
    }

}