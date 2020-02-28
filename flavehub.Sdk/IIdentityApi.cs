using System.Threading.Tasks;
using Refit;
using flavehub;
using flavehub.Contracts.RequestObjs;

namespace flavehub.Sdk
{
    public interface IIdentityApi
    {
        [Post("/api/v1/identity/register")]
        Task<ApiResponse<AuthSuccessResponse>> RegisterAsync([Body] UserRegistrationReqObj registrationRequest);
        
        [Post("/api/v1/identity/login")]
        Task<ApiResponse<AuthSuccessResponse>> LoginAsync([Body] UserLoginReqObj loginRequest);
        
        [Post("/api/v1/identity/refersh")]
        Task<ApiResponse<AuthSuccessResponse>> RefreshAsync([Body] UserRefreshTokenReqObj refreshRequest);
    }
}