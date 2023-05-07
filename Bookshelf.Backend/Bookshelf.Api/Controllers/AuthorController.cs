using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Bookshelf.Domain.Base;
using Bookshelf.Application.Authors.Models;
using Bookshelf.Application.Authors.Queries.GetAuthorList;
using Bookshelf.Application.Authors.Queries.GetAuthorDetails;
using Bookshelf.Application.Authors.Commands.CreateAuthor;
using Bookshelf.Application.Authors.Commands.UpdateAuthor;
using Bookshelf.Application.Authors.Commands.DeleteAuthor;

using Bookshelf.Api.Controllers.Base;
using Bookshelf.Api.Models.Author;
using Bookshelf.Application.Authors.Commands.PublishAuthor;
using Bookshelf.Application.Authors.Commands.HideAuthor;

namespace Bookshelf.Api.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
public class AuthorController : BaseController
{
    private readonly IMapper _mapper;

    public AuthorController(IMapper mapper) => _mapper = mapper;

    #region CRUD

    /// <summary>
    /// Получает список всех авторов.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// GET /author
    /// </remarks>
    /// <returns>Список авторов в формате AuthorListVm.</returns>
    /// <response code="200">Успешный запрос.</response>
    [HttpGet]
    [ProducesResponseType(typeof(AuthorListVm), StatusCodes.Status200OK)]
    public async Task<ActionResult<AuthorListVm>> GetAll()
    {
        var query = new GetAuthorListQuery();
        var vm = await Mediator.Send(query);

        return Ok(vm);
    }

    /// <summary>
    /// Получает подробную информацию об авторе по указанному идентификатору.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// GET /author/30647DF9-CFE4-42E0-AAC8-81D8D10A7850
    /// </remarks>
    /// <param name="id">Идентификатор автора.</param>
    /// <returns>Информация об авторе в формате AuthorDetailsVm.</returns>
    /// <response code="200">Успешный запрос.</response>
    /// <response code="404">Не удалось найти автора с указанным идентификатором.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AuthorDetailsVm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AuthorDetailsVm>> Get(Guid id)
    {
        var query = new GetAuthorDetailsQuery(id);
        var vm = await Mediator.Send(query);

        return Ok(vm);
    }

    /// <summary>
    /// Создает нового автора.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// POST /author
    /// {
    ///     firstname: "author first name",
    ///     lastname: "author last name"
    /// }
    /// </remarks>
    /// <param name="createAuthorDto">Данные для создания автора в формате CreateAuthorDto.</param>
    /// <returns>Идентификатор созданного автора (guid).</returns>
    /// <response code="201">Успешный запрос.</response>
    /// <response code="401">Требуется авторизация.</response>
    /// <response code="403">Отказано в доступе.</response>
    [HttpPost]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateAuthorDto createAuthorDto)
    {
        var command = _mapper.Map<CreateAuthorCommand>(createAuthorDto);
        var authorId = await Mediator.Send(command);

        return Ok(authorId);
    }

    /// <summary>
    /// Обновляет данные автора с заданным идентификатором.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// PUT /author
    /// {
    ///     id: "30647DF9-CFE4-42E0-AAC8-81D8D10A7850",
    ///     firstname: "updated first name",
    ///     lastname: "updated last name"
    /// }
    /// </remarks>
    /// <param name="updateAuthorDto">Данные для обновления автора в формате UpdateAuthorDto.</param>
    /// <returns>Без содержимого.</returns>
    /// <response code="204">Успешный запрос.</response>
    /// <response code="401">Требуется авторизация.</response>
    /// <response code="403">Отказано в доступе.</response>
    [HttpPut]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Update([FromBody] UpdateAuthorDto updateAuthorDto)
    {
        var command = _mapper.Map<UpdateAuthorCommand>(updateAuthorDto);
        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    /// Удаляет автора с заданным идентификатором.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// DELETE /author/30647DF9-CFE4-42E0-AAC8-81D8D10A7850
    /// </remarks>
    /// <param name="id">Идентификатор автора.</param>
    /// <returns>Без содержимого.</returns>
    /// <response code="204">Успешный запрос.</response>
    /// <response code="401">Требуется авторизация.</response>
    /// <response code="403">Отказано в доступе.</response>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new DeleteAuthorCommand(id);
        await Mediator.Send(command);

        return NoContent();
    }


    #endregion

    #region Visibility control

    /// <summary>
    /// Публикует автора с заданным идентификатором.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// POST /author/publish
    /// {
    ///     id: "30647DF9-CFE4-42E0-AAC8-81D8D10A7850",
    ///     publishbooks: false
    /// }
    /// </remarks>
    /// <param name="publishAuthorDto">Данные для публикации автора в формате PublishAuthorDto.</param>
    /// <returns>Без содержимого.</returns>
    /// <response code="204">Успешный запрос.</response>
    /// <response code="401">Требуется авторизация.</response>
    /// <response code="403">Отказано в доступе.</response>
    [HttpPost("[action]")]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Publish([FromBody] PublishAuthorDto publishAuthorDto)
    {
        var command = _mapper.Map<PublishAuthorCommand>(publishAuthorDto);
        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    /// Скрывает автора с указанным идентификатором.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// POST /author/hide
    /// {
    ///     id: "9d9e6f34-b0fa-4f6d-b21c-a5e1d32801fd"
    /// }
    /// </remarks>
    /// <param name="hideAuthorDto">Данные для скрытия автора в формате HideAuthorDto.</param>
    /// <returns>Без содержимого.</returns>
    /// <response code="204">Успешный запрос.</response>
    /// <response code="401">Требуется авторизация.</response>
    /// <response code="403">Отказано в доступе.</response>
    [HttpPost("[action]")]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Hide([FromBody] HideAuthorDto hideAuthorDto)
    {
        var command = _mapper.Map<HideAuthorCommand>(hideAuthorDto);
        await Mediator.Send(command);

        return NoContent();
    }

    #endregion
}
