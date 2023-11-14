namespace HackerNews
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Memory;
    using Newtonsoft.Json;

    public class HackerNewsService : IHackerNewsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _cache; 

        public HackerNewsService(IHttpClientFactory httpClientFactory, IMemoryCache cache)
        {
            _httpClientFactory = httpClientFactory;
            _cache = cache;
        }

        public async Task<IEnumerable<int>> GetBestStoryIdsAsync(int n)
        {
            var allStoryIds = await _cache.GetOrCreateAsync("AllBestStoryIds", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);

                var httpClient = _httpClientFactory.CreateClient("HackerNewsClient");
                var response = await httpClient.GetStringAsync("beststories.json");
                return JsonConvert.DeserializeObject<IEnumerable<int>>(response);
            });

            return allStoryIds?.Take(n) ?? Enumerable.Empty<int>();

        }

        public async Task<Story> GetStoryDetailsAsync(int storyId)
        {
            return await _cache.GetOrCreateAsync($"StoryDetails_{storyId}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);

                try
                {
                    var httpClient = _httpClientFactory.CreateClient("HackerNewsClient");
                    var response = await httpClient.GetStringAsync($"item/{storyId}.json");
                    var story = JsonConvert.DeserializeObject<Story>(response) ?? throw new BadHttpRequestException($"No story at {storyId}");
                    return story;
                }
                catch (Exception e)
                {
                    throw new Exception($"Error fetching details for story ID {storyId} from Hacker News API.", e);
                }
            });

        }
    }

}

