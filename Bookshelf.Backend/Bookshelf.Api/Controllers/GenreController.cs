using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Bookshelf.Domain.Base;

using Bookshelf.Api.Models;
using Bookshelf.Api.Controllers.Base;

using Bookshelf.Application.Genres.Models;
using Bookshelf.Application.Genres.Queries.GetGenreList;
using Bookshelf.Application.Genres.Queries.GetGenreDetails;
using Bookshelf.Application.Genres.Commands.CreateGenre;
using Bookshelf.Application.Genres.Commands.DeleteGenre;
using Bookshelf.Application.Genres.Commands.UpdateGenre;

namespace Bookshelf.Api.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
public class GenreController : BaseController
{
    private readonly IMapper _mapper;

    public GenreController(IMapper mapper) => _mapper = mapper;

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<GenreListVm>> GetAll()
    {
        var query = new GetGenreListQuery();
        var vm = await Mediator.Send(query);

        return Ok(vm);
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<GenreDetailsVm>> Get(Guid id)
    {
        var query = new GetGenreDetailsQuery(id);
        var vm = await Mediator.Send(query);

        return Ok(vm);
    }

    [HttpPost]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateGenreDto createGenreDto)
    {
        var command = _mapper.Map<CreateGenreCommand>(createGenreDto);
        var genreId = await Mediator.Send(command);

        return Ok(genreId);
    }

    [HttpPut]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    public async Task<ActionResult> Update([FromBody] UpdateGenreDto updateGenreDto)
    {
        var command = _mapper.Map<UpdateGenreCommand>(updateGenreDto);
        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new DeleteGenreCommand(id);
        await Mediator.Send(command);

        return NoContent();
    }
}
