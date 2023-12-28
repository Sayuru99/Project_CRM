using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.SharedDomain.Commands;
using MyApp.SharedDomain.Exceptions;
using MyApp.SharedDomain.Exceptions.ValidacaoException;
using MyApp.SharedDomain.Queries;
using MyApp.SharedDomain.ValueObjects;
using System.Linq;
using System.Net;
using User.Core.Contracts.Queries;

namespace MyApp.Application.Controllers
{
    public abstract class MediatRControllerBase<
        TEntity,
        TGetPaginateQuery,
        TGetPaginateResponse,
        TGetQuery,
        TGetResponse,
        TInsertCommand,
        TUpdateCommand,
        TDeleteCommand> : ControllerBase
        where TEntity : Entity
        where TGetPaginateQuery : PaginateQueryBase<TGetResponse>
        where TGetPaginateResponse : PaginateQueryResponseBase<TGetResponse>
        where TGetQuery : QueryBase<TGetResponse>
        where TGetResponse : QueryResponseBase
        where TInsertCommand : InsertCommandBase
        where TUpdateCommand : CommandBase
        where TDeleteCommand : CommandBase
    {
        public readonly IMediator _mediator;

        public MediatRControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public virtual async Task<IActionResult> GetAll(int page, int pageSize)
        {
            var request = Activator.CreateInstance(typeof(TGetPaginateQuery)) as TGetPaginateQuery
                ?? throw new ExceptionBase("Invalid request type.", HttpStatusCode.InternalServerError);

            request.Page = page;
            request.PageSize = pageSize;

            return await Result(request, HttpStatusCode.OK);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(string id)
        {
            var request = Activator.CreateInstance(typeof(TGetQuery)) as TGetQuery
                ?? throw new ExceptionBase("Invalid request type.", HttpStatusCode.InternalServerError);

            request.Id = new Guid(id);

            return await Result(request, HttpStatusCode.OK);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Insert(TInsertCommand request)
        {
            return await Result(request, HttpStatusCode.Created);
        }

        [HttpPut]
        [Authorize]
        public virtual async Task<IActionResult> Update(TUpdateCommand request)
        {
            return await Result(request, HttpStatusCode.OK);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            var request = Activator.CreateInstance(typeof(TDeleteCommand)) as TDeleteCommand
                   ?? throw new ExceptionBase("Invalid request type.", HttpStatusCode.InternalServerError);

            request.Id = id;

            return await Result(request, HttpStatusCode.NoContent);
        }

        protected virtual async Task<IActionResult> Result(IBaseRequest request, HttpStatusCode statusCode)
        {
            try
            {
                var response = await _mediator.Send(request);

                if (statusCode == HttpStatusCode.NoContent)
                {
                    return NoContent();
                }

                return new ObjectResult(response)
                {
                    StatusCode = (int)statusCode
                };
            }
            catch (ValidacaoException ex)
            {
                return new ObjectResult(ex.FormatedMessage)
                {
                    StatusCode = ex.StatusCode
                };
            }
            catch (ExceptionBase ex)
            {
                return new ObjectResult(new { ex.Message })
                {
                    StatusCode = ex.StatusCode
                };
            }
        }
    }
}
