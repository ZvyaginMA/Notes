using Microsoft.AspNetCore.Mvc;
using Notes.Application.Notes.Query.GetNoteDetails;
using Notes.Application.Notes.Query.GetNoteList;

namespace Notes.Api.Controllers
{
    public class NoteController : BaseController
    {
        public async Task<ActionResult<NoteListVm>> GetAll()
        {
            var query = new GetNoteListQuery()
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NoteDetailsVm>> Get(Guid id)
        {
            var query = new GetNoteDetailsQuery()
            {
                UserId = UserId,
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
    }
}
