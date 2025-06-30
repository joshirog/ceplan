using System.Net.Http.Json;
using Web.Shared.Bases;

namespace Web.Features.Auth.SignUp;

public interface ISignUpService
{
    Task<ApiResponse<SignUpResponse>> Invoke(SignUpRequest request);
}

public class SignUpService(HttpClient http) : ISignUpService
{
    public async Task<ApiResponse<SignUpResponse>> Invoke(SignUpRequest request)
    {
        var response = await http.PostAsJsonAsync("/api/auth/account/signup", request);
    
        if (!response.IsSuccessStatusCode)
        {
            return new ApiResponse<SignUpResponse>
            {
                Success = false,
                Message = "Error en el servidor",
                Exception = await response.Content.ReadAsStringAsync()
            };
        }

        var result = await response.Content.ReadFromJsonAsync<ApiResponse<SignUpResponse>>();
        return result!;
    }
}