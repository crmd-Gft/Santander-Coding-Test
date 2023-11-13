
using Microsoft.AspNetCore.Mvc;

namespace HackerNews
{

[ApiController]
    [Route("api/[controller]")]
    public class HackerNewsController : Controller
    {
        private readonly HackerNewsService _hackerNewsService;

        public HackerNewsController(HackerNewsService hackerNewsService)
        {
            _hackerNewsService = hackerNewsService;
        }

        [HttpGet("best-stories")]
        public async Task<ActionResult<IEnumerable<Story>>> GetBestStories(int n)
        {
            try
            {
                var storyIds = await _hackerNewsService.GetBestStoryIdsAsync(n);
                var storyTasks = storyIds.Select(async storyId => await _hackerNewsService.GetStoryDetailsAsync(storyId));
                var stories = await Task.WhenAll(storyTasks);

                var sortedStories = stories.OrderByDescending(s => s.Score);
                return Ok(sortedStories);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }

}


