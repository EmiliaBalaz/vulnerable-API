using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using secure_online_bookstore.Models;

namespace secure_online_bookstore.Services
{
    public interface IBookService
    {
        GetBookDto AddBook(AddBookDto newBook);
        List<GetBookDto> GetAllBooks();
        GetBookDto GetBookById(int id);
        string DeleteBook(int id);
        UpdateBookDto UpdateBook(UpdateBookDto updatedBook, int id);
    }
}