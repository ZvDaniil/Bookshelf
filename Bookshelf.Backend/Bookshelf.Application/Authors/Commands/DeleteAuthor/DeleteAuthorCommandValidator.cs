using FluentValidation;

namespace Bookshelf.Application.Authors.Commands.DeleteAuthor;

public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorCommandValidator()
    {
        RuleFor(deleteAuthorCommand => deleteAuthorCommand.Id).NotEqual(Guid.Empty);
    }
}