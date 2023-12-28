using AutoMapper;
using MyApp.SharedDomain.Queries;
using MyApp.SharedDomain.ValueObjects;
using User.Core.Contracts.Commands;
using User.Core.Contracts.Queries;
using User.Core.Contracts.Queries.User.Image;
using User.Core.Models.User;
using User.Core.Models.User.Image;

namespace MyApp.Core.Users.Mappers
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            DomainToResponse();
            CommandToDomain();
        }

        private void DomainToResponse()
        {
            CreateMap<UserModel, GetUserResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(x => x.Email))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(x => x.IsActive));

            CreateMap<ImageModel, GetUserImageResponse>();
            CreateMap<PaginationResponse<UserModel>, PaginateQueryResponseBase<GetUserResponse>>();
        }

        private void CommandToDomain()
        {
            CreateMap<InsertUserCommand, UserModel>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(x => x.Password));

            CreateMap<UpdateUserCommand, UserModel>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(x => x.Password));
        }
    }
}
