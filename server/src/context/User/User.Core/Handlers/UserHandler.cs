using MediatR;
using MyApp.Core.Users.Services;
using MyApp.SharedDomain.Commands;
using MyApp.SharedDomain.Exceptions.ValidacaoException;
using MyApp.SharedDomain.Handlers;
using MyApp.SharedDomain.Queries;
using User.Core.Contracts.Commands;
using User.Core.Contracts.Queries;
using User.Core.Models.User;

namespace MyApp.Core.Users.Handlers
{
    public class UserHandler :
        HandlerBase<
            UserModel,
            GetUserQuery,
            GetUserResponse,
            GetUsersPaginateQuery,
            InsertUserCommand,
            UpdateUserCommand,
            DeleteUserCommand>,
        IRequestHandler<GetUserQuery, GetUserResponse>,
        IRequestHandler<GetUsersPaginateQuery, PaginateQueryResponseBase<GetUserResponse>>,
        IRequestHandler<InsertUserCommand, CommandResponse>,
        IRequestHandler<UpdateUserCommand, CommandResponse>,
        IRequestHandler<DeleteUserCommand, CommandResponse>,
        IRequestHandler<InactiveUserCommand, CommandResponse>,
        IRequestHandler<UpdateUserPassword, CommandResponse>
    {
        private readonly UserService _userService;

        public UserHandler(UserService userService) : base(userService)
        {
            _userService = userService;
        }

        public new async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetAsync(request);
        }

        public async Task<CommandResponse> Handle(InactiveUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.Valid(out var validationResult))
            {
                throw new ValidacaoException(INVALID_COMMAND, validationResult);
            }

            return await _userService.InactiveUserAsync(request);
        }

        public async Task<CommandResponse> Handle(UpdateUserPassword request, CancellationToken cancellationToken)
        {
            if (!request.Valid(out var validationResult))
            {
                throw new ValidacaoException(INVALID_COMMAND, validationResult);
            }

            return await _userService.UpdatePasswordAsync(request);
        }

        public override Task<CommandResponse> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            return _userService.InsertAsync(request);
        }
    }
}
