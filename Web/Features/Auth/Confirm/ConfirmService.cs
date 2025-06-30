using System.Net.Http.Json;
using Web.Shared.Bases;

namespace Web.Features.Auth.Confirm;

public interface IConfirmService
{
    Task<ApiResponse<ConfirmResponse>> Invoke(Guid userId, string token);
}

public class ConfirmService(HttpClient http) : IConfirmService
{
    public async Task<ApiResponse<ConfirmResponse>> Invoke(Guid userId, string token)
    {
        var response = await http.PostAsJsonAsync("/api/auth/account/confirm", new
        {
            userId,
            token
        });

        return await response.Content.ReadFromJsonAsync<ApiResponse<ConfirmResponse>>()
               ?? new ApiResponse<ConfirmResponse> { Success = false, Message = "Respuesta vacía del servidor." };
    }
}