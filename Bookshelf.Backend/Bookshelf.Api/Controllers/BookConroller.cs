using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Bookshelf.Api.Models;
using Bookshelf.Api.Controllers.Base;

using Bookshelf.Application.Books.Models;
using Bookshelf.Application.Books.Queries.GetBookList;
using Bookshelf.Application.Books.Queries.GetBookDetails;
using Bookshelf.Application.Books.Commands.CreateBook;
using Bookshelf.Application.Books.Commands.UpdateBook;
using Bookshelf.Application.Books.Commands.DeleteBook;
using Bookshelf.Domain.Base;
using Bookshelf.Application.Books.Commands.DeleteBookGenre;
using Bookshelf.Application.Books.Commands.AddBookGenre;

namespace Bookshelf.Api.Controllers;

//[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/[controller]")]
public class BookController : BaseController
{
    private readonly IMapper _mapper;

    public BookController(IMapper mapper) => _mapper = mapper;

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<BookListVm>> GetAll()
    {
        var query = new GetBookListQuery();
        var vm = await Mediator.Send(query);

        return Ok(vm);
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<BookDetailsVm>> Get(Guid id)
    {
        var query = new GetBookDetailsQuery(id);
        var vm = await Mediator.Send(query);

        return Ok(vm);
    }

    [HttpPost]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateBookDto createBookDto)
    {
        var command = _mapper.Map<CreateBookCommand>(createBookDto);
        var bookId = await Mediator.Send(command);

        return Ok(bookId);
    }

    [HttpPut]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    public async Task<ActionResult> Update(UpdateBookDto updateBookDto)
    {
        var command = _mapper.Map<UpdateBookCommand>(updateBookDto);
        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new DeleteBookCommand(id);
        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{bookId:guid}/genres/{genreId:guid}")]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    public async Task<ActionResult> DeleteBookGenre(Guid bookId, Guid genreId)
    {
        var command = new DeleteBookGenreCommand(bookId, genreId);
        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPost("{bookId:guid}/genres")]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    public async Task<ActionResult> AddBookGenre(Guid bookId, [FromBody] Guid GenreId)
    {
        var command = new AddBookGenreCommand(bookId, GenreId);
        await Mediator.Send(command);

        return NoContent();
    }
}

