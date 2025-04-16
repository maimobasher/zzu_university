using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zzu_university.data.Data;
using zzu_university.data.Model.News;
using zzu_university.domain.DTOS;
using zzu_university.domain.Service.NewsService;

namespace zzu_university.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNews()
        {
            var newsList = await _newsService.GetAllNewsAsync();
            return Ok(newsList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNewsById(int id)
        {
            var news = await _newsService.GetNewsByIdAsync(id);
            if (news == null)
                return NotFound();
            return Ok(news);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNews([FromBody] NewsDto newsDto)
        {
            if (newsDto == null)
                return BadRequest("Invalid data.");

            var createdNews = await _newsService.CreateNewsAsync(newsDto);
            return CreatedAtAction(nameof(GetNewsById), new { id = createdNews.Id }, createdNews);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNews(int id, [FromBody] NewsDto newsDto)
        {
            if (newsDto == null)
                return BadRequest("Invalid data.");

            var updated = await _newsService.UpdateNewsAsync(id, newsDto);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            var deleted = await _newsService.DeleteNewsAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }

}