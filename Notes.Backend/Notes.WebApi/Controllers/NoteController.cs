using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Notes.Aplication.Notes.Commands.CreateNote;
using Notes.Aplication.Notes.Commands.DeleteNote;
using Notes.Aplication.Notes.Commands.UpdateNote;
using Notes.Aplication.Notes.Queries.GetNoteDetails;
using Notes.Aplication.Notes.Queries.GetNoteList;
using Notes.WebApi.Models;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Notes.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class NoteController : BaseController
    {
        private readonly IMapper _mapper;

        public NoteController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Get the list of notes
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /note
        /// </remarks>
        /// <returns>Return NoteListVm</returns>
        /// <responce code="200">Success</responce>
        /// <responce code="401">If the user is unnauthorized</responce>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<NoteListVm>> GetAll()
        {
            var query = new GetNoteListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Get the note by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /note/B23A2EBB-6895-4C4A-BD6F-A468B97FB250
        /// </remarks>
        /// <param name="id">Note id (guid)</param>
        /// <returns>Return NoteDetailsVm</returns>
        /// <responce code="200">Success</responce>
        /// <responce code="401">If the user is unnauthorized</responce>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<NoteDetailsVm>> Get(Guid id)
        {
            var query = new GetNoteDetailsQuery
            {
                UserId = UserId,
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the note
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /note
        /// {
        ///     title: "note title",
        ///     details: "note details"
        /// }
        /// </remarks>
        /// <param name="createNoteDto">createNoteDto object</param>
        /// <returns>Return id (guid)</returns>
        /// <responce code="200">Success</responce>
        /// <responce code="401">If the user is unnauthorized</responce>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDto createNoteDto)
        {
            var command = _mapper.Map<CreateNoteCommand>(createNoteDto);
            command.UserId = UserId;
            var noteId = await Mediator.Send(command);
            return Ok(noteId);
        }

        /// <summary>
        /// Updates the note
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /note
        /// {
        ///     title: "updated note title"
        /// }
        /// </remarks>
        /// <param name="updateNoteDto">updateNoteDto object</param>
        /// <returns>Return NoContent</returns>
        /// <responce code="204">Success</responce>
        /// <responce code="401">If the user is unnauthorized</responce>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromBody] UpdateNoteDto updateNoteDto)
        {
            var command = _mapper.Map<UpdateNoteCommand>(updateNoteDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes the note by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /note/B23A2EBB-6895-4C4A-BD6F-A468B97FB250
        /// </remarks>
        /// <param name="id">id of the note (guid)</param>
        /// <returns>Return NoContent</returns>
        /// <responce code="204">Success</responce>
        /// <responce code="401">If the user is unnauthorized</responce>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteNoteCommand
            {
                Id = id,
                UserId = UserId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
