using bookDemo.Data;
using bookDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace bookDemo.Controllers
{
    [Route("api/books")]
    [ApiController] 
    public class BooksController : ControllerBase
    {
        
        [HttpGet] //Get isteği gönderme
        public IActionResult GetAllBooks()
        {
            var books = ApplicationContext.Books.ToList();
            return Ok(books);
        }

        [HttpGet("{id:int}")] //Get isteği gönderme tek kitap
        public IActionResult GetOneBooks([FromRoute(Name="id")]int id)
        {
            var book = ApplicationContext //Lınq sorgusu
                .Books
                .Where(b => b.Id.Equals(id))
                .SingleOrDefault(); // buldu ise kitap bulmadı ise null

            if(book == null)
                return NotFound(); //404
            
            return Ok(book);
        }

       
        [HttpPost] //kaynak olustur
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book == null)
                    return BadRequest(); //400

                ApplicationContext.Books.Add(book);
                return StatusCode(201, book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        [HttpPut("{id:int}")] //güncelleme
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id,
            [FromBody] Book book)
        {
            //check book?
            var entity =  ApplicationContext
                .Books.Find(b => b.Id.Equals(id));

            if (entity == null)
                return NotFound();

            //check id
            if(id != book.Id)
                return BadRequest(); //400

            ApplicationContext .Books.Remove(entity);
            book.Id = entity.Id;
            ApplicationContext .Books.Add(book);
            return Ok(book);
        }

        [HttpDelete]
        public IActionResult DeleteAllBooks()
        {
            ApplicationContext.Books.Clear();
            return NoContent(); //204
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name="id")] int id)
        {
            var entity = ApplicationContext
                .Books
                .Find(b=> b.Id.Equals(id));
            if(entity == null)
                return NotFound(new
                {
                    statusCode = 404,
                    message = $"Book with id:{id} could not found."
                }); //404
            ApplicationContext .Books.Remove(entity);
            return NoContent();
        }

    }
}
