using MediatR;

namespace Bookshelf.Application.Books.Commands.PublishBook;

public record BookPublishedEvent(Guid BookId) : INotification;
