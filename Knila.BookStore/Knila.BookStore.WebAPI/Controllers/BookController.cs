using Knila.BookStore.Domain;
using Knila.BookStore.RepositoryInterface;
using Knila.BookStore.ServiceInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Knila.BookStore.WebAPI.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookService _bookService;

        public BookController(ILogger<BookController> logger, IBookService bookService)
        {
            this._logger = logger;
            this._bookService = bookService;
        }

        [HttpGet]
        [Route("GetAllBookDetailsSort1")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Book>))]
        public async Task<IActionResult> GetAllBookDetailsSort1Async()
        {
            List<Book> books = new List<Book>();
            books = await this._bookService.GetAllBookDetailsSort1Async();
            return this.Ok(books);
        }
    }
}