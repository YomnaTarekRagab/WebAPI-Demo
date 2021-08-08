using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BooksApi.Models;
using Microsoft.AspNetCore.Http;


namespace BooksApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        public List <Book> Books = new List<Book>(){
            new Book{ Id = 1, Title = "David Copperfield", AuthorName = "Charles Dickens", Price = 12 },
            new Book{ Id = 2, Title = " The Tale Of Two Cities", AuthorName = "Charles Dickens", Price = 20 }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            if(Books is not null)
            {
                return Books;
            }
            else
            {
                return NotFound();
            }
        }
        
        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            foreach (var book in Books)
            {
                if(id == book.Id)
                {
                    return book;
                }
            }
            return NotFound();
        }
        [HttpPut("{id}")]
        public ActionResult<Book> PutBook(int id, Book bookModified)
        {
            foreach (var book in Books)
            {
                if(id == book.Id)
                {
                    book.Id = bookModified.Id;
                    book.AuthorName = bookModified.AuthorName;
                    book.Title = bookModified.Title;
                    book.Price = bookModified.Price;
                    return Created("A book has been updated", book);
                }
            }
            return NotFound();

        }
        [HttpPost]
        public ActionResult<Book> PostBook(Book newBook)
        {
            try
            {
                Books.Add(newBook);
                return Created("New Book inserted", newBook);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteBook(int id)
        {
            foreach (var book in Books)
            {
                if(id == book.Id)
                {
                    Books.Remove(book);
                    return NoContent();
                }
            }
            return BadRequest();
        }
    }
}
