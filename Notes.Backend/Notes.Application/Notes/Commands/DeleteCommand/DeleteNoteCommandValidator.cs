using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Commands.DeleteCommand
{
    public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
    {
        public DeleteNoteCommandValidator() 
        {
            RuleFor(deleteCommand =>
                deleteCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(deleteCommand =>
                deleteCommand.Id).NotEqual(Guid.Empty);
        }
    }
}
