using AutoMapper;
using MyApp.SharedDomain.Commands;
using MyApp.SharedDomain.Exceptions;
using MyApp.SharedDomain.Exceptions.ValidacaoException;
using MyApp.SharedDomain.Interfaces;
using MyApp.SharedDomain.Queries;
using MyApp.SharedDomain.ValueObjects;

namespace MyApp.SharedDomain.Services
{
    public class BaseService<TEntity> where TEntity : Entity
    {
        protected readonly IMapper _mapper;
        protected readonly IEFRepository<TEntity> _repository;

        protected const string INVALID_ENTITY = "Invalid Entity";

        protected BaseService(IMapper mapper, IEFRepository<TEntity> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public virtual async Task<TResponse> GetAsync<TResponse>(QueryBase<TResponse> query)
        {
            var entity = await _repository.GetAsync(query.Id) ?? throw new NotFoundException(query.Id);

            return _mapper.Map<TResponse>(entity);
        }

        public virtual async Task<TEntity> GetEntityByIdAsync(Guid id)
        {
            return await _repository.GetAsync(id) ?? throw new NotFoundException(id);
        }

        public virtual async Task<PaginateQueryResponseBase<TResponse>> GetAllAsync<TResponse>(PaginateQueryBase<TResponse> paginateQuery)
        {
            var pagination = new Pagination(paginateQuery.Page, paginateQuery.PageSize);
            var teste = await _repository.GetAllAsync(pagination);
            return _mapper.Map<PaginateQueryResponseBase<TResponse>>(teste);
        }

        public virtual async Task<CommandResponse> InsertAsync(InsertCommandBase command)
        {
            var entity = _mapper.Map<TEntity>(command);

            if (!entity.Valid(out var validationResult))
            {
                throw new ValidacaoException(INVALID_ENTITY, validationResult);
            }

            await _repository.InsertAsync(entity);
            await _repository.SaveChangesAsync();

            return new CommandResponse { Id = entity.Id, Message = "Successfully created" };
        }

        public virtual async Task<CommandResponse> UpdateAsync(CommandBase command)
        {
            var entity = _mapper.Map<TEntity>(command);

            if (!entity.Valid(out var validationResult))
            {
                throw new ValidacaoException(INVALID_ENTITY, validationResult);
            }

            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();

            return new CommandResponse { Id = entity.Id, Message = "Successfully updated" };
        }

        public virtual async Task<CommandResponse> DeleteAsync(CommandBase command)
        {
            var entity = await GetEntityByIdAsync(command.Id);

            await _repository.Delete(entity);
            await _repository.SaveChangesAsync();

            return new CommandResponse { Id = entity.Id, Message = "Successfully deleted" };
        }
    }
}
