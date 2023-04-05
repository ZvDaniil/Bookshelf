using FluentValidation;

namespace Bookshelf.Application.Authors.Commands.DeleteAuthor;

internal class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorCommandValidator()
    {
        RuleFor(deleteAuthorCommand => deleteAuthorCommand.Id).NotEqual(Guid.Empty);
    }
}