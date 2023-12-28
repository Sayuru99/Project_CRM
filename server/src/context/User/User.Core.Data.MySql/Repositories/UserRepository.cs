using Microsoft.EntityFrameworkCore;
using MyApp.Core.Users.Data.MySql.Contexts;
using MyApp.Core.Users.Interfaces;
using MyApp.SharedDomain.Repositories;
using User.Core.Contracts.Queries;
using User.Core.Extensions;
using User.Core.Models.User;

namespace MyApp.Core.Users.Data.MySql.Repositories
{
    public class UserRepository : EFRepository<UserModel>, IUserRepository
    {
        public UserRepository(UserContext context) : base(context)
        {
        }

        public async Task<UserModel?> GetAsync(GetUserQuery query)
        {
            // TODO: Implements commandTimeout on EFRepository.
            return await _dbSet.Include(x => x.Image).Where(UserExtension.Filter(query)).FirstOrDefaultAsync();
        }
    }
}
