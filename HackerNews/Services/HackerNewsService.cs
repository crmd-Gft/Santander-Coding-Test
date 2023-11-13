namespace HackerNews
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class HackerNewsService
    {
        private readonly HttpClient _httpClient;

        public HackerNewsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<int>> GetBestStoryIdsAsync(int n)
        {
            try
            {
                var response = await _httpClient.GetStringAsync("https://hacker-news.firebaseio.com/v0/beststories.json");
                var storyIds = JsonConvert.DeserializeObject<IEnumerable<int>>(response);
                return storyIds.Take(n);
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
                var response = await _httpClient.GetStringAsync($"https://hacker-news.firebaseio.com/v0/item/{storyId}.json");
                var story = JsonConvert.DeserializeObject<Story>(response);
                return story;
            }
            catch(Exception e)
            {
                throw new Exception($"Error fetching details for story ID {storyId} from Hacker News API.", e);
            }

        }
    }

}

