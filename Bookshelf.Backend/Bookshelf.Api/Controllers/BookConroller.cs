using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Bookshelf.Api.Models;
using Bookshelf.Api.Controllers.Base;
using Bookshelf.Application.Books.Models;
using Bookshelf.Application.Books.Queries.GetBookList;
using Bookshelf.Application.Books.Queries.GetBookDetails;
using Bookshelf.Application.Books.Commands.CreateBook;
using Bookshelf.Application.Books.Commands.UpdateBook;
using Bookshelf.Application.Books.Commands.DeleteBook;

namespace Bookshelf.Api.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
public class BookController : BaseController
{
    private readonly IMapper _mapper;

    public BookController(IMapper mapper) => _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult<BookListVm>> GetAll()
    {
        try
        {
            var vm = await Mediator.Send(new GetBookListQuery());
            return Ok(vm);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BookDetailsVm>> Get(Guid id)
    {
        var vm = await Mediator.Send(new GetBookDetailsQuery(id));

        return Ok(vm);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateBookDto createBookDto)
    {
        var command = _mapper.Map<CreateBookCommand>(createBookDto);
        var bookId = await Mediator.Send(command);

        return Ok(bookId);
    }

    [HttpPut]
    public async Task<ActionResult> Update(UpdateBookDto updateBookDto)
    {
        var command = _mapper.Map<UpdateBookCommand>(updateBookDto);
        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new DeleteBookCommand(id);
        await Mediator.Send(command);

        return NoContent();
    }
}

//Получение списка отзывов для книги: GET /api/books/{bookId}/ reviews
//Добавление нового отзыва для книги: POST / api / books /{ bookId}/ reviews
//Получение информации о конкретном отзыве для книги: GET / api / books /{ bookId}/ reviews /{ reviewId}
//Обновление информации о отзыве для книги: PUT / api / books /{ bookId}/ reviews /{ reviewId}
//Удаление отзыва для книги: DELETE / api / books /{ bookId}/ reviews /{ reviewId}

//GET /books

//POST /books

//GET /books/{bookId}

//PUT / books /{ bookId}

//DELETE / books /{ bookId}


//GET /genres

//POST /genres

//GET /genres/{genreId}

//PUT / genres /{ genreId}

//DELETE / genres /{ genreId}


//PUT /books/{bookId}/ genres

//DELETE / books /{ bookId}/ genres

//PUT / genres /{ genreId}/ books

//DELETE / genres /{ genreId}/ books