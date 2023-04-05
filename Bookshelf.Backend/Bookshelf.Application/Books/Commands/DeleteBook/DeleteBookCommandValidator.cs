using FluentValidation;

namespace Bookshelf.Application.Books.Commands.DeleteBook;

internal class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(deleteBookCommand => deleteBookCommand.Id).NotEqual(Guid.Empty);
    }
}
