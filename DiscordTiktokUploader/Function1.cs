using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using Microsoft.Extensions.Logging;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Net.Http;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using DiscordTiktokUploader.Models;

namespace DiscordTiktokUploader
{
    public static class Function1
    {
        private const ulong PRIVATE_CHANNEL_ID = 312968558504312834;
        private const ulong TIKTOK_CHANNEL_ID = 585703751143391263;
        private const ulong GUILD_ID = 221620541327540224;
        private readonly static string? DISCORD_TOKEN = Environment.GetEnvironmentVariable("discord_token");

        private static CancellationTokenSource _cancellationToken;
        private static DiscordSocketClient _discordClient;
        private static HttpClient _httpClient;
        private static IEnumerable<PreviousPost> _previousPosts;
        private static IAsyncCollector<PreviousPost> _postsToUpload;

        [FunctionName("Function1")]
        public static void Run([TimerTrigger("0 */20 * * * *")]TimerInfo myTimer, ILogger log,
            [CosmosDB(
                databaseName:"DiscordTikTok",
                collectionName:"DiscordUploads",
                ConnectionStringSetting = "alannitro_DOCUMENTDB",
                SqlQuery = "select * from DiscordUploads")]IEnumerable<PreviousPost> previousPosts,
            [CosmosDB(
                databaseName: "DiscordTikTok",
                collectionName: "DiscordUploads",
                ConnectionStringSetting = "alannitro_DOCUMENTDB")]
                IAsyncCollector<PreviousPost> postsOut)
            => Function1.MainAsync(log, previousPosts, postsOut).GetAwaiter().GetResult();

        public static async Task MainAsync(ILogger log, IEnumerable<PreviousPost> previousPosts, IAsyncCollector<PreviousPost> postsToUpload)
        {
            _postsToUpload = postsToUpload;
            _previousPosts = previousPosts;
            _discordClient = new DiscordSocketClient();
            _cancellationToken = new CancellationTokenSource();
            _httpClient = new HttpClient();

            await _discordClient.LoginAsync(Discord.TokenType.Bot, DISCORD_TOKEN);
            _discordClient.Ready += _discordClient_Ready;
            await _discordClient.StartAsync();






            try
            {
                await Task.Delay(-1, _cancellationToken.Token);
            }
            catch (Exception e)
            {
                if (_cancellationToken != null)
                {
                    _cancellationToken.Cancel();
                }
                log.LogWarning(e.Message);
            }

        }

        private static async Task _discordClient_Ready()
        {
            var channel = _discordClient.GetGuild(GUILD_ID)?.GetChannel(TIKTOK_CHANNEL_ID);
            var tikTokPosts = (await GetTikTokLinks()).Where(i => !string.IsNullOrEmpty(i.Id));

            if (channel != null && tikTokPosts?.Count() > 0)
            {
                var tikTokChannel = (channel as SocketTextChannel);
                

                foreach (var post in tikTokPosts)
                {
                    if (!_previousPosts.Any(p => p.postId == post.Id))
                    {

                        var r = await _httpClient.GetAsync(post.Video.DownloadAddr);
                        await tikTokChannel.SendFileAsync(stream: await r.Content.ReadAsStreamAsync(),$"TikTok_{post.Author}_{post.CreateTime}.mp4",post.Desc.Trim());
                        await _postsToUpload.AddAsync(new PreviousPost { postId = post.Id });
                    }
                }
            }

            await StopDiscordClient();
        }


        private static async Task StopDiscordClient()
        {
            await _discordClient.LogoutAsync();
            _httpClient.Dispose();
            _cancellationToken.Cancel();
            _cancellationToken = null;
        }

        private static async Task<IEnumerable<Item>> GetTikTokLinks()
        {

            var client = _httpClient;
            client.DefaultRequestHeaders
                .TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.129 Safari/537.36");

            var response =
                await client.
                GetAsync("https://m.tiktok.com/api/item_list/?count=30&id=6613927298121564165&type=2&secUid=&maxCursor=0&minCursor=0&appId=1233&language=en&region=US&sourceType=9&verifyFp=&_signature=aEkOcAAgEBSEdXVCAHBOPGhJD2AADbl");


            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsByteArrayAsync();

            var tikTokResponse = JsonSerializer.Deserialize<TikTokResponse>(utf8Json: content);

            return tikTokResponse.Items;

        }
    }
}
