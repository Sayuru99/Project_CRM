using MyApp.SharedDomain.Commands;
using MyApp.SharedDomain.Exceptions.ValidacaoException;
using MyApp.SharedDomain.Queries;
using MyApp.SharedDomain.Services;
using MyApp.SharedDomain.ValueObjects;

namespace MyApp.SharedDomain.Handlers
{
    public abstract class HandlerBase<
        TEntity,
        TGetQuery,
        TGetResponse,
        TGetPaginateQuery,
        TInsertCommand,
        TUpdateCommand,
        TDeleteCommand>
        where TEntity : Entity
        where TGetQuery : QueryBase<TGetResponse>
        where TGetPaginateQuery : PaginateQueryBase<TGetResponse>
        where TGetResponse : QueryResponseBase
        where TInsertCommand : InsertCommandBase
        where TUpdateCommand : CommandBase
        where TDeleteCommand : CommandBase
    {
        private readonly BaseService<TEntity> _service;

        protected const string INVALID_COMMAND = "Invalid command";
        protected const string INVALID_QUERY = "Invalid query";

        public HandlerBase(BaseService<TEntity> service)
        {
            _service = service;
        }

        public virtual async Task<PaginateQueryResponseBase<TGetResponse>> Handle(TGetPaginateQuery request, CancellationToken cancellationToken)
        {
            if (!request.Valid(out var validationResult))
            {
                throw new ValidacaoException(INVALID_QUERY, validationResult);
            }

            return await _service.GetAllAsync(request);
        }

        public virtual async Task<TGetResponse> Handle(TGetQuery request, CancellationToken cancellationToken)
        {
            if (!request.Valid(out var validationResult))
            {
                throw new ValidacaoException(INVALID_QUERY, validationResult);
            }

            return await _service.GetAsync(request);
        }

        public virtual async Task<CommandResponse> Handle(TInsertCommand request, CancellationToken cancellationToken)
        {
            if (!request.Valid(out var validationResult))
            {
                throw new ValidacaoException(INVALID_COMMAND, validationResult);
            }

            return await _service.InsertAsync(request);
        }

        public virtual async Task<CommandResponse> Handle(TUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!request.Valid(out var validationResult))
            {
                throw new ValidacaoException(INVALID_COMMAND, validationResult);
            }

            return await _service.UpdateAsync(request);
        }

        public virtual async Task<CommandResponse> Handle(TDeleteCommand request, CancellationToken cancellationToken)
        {
            if (!request.Valid(out var validationResult))
            {
                throw new ValidacaoException(INVALID_COMMAND, validationResult);
            }

            return await _service.DeleteAsync(request);
        }
    }
}
