using BooksWepAPiDtos.BookDtos;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksWebAPi_New_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = await _bookService.GetBooksAsync();
            return  Ok(books);
        }


        //[HttpGet("get-with-category-name")]
        //public async Task<IActionResult> GetWithName()
        //{
        //    var books = await _bookService.GetBooksWithCategoryName();
        //    return Ok(books);
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddBookDto bookDto)
        {
            if (ModelState.IsValid)
            {
                await _bookService.AddBookAsync(bookDto);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(BookDto bookDto)
        {
            if (ModelState.IsValid)
            {
                await _bookService.UpdateBookAsync(bookDto);
                return Ok();
            }
            return  BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return Ok();
        }

    }
}
