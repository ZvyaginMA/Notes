using AutoMapper;
using Notes.Application.Common.Exceptions;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Query.GetNoteDetails;
using Notes.Persistence;
using Notes.Tests.Common;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Notes.Tests.Notes.Queries
{
    [Collection("QueryCollection")]
    public class GetNoteDetailsQueryHandlerTests
    {
        private readonly NotesDbContext Context;
        private readonly IMapper Mapper;

        public GetNoteDetailsQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetNoteDetailsQueryHandler_Success()
        {
            var handler = new GetNoteDetailsQueryHandler(Context, Mapper);

            var result = await handler.Handle(
                new GetNoteDetailsQuery
                {
                    UserId = NotesContextFactory.UserBId,
                    Id = Guid.Parse("27fa3329-866c-46b4-bdcd-9c8387aa85dd")
                }, CancellationToken.None);

            result.ShouldBeOfType<NoteDetailsVm>();
            result.Title.ShouldBe("Title2");
            result.CreationDate.ShouldBe(DateTime.Today);
        }

        [Fact]
        public async Task GetNoteDetailsQueryHandler_FailOnWrongUserId()
        {
            var handler = new GetNoteDetailsQueryHandler(Context, Mapper);

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
                new GetNoteDetailsQuery
                {
                    UserId = NotesContextFactory.UserAId,
                    Id = Guid.Parse("27fa3329-866c-46b4-bdcd-9c8387aa85dd")
                }, CancellationToken.None));
        }
    }
}
