using System.Linq.Expressions;
using User.Core.Contracts.Queries;
using User.Core.Models.User;

namespace User.Core.Extensions
{
    public class UserExtension
    {
        public static Expression<Func<UserModel, bool>> Filter(GetUserQuery query)
        {
            Expression<Func<UserModel, bool>> predicate = user =>
                (query.Id == Guid.Empty || user.Id == query.Id) &&
                (string.IsNullOrEmpty(query.Email) || user.Email == query.Email);

            return predicate;
        }
    }
}
