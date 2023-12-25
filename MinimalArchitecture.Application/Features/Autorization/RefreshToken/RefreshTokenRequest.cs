using MediatR;
using MinimalArchitecture.Common.Results;

namespace MinimalArchitecture.Application.Features.Autorization.RefreshToken
{
    public class RefreshTokenRequest:IRequest<Result<RefreshTokenResponse>>
    {
        public string Token { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
       
    }
}
