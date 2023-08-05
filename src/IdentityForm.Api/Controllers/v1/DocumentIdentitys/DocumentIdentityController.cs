using IdentityForm.Application.Features.DocumentIdentitys.Commands.AddEdit;
using IdentityForm.Shared.Wrapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IdentityForm.Api.Controllers.v1.DocumentIdentitys
{
    public class DocumentIdentityController : BaseApiController<DocumentIdentityController>
    {
        /// <summary>
        /// Add/Edit a DocumentIdentity
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPost]
        public async Task<ActionResult<Result<Guid>>> Post([FromForm] AddEditDocumentIdentityCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}