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

        public HackerNewsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<int>> GetBestStoryIdsAsync(int n)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("HackerNewsClient");
                var response = await httpClient.GetStringAsync("beststories.json");
                var storyIds = JsonConvert.DeserializeObject<IEnumerable<int>>(response);
                return storyIds?.Take(n) ?? Enumerable.Empty<int>();
            }
            catch (Exception e)
            {
                throw new Exception($"Error fetching best story IDs from Hacker News API.", e);
            }
        }

        public async Task<Story> GetStoryDetailsAsync(int storyId)
        {
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
        }
    }

}

