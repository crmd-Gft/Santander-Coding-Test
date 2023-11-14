namespace HackerNews
{
	public interface IHackerNewsService
	{
        public Task<IEnumerable<int>> GetBestStoryIdsAsync(int n);
        public Task<Story> GetStoryDetailsAsync(int storyId);
    }
}

