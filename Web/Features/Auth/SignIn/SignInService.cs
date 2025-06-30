using System.Net.Http.Json;
using Web.Shared.Bases;

namespace Web.Features.Auth.SignIn;

public interface ISignInService
{
    Task<ApiResponse<SignInResponse>> Invoke(SignInRequest request);
}

public class SignInService(HttpClient http) : ISignInService
{
    public async Task<ApiResponse<SignInResponse>> Invoke(SignInRequest request)
    {
        var response = await http.PostAsJsonAsync("/api/auth/account/signin", request);

        return await response.Content.ReadFromJsonAsync<ApiResponse<SignInResponse>>()
               ?? new ApiResponse<SignInResponse> { Success = false, Message = "Respuesta vacía del servidor." };
    }
}