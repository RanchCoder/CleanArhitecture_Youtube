using BuberDinner.Application.Services.Authentication.Common;
using ErrorOr;
using MediatR;

namespace BuberDiner.Application.Authentication.Quries.Login;

public record LoginQuery(
 string Email,
 string Password
) : IRequest<ErrorOr<AuthenticationResult>>;