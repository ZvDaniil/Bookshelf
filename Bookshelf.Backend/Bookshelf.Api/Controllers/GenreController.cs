using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Bookshelf.Domain.Base;

using Bookshelf.Application.Genres.Models;
using Bookshelf.Application.Genres.Queries.GetGenreList;
using Bookshelf.Application.Genres.Queries.GetGenreDetails;
using Bookshelf.Application.Genres.Commands.CreateGenre;
using Bookshelf.Application.Genres.Commands.DeleteGenre;
using Bookshelf.Application.Genres.Commands.UpdateGenre;
using Bookshelf.Application.Genres.Commands.PublishGenre;

using Bookshelf.Api.Controllers.Base;
using Bookshelf.Api.Models.Genre;

namespace Bookshelf.Api.Controllers;

[Produces("application/json")]
public class GenreController : BaseController
{
    private readonly IMapper _mapper;

    public GenreController(IMapper mapper) => _mapper = mapper;

    #region CRUD

    /// <summary>
    /// Получает список всех жанров.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// GET /genre
    /// </remarks>
    /// <returns>Список жанров в формате GenreListVm.</returns>
    /// <response code="200">Успешный запрос.</response>
    [HttpGet]
    [ProducesResponseType(typeof(GenreListVm), StatusCodes.Status200OK)]
    public async Task<ActionResult<GenreListVm>> GetAll()
    {
        var query = new GetGenreListQuery();
        var vm = await Mediator.Send(query);

        return Ok(vm);
    }

    /// <summary>
    /// Получает подробную информацию о жанре по указанному идентификатору.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// GET /genre/30647DF9-CFE4-42E0-AAC8-81D8D10A7850
    /// </remarks>
    /// <param name="id">Идентификатор жанра.</param>
    /// <returns>Информация о жанре в формате GenreDetailsVm.</returns>
    /// <response code="200">Успешный запрос.</response>
    /// <response code="404">Не удалось найти жанр с указанным идентификатором.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GenreDetailsVm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GenreDetailsVm>> Get(Guid id)
    {
        var query = new GetGenreDetailsQuery(id);
        var vm = await Mediator.Send(query);

        return Ok(vm);
    }

    /// <summary>
    /// Создает новый жанр.
    /// </summary>
    /// <param name="createGenreDto">Данные для создания жанра в формате CreateGenreDto.</param>
    /// <returns>Идентификатор созданного жанра (guid).</returns>
    /// <response code="201">Успешный запрос.</response>
    /// <response code="401">Требуется авторизация.</response>
    /// <response code="403">Отказано в доступе.</response>
    [HttpPost]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateGenreDto createGenreDto)
    {
        var command = _mapper.Map<CreateGenreCommand>(createGenreDto);
        var genreId = await Mediator.Send(command);

        return Ok(genreId);
    }

    /// <summary>
    /// Обновляет данные жанра с заданным идентификатором.
    /// </summary>
    /// <param name="updateGenreDto">Данные для обновления жанра в формате UpdateGenreDto.</param>
    /// <returns>Без содержимого.</returns>
    /// <response code="204">Успешный запрос.</response>
    /// <response code="401">Требуется авторизация.</response>
    /// <response code="403">Отказано в доступе.</response>
    [HttpPut]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Update([FromBody] UpdateGenreDto updateGenreDto)
    {
        var command = _mapper.Map<UpdateGenreCommand>(updateGenreDto);
        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    /// Удаляет книгу с заданным идентификатором.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// DELETE /genre/6B764958-FB23-4907-AEBE-5B6BCA73CE50
    /// </remarks>
    /// <param name="id">Идентификатор жанра.</param>
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
        var command = new DeleteGenreCommand(id);
        await Mediator.Send(command);

        return NoContent();
    }

    #endregion

    #region Visibility control

    /// <summary>
    /// Публикует жанр с заданным идентификатором.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// POST /genre/publish
    /// {
    ///     id: "6B764958-FB23-4907-AEBE-5B6BCA73CE50"
    /// }
    /// </remarks>
    /// <param name="publishGenreDto">Данные для публикации книги в формате PublishGenreDto.</param>
    /// <returns>Без содержимого.</returns>
    /// <response code="204">Успешный запрос.</response>
    /// <response code="401">Требуется авторизация.</response>
    /// <response code="403">Отказано в доступе.</response>
    [HttpPost("[action]")]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Guid>> Publish([FromBody] PublishGenreDto publishGenreDto)
    {
        var command = _mapper.Map<PublishGenreCommand>(publishGenreDto);
        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    /// Скрывает жанр с указанным идентификатором.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// POST /genre/hide
    /// {
    ///     id: "6B764958-FB23-4907-AEBE-5B6BCA73CE50"
    /// }
    /// </remarks>
    /// <param name="hideGenreDto">Данные для скрытия жанра в формате HideGenreDto.</param>
    /// <returns>Без содержимого.</returns>
    /// <response code="204">Успешный запрос.</response>
    /// <response code="401">Требуется авторизация.</response>
    /// <response code="403">Отказано в доступе.</response>
    [HttpPost("[action]")]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Guid>> Hide([FromBody] HideGenreDto hideGenreDto)
    {
        var command = _mapper.Map<HideGenreDto>(hideGenreDto);
        await Mediator.Send(command);

        return NoContent();
    }

    #endregion
}
