
using Microsoft.AspNetCore.Mvc;

namespace HackerNews
{

[ApiController]
[Route("api/[controller]")]
public class HackerNewsController : Controller
{
    private readonly IHackerNewsService _hackerNewsService;

    public HackerNewsController(IHackerNewsService hackerNewsService)
    {
        _hackerNewsService = hackerNewsService;
    }

    [HttpGet("best-stories")]
    public async Task<ActionResult<IEnumerable<Story>>> GetBestStories(int n)
    {
        try
        {
            var storyIds = await _hackerNewsService.GetBestStoryIdsAsync(n);
            var stories = new List<Story>();

            foreach (var storyId in storyIds)
            {
                var story = await _hackerNewsService.GetStoryDetailsAsync(storyId);
                stories.Add(story);
            }

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


