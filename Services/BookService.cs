using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using secure_online_bookstore.Models;
using secure_online_bookstore.Data;
using AutoMapper;
using System.Text.RegularExpressions;

namespace secure_online_bookstore.Services
{
    public class BookService : IBookService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public BookService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private static List<Book> books = new List<Book>();

        public GetBookDto AddBook(AddBookDto newBook)
        {
            bool matchAuthor = Regex.IsMatch(newBook.Author, @"[a-zA-Z]+[a-zA-Z]*");
            if(newBook.Name == "" || newBook.Author == "")
            {
                throw new Exception("All fields are required!");
            }
            if(matchAuthor == false)
            {
                throw new Exception("Numerics and spec characters are not allowed.");
            }
            bool lengthOfName = newBook.Name.Length < 1 || newBook.Name.Length > 50;
            bool lengthofAuthor = newBook.Author.Length < 1 || newBook.Author.Length > 50;
            if(lengthOfName == true || lengthofAuthor == true)
            {
                throw new Exception("The field must be a string with a minimum length of 1 and a maximum length of 50.");
            }
            var book = _mapper.Map<Book>(newBook);
            _context.Add<Book>(book);
            _context.SaveChanges();
            return _mapper.Map<GetBookDto>(book);
        }

        public List<GetBookDto> GetAllBooks()
        {
            books = _context.Books.ToList();  //get the books from DB
            var dtoBooks = books.Select(b => _mapper.Map<GetBookDto>(b)).ToList();
            return dtoBooks;
        }

        public GetBookDto GetBookById(int id)
        {
            Book book = _context.Books.ToList().FirstOrDefault(b => b.Id == id);

            if(book is not null)
            {
                var dtoBook = _mapper.Map<GetBookDto>(book);
                return dtoBook;
            }
            throw new Exception($"Book with Id '{id}' not found.");
        }

        public string DeleteBook(int id)
        {
                Book book = _context.Books.ToList().First(b => b.Id == id);
                if(book is not null)
                {
                    _context.Remove(book);
                    _context.SaveChanges();
                    return "Success!"; 
                }
                if(book.Id != id)
                {
                    return $"Book with Id '{id}' not found.";
                }
                throw new Exception($"Book with Id '{id}' not found.");
        }

        public UpdateBookDto UpdateBook(UpdateBookDto updatedBook, int id)
        {
            Book book = _context.Books.ToList().FirstOrDefault(b => b.Id == id);
            if(book is null)
            {
                throw new Exception($"Book with Id '{id}' not found.");
            }
            bool matchAuthor = Regex.IsMatch(updatedBook.Author, @"[a-zA-Z]+[a-zA-Z]*");
            if(updatedBook.Name == "" || updatedBook.Author == "")
            {
                throw new Exception("All fields are required!");
            }
            if(matchAuthor == false)
            {
                throw new Exception("Numerics and spec characters are not allowed.");
            }
            bool lengthOfName = updatedBook.Name.Length < 1 || updatedBook.Name.Length > 50;
            bool lengthofAuthor = updatedBook.Author.Length < 1 || updatedBook.Author.Length > 50;
            if(lengthOfName == true || lengthofAuthor == true)
            {
                throw new Exception("The field must be a string with a minimum length of 1 and a maximum length of 50.");
            }
            book.Name = updatedBook.Name;
            book.Author = updatedBook.Author;
            var _book = _mapper.Map<Book>(book);
            _context.Update(_book);
            _context.SaveChanges();
            return _mapper.Map<UpdateBookDto>(_book);
        }
     }
}