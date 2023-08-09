﻿using Discord;
using Discord.Interactions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SomeCatIDK.PirateJim.src.Modules
{
    public class SupportCommandModule : InteractionModuleBase
    {
        [RequireRole(553316077636157451)] //supporter role
        [SlashCommand("article", "Finds a support article relevant to the search query.")]
        public async Task GetZendeskArticle([ChannelTypes(ChannelType.Text, ChannelType.Forum, ChannelType.PublicThread, ChannelType.PrivateThread)] IChannel channel, string search)
        {
            try
            {
                HttpWebRequest request = WebRequest.CreateHttp($"https://support.smartlydressedgames.com/api/v2/help_center/articles/search?query={Uri.EscapeUriString(search)}");
                request.Method = "GET";
                request.ContentType = "application/json";

                var res = (HttpWebResponse)(await request.GetResponseAsync());

                using StreamReader readStream = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
                {
                    var zendeskRes = JObject.Parse(readStream.ReadToEnd());
                    JArray articles = (JArray)zendeskRes["results"];
                    if (articles.Count < 1)
                    {
                        await RespondAsync("Could not find the proper article.");
                        return;
                    }
                    var embed = new EmbedBuilder()
                    {
                        Title = "Found SDG Article",
                        Description = $"[{articles[0]["name"].ToObject<string>()}]({articles[0]["html-url"].ToObject<string>()})",
                        Color = Color.DarkGrey
                    };
                    await RespondAsync(embed: embed.Build());
                }
            }
            catch
            {
                Console.WriteLine("Failed to get an article from Zendesk");
                await RespondAsync("Could not find a proper article.");
            }
        }
    }
}