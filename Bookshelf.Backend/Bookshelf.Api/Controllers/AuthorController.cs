using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Bookshelf.Api.Models;
using Bookshelf.Api.Controllers.Base;
using Bookshelf.Application.Authors.Models;
using Bookshelf.Application.Authors.Queries.GetAuthorList;
using Bookshelf.Application.Authors.Queries.GetAuthorDetails;
using Bookshelf.Application.Authors.Commands.CreateAuthor;
using Bookshelf.Application.Authors.Commands.UpdateAuthor;
using Bookshelf.Application.Authors.Commands.DeleteAuthor;

namespace Bookshelf.Api.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
public class AuthorController : BaseController
{
    private readonly IMapper _mapper;

    public AuthorController(IMapper mapper) => _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult<AuthorListVm>> GetAll()
    {
        var query = new GetAuthorListQuery();
        var vm = await Mediator.Send(query);

        return Ok(vm);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AuthorDetailsVm>> Get(Guid id)
    {
        var query = new GetAuthorDetailsQuery(id);
        var vm = await Mediator.Send(query);

        return Ok(vm);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateAuthorDto createAuthorDto)
    {
        var command = _mapper.Map<CreateAuthorCommand>(createAuthorDto);
        var authorId = await Mediator.Send(command);

        return Ok(authorId);
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] UpdateAuthorDto updateAuthorDto)
    {
        var command = _mapper.Map<UpdateAuthorCommand>(updateAuthorDto);
        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new DeleteAuthorCommand(id);
        await Mediator.Send(command);

        return NoContent();
    }
}
