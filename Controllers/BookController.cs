
using Microsoft.AspNetCore.Mvc;
using secure_online_bookstore.Services;
using secure_online_bookstore.Models;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;
    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }
   
   [HttpPost]
   [Authorize(Roles = "Admin")]
   public ActionResult<GetBookDto> AddBook(AddBookDto newBook)
   {
          try
          { 
               return Ok(_bookService.AddBook(newBook));
          }
          catch (Exception ex)
          {
               return BadRequest(ex.Message);
          }
   }

   [HttpGet("GetAll")]
   public ActionResult<List<GetBookDto>> GetAllBooks()
   {
        return Ok(_bookService.GetAllBooks());
   }

   [HttpGet("{id}")]
   public ActionResult<GetBookDto> GetBookById(int id)
   {
     try
     {
        return Ok(_bookService.GetBookById(id));
     }
     catch (ArgumentException argEx)
     {
          var message = "The input is not valid.";
          return NotFound(message);
     }
     catch (Exception ex)
     {
          return NotFound(ex.Message);
     } 
   }

   [HttpDelete("{id}")]
   [Authorize(Roles = "Admin")]
   public ActionResult<string> DeleteBook(int id)
   {
     try
     {
         
          return Ok( _bookService.DeleteBook(id));
     }
     catch (ArgumentException argEx)
     {
          var message = "The input is not valid.";
          return NotFound(message);
     }
     catch (Exception ex)
     {
          return NotFound(ex.Message);
     }
   }

   [HttpPut("{id}")]
   [Authorize(Roles = "Admin")]
   public ActionResult<UpdateBookDto> UpdateBook([FromBody]UpdateBookDto updatedBook, int id)
   {
     try
     {
          return Ok(_bookService.UpdateBook(updatedBook, id));
     }
     catch (ArgumentException argEx)
     {
          var message = "The input is not valid.";
          return NotFound(message);
     }
     catch (Exception ex)
     {
          return NotFound(ex.Message);
     } 
   }
}