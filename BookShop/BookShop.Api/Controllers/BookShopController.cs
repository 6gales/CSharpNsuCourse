using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Api.JsonModels;
using BookShop.Api.Proxy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookShop.Api.Controllers
{
    [ApiController]
    [Route("books")]
    public class BookShopController : ControllerBase
    {
        private readonly IBookServiceProxy _bookService;

        public BookShopController(IBookServiceProxy bookService)
        {
            _bookService = bookService;
        }

        [HttpGet, Route("{count}")]
        public async Task<IEnumerable<JsonBook>> Get(int count)
        {
            return await _bookService.GetBooks(count);
        }
    }
}