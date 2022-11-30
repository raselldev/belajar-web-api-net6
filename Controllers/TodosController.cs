using BelajarWebApi.Data;
using BelajarWebApi.Dtos;
using BelajarWebApi.Models;
using BelajarWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BelajarWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IIdentityService _identityService;

        public TodosController(DataContext context, IIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var results = _context.Todos
                .Where(e => e.UserId == _identityService.GetUserId())
                .ToList();

            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle(long id)
        {
            var result = _context.Todos
                .FirstOrDefault(e => e.Id == id && e.UserId == _identityService.GetUserId());
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TodoWriteDto input)
        {
            var data = new Todo
            {
                Title = input.Title,
                Note = input.Note ?? "",
                IsCompleted = false,
                UserId = _identityService.GetUserId()
            };

            _context.Todos.Add(data);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetSingle), new { data.Id }, data);
        }

        [HttpPost("{id}/[action]")]
        public IActionResult Stop(long id)
        {
            var existing = _context.Todos
                .FirstOrDefault(e => e.Id == id && e.UserId == _identityService.GetUserId());
            if (existing is null)
            {
                return NotFound();
            }

            existing.IsCompleted = true;

            _context.Todos.Update(existing);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] TodoWriteDto input)
        {
            var existing = _context.Todos
                .FirstOrDefault(e => e.Id == id && e.UserId == _identityService.GetUserId());
            if (existing is null)
            {
                return NotFound();
            }

            existing.Title = input.Title;
            existing.Note = input.Note ?? "";

            _context.Todos.Update(existing);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var existing = _context.Todos
                .FirstOrDefault(e => e.Id == id && e.UserId == _identityService.GetUserId());
            if (existing is null)
            {
                return NotFound();
            }

            _context.Todos.Remove(existing);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
