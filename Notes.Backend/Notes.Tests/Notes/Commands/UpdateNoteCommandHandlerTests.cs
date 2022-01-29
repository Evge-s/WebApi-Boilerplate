using Notes.Aplication.Notes.Commands.UpdateNote;
using Notes.Tests.Common;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Notes.Tests.Notes.Commands
{
    public class UpdateNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateNoteCommandHandler_Succes()
        {
            // Arrange
            var handler = new UpdateNoteCommandHandler(Context);
            var updateTitle = "new title";

            // Act
            await handler.Handle(new UpdateNoteCommand
            {
                Id = NotesContextFactory.NoteIdForUpdate,
                UserId = NotesContextFactory.UserBId,
                Title = updateTitle
            }, 
            CancellationToken.None);

            // Assert
            Assert.NotNull(await Context.Notes.SingleOrDefaultAsync(note =>
                note.Id == NotesContextFactory.NoteIdForUpdate &&
                note.Title == updateTitle));
        }

        [Fact]
        public async Task UpdateNoteCommandHaandler_FailOnWtongId()
        {
            // Arrange
            var handler = new UpdateNoteCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<DllNotFoundException>(async () =>
                await handler.Handle(
                    new UpdateNoteCommand
                    {
                        Id = Guid.NewGuid(),
                        UserId = NotesContextFactory.UserAId
                    }, 
                    CancellationToken.None));
        }

        [Fact]
        public async Task UpdateNoteCommandHaandler_FailOnWtongUserId()
        {
            // Arrange
            var handler = new UpdateNoteCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<DllNotFoundException>(async () =>
                await handler.Handle(
                    new UpdateNoteCommand
                    {
                        Id = NotesContextFactory.NoteIdForUpdate,
                        UserId = NotesContextFactory.UserAId
                    },
                    CancellationToken.None));
        }
    }
}
