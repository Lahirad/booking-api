using Bookify.Application.Abstractions.Messging;

namespace Bookify.Application.Users.GetLoggedInUser;

public sealed record GetLoggedInUserQuery : IQuery<UserResponse>;
