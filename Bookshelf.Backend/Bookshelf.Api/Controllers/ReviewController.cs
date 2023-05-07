using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Bookshelf.Domain.Base;

using Bookshelf.Application.Reviews.Models;
using Bookshelf.Application.Reviews.Queries.GetReviewList;
using Bookshelf.Application.Reviews.Queries.GetReviewDetails;
using Bookshelf.Application.Reviews.Commands.CreateReview;
using Bookshelf.Application.Reviews.Commands.UpdateReview;
using Bookshelf.Application.Reviews.Commands.DeleteReview;
using Bookshelf.Application.Reviews.Commands.PublishReview;
using Bookshelf.Application.Reviews.Commands.HideReview;

using Bookshelf.Api.Controllers.Base;
using Bookshelf.Api.Models.Review;

namespace Bookshelf.Api.Controllers;

[Produces("application/json")]
public class ReviewController : BaseController
{
    private readonly IMapper _mapper;

    public ReviewController(IMapper mapper) => _mapper = mapper;

    #region CRUD

    /// <summary>
    /// Получает список всех отзывов по идентификатору книги.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// GET /review?bookId=DA6911D8-F406-4080-A3D5-ABBC89C59205
    /// </remarks>
    /// <returns>Список отзывов в формате ReviewListVm.</returns>
    /// <response code="200">Успешный запрос.</response>
    /// <response code="401">Требуется авторизация.</response>
    [HttpGet("{bookId:guid}")]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    [ProducesResponseType(typeof(ReviewListVm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ReviewListVm>> GetAll(Guid bookId)
    {
        var query = new GetReviewListQuery(bookId);
        var vm = await Mediator.Send(query);

        return Ok(vm);
    }

    /// <summary>
    /// Получает подробную информацию о отзыве, по указанному идентификатору.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// GET /review/DA6911D8-F406-4080-A3D5-ABBC89C59205
    /// </remarks>
    /// <param name="id">Идентификатор отзыва.</param>
    /// <returns>Информация о отзыве в формате ReviewDetailsVm.</returns>
    /// <response code="200">Успешный запрос.</response>
    /// <response code="404">Не удалось найти отзыв с указанным идентификатором.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ReviewDetailsVm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReviewDetailsVm>> Get(Guid id)
    {
        var query = new GetReviewDetailsQuery(id);
        var vm = await Mediator.Send(query);

        return Ok(vm);
    }

    /// <summary>
    /// Создает новый отзыв.
    /// </summary>
    /// <param name="createReviewDto">Данные для создания отзыва в формате CreateReviewDto.</param>
    /// <returns>Идентификатор созданного отзыва (guid).</returns>
    /// <response code="201">Успешный запрос.</response>
    /// <response code="401">Требуется авторизация.</response>
    /// <response code="403">Отказано в доступе.</response>
    [HttpPost]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Create([FromBody] CreateReviewDto createReviewDto)
    {
        var command = _mapper.Map<CreateReviewCommand>(createReviewDto);
        var id = await Mediator.Send(command);

        return Ok(id);
    }

    /// <summary>
    /// Обновляет данные отзыва с заданным идентификатором.
    /// </summary>
    /// <param name="updateReviewDto">Данные для обновления отзыва в формате UpdateReviewDto.</param>
    /// <returns>Без содержимого.</returns>
    /// <response code="204">Успешный запрос.</response>
    /// <response code="401">Требуется авторизация.</response>
    /// <response code="403">Отказано в доступе.</response>
    [HttpPut]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Update([FromBody] UpdateReviewDto updateReviewDto)
    {
        var command = _mapper.Map<UpdateReviewCommand>(updateReviewDto);
        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    /// Удаляет отзыв с заданным идентификатором.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// DELETE /review/DA6911D8-F406-4080-A3D5-ABBC89C59205
    /// </remarks>
    /// <param name="id">Идентификатор отзыва.</param>
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
        var command = new DeleteReviewCommand(id);
        await Mediator.Send(command);

        return NoContent();
    }

    #endregion

    #region Visibility control

    /// <summary>
    /// Публикует отзыв с заданным идентификатором.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// POST /review/publish
    /// {
    ///     id: "DA6911D8-F406-4080-A3D5-ABBC89C59205"
    /// }
    /// </remarks>
    /// <param name="publishReviewDto">Данные для публикации отзыва в формате PublishReviewDto.</param>
    /// <returns>Без содержимого.</returns>
    /// <response code="204">Успешный запрос.</response>
    /// <response code="401">Требуется авторизация.</response>
    /// <response code="403">Отказано в доступе.</response>
    [HttpPost("[action]")]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Publish([FromBody] PublishReviewDto publishReviewDto)
    {
        var command = _mapper.Map<PublishReviewCommand>(publishReviewDto);
        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    /// Скрывает отзыв с указанным идентификатором.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// POST /review/hide
    /// {
    ///     id: "DA6911D8-F406-4080-A3D5-ABBC89C59205"
    /// }
    /// </remarks>
    /// <param name="hideReviewDto">Данные для скрытия отзыва в формате HideReviewDto.</param>
    /// <returns>Без содержимого.</returns>
    /// <response code="204">Успешный запрос.</response>
    /// <response code="401">Требуется авторизация.</response>
    /// <response code="403">Отказано в доступе.</response>
    [HttpPost("[action]")]
    [Authorize(Roles = AppData.SystemAdministratorRoleName)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Hide([FromBody] HideReviewDto hideReviewDto)
    {
        var command = _mapper.Map<HideReviewCommand>(hideReviewDto);
        await Mediator.Send(command);

        return NoContent();
    }

    #endregion
}
