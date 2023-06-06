using AutoMapper;
using Knila.BookStore.Domain;
using Knila.BookStore.RepositoryInterface;
using Knila.BookStore.ServiceInterface;
using Knila.BookStore.WebAPI.Models;
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

        private readonly IMapper _mapper;

        public BookController(ILogger<BookController> logger, IBookService bookService, IMapper mapper)
        {
            this._logger = logger;
            this._bookService = bookService;
            this._mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllBookDetailsSort1")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BookViewModel>))]
        public async Task<IActionResult> GetAllBookDetailsSort1Async()
        {
            List<Book> books = new List<Book>();
            List<BookViewModel> booksViewModel = new List<BookViewModel>();
            books = await this._bookService.GetAllBookDetailsSort1Async();

            booksViewModel = this._mapper.Map<List<BookViewModel>>(books);

            return this.Ok(booksViewModel);
        }

        [HttpGet]
        [Route("GetAllBookDetailsSort2")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BookViewModel>))]
        public async Task<IActionResult> GetAllBookDetailsSort2Async()
        {
            List<Book> books = new List<Book>();
            List<BookViewModel> booksViewModel = new List<BookViewModel>();
            books = await this._bookService.GetAllBookDetailsSort2Async();

            booksViewModel = this._mapper.Map<List<BookViewModel>>(books);

            return this.Ok(booksViewModel);
        }

        [HttpGet]
        [Route("LoadBookData")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<IActionResult> LoadBookDataAsync(int count)
        {
            await this._bookService.LoadBookDataAsync(count);
            return this.Ok("Book Details Loaded");
        }

        [HttpGet]
        [Route("GetTotalBookPrice")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(double))]
        public async Task<IActionResult> GetTotalBookPriceAsync()
        {
            double sumOfBookPrice = 0.00;
            sumOfBookPrice = await this._bookService.GetTotalBookPriceAsync();
            return this.Ok(sumOfBookPrice);
        }

        [HttpGet]
        [Route("GetTotalNumberOfBooks")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> TotalNumberOfBooksAsync()
        {
            int sumOfBooks = 0;
            sumOfBooks = await this._bookService.GetTotalNumberOfBooksAsync();
            return this.Ok(sumOfBooks);
        }

        [HttpGet]
        [Route("GetLast5BookDetails")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BookViewModel>))]
        public async Task<IActionResult> GetLast5BookDetailsAsync()
        {
            List<Book> books = new List<Book>();
            List<BookViewModel> booksViewModel = new List<BookViewModel>();
            books = await this._bookService.GetLast5BookDetailsAsync();

            booksViewModel = this._mapper.Map<List<BookViewModel>>(books);

            return this.Ok(booksViewModel);
        }
    }
}