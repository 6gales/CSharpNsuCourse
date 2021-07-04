using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop.Logic;
using BookShop.Logic.Requests;
using BookShop.Logic.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookShopController : ControllerBase
    {
        private readonly BookShopService _bookService;

        public BookShopController(BookShopService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("{id}")]
        public async Task<BookResponse> GetBook(int id)
        {
            return await _bookService.GetBookById(id);
        }

        [HttpGet]
        public async Task<IEnumerable<BookResponse>> GetBooks(
            [FromQuery(Name = "offset")] int? offset,
            [FromQuery(Name = "count")] int? count)
        {
            return await _bookService.GetBooks(offset, count);
        }

        [HttpDelete("{id}")]
        public async Task DeleteBook(int id)
        {
            await _bookService.DeleteBookById(id);
        }

        [HttpDelete("sell/{id}")]
        public async Task SellBook(int id)
        {
            await _bookService.DeleteBookById(id, true);
        }

        [HttpPost]
        public async Task<BookResponse> UpdateBook([FromBody] UpdateBookRequest updateBookRequest)
        {
            return await _bookService.UpdateBook(updateBookRequest);
        }

        [HttpPut]
        public async Task<BookResponse> CreateBook([FromBody] CreateBookRequest createBookRequest)
        {
            return await _bookService.CreateBook(createBookRequest);
        }
    }
}