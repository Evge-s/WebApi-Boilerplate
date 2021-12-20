using FluentValidation;
using System;

namespace Notes.Aplication.Notes.Commands.DeleteNote
{
    public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
    {
        public DeleteNoteCommandValidator()
        {
            RuleFor(createNoteCommand =>
                createNoteCommand.Id).NotEqual(Guid.Empty);
            RuleFor(createNoteCommand =>
                createNoteCommand.UserId).NotEqual(Guid.Empty);
        }
    }
}
