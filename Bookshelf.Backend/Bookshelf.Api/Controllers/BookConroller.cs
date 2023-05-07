using System.ComponentModel.DataAnnotations;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Bookshelf.Domain.Base;

using Bookshelf.Application.Books.Models;
using Bookshelf.Application.Books.Queries.GetBookList;
using Bookshelf.Application.Books.Queries.GetBookDetails;
using Bookshelf.Application.Books.Commands.CreateBook;
using Bookshelf.Application.Books.Commands.UpdateBook;
using Bookshelf.Application.Books.Commands.DeleteBook;
using Bookshelf.Application.Books.Commands.DeleteBookGenre;
using Bookshelf.Application.Books.Commands.AddBookGenre;
using Bookshelf.Application.Books.Commands.PublishBook;
using Bookshelf.Application.Books.Commands.HideBook;

using Bookshelf.Api.Controllers.Base;
using Bookshelf.Api.Models.Book;

namespace Bookshelf.Api.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
public class BookController : BaseController
{
    private readonly IMapper _mapper;

    public BookController(IMapper mapper) => _mapper = mapper;

    #region CRUD

    /// <summary>
    /// Получает список всех книг (не рекомендуется).
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// GET /book
    /// </remarks>
    /// <returns>Список книг в формате BookListVm.</returns>
    /// <response code="200">Успешный запрос.</response>
    /// <response code="401">Требуется авторизация.</response>
    [HttpGet]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    [ProducesResponseType(typeof(BookListVm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<BookListVm>> GetAll()
    {
        var query = new GetBookListQuery();
        var vm = await Mediator.Send(query);

        return Ok(vm);
    }

    /// <summary>
    /// Получает подробную информацию о книге, по указанному идентификатору.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// GET /book/6B764958-FB23-4907-AEBE-5B6BCA73CE50
    /// </remarks>
    /// <param name="id">Идентификатор книги.</param>
    /// <returns>Информация о книге в формате BookDetailsVm.</returns>
    /// <response code="200">Успешный запрос.</response>
    /// <response code="404">Не удалось найти книгу с указанным идентификатором.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BookDetailsVm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookDetailsVm>> Get(Guid id)
    {
        var query = new GetBookDetailsQuery(id);
        var vm = await Mediator.Send(query);

        return Ok(vm);
    }

    /// <summary>
    /// Создает новую книгу.
    /// </summary>
    /// <param name="createBookDto">Данные для создания книги в формате CreateBookDto.</param>
    /// <returns>Идентификатор созданной книги (guid).</returns>
    /// <response code="201">Успешный запрос.</response>
    /// <response code="401">Требуется авторизация.</response>
    /// <response code="403">Отказано в доступе.</response>
    [HttpPost]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateBookDto createBookDto)
    {
        var command = _mapper.Map<CreateBookCommand>(createBookDto);
        var bookId = await Mediator.Send(command);

        return Ok(bookId);
    }

    /// <summary>
    /// Обновляет данные книги с заданным идентификатором.
    /// </summary>
    /// <param name="updateBookDto">Данные для обновления книги в формате UpdateBookDto.</param>
    /// <returns>Без содержимого.</returns>
    /// <response code="204">Успешный запрос.</response>
    /// <response code="401">Требуется авторизация.</response>
    /// <response code="403">Отказано в доступе.</response>
    [HttpPut]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Update([FromBody] UpdateBookDto updateBookDto)
    {
        var command = _mapper.Map<UpdateBookCommand>(updateBookDto);
        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    /// Удаляет книгу с заданным идентификатором.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// DELETE /book/6B764958-FB23-4907-AEBE-5B6BCA73CE50
    /// </remarks>
    /// <param name="id">Идентификатор книги.</param>
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
        var command = new DeleteBookCommand(id);
        await Mediator.Send(command);

        return NoContent();
    }

    #endregion

    #region Visibility control

    /// <summary>
    /// Публикует книгу с заданным идентификатором.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// POST /book/publish
    /// {
    ///     id: "6B764958-FB23-4907-AEBE-5B6BCA73CE50"
    /// }
    /// </remarks>
    /// <param name="publishBookDto">Данные для публикации книги в формате PublishBookDto.</param>
    /// <returns>Без содержимого.</returns>
    /// <response code="204">Успешный запрос.</response>
    /// <response code="401">Требуется авторизация.</response>
    /// <response code="403">Отказано в доступе.</response>
    [HttpPost("[action]")]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Publish([FromBody] PublishBookDto publishBookDto)
    {
        var command = _mapper.Map<PublishBookCommand>(publishBookDto);
        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    /// Скрывает книгу с указанным идентификатором.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// POST /book/hide
    /// {
    ///     id: "6B764958-FB23-4907-AEBE-5B6BCA73CE50"
    /// }
    /// </remarks>
    /// <param name="hideBookDto">Данные для скрытия книги в формате HideBookDto.</param>
    /// <returns>Без содержимого.</returns>
    /// <response code="204">Успешный запрос.</response>
    /// <response code="401">Требуется авторизация.</response>
    /// <response code="403">Отказано в доступе.</response>
    [HttpPost("[action]")]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Hide([FromBody] HideBookDto hideBookDto)
    {
        var command = _mapper.Map<HideBookCommand>(hideBookDto);
        await Mediator.Send(command);

        return NoContent();
    }

    #endregion

    #region Genre management

    /// <summary>
    /// Удаляет жанр книги.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// DELETE /book/6B764958-FB23-4907-AEBE-5B6BCA73CE50/genres/genreId?=a406edc7-02e3-4297-9f17-1f7d2d85b8f3
    /// </remarks>
    /// <param name="id">Идентификатор книги.</param>
    /// <param name="genreId">Идентификатор жанра.</param>
    /// <returns>Результат выполнения операции.</returns>
    /// <response code="204">Жанр успешно удален.</response>
    /// <response code="400">В запросе не указан идентификатор жанра.</response>
    /// <response code="401">Требуется авторизация.</response>
    /// <response code="404">Книга или жанр не найдены.</response>
    [HttpDelete("{id:guid}/genres")]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteBookGenre([FromRoute] Guid id, [FromQuery][Required] Guid genreId)
    {
        var command = new DeleteBookGenreCommand(id, genreId);
        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    /// Добавляет жанр книги.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// POST /book/6B764958-FB23-4907-AEBE-5B6BCA73CE50/genres
    /// {
    ///   "GenreId": "a406edc7-02e3-4297-9f17-1f7d2d85b8f3"
    /// }
    /// </remarks>
    /// <param name="id">Идентификатор книги.</param>
    /// <param name="GenreId">Идентификатор жанра.</param>
    /// <returns>Код ответа HTTP 204 No Content.</returns>
    /// <response code="204">Жанр успешно добавлен к книге.</response>
    /// <response code="401">Требуется авторизация.</response>
    /// <response code="403">Отказано в доступе.</response>
    [HttpPost("{id:guid}/genres")]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AddBookGenre([FromRoute] Guid id, [FromBody] Guid GenreId)
    {
        var command = new AddBookGenreCommand(id, GenreId);
        await Mediator.Send(command);

        return NoContent();
    }

    #endregion
}

